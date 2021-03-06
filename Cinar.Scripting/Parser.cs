﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Cinar.Scripting
{
    public class Parser
    {
        Tokenizer fTokenizer; // the tokenizer to read tokens from
        Token fCurrentToken; // the current token

        public int CurrentLineNumber { get { return fTokenizer.CurrentLineNumber; } }

        public Parser(TextReader source)
        {
            if (source == null) throw new ArgumentNullException("source");

            fTokenizer = new Tokenizer(source);

            // read the first token
            ReadNextToken();
        }

        void ReadNextToken() { fCurrentToken = fTokenizer.ReadNextToken(); }

        bool AtEndOfSource { get { return fCurrentToken == null; } }

        void CheckForUnexpectedEndOfSource()
        {
            if (AtEndOfSource)
                throw new ParserException("Unexpected end of source.");
        }

        void SkipExpected(TokenType type, string value)
        {
            CheckForUnexpectedEndOfSource();
            if (!fCurrentToken.Equals(type, value))
                throw new ParserException("Expected " + value);
            ReadNextToken();
        }

        public Statement ReadNextStatement()
        {
            if (AtEndOfSource)
                return null;

            // all the statements start with a word
            if (!(fCurrentToken.Type == TokenType.Word || fCurrentToken.Type==TokenType.EchoBlock || fCurrentToken.Value=="$="))
                throw new ParserException("Expected a statement.");

            Statement res = null;
            int currLineStart = fTokenizer.CurrentLineNumber;
            int currColumnStart = fTokenizer.CurrentColumnNumber - (fCurrentToken.Type==TokenType.Word ? fCurrentToken.Value.Length + 1 : 0);

            if (fCurrentToken.Type == TokenType.EchoBlock)
                res = ParseEchoBlockStatement();
            else if (fCurrentToken.Value == "$=")
                res = ParseShortCutEchoStatement();
            else if (fCurrentToken.Value == "var")
                res = ParseVarStatement(null);
            else if (fCurrentToken.Value == "if")
                res = ParseIfStatement();
            else if (fCurrentToken.Value == "try")
                res = ParseTryStatement();
            else if (fCurrentToken.Value == "while")
                res = ParseWhileStatement();
            else if (fCurrentToken.Value == "for")
                res = ParseForStatement();
            else if (fCurrentToken.Value == "foreach")
                res = ParseForEachStatement();
            else if (fCurrentToken.Value == "break")
                res = ParseBreakStatement();
            else if (fCurrentToken.Value == "continue")
                res = ParseContinueStatement();
            else if (fCurrentToken.Value == "debugger")
                res = ParseDebuggerStatement();
            else if (fCurrentToken.Value == "return")
                res = ParseReturnStatement();
            else if (fCurrentToken.Value == "throw")
                res = ParseThrowStatement();
            else if (fCurrentToken.Value == "function")
                res = ParseFunctionDefinitionStatement();
            else if (fCurrentToken.Value == "using")
                res = ParseUsingStatement();
            
            if (res == null)
                res = ParseAssignmentOrFunctionCallStatement();

            if (res != null)
            {
                res.LineNumber = currLineStart;
                res.ColumnNumber = currColumnStart;
                res.LastLineNumber = fTokenizer.CurrentLineNumber;
                res.LastColumnNumber = fTokenizer.CurrentColumnNumber;
                skipEmptyStatements();
            }

            return res;
        }

        ShortCutEchoStatement ParseShortCutEchoStatement()
        {
            ReadNextToken(); // skip $=
            Expression exp = ParseExpression();
            SkipExpected(TokenType.Symbol, "$");
            return new ShortCutEchoStatement(exp);
        }

        EchoBlockStatement ParseEchoBlockStatement()
        {
            string val = fCurrentToken.Value;
            ReadNextToken();
            return new EchoBlockStatement(val);
        }

        VariableDefinition ParseVarStatement(string varType)
        {
            // var-statement:
            //   var {assignment} | {variable}
            //   DateTime {assignment} | {variable}

            if(string.IsNullOrEmpty(varType))
                ReadNextToken(); // skip 'var'

            Variable v = new Variable(fCurrentToken.Value, varType);

            ReadNextToken(); // skip variable name

            if (fCurrentToken.Equals(TokenType.Symbol, "="))
            {
                ReadNextToken(); // skip '='
                Expression value = ParseExpression();
                return new VariableDefinition(v, value);
            }
            else
                return new VariableDefinition(v, null);
        }

        IfStatement ParseIfStatement()
        {
            // if-statement:
            //   if {condition} then {true-statements} end if
            //   if ( {condition} ) { {true-statements} }
            //   if {condition} then {true-statements} else {false-statements} end if

            ReadNextToken(); // skip 'if'
            SkipExpected(TokenType.Symbol, "(");

            Expression lCondition = ParseExpression();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            List<Statement> lTrueStatements = new List<Statement>();
            bool lTrueStatementsHasBracet = false;
            List<Statement> lFalseStatements = new List<Statement>();
            bool lFalseStatementsHasBracet = false;
            List<Statement> lStatements = lTrueStatements;
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
                lTrueStatementsHasBracet = true;
                SkipExpected(TokenType.Symbol, "{"); // skip '{'

                CheckForUnexpectedEndOfSource();
                while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
                {
                    if ((lStatement = ReadNextStatement()) != null)
                        lStatements.Add(lStatement);
                    else 
                        throw new ParserException("Unexpected end of source.");
                }

                ReadNextToken(); // skip '}'
            }
            else
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lStatements.Add(lStatement);
                else 
                    throw new ParserException("Unexpected end of source.");
            }

            if (fCurrentToken != null)
            {
                lStatements = lFalseStatements;

                if (fCurrentToken.Equals(TokenType.Word, "else"))
                {
                    ReadNextToken(); // skip 'else'

                    if (fCurrentToken.Equals(TokenType.Symbol, "{"))
                    {
                        lFalseStatementsHasBracet = true;
                        ReadNextToken(); // skip '{'
                        CheckForUnexpectedEndOfSource();
                        while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
                        {
                            if ((lStatement = ReadNextStatement()) != null)
                                lStatements.Add(lStatement);
                            else throw new ParserException("Unexpected end of source.");
                        }
                        SkipExpected(TokenType.Symbol, "}"); // skip '}'
                    }
                    else
                    {
                        if ((lStatement = ReadNextStatement()) != null)
                            lStatements.Add(lStatement);
                        else throw new ParserException("Unexpected end of source.");
                    }
                }
            }

            return new IfStatement(lCondition
                , new StatementCollection(lTrueStatements, lTrueStatementsHasBracet)
                , new StatementCollection(lFalseStatements, lFalseStatementsHasBracet));
        }
        TryCatchStatement ParseTryStatement()
        {
            List<Statement> lTryStatements = new List<Statement>();
            List<Statement> lCatchStatements = new List<Statement>();
            Statement lStatement;
            string exVarName = null;

            ReadNextToken(); // skip 'try'
            SkipExpected(TokenType.Symbol, "{"); // skip '{'

            CheckForUnexpectedEndOfSource();
            while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lTryStatements.Add(lStatement);
                else
                    throw new ParserException("Unexpected end of source. Incomplete try statement.");
            }

            SkipExpected(TokenType.Symbol, "}"); // skip '}'
            SkipExpected(TokenType.Word, "catch"); // skip 'catch'

            if (fCurrentToken.Equals(TokenType.Symbol, "("))
            {
                ReadNextToken(); // skip '('
                if (fCurrentToken.Type == TokenType.Word)
                    exVarName = fCurrentToken.Value.ToString();
                ReadNextToken();
                SkipExpected(TokenType.Symbol, ")"); // skip ')'
            }

            SkipExpected(TokenType.Symbol, "{"); // skip '{'

            CheckForUnexpectedEndOfSource();
            while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lCatchStatements.Add(lStatement);
                else
                    throw new ParserException("Unexpected end of source. Incomplete catch statement.");
            }

            SkipExpected(TokenType.Symbol, "}"); // skip '}'

            return new TryCatchStatement(
                new StatementCollection(lTryStatements, true),
                exVarName,
                new StatementCollection(lCatchStatements, true));
        }

        WhileStatement ParseWhileStatement()
        {
            // while-statement:
            //   while {condition} do {statements} end while
            //   while ( {condition} ) { {statements} }

            ReadNextToken(); // skip 'while'
            ReadNextToken(); // skip '('

            Expression lCondition = ParseExpression();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            List<Statement> lStatements = new List<Statement>();
            bool hasBrace = false;
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
                hasBrace = true;
                SkipExpected(TokenType.Symbol, "{"); // skip '{'

                CheckForUnexpectedEndOfSource();
                while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
                {
                    if ((lStatement = ReadNextStatement()) != null)
                        lStatements.Add(lStatement);
                    else throw new ParserException("Unexpected end of source.");
                }

                ReadNextToken(); // skip '}'
            }
            else
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lStatements.Add(lStatement);
                else throw new ParserException("Unexpected end of source.");
            }

            return new WhileStatement(lCondition, new StatementCollection(lStatements, hasBrace));
        }

        ForStatement ParseForStatement()
        {
            // for-statement:
            //   for ( {a-statement} ; {an-expression} ; {a-statement} ) { {statements} }

            ReadNextToken(); // skip 'for'
            CheckForUnexpectedEndOfSource();
            ReadNextToken(); // skip '('
            CheckForUnexpectedEndOfSource();

            Statement lStart = null, lEnd = null;
            Expression lCompare = null;


            if (!fCurrentToken.Equals(TokenType.Symbol, ";"))
                lStart = ReadNextStatement();

            if(fCurrentToken.Equals(TokenType.Symbol, ";"))
                ReadNextToken(); // skip ';'

            if (!fCurrentToken.Equals(TokenType.Symbol, ";"))
                lCompare = ParseExpression();

            SkipExpected(TokenType.Symbol, ";"); // skip ';'

            if (!fCurrentToken.Equals(TokenType.Symbol, ")"))
                lEnd = ReadNextStatement();
            CheckForUnexpectedEndOfSource();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            List<Statement> lStatements = new List<Statement>();
            bool hasBrace = false;
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
                hasBrace = true;
                SkipExpected(TokenType.Symbol, "{"); // skip '{'

                CheckForUnexpectedEndOfSource();
                while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
                {
                    if ((lStatement = ReadNextStatement()) != null)
                        lStatements.Add(lStatement);
                    else throw new ParserException("Unexpected end of source.");
                }

                ReadNextToken(); // skip '}'
            }
            else
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lStatements.Add(lStatement);
                else throw new ParserException("Unexpected end of source.");
            }

            return new ForStatement(lStart, lCompare, lEnd, new StatementCollection(lStatements, hasBrace));
        }

        ForEachStatement ParseForEachStatement()
        {
            // foreach-statement:
            //   foreach ({variable} in {end-value}) { {statements} }
            //   foreach (var {variable} in {end-value}) { {statements} }
            //   foreach (int {variable} in {end-value}) { {statements} }

            ReadNextToken(); // skip 'foreach'
            CheckForUnexpectedEndOfSource();
            ReadNextToken(); // skip '('
            CheckForUnexpectedEndOfSource();

            Token lToken = fCurrentToken;
            ReadNextToken();

            Variable lVariable;
            if (fCurrentToken.Value == "in")
                lVariable = new Variable(lToken.Value, "var");
            else
            {
                lVariable = new Variable(fCurrentToken.Value, lToken.Value);
                ReadNextToken();
            }

            SkipExpected(TokenType.Word, "in"); // skip 'in'

            Expression lCollection = ParseExpression();
            CheckForUnexpectedEndOfSource();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'
            List<Statement> lStatements = new List<Statement>();
            bool hasBrace = false;
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
                hasBrace = true;
                SkipExpected(TokenType.Symbol, "{"); // skip '{'

                CheckForUnexpectedEndOfSource();
                while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
                {
                    if ((lStatement = ReadNextStatement()) != null)
                        lStatements.Add(lStatement);
                    else throw new ParserException("Unexpected end of source.");
                }

                ReadNextToken(); // skip '}'
            }
            else
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lStatements.Add(lStatement);
                else throw new ParserException("Unexpected end of source.");
            }

            return new ForEachStatement(lVariable, lCollection, new StatementCollection(lStatements, hasBrace));
        }

        FunctionDefinitionStatement ParseFunctionDefinitionStatement()
        {
            // function definition:
            //   function {function-name} (  {expression}, {expression}, ... ) { {statements} }

            ReadNextToken(); // skip 'function'

            string funcName = fCurrentToken.Value;
            ReadNextToken(); // skip function name
            ReadNextToken(); // skip '('

            List<string> lParams = new List<string>();
            if (!fCurrentToken.Equals(TokenType.Symbol, ")"))
            {
                lParams.Add(fCurrentToken.Value);
                ReadNextToken(); // skip param name

                while (fCurrentToken.Equals(TokenType.Symbol, ","))
                {
                    ReadNextToken(); // skip ','
                    lParams.Add(fCurrentToken.Value);
                    ReadNextToken(); // skip lParam
                }

                if (!fCurrentToken.Equals(TokenType.Symbol, ")"))
                    throw new ParserException("Expected ')'.");
            }

            ReadNextToken(); // skip ')'

            List<Statement> lStatements = new List<Statement>();
            Statement lStatement;

            SkipExpected(TokenType.Symbol, "{"); // skip '{'

            CheckForUnexpectedEndOfSource();
            while (!fCurrentToken.Equals(TokenType.Symbol, "}"))
            {
                if ((lStatement = ReadNextStatement()) != null)
                    lStatements.Add(lStatement);
                else throw new ParserException("Unexpected end of source.");
            }

            ReadNextToken(); // skip '}'

            return new FunctionDefinitionStatement(funcName, lParams, new StatementCollection(lStatements, true));
        }

        BreakStatement ParseBreakStatement()
        {
            // break-statement:
            //   break

            SkipExpected(TokenType.Word, "break");

            return new BreakStatement();
        }

        ContinueStatement ParseContinueStatement()
        {
            // continue-statement:
            //   continue

            SkipExpected(TokenType.Word, "continue");

            return new ContinueStatement();
        }

        DebuggerStatement ParseDebuggerStatement()
        {
            // continue-statement:
            //   continue

            SkipExpected(TokenType.Word, "debugger");

            return new DebuggerStatement();
        }

        ReturnStatement ParseReturnStatement()
        {
            // return-statement:
            //   return expression

            SkipExpected(TokenType.Word, "return");
            Expression exp = ParseExpression();

            return new ReturnStatement(exp);
        }

        ThrowStatement ParseThrowStatement()
        {
            // return-statement:
            //   return expression

            SkipExpected(TokenType.Word, "throw");
            Expression exp = ParseExpression();

            return new ThrowStatement(exp);
        }

        UsingStatement ParseUsingStatement()
        {
            // return-statement:
            //   return expression

            SkipExpected(TokenType.Word, "using");

            string str = fCurrentToken.Value;
            ReadNextToken();
            while (fCurrentToken.Equals(TokenType.Symbol, "."))
            {
                ReadNextToken();
                str += "." + fCurrentToken.Value;
                ReadNextToken();
            }
            ReadNextToken();

            return new UsingStatement(str);
        }

        Statement ParseAssignmentOrFunctionCallStatement()
        {
            Token lToken = fCurrentToken;

            ReadNextToken();
            CheckForUnexpectedEndOfSource();

            if (fCurrentToken.Equals(TokenType.Symbol, "="))
                return ParseAssignment(new Variable(lToken.Value, "var"), "=");
            if (fCurrentToken.Equals(TokenType.Symbol, "+="))
                return ParseAssignment(new Variable(lToken.Value, "var"), "+=");
            if (fCurrentToken.Equals(TokenType.Symbol, "++"))
                return ParseAssignment(new Variable(lToken.Value, "var"), "++");
            if (fCurrentToken.Equals(TokenType.Symbol, "-="))
                return ParseAssignment(new Variable(lToken.Value, "var"), "-=");
            if (fCurrentToken.Equals(TokenType.Symbol, "--"))
                return ParseAssignment(new Variable(lToken.Value, "var"), "--");
            if (fCurrentToken.Equals(TokenType.Symbol, "/="))
                return ParseAssignment(new Variable(lToken.Value, "var"), "/=");
            if (fCurrentToken.Equals(TokenType.Symbol, "*="))
                return ParseAssignment(new Variable(lToken.Value, "var"), "*=");
            if (fCurrentToken.Equals(TokenType.Symbol, "."))
                return ParseMemberAssignmentOrMethodCallStatement(lToken.Value);

            if (fCurrentToken.Equals(TokenType.Symbol, "("))
                return new FunctionCallStatement(ParseFunctionCall(lToken.Value));

            if (fCurrentToken.Type == TokenType.Word)  // DateTime d = null; gibi VarStatement'ları bulur...
                return ParseVarStatement(lToken.Value);

            if (fCurrentToken.Equals(TokenType.Symbol, "["))
                return ParseMemberAssignmentOrMethodCallStatement(lToken.Value);

            throw new ParserException("Expected a statement.");
        }

        Assignment ParseAssignment(Variable variable, string op)
        {
            // assignment:
            //   {variable} += {value}

            ReadNextToken(); // skip '+='

            Assignment a = null;

            if (op == "++" || op == "--")
                a = new Assignment(variable, op, null);
            else
                a = new Assignment(variable, op, ParseExpression());

            return a;
        }
        MemberAssignment ParseMemberAssignment(MemberAccess variable, string op)
        {
            // assignment:
            //   {variable} += {value}

            ReadNextToken(); // skip '+='

            MemberAssignment a = null;

            if (op == "++" || op == "--")
                a = new MemberAssignment(variable, op, null);
            else
                a = new MemberAssignment(variable, op, ParseExpression());

            return a;
        }

        Statement ParseMemberAssignmentOrMethodCallStatement(string variable)
        {
            // assignment:
            //   {variable} = {value}

            string dotType = fCurrentToken.Value;
                
            ReadNextToken(); // skip '.'

            MemberAccess ma = new MemberAccess(new Variable(variable, "var"), ParseDotExpression());

            if (dotType == "[")
                SkipExpected(TokenType.Symbol, "]");

            if (ma.RightChildExpression is MethodCall)
                return new MethodCallStatement(ma);
            else if(ma.RightChildExpression is FunctionCall)
                return new MethodCallStatement(ma);
            else
                return ParseMemberAssignment(ma, fCurrentToken.Value);
        }

        Expression ParseExpression()
        {
            return ParseIfShortCutExpression();
        }

        Expression ParseIfShortCutExpression()
        {
            // multiplicative expression:
            //   {dot-expression}
            //   {dot-expression} ? {dot-expression} : {dot-expression}

            Expression lNode = ParseIsNullShortCutExpression();

            if (!AtEndOfSource)
            {
                Expression first = null, second = null;
                if (fCurrentToken.Equals(TokenType.Symbol, "?"))
                {
                    ReadNextToken(); // skip '?'
                    first = ParseIsNullShortCutExpression();
                }
                if (fCurrentToken.Equals(TokenType.Symbol, ":"))
                {
                    ReadNextToken(); // skip ':'
                    second = ParseIsNullShortCutExpression();
                }
                if (first != null && second != null)
                    lNode = new IfShortCut(lNode, first, second);
            }

            return lNode;
        }

        Expression ParseIsNullShortCutExpression()
        {
            // multiplicative expression:
            //   {dot-expression}
            //   {dot-expression} ?? {dot-expression}

            Expression lNode = ParseAndExpression();

            if (!AtEndOfSource)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "??"))
                {
                    ReadNextToken(); // skip '??'
                    lNode = new IsNullShortCut(lNode, ParseAndExpression());
                }
            }

            return lNode;
        }

        Expression ParseAndExpression()
        {
            // AND-expression:
            //   {or-expression}
            //   {or-expression} and {or-expression}
            //   {or-expression} and {or-expression} and ...

            Expression lNode = ParseOrExpression();

            while (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "&&"))
            {
                ReadNextToken(); // skip '&&'
                lNode = new AndExpression(lNode, ParseOrExpression());
            }

            return lNode;
        }

        Expression ParseOrExpression()
        {
            // OR-expression:
            //   {comparison}
            //   {comparison} or {comparison}
            //   {comparison} or {comparison} or ...

            Expression lNode = ParseXorExpression();

            while (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "||"))
            {
                ReadNextToken(); // skip '||'
                lNode = new OrExpression(lNode, ParseXorExpression());
            }

            return lNode;
        }

        Expression ParseXorExpression()
        {
            // XOR-expression:
            //   {comparison}
            //   {comparison} ^ {comparison}
            //   {comparison} ^ {comparison} ^ ...

            Expression lNode = ParseComparison();

            while (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "^"))
            {
                ReadNextToken(); // skip '||'
                lNode = new XorExpression(lNode, ParseComparison());
            }

            return lNode;
        }

        Expression ParseComparison()
        {
            // comparison:
            //   {additive-expression}
            //   {additive-expression} == {additive-expression}
            //   {additive-expression} != {additive-expression}
            //   {additive-expression} < {additive-expression}
            //   {additive-expression} > {additive-expression}
            //   {additive-expression} <= {additive-expression}
            //   {additive-expression} >= {additive-expression}

            Expression lNode = ParseAdditiveExpression();

            if (!AtEndOfSource && fCurrentToken.Type == TokenType.Symbol)
            {
                ComparisonOperator lOperator;
                switch (fCurrentToken.Value)
                {
                    case "==": lOperator = ComparisonOperator.Equal; break;
                    case "!=": lOperator = ComparisonOperator.NotEqual; break;
                    case "<": lOperator = ComparisonOperator.LessThan; break;
                    case ">": lOperator = ComparisonOperator.GreaterThan; break;
                    case "<=": lOperator = ComparisonOperator.LessThanOrEqual; break;
                    case ">=": lOperator = ComparisonOperator.GreaterThanOrEqual; break;
                    default: return lNode;
                }

                ReadNextToken(); // skip comparison operator
                return new Comparison(lOperator, lNode, ParseAdditiveExpression());
            }
            else return lNode;
        }

        Expression ParseAdditiveExpression()
        {
            // additive expression:
            //   {mod-expression}
            //   {mod-expression} [+ or -] {mod-expression}
            //   {mod-expression} [+ or -] {mod-expression} [+ or -] ...

            Expression lNode = ParseModExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "+"))
                {
                    ReadNextToken(); // skip '+'
                    lNode = new Addition(lNode, ParseModExpression());
                }
                else if (fCurrentToken.Equals(TokenType.Symbol, "-"))
                {
                    ReadNextToken(); // skip '-'
                    lNode = new Subtraction(lNode, ParseModExpression());
                }
                else break;
            }

            return lNode;
        }

        Expression ParseModExpression()
        {
            // mod expression:
            //   {multiplicative-expression}
            //   {multiplicative-expression} [%] {multiplicative-expression}
            //   {multiplicative-expression} [%] {multiplicative-expression} [%] ...

            Expression lNode = ParseMultiplicativeExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "%"))
                {
                    ReadNextToken(); // skip '+'
                    lNode = new Mod(lNode, ParseMultiplicativeExpression());
                }
                else break;
            }

            return lNode;
        }

        Expression ParseMultiplicativeExpression()
        {
            // multiplicative expression:
            //   {unary-expression}
            //   {unary-expression} [* or /] {unary-expression}
            //   {unary-expression} [* or /] {unary-expression} [* or /] ...

            Expression lNode = ParseUnaryExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "*"))
                {
                    ReadNextToken(); // skip '*'
                    lNode = new Multiplication(lNode, ParseUnaryExpression());
                }
                else if (fCurrentToken.Equals(TokenType.Symbol, "/"))
                {
                    ReadNextToken(); // skip '/'
                    lNode = new Division(lNode, ParseUnaryExpression());
                }
                else break;
            }

            return lNode;
        }

        Expression ParseUnaryExpression()
        {
            // unary expression:
            //   {base-expression}
            //   -{base-expression}
            //   !{base-expression}

            CheckForUnexpectedEndOfSource();
            if (fCurrentToken.Equals(TokenType.Symbol, "-"))
            {
                ReadNextToken(); // skip '-'
                return new Negation(ParseIsExpression());
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "!"))
            {
                ReadNextToken(); // skip '!'
                return new NotExpression(ParseIsExpression());
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "~"))
            {
                ReadNextToken(); // skip '~'
                return new BitwiseComplementExpression(ParseIsExpression());
            }
            else if (fCurrentToken.Equals(TokenType.Word, "new"))
            {
                ReadNextToken(); // skip 'new'
                if (fCurrentToken.Type != TokenType.Word)
                    throw new ParserException("Type name expected");
                string constructorName = fCurrentToken.Value;
                ReadNextToken(); // skip constructorName
                return new NewExpression(ParseFunctionCall(constructorName));
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "+"))
                ReadNextToken(); // skip '+'

            return ParseIsExpression();
        }

        Expression ParseIsExpression()
        {
            // is expression:
            //   {unary-expression} [is] type-name

            Expression lNode = ParseDotExpression();

            if (!AtEndOfSource && fCurrentToken.Equals(TokenType.Word, "is"))
            {
                ReadNextToken(); // skip 'is'
                if (fCurrentToken.Type != TokenType.Word)
                    throw new ParserException("is operator must be followed by a type name");
                lNode = new Is(lNode, fCurrentToken.Value);
                ReadNextToken(); //skip type name
            }

            return lNode;
        }

        Expression ParseDotExpression()
        {
            // DOT-expression:
            //   {unary-expression} . {functionCallOrVariable}

            Expression lNode = ParseBaseExpression();

            while (!AtEndOfSource && (fCurrentToken.Value == "." || fCurrentToken.Value == "["))
            {
                string currToken = fCurrentToken.Value;
                if (currToken == ".")
                {
                    ReadNextToken(); // skip '.'

                    //if (fCurrentToken.Value == "$=")
                    //    return lNode;

                    Expression exp = ParseVariableOrFunctionCall();
                    if (exp is FunctionCall)
                        lNode = new MethodCall(lNode, exp);
                    else
                        lNode = new MemberAccess(lNode, exp);
                }
                else
                {
                    ReadNextToken(); // skip '['
                    lNode = new IndexerAccess(lNode, ParseExpression());
                    SkipExpected(TokenType.Symbol, "]");
                }
            }

            return lNode;
        }

        Expression ParseBaseExpression()
        {
            CheckForUnexpectedEndOfSource();

            switch (fCurrentToken.Type)
            {
                case TokenType.Integer: 
                    return ParseIntegerConstant();
                case TokenType.Decimal: 
                    return ParseDecimalConstant();
                case TokenType.String: 
                    return ParseStringConstant();
                case TokenType.Word: 
                    return ParseVariableOrFunctionCall();
                default: // TokenType.Symbol
                    if (fCurrentToken.Value == "(")
                        return ParseGroupExpression();
                    else 
                        throw new ParserException("Expected an expression.");
            }
        }

        Expression ParseGroupExpression()
        {
            ReadNextToken(); // skip '('

            Expression lExpression = ParseExpression();
            lExpression.InParanthesis = true;

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            return lExpression;
        }

        Expression ParseVariableOrFunctionCall()
        {
            string lName = fCurrentToken.Value;

            ReadNextToken();

            if(AtEndOfSource)
                return new Variable(lName, "var");

            if (fCurrentToken.Equals(TokenType.Symbol, "("))
                return ParseFunctionCall(lName);
            else if (fCurrentToken.Equals(TokenType.Symbol, "++"))
            {
                ReadNextToken();
                return new VariableIncrement(new Variable(lName, "var"), 1);
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "--"))
            {
                ReadNextToken();
                return new VariableIncrement(new Variable(lName, "var"), -1);
            }
            else
                return new Variable(lName, "var");
        }

        Expression ParseStringConstant()
        {
            string lValue = fCurrentToken.Value;
            ReadNextToken(); // skip string constant
            return new StringConstant(lValue);
        }

        private Expression ParseIntegerConstant()
        {
            int lValue;
            if (int.TryParse(fCurrentToken.Value, out lValue))
            {
                ReadNextToken(); // skip integer constant
                return new IntegerConstant(lValue);
            }
            else throw new ParserException("Invalid integer constant " + fCurrentToken.Value);
        }
        private Expression ParseDecimalConstant()
        {
            decimal lValue;
            if (decimal.TryParse(fCurrentToken.Value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out lValue))
            {
                ReadNextToken(); // skip decimal constant
                return new DecimalConstant(lValue);
            }
            else throw new ParserException("Invalid decimal constant " + fCurrentToken.Value);
        }

        FunctionCall ParseFunctionCall(string name)
        {
            // function call:
            //   {function-name} ( )
            //   {function-name} ( {expression} )
            //   {function-name} ( {expression}, {expression}, ... )

            if(fCurrentToken.Equals(TokenType.Symbol, "("))
                ReadNextToken(); // skip '('
            CheckForUnexpectedEndOfSource();

            List<Expression> lArguments = new List<Expression>();
            if (!fCurrentToken.Equals(TokenType.Symbol, ")"))
            {
                lArguments.Add(ParseExpression());
                CheckForUnexpectedEndOfSource();

                while (fCurrentToken.Equals(TokenType.Symbol, ","))
                {
                    ReadNextToken(); // skip ','
                    lArguments.Add(ParseExpression());
                    CheckForUnexpectedEndOfSource();
                }

                if (!fCurrentToken.Equals(TokenType.Symbol, ")"))
                    throw new ParserException("Expected ')'.");
            }

            ReadNextToken(); // skip ')'

            return new FunctionCall(name, lArguments.ToArray());
        }

        private void skipEmptyStatements()
        {
            while (fCurrentToken!=null && fCurrentToken.Equals(TokenType.Symbol, ";"))
                ReadNextToken();
        }
    }
    [Serializable]
    public class ParserException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ParserException"/> class.</summary>
        public ParserException() { }

        public ParserException(string message) : base(message) { }

        public ParserException(string message, Exception innerException) : base(message, innerException) { }

        protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    public class Tokenizer
    {
        TextReader fSource; // the source to read characters from
        char fCurrentChar; // the current character
        StringBuilder fTokenValueBuffer; // a buffer for building the value of a token
        public int CurrentColumnNumber;
        public int CurrentLineNumber;

        public Tokenizer(TextReader source)
        {
            if (source == null) throw new ArgumentNullException("source");

            fSource = source;
            fTokenValueBuffer = new StringBuilder();

            // read the first character
            ReadNextChar();
        }

        void ReadNextChar()
        {
            int lChar = fSource.Read();
            CurrentColumnNumber++;
            if (lChar > 0)
                fCurrentChar = (char)lChar;
            else 
                fCurrentChar = '\0';

            if (fCurrentChar == '\n')
            {
                CurrentLineNumber++;
                CurrentColumnNumber = 0;
            }
        }

        void SkipWhitespace()
        {
            while (char.IsWhiteSpace(fCurrentChar))
                ReadNextChar();
        }

        void SkipComment(string commentSymbol)
        {
            if (commentSymbol == "/*")
            {
                char prevChar = ' ';
                while (commentSymbol != "*/")
                {
                    ReadNextChar();
                    commentSymbol = prevChar.ToString() + fCurrentChar.ToString();
                    prevChar = fCurrentChar;
                }
                ReadNextChar();
            }
            if (commentSymbol == "//")
            {
                while (fCurrentChar != '\n')
                    ReadNextChar();
                //ReadNextChar();
            }
        }

        bool AtEndOfSource { get { return fCurrentChar == '\0'; } }

        void StoreCurrentCharAndReadNext()
        {
            fTokenValueBuffer.Append(fCurrentChar);
            ReadNextChar();
        }

        string ExtractStoredChars()
        {
            string lValue = fTokenValueBuffer.ToString();
            fTokenValueBuffer.Length = 0;
            return lValue;
        }

        void CheckForUnexpectedEndOfSource()
        {
            if (AtEndOfSource)
                throw new ParserException("Unexpected end of source.");
        }

        void ThrowInvalidCharException()
        {
            if (fTokenValueBuffer.Length == 0)
                throw new ParserException("Invalid character '" + fCurrentChar.ToString() + "'.");
            else
            {
                throw new ParserException("Invalid character '"
                    + fCurrentChar.ToString() + "' after '"
                    + fTokenValueBuffer.ToString() + "'.");
            }
        }

        public Token ReadNextToken()
        {
            return ReadNextToken(false);
        }

        private bool readingCode = false;
        private bool readingShortCutEcho = false;

        public Token ReadNextToken(bool keepWhitespaces)
        {
            if (AtEndOfSource)
                return null;

            if (readingCode)
            {
                if (!keepWhitespaces)
                    SkipWhitespace();

                // if the first character is a letter, the token is a word
                if ((char.IsLetter(fCurrentChar) || fCurrentChar == '_') && fCurrentChar != '$')
                    return ReadWord();

                // if the first character is a digit, the token is an integer constant
                if (char.IsDigit(fCurrentChar))
                    return ReadIntegerOrDecimalConstant();

                // if the first character is a quote, the token is a string constant
                if (fCurrentChar == '"' || fCurrentChar == '\'')
                    return ReadStringConstant();

                // in all other cases, the token should be a symbol
                Token token = ReadSymbol();

                if (token.Value == "/*" || token.Value == "//")
                {
                    SkipComment(token.Value);
                    token = ReadNextToken();
                }

                if (token != null && token.Value == "$")
                {
                    if (readingShortCutEcho)
                    {
                        readingShortCutEcho = false;
                        readingCode = false;
                        return token;
                    }
                    readingCode = false;
                    ReadNextChar();
                    return ReadNextToken();
                }
                else
                    return token;
            }
            else
            {
                if (fCurrentChar != '$')
                    return ReadEchoBlock();
                else
                {
                    Token t = ReadSymbol();
                    readingCode = true;
                    if (t.Value == "$=")
                        readingShortCutEcho = true;
                    if (t.Value == "$")
                        return ReadNextToken();
                    else
                        return t;
                }
            }
        }

        Token ReadEchoBlock()
        {
            do
            {
                bool dolarFound = false;
                if (fCurrentChar == '\\')
                {
                    ReadNextChar();
                    if (fCurrentChar == '$')
                    {
                        StoreCurrentCharAndReadNext();
                        dolarFound = true;
                    }
                }
                if(!dolarFound)
                    StoreCurrentCharAndReadNext();
            }
            while (!(fCurrentChar == '$' || fCurrentChar=='\0'));

            return new Token(TokenType.EchoBlock, ExtractStoredChars());
        }

        Token ReadWord()
        {
            do
            {
                StoreCurrentCharAndReadNext();
            }
            while (char.IsLetterOrDigit(fCurrentChar) || fCurrentChar == '_');

            return new Token(TokenType.Word, ExtractStoredChars());
        }

        Token ReadIntegerOrDecimalConstant()
        {
            do
            {
                StoreCurrentCharAndReadNext();
            }
            while (char.IsDigit(fCurrentChar) || fCurrentChar=='.');

            string val = ExtractStoredChars();
            if(val.Contains("."))
                return new Token(TokenType.Decimal, val);
            else
                return new Token(TokenType.Integer, val);
        }

        Token ReadStringConstant()
        {
            char quoteChar = fCurrentChar;
            ReadNextChar();
            while (!AtEndOfSource && fCurrentChar != quoteChar)
            {
                if (fCurrentChar == '\\')
                {
                    ReadNextChar();
                    switch (fCurrentChar)
                    {
                        case '\'':
                        case '"':
                        case '\\':
                            StoreCurrentCharAndReadNext();
                            break;
                        case 'r':
                            fTokenValueBuffer.Append("\r");
                            ReadNextChar();
                            break;
                        case 'n':
                            fTokenValueBuffer.Append("\n");
                            ReadNextChar();
                            break;
                        case 't':
                            fTokenValueBuffer.Append("\t");
                            ReadNextChar();
                            break;
                    }
                }
                else
                    StoreCurrentCharAndReadNext();
            }

            CheckForUnexpectedEndOfSource();
            ReadNextChar();

            return new Token(TokenType.String, ExtractStoredChars());
        }

        Token ReadSymbol()
        {
            switch (fCurrentChar)
            {
                // the symbols + += ++ - -= --
                case '+':
                case '-':
                    char firstChar = fCurrentChar;
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == firstChar || fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols * *= */
                case '*':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=' || fCurrentChar == '/')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols / /= /* //
                case '/':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=' || fCurrentChar == '*' || fCurrentChar == '/')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '%':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '(':
                case ')':
                case '[':
                case ']':
                case ',':
                case '.':
                case '}':
                case '{':
                case ';':
                case ':':
                case '~':
                case '^':
                    StoreCurrentCharAndReadNext();
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols = ==
                case '=':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols ? ??
                case '?':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '?')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols ! !=
                case '!':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols < <=
                case '<':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols > >=
                case '>':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '&':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '&')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '|':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '|')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '$':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                default:
                    CheckForUnexpectedEndOfSource();
                    ThrowInvalidCharException();
                    break;
            }

            return null;
        }
    }
    public class Token
    {
        public Token(TokenType type, string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            fType = type;
            fValue = value;
        }

        readonly TokenType fType;
        public TokenType Type { get { return fType; } }

        readonly string fValue;
        public string Value { get { return fValue; } }

        public bool Equals(TokenType type, string value)
        {
            return fType == type && fValue == value;
        }

        public override string ToString()
        {
            return string.Format("Token {0}: {1}", fType, fValue);
        }
    }
    public enum TokenType
    {
        None = 0,
        Word,
        Integer,
        Decimal,
        String,
        Symbol,
        EchoBlock
    }
}
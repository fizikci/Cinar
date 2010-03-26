using System;
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
                throw new ParserException("Expected '" + value + "'.");
            ReadNextToken();
        }

        public Statement ReadNextStatement()
        {
            skipEmptyStatements();

            if (AtEndOfSource)
                return null;

            // all the statements start with a word
            if (fCurrentToken.Type != TokenType.Word)
                throw new ParserException("Expected a statement.");

            Statement res = null;

            if (fCurrentToken.Value == "var")
                res = ParseVarStatement();
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

            res.LineNumber = fTokenizer.CurrentLineNumber;
            return res;
        }

        VariableDefinition ParseVarStatement()
        {
            // var-statement:
            //   var {assignment} | {variable}

            ReadNextToken(); // skip 'var'

            Variable v = new Variable(fCurrentToken.Value);

            ReadNextToken(); // skip variable name

            if (fCurrentToken.Equals(TokenType.Symbol, "="))
            {
                ReadNextToken(); // skip '='
                Expression value = ParseExpression();
                skipEmptyStatements();
                return new VariableDefinition(v, value);
            }
            else
            {
                skipEmptyStatements();
                return new VariableDefinition(v, null);
            }
        }

        IfStatement ParseIfStatement()
        {
            // if-statement:
            //   if {condition} then {true-statements} end if
            //   if ( {condition} ) { {true-statements} }
            //   if {condition} then {true-statements} else {false-statements} end if

            ReadNextToken(); // skip 'if'
            ReadNextToken(); // skip '('

            Expression lCondition = ParseExpression();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            List<Statement> lTrueStatements = new List<Statement>();
            List<Statement> lFalseStatements = new List<Statement>();
            List<Statement> lStatements = lTrueStatements;
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
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

            if (fCurrentToken != null)
            {
                lStatements = lFalseStatements;

                if (fCurrentToken.Equals(TokenType.Word, "else"))
                {
                    ReadNextToken(); // skip 'else'

                    if (fCurrentToken.Equals(TokenType.Symbol, "{"))
                    {
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

            skipEmptyStatements();

            return new IfStatement(lCondition
                , new StatementCollection(lTrueStatements)
                , new StatementCollection(lFalseStatements));
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

            skipEmptyStatements();

            return new TryCatchStatement(
                new StatementCollection(lTryStatements),
                exVarName,
                new StatementCollection(lCatchStatements));
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
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
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

            skipEmptyStatements();

            return new WhileStatement(lCondition, new StatementCollection(lStatements));
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
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
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

            skipEmptyStatements();

            return new ForStatement(lStart, lCompare, lEnd, new StatementCollection(lStatements));
        }

        ForEachStatement ParseForEachStatement()
        {
            // foreach-statement:
            //   foreach ({variable} in {end-value}) { {statements} }

            ReadNextToken(); // skip 'foreach'
            CheckForUnexpectedEndOfSource();
            ReadNextToken(); // skip '('
            CheckForUnexpectedEndOfSource();

            if (fCurrentToken.Type != TokenType.Word)
                throw new ParserException("Expected a variable.");

            Variable lVariable = new Variable(fCurrentToken.Value);
            ReadNextToken();

            SkipExpected(TokenType.Word, "in"); // skip 'in'

            Expression lCollection = ParseExpression();
            CheckForUnexpectedEndOfSource();

            SkipExpected(TokenType.Symbol, ")"); // skip ')'
            List<Statement> lStatements = new List<Statement>();
            Statement lStatement;

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
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

            skipEmptyStatements();

            return new ForEachStatement(lVariable, lCollection, new StatementCollection(lStatements));
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

            if (fCurrentToken.Equals(TokenType.Symbol, "{"))
            {
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

            // if this ends with ; skip it.
            skipEmptyStatements();

            return new FunctionDefinitionStatement(funcName, lParams, new StatementCollection(lStatements));
        }

        BreakStatement ParseBreakStatement()
        {
            // break-statement:
            //   break

            SkipExpected(TokenType.Word, "break");
            skipEmptyStatements();

            return new BreakStatement();
        }

        ContinueStatement ParseContinueStatement()
        {
            // continue-statement:
            //   continue

            SkipExpected(TokenType.Word, "continue");
            skipEmptyStatements();

            return new ContinueStatement();
        }

        DebuggerStatement ParseDebuggerStatement()
        {
            // continue-statement:
            //   continue

            SkipExpected(TokenType.Word, "debugger");
            skipEmptyStatements();

            return new DebuggerStatement();
        }

        ReturnStatement ParseReturnStatement()
        {
            // return-statement:
            //   return expression

            SkipExpected(TokenType.Word, "return");
            Expression exp = ParseExpression();
            skipEmptyStatements();

            return new ReturnStatement(exp);
        }

        ThrowStatement ParseThrowStatement()
        {
            // return-statement:
            //   return expression

            SkipExpected(TokenType.Word, "throw");
            Expression exp = ParseExpression();
            skipEmptyStatements();

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
            
            skipEmptyStatements();

            return new UsingStatement(str);
        }

        Statement ParseAssignmentOrFunctionCallStatement()
        {
            Token lToken = fCurrentToken;

            ReadNextToken();
            CheckForUnexpectedEndOfSource();

            if (fCurrentToken.Equals(TokenType.Symbol, "="))
                return ParseAssignment(new Variable(lToken.Value), "=");
            if (fCurrentToken.Equals(TokenType.Symbol, "+="))
                return ParseAssignment(new Variable(lToken.Value), "+=");
            if (fCurrentToken.Equals(TokenType.Symbol, "++"))
                return ParseAssignment(new Variable(lToken.Value), "++");
            if (fCurrentToken.Equals(TokenType.Symbol, "-="))
                return ParseAssignment(new Variable(lToken.Value), "-=");
            if (fCurrentToken.Equals(TokenType.Symbol, "--"))
                return ParseAssignment(new Variable(lToken.Value), "--");
            if (fCurrentToken.Equals(TokenType.Symbol, "/="))
                return ParseAssignment(new Variable(lToken.Value), "/=");
            if (fCurrentToken.Equals(TokenType.Symbol, "*="))
                return ParseAssignment(new Variable(lToken.Value), "*=");
            if (fCurrentToken.Equals(TokenType.Symbol, "."))
                return ParseMemberAssignmentOrMethodCallStatement(lToken.Value);

            if (fCurrentToken.Equals(TokenType.Symbol, "("))
                return new FunctionCallStatement(ParseFunctionCall(lToken.Value));

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

            // if this ends with ; skip it.
            skipEmptyStatements();

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

            // if this ends with ; skip it.
            skipEmptyStatements();

            return a;
        }

        Statement ParseMemberAssignmentOrMethodCallStatement(string variable)
        {
            // assignment:
            //   {variable} = {value}

            ReadNextToken(); // skip '.'

            MemberAccess ma = new MemberAccess(new Variable(variable), ParseDotExpression());

            // if this ends with ; skip it.
            skipEmptyStatements();

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

            Expression lNode = ParseComparison();

            while (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "||"))
            {
                ReadNextToken(); // skip '||'
                lNode = new OrExpression(lNode, ParseComparison());
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

            Expression lNode = ParseDotExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "*"))
                {
                    ReadNextToken(); // skip '*'
                    lNode = new Multiplication(lNode, ParseDotExpression());
                }
                else if (fCurrentToken.Equals(TokenType.Symbol, "/"))
                {
                    ReadNextToken(); // skip '/'
                    lNode = new Division(lNode, ParseDotExpression());
                }
                else break;
            }

            return lNode;
        }

        Expression ParseDotExpression()
        {
            // DOT-expression:
            //   {unary-expression} . {functionCallOrVariable}

            Expression lNode = ParseUnaryExpression();

            while (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "."))
            {
                ReadNextToken(); // skip '.'
                Expression exp = ParseVariableOrFunctionCall();
                if (exp is FunctionCall)
                    lNode = new MethodCall(lNode, exp);
                else
                    lNode = new MemberAccess(lNode, exp);
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
                return new Negation(ParseBaseExpression());
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "!"))
            {
                ReadNextToken(); // skip '!'
                return new NotExpression(ParseBaseExpression());
            }
            else if (fCurrentToken.Equals(TokenType.Word, "new"))
            {
                ReadNextToken(); // skip 'new'
                string constructorName = fCurrentToken.Value;
                ReadNextToken(); // skip constructorName
                return new NewExpression(ParseFunctionCall(constructorName));
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "+"))
                ReadNextToken(); // skip '+'

            return ParseBaseExpression();
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

            SkipExpected(TokenType.Symbol, ")"); // skip ')'

            return lExpression;
        }

        Expression ParseVariableOrFunctionCall()
        {
            string lName = fCurrentToken.Value;

            ReadNextToken();

            if (!AtEndOfSource && fCurrentToken.Equals(TokenType.Symbol, "("))
                return ParseFunctionCall(lName);
            else if (fCurrentToken.Equals(TokenType.Symbol, "++"))
            {
                ReadNextToken();
                return new VariableIncrement(new Variable(lName), 1);
            }
            else if (fCurrentToken.Equals(TokenType.Symbol, "--"))
            {
                ReadNextToken();
                return new VariableIncrement(new Variable(lName), -1);
            }
            else
                return new Variable(lName);
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

            // if this ends with ; skip it.
            skipEmptyStatements();

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
        public int CurrentLineNumber = 0;

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
            if (lChar > 0)
                fCurrentChar = (char)lChar;
            else fCurrentChar = '\0';
        }

        void SkipWhitespace()
        {
            while (char.IsWhiteSpace(fCurrentChar))
            {
                if (fCurrentChar == '\n') CurrentLineNumber++;
                ReadNextChar();
            }
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
            SkipWhitespace();

            if (AtEndOfSource)
                return null;

            // if the first character is a letter, the token is a word
            if (char.IsLetter(fCurrentChar))
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

            return token;
        }

        Token ReadWord()
        {
            do
            {
                StoreCurrentCharAndReadNext();
            }
            while (char.IsLetterOrDigit(fCurrentChar) || fCurrentChar=='_');

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
                case ',':
                case '.':
                case '}':
                case '{':
                case ';':
                case ':':
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
        Symbol
    }
}
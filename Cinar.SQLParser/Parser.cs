using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Cinar.Database;

namespace Cinar.SQLParser
{
    public class Parser
    {
        Tokenizer fTokenizer; // the tokenizer to read tokens from
        Token fCurrentToken; // the current token

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

        void SkipExpected(string value)
        {
            CheckForUnexpectedEndOfSource();
            if (!fCurrentToken.Equals(value))
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

            if (fCurrentToken.Equals("SELECT"))
                return ParseSelectStatement();

            if (fCurrentToken.Equals("INSERT"))
                return ParseInsertStatement();

            if (fCurrentToken.Equals("UPDATE"))
                return ParseUpdateStatement();

            if (fCurrentToken.Equals("DELETE"))
                return ParseDeleteStatement();

            if (fCurrentToken.Equals("CREATE"))
            {
                ReadNextToken(); // skip 'create'
                if(fCurrentToken.Equals("DATABASE"))
                    return ParseCreateDatabaseStatement();
                //else if (fCurrentToken.Equals("INDEX"))
                //    return ParseCreateIndexStatement();
                //else if (fCurrentToken.Equals("FUNCTION"))
                //    return ParseCreateFunctionStatement();
                else if (fCurrentToken.Equals("TABLE"))
                    return ParseCreateTableStatement();
                //else if (fCurrentToken.Equals("TRIGGER"))
                //    return ParseCreateTriggerStatement();
                //else if (fCurrentToken.Equals("VIEW"))
                //    return ParseCreateViewStatement();
            }

            /*
            if (fCurrentToken.Value == "alter")
                return ParseAlterStatement();

            if (fCurrentToken.Value == "drop")
                return ParseVarStatement();
             * */

            throw new Exception("Undefined statement");
        }




        SelectStatement ParseSelectStatement()
        {
            SelectStatement ss = new SelectStatement();

            ReadNextToken(); // skip 'select'

            if (fCurrentToken.Equals("ALL"))
            {
                ss.All = true;
                ReadNextToken();
            }

            if (fCurrentToken.Equals("DISTINCT"))
            {
                ss.Distinct = true;
                ReadNextToken();
            }

            if (fCurrentToken.Equals("TOP"))
            {
                ReadNextToken();
                ss.Limit = ParseExpression();
            }

            ss.Select.Add(parseListItem());

            while (!AtEndOfSource && fCurrentToken.Value == ",")
            {
                ReadNextToken(); // skip ,
                ss.Select.Add(parseListItem());
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("FROM"))
            {
                ReadNextToken(); // skip 'from'

                if (fCurrentToken.Type == TokenType.Word)
                {
                    // from kısmını sorgula
                    string firstTableName = fCurrentToken.Value.TrimQuotation();
                    string firstAlias = firstTableName;
                    Dictionary<string, Expression> tableOptions = null;
                    ReadNextToken();

                    if (fCurrentToken.Equals("."))
                    {
                        ReadNextToken();
                        firstTableName += "." + fCurrentToken.Value;
                        firstAlias = firstTableName;
                        ReadNextToken();
                    }

                    if (AtEndOfSource)
                    {
                        ss.From.Add(new Join { Alias = firstAlias, TableName = firstTableName, JoinType = JoinType.Cross });
                        return ss; //***
                    }

                    if (fCurrentToken.Equals("(")) // Cinar table options
                    {
                        ReadNextToken();
                        tableOptions = parseSetItems();
                        SkipExpected(")");
                    }

                    if (fCurrentToken != null)
                    {
                        if (fCurrentToken.Equals("AS"))
                        {
                            ReadNextToken();
                            firstAlias = fCurrentToken.Value.TrimQuotation();
                            ReadNextToken();
                        }
                        else if (!(fCurrentToken.Equals(",") || fCurrentToken.Equals("JOIN") || fCurrentToken.Equals("LEFT") || fCurrentToken.Equals("RIGHT") || fCurrentToken.Equals("INNER") || fCurrentToken.Equals("WHERE") || fCurrentToken.Equals("GROUP") || fCurrentToken.Equals("LIMIT") || fCurrentToken.Equals("ORDER")))
                        {
                            firstAlias = fCurrentToken.Value.TrimQuotation();
                            ReadNextToken();
                        }
                    }
                    ss.From.Add(new Join { Alias = firstAlias, TableName = firstTableName, JoinType = JoinType.Cross, CinarTableOptions = tableOptions });

                    if (AtEndOfSource) return ss;

                    while (fCurrentToken!=null &&
                        (fCurrentToken.Equals(",") ||
                        fCurrentToken.Equals("JOIN") ||
                        fCurrentToken.Equals("LEFT") ||
                        fCurrentToken.Equals("RIGHT") ||
                        fCurrentToken.Equals("INNER") ||
                        fCurrentToken.Equals("CROSS")))
                    {
                        if (fCurrentToken.Equals(","))
                            ReadNextToken();
                        ss.From.Add(parseJoin());
                    }
                }
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("WHERE"))
            {
                ReadNextToken(); // skip 'where'

                ss.Where = ParseExpression();
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("GROUP"))
            {
                ReadNextToken(); // skip 'group'
                SkipExpected("BY");

                ss.GroupBy.Add(ParseExpression());

                if (AtEndOfSource) return ss;

                while (fCurrentToken.Value == ",")
                {
                    ReadNextToken(); // skip ,
                    ss.GroupBy.Add(ParseExpression());
                }
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("HAVING"))
            {
                ReadNextToken(); // skip 'having'

                ss.Having = ParseExpression();
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("ORDER"))
            {
                ReadNextToken(); // skip 'order'
                SkipExpected("BY");

                ss.OrderBy.Add(parseOrder());

                if (AtEndOfSource) return ss;

                while (!AtEndOfSource && fCurrentToken.Value == ",")
                {
                    ReadNextToken(); // skip ,
                    ss.OrderBy.Add(parseOrder());
                }
            }

            if (AtEndOfSource) return ss;

            if (fCurrentToken.Equals("LIMIT"))
            {
                ReadNextToken(); // skip 'limit'

                ss.Limit = ParseExpression();

                if (AtEndOfSource) return ss;

                if (fCurrentToken.Equals(",") || fCurrentToken.Equals("OFFSET"))
                {
                    ReadNextToken();
                    ss.Offset = ParseExpression();
                }
            }

            skipEmptyStatements();

            return ss;
        }

        private Dictionary<string, Expression> parseSetItems()
        {
            Dictionary<string, Expression> setItems = new Dictionary<string, Expression>();

            KeyValuePair<string, Expression> kvp = parseSetItem();
            setItems.Add(kvp.Key, kvp.Value);

            while (fCurrentToken.Value == ",")
            {
                ReadNextToken(); // skip ,
                KeyValuePair<string, Expression> kvp2 = parseSetItem();
                setItems.Add(kvp2.Key, kvp2.Value);
            }

            return setItems;
        }
        private Order parseOrder()
        {
            Order o = new Order();
            o.By = ParseExpression();
            if (o.By is Variable)
                o.By = new DbObjectName((o.By as Variable).Name.TrimQuotation());
            if (AtEndOfSource)
                return o;
            if (fCurrentToken.Equals("ASC"))
                ReadNextToken();
            if (fCurrentToken.Equals("DESC"))
            {
                o.Desc = true;
                ReadNextToken();
            }
            return o;
        }
        private Join parseJoin()
        {
            Join join = new Join();

            switch (fCurrentToken.Value.ToUpperInvariant())
            {
                case "LEFT":
                    join.JoinType = JoinType.Left;
                    break;
                case "RIGHT":
                    join.JoinType = JoinType.Right;
                    break;
                case "INNER":
                    join.JoinType = JoinType.Inner;
                    break;
                case "FULL":
                    join.JoinType = JoinType.Full;
                    break;
                case "CROSS":
                case "JOIN":
                    join.JoinType = JoinType.Cross;
                    break;
                default:
                    join.JoinType = JoinType.Cross;
                    join.TableName = join.Alias = fCurrentToken.Value.TrimQuotation();
                    ReadNextToken();
                    if (fCurrentToken.Equals("AS"))
                    {
                        ReadNextToken();
                        join.Alias = fCurrentToken.Value.TrimQuotation();
                        ReadNextToken();
                    }
                    return join;
            }

            ReadNextToken();
            if (fCurrentToken.Equals("OUTER")) ReadNextToken();
            if (fCurrentToken.Equals("JOIN")) ReadNextToken();
            join.TableName = join.Alias = fCurrentToken.Value.TrimQuotation();
            ReadNextToken();

            if (AtEndOfSource) return join;

            if (fCurrentToken.Equals("(")) // Cinar table options
            {
                ReadNextToken();
                join.CinarTableOptions = parseSetItems();
                SkipExpected(")");
            }

            if (AtEndOfSource) return join;

            if (fCurrentToken.Equals("AS"))
            {
                ReadNextToken();
                join.Alias = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();
            }
            else if (!(fCurrentToken.Equals(",") || fCurrentToken.Equals("ON") || fCurrentToken.Equals("JOIN") || fCurrentToken.Equals("LEFT") || fCurrentToken.Equals("RIGHT") || fCurrentToken.Equals("INNER") || fCurrentToken.Equals("CROSS") || fCurrentToken.Equals("WHERE") || fCurrentToken.Equals("GROUP") || fCurrentToken.Equals("LIMIT") || fCurrentToken.Equals("ORDER")))
            {
                join.Alias = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();
            }

            if (AtEndOfSource) return join;

            if (fCurrentToken.Equals("ON"))
            {
                ReadNextToken();
                join.On = ParseExpression();
            }

            return join;
        }
        private Select parseListItem()
        {
            if (fCurrentToken.Value == "*")
            {
                ReadNextToken(); // skip *
                return new Select() { Field = new Variable("*") };
            }

            Expression field = ParseExpression();
            if (field is Variable)
                field = new DbObjectName((field as Variable).Name.TrimQuotation());
            string alias = field.ToString();
            if (!AtEndOfSource && fCurrentToken.Equals("AS"))
            {
                ReadNextToken();
                alias = fCurrentToken.Value;
                ReadNextToken();
            }
            return new Select() { Field = field, Alias = alias };
        }

        InsertStatement ParseInsertStatement()
        {
            InsertStatement stm = new InsertStatement();

            ReadNextToken(); // skip 'insert'
            SkipExpected("INTO"); // skip 'into'

            stm.TableName = fCurrentToken.Value.TrimQuotation();
            ReadNextToken();

            if (fCurrentToken.Value == "(")
            {
                ReadNextToken();

                stm.Fields = parseListString();
                SkipExpected(")");
            }

            SkipExpected("VALUES");

            while (!AtEndOfSource && fCurrentToken.Value == "(")
            {
                ReadNextToken();
                List<Expression> values = new List<Expression>();
                values.Add(ParseExpression());
                while (fCurrentToken.Value == ",")
                {
                    ReadNextToken();
                    values.Add(ParseExpression());
                }
                stm.Values.Add(values);

                SkipExpected(")");
                if (!AtEndOfSource && fCurrentToken.Value == ",")
                    ReadNextToken();
            }

            skipEmptyStatements();

            return stm;
        }

        private List<string> parseListString()
        {
            List<string> res = new List<string>();
            res.Add(fCurrentToken.Value.TrimQuotation());
            ReadNextToken();

            while (fCurrentToken.Value == ",")
            {
                ReadNextToken();
                res.Add(fCurrentToken.Value.TrimQuotation());
                ReadNextToken();
            }
            return res;
        }

        UpdateStatement ParseUpdateStatement()
        {
            UpdateStatement ss = new UpdateStatement();

            ReadNextToken(); // skip 'update'

            ss.TableName = fCurrentToken.Value;
            ReadNextToken();

            SkipExpected("SET");

            KeyValuePair<string, Expression> kvp = parseSetItem();
            ss.Set.Add(kvp.Key, kvp.Value);

            while (fCurrentToken.Value == ",")
            {
                ReadNextToken(); // skip ,
                KeyValuePair<string, Expression> kvp2 = parseSetItem();
                ss.Set.Add(kvp2.Key, kvp2.Value);
            }

            if (fCurrentToken.Equals("FROM"))
            {
                ReadNextToken(); // skip 'from'

                if (fCurrentToken.Type == TokenType.Word)
                {
                    // from kısmını sorgula
                    string firstTableName = fCurrentToken.Value.TrimQuotation();
                    string firstAlias = firstTableName;

                    ReadNextToken();

                    if (fCurrentToken != null && fCurrentToken.Equals("AS"))
                    {
                        ReadNextToken();
                        firstAlias = fCurrentToken.Value.TrimQuotation();
                    }

                    ss.From.Add(new Join { Alias = firstAlias, TableName = firstTableName, JoinType = JoinType.Cross });

                    if (fCurrentToken == null)
                        return ss; //***

                    while (fCurrentToken != null &&
                        (fCurrentToken.Equals(",") ||
                        fCurrentToken.Equals("LEFT") ||
                        fCurrentToken.Equals("RIGHT") ||
                        fCurrentToken.Equals("INNER") ||
                        fCurrentToken.Equals("CROSS")))
                    {
                        if (fCurrentToken.Equals(","))
                            ReadNextToken();
                        ss.From.Add(parseJoin());
                    }
                }
            }

            if (fCurrentToken == null)
                return ss;

            if (fCurrentToken.Equals("WHERE"))
            {
                ReadNextToken(); // skip 'where'

                ss.Where = ParseExpression();
            }

            skipEmptyStatements();

            return ss;
        }
        private KeyValuePair<string, Expression> parseSetItem()
        {
            string field = fCurrentToken.Value;
            ReadNextToken();
            SkipExpected("=");
            Expression val = ParseExpression();
            return new KeyValuePair<string, Expression>(field, val);
        }

        DeleteStatement ParseDeleteStatement()
        {
            DeleteStatement ss = new DeleteStatement();

            ReadNextToken(); // skip 'delete'
            SkipExpected("FROM");

            ss.TableName = fCurrentToken.Value;

            if (fCurrentToken == null)
                return ss;

            if (fCurrentToken.Equals("WHERE"))
            {
                ReadNextToken(); // skip 'where'
                ss.Where = ParseExpression();
            }

            skipEmptyStatements();

            return ss;
        }


        CreateDatabaseStatement ParseCreateDatabaseStatement()
        {
            CreateDatabaseStatement ss = new CreateDatabaseStatement();

            ReadNextToken(); //skip "database"

            ss.DatabaseName = fCurrentToken.Value.TrimQuotation();

            ReadNextToken();

            return ss;
        }

        CreateTableStatement ParseCreateTableStatement()
        {
            CreateTableStatement ss = new CreateTableStatement();

            ReadNextToken(); //skip "table"

            ss.TableName = fCurrentToken.Value.TrimQuotation();

            ReadNextToken();
            SkipExpected("(");

            if (isTableConstraint())
                ss.Constraints.Add(parseTableConstraint());
            else
                ss.Columns.Add(parseColumnDef());

            while (!AtEndOfSource && fCurrentToken.Equals(","))
            {
                ReadNextToken(); // skip ","
                if (isTableConstraint())
                    ss.Constraints.Add(parseTableConstraint());
                else
                    ss.Columns.Add(parseColumnDef());
            }

            SkipExpected(")");

            return ss;
        }

        private bool isTableConstraint()
        {
            return fCurrentToken.Equals("CONSTRAINT") ||
                fCurrentToken.Equals("CHECK") ||
                fCurrentToken.Equals("UNIQUE") ||
                fCurrentToken.Equals("PRIMARY") ||
                fCurrentToken.Equals("DEFAULT") ||
                fCurrentToken.Equals("CHARACTER") ||
                fCurrentToken.Equals("COLLATE") ||
                fCurrentToken.Equals("FOREIGN");
        }
        private TableConstraint parseTableConstraint()
        {
            TableConstraint con = new TableConstraint();

            if (fCurrentToken.Equals("CONSTRAINT"))
            {
                ReadNextToken(); // skip "constraint"
                con.Name = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();
            }

            if (fCurrentToken.Equals("CHECK"))
            {
                con.ConstraintType = ConstraintTypes.Check;
                ReadNextToken();
                con.Check = ParseExpression();
            }
            if (fCurrentToken.Equals("DEFAULT"))
            {
                con.ConstraintType = ConstraintTypes.Default;
                ReadNextToken();
                con.Default = ParseExpression();
            }
            if (fCurrentToken.Equals("FOREIGN"))
            {
                con.ConstraintType = ConstraintTypes.ForeignKey;
                ReadNextToken();
                SkipExpected("KEY");
                SkipExpected("(");
                con.Columns = parseListString();
                SkipExpected(")");
                SkipExpected("REFERENCES");

                con.RefTable = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();

                if (fCurrentToken.Equals("("))
                {
                    SkipExpected("(");
                    con.RefColumns = parseListString();
                    SkipExpected(")");
                }
            }
            if (fCurrentToken.Equals("PRIMARY"))
            {
                con.ConstraintType = ConstraintTypes.PrimaryKey;
                ReadNextToken();
                SkipExpected("KEY");
                SkipExpected("(");
                con.Columns = parseListString();
                SkipExpected(")");
            }
            if (fCurrentToken.Equals("UNIQUE"))
            {
                con.ConstraintType = ConstraintTypes.Unique;
                ReadNextToken();
                SkipExpected("(");
                con.Columns = parseListString();
                SkipExpected(")");
            }

            return con;
        }
        private ColumnConstraint parseColumnConstraint()
        {
            ColumnConstraint con = new ColumnConstraint();

            if (fCurrentToken.Equals("CONSTRAINT"))
            {
                ReadNextToken(); // skip "constraint"
                con.Name = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();
            }

            if (fCurrentToken.Equals("DEFAULT"))
            {
                con.ConstraintType = ConstraintTypes.Default;
                ReadNextToken();
                con.Default = ParseExpression();

                return con;
            }

            if (fCurrentToken.Equals("CHECK"))
            {
                con.ConstraintType = ConstraintTypes.Check;
                ReadNextToken();
                con.Check = ParseExpression();

                return con;
            }

            if (fCurrentToken.Equals("NOT"))
            {
                ReadNextToken(); // skip "not"
                SkipExpected("NULL");
                con.ConstraintType = ConstraintTypes.NotNull;

                return con;
            }

            if (fCurrentToken.Equals("NULL"))
            {
                ReadNextToken(); // skip "null"
                con.ConstraintType = ConstraintTypes.Null;
                return con;
            }
            if (fCurrentToken.Equals("REFERENCES"))
            {
                con.ConstraintType = ConstraintTypes.ForeignKey;
                ReadNextToken(); // skip "REFERENCES"

                con.RefTable = fCurrentToken.Value.TrimQuotation();
                ReadNextToken();

                if (fCurrentToken.Equals("("))
                {
                    SkipExpected("(");
                    con.RefColumns = parseListString();
                    SkipExpected(")");
                }
                return con;
            }
            if (fCurrentToken.Equals("PRIMARY"))
            {
                con.ConstraintType = ConstraintTypes.PrimaryKey;
                ReadNextToken();
                SkipExpected("KEY");
                SkipExpected("(");
                con.Columns = parseListString();
                SkipExpected(")");
                return con;
            }
            if (fCurrentToken.Equals("UNIQUE"))
            {
                con.ConstraintType = ConstraintTypes.Unique;
                ReadNextToken();
                SkipExpected("(");
                con.Columns = parseListString();
                SkipExpected(")");
                return con;
            }
            if (fCurrentToken.Equals("CHARACTER"))
            {
                ReadNextToken();
                SkipExpected("SET");
                con.ConstraintType = ConstraintTypes.CharacterSet;
                con.CharacterSet = fCurrentToken.Value;
                ReadNextToken();
                return con;
            }
            if (fCurrentToken.Equals("COLLATE"))
            {
                ReadNextToken();
                con.ConstraintType = ConstraintTypes.Collate;
                con.Collate = fCurrentToken.Value;
                ReadNextToken();
                return con;
            }
            if (fCurrentToken.Equals("AUTO_INCREMENT") || fCurrentToken.Equals("IDENTITY"))
            {
                ReadNextToken();
                con.ConstraintType = ConstraintTypes.AutoIncrement;
                return con;
            }

            throw new ParserException("Unexpected keyword: " + fCurrentToken.Value);
        }
        private ColumnDef parseColumnDef()
        {
            ColumnDef col = new ColumnDef();
            col.ColumnName = fCurrentToken.Value.TrimQuotation();

            ReadNextToken();

            col.ColumnType = fCurrentToken.Value;

            ReadNextToken();
            if (fCurrentToken.Equals("("))
            {
                ReadNextToken();
                Expression exp = ParseIntegerConstant();
                if (exp is IntegerConstant)
                    col.Length = (exp as IntegerConstant).Value;
                if (fCurrentToken.Equals(","))
                {
                    ReadNextToken();
                    Expression exp2 = ParseIntegerConstant();
                    if (exp2 is IntegerConstant)
                        col.Scale = (exp2 as IntegerConstant).Value;
                }
                SkipExpected(")");
            }

            while (!AtEndOfSource && !(fCurrentToken.Equals(",") || fCurrentToken.Equals(")")))
                col.Constraints.Add(parseColumnConstraint());

            return col;
        }

        Expression ParseExpression()
        {
            return ParseIfShortCutExpression();
        }

        Expression ParseIfShortCutExpression()
        {
            Expression lNode = ParseIsNullShortCutExpression();

            if (!AtEndOfSource)
            {
                Expression first = null, second = null;
                if (fCurrentToken.Equals("?"))
                {
                    ReadNextToken(); // skip '?'
                    first = ParseIsNullShortCutExpression();
                }
                if (fCurrentToken.Equals(":"))
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
            Expression lNode = ParseAndExpression();

            if (!AtEndOfSource)
            {
                if (fCurrentToken.Equals("??"))
                {
                    ReadNextToken(); // skip '??'
                    lNode = new IsNullShortCut(lNode, ParseAndExpression());
                }
            }

            return lNode;
        }

        Expression ParseAndExpression()
        {
            Expression lNode = ParseOrExpression();

            while (!AtEndOfSource && (fCurrentToken.Equals("AND") || fCurrentToken.Equals("&&")))
            {
                ReadNextToken(); // skip '&&'
                lNode = new AndExpression(lNode, ParseOrExpression());
            }

            return lNode;
        }

        Expression ParseOrExpression()
        {
            Expression lNode = ParseComparison();

            while (!AtEndOfSource && (fCurrentToken.Equals("OR") || fCurrentToken.Equals("||")))
            {
                ReadNextToken(); // skip '||'
                lNode = new OrExpression(lNode, ParseComparison());
            }

            return lNode;
        }

        Expression ParseComparison()
        {
            Expression lNode = ParseAdditiveExpression();

            if (!AtEndOfSource)
            {
                ComparisonOperator lOperator = ComparisonOperator.None;
                switch (fCurrentToken.Value.ToLowerInvariant())
                {
                    case "=": lOperator = ComparisonOperator.Equal; break;
                    case "==": lOperator = ComparisonOperator.Equal; break;
                    case "<>": lOperator = ComparisonOperator.NotEqual; break;
                    case "!=": lOperator = ComparisonOperator.NotEqual; break;
                    case "<": lOperator = ComparisonOperator.LessThan; break;
                    case ">": lOperator = ComparisonOperator.GreaterThan; break;
                    case "<=": lOperator = ComparisonOperator.LessThanOrEqual; break;
                    case ">=": lOperator = ComparisonOperator.GreaterThanOrEqual; break;
                    case "like": lOperator = ComparisonOperator.Like; break;
                    default: return lNode;
                }
                if (lOperator != ComparisonOperator.None)
                {
                    ReadNextToken(); // skip comparison operator
                    return new Comparison(lOperator, lNode, ParseAdditiveExpression());
                }
            }
            
            return lNode;
        }

        Expression ParseAdditiveExpression()
        {
            Expression lNode = ParseModExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals("+"))
                {
                    ReadNextToken(); // skip '+'
                    lNode = new Addition(lNode, ParseModExpression());
                }
                else if (fCurrentToken.Equals("-"))
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
            Expression lNode = ParseMultiplicativeExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals("%") || fCurrentToken.Equals("MOD"))
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
            Expression lNode = ParseDotExpression();

            while (!AtEndOfSource)
            {
                if (fCurrentToken.Equals("*"))
                {
                    ReadNextToken(); // skip '*'
                    lNode = new Multiplication(lNode, ParseDotExpression());
                }
                else if (fCurrentToken.Equals("/"))
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
            Expression lNode = ParseUnaryExpression();

            while (!AtEndOfSource && fCurrentToken.Equals("."))
            {
                ReadNextToken(); // skip '.'
                Expression exp = ParseVariableOrFunctionCall();
                lNode = new MemberAccess(lNode, exp);
            }

            return lNode;
        }

        Expression ParseUnaryExpression()
        {
            CheckForUnexpectedEndOfSource();
            if (fCurrentToken.Equals("-"))
            {
                ReadNextToken(); // skip '-'
                return new Negation(ParseBaseExpression());
            }
            else if (fCurrentToken.Equals("!") || fCurrentToken.Equals("NOT"))
            {
                ReadNextToken(); // skip '!'
                return new NotExpression(ParseBaseExpression());
            }
            else if (fCurrentToken.Equals("+"))
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

            SkipExpected(")"); // skip ')'

            return lExpression;
        }

        Expression ParseVariableOrFunctionCall()
        {
            string lName = fCurrentToken.Value;

            ReadNextToken();

            if (AtEndOfSource)
                return new Variable(lName);

            if (fCurrentToken.Equals("("))
                return ParseFunctionCall(lName);
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
            if(fCurrentToken.Equals("("))
                ReadNextToken(); // skip '('
            CheckForUnexpectedEndOfSource();

            List<Expression> lArguments = new List<Expression>();
            if (!fCurrentToken.Equals(")"))
            {
                Expression arg = null;
                if (fCurrentToken.Equals("*"))
                {
                    arg = new Variable("*");
                    ReadNextToken();
                }
                else
                    arg = ParseExpression();
                lArguments.Add(arg);
                CheckForUnexpectedEndOfSource();

                while (fCurrentToken.Equals(","))
                {
                    ReadNextToken(); // skip ','
                    lArguments.Add(ParseExpression());
                    CheckForUnexpectedEndOfSource();
                }

                if (!fCurrentToken.Equals(")"))
                    throw new ParserException("Expected ')'.");
            }

            ReadNextToken(); // skip ')'

            // if this ends with ; skip it.
            //skipEmptyStatements();

            return new FunctionCall(name, lArguments.ToArray());
        }

        private void skipEmptyStatements()
        {
            while (fCurrentToken!=null && fCurrentToken.Equals(";"))
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
            if (commentSymbol == "//" || commentSymbol == "--")
            {
                while (fCurrentChar != '\n')
                    ReadNextChar();
                ReadNextChar();
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
                    + fCurrentChar + "' after '"
                    + fTokenValueBuffer + "'.");
            }
        }

        public Token ReadNextToken()
        {
            SkipWhitespace();

            Token token = null;

            if (AtEndOfSource)
                token =  null;
            else if (currentCharIsLetter())      // if the first character is a letter, the token is a word
                token = ReadWord();
            else if (char.IsDigit(fCurrentChar)) // if the first character is a digit, the token is an integer constant
                token = ReadIntegerOrDecimalConstant();
            else if (fCurrentChar == '\'')       // if the first character is a quote, the token is a string constant
                token = ReadStringConstant();
            else                                 // in all other cases, the token should be a symbol
            {
                token = ReadSymbol();

                if (token.Value == "/*" || token.Value == "//" || token.Value == "--")
                {
                    SkipComment(token.Value);
                    token = ReadNextToken();
                }
            }

            return token;
        }

        private bool currentCharIsLetter()
        {
            //                                                                       mysql quote           MS SQL quote                              postgre quote
            return char.IsLetter(fCurrentChar) || fCurrentChar == '_' || fCurrentChar == '`' || fCurrentChar == '[' || fCurrentChar == ']' || fCurrentChar == '"';
        }
        Token ReadWord()
        {
            if (fCurrentChar == '`')
            {
                do StoreCurrentCharAndReadNext(); while (fCurrentChar != '`');
                StoreCurrentCharAndReadNext();
            }
            else if (fCurrentChar == '[')
            {
                do StoreCurrentCharAndReadNext(); while (fCurrentChar != ']');
                StoreCurrentCharAndReadNext();
            }
            else if (fCurrentChar == '"')
            {
                do StoreCurrentCharAndReadNext(); while (fCurrentChar != '"');
                StoreCurrentCharAndReadNext();
            }
            else
                do
                {
                    StoreCurrentCharAndReadNext();
                }
                while (currentCharIsLetter() || char.IsLetterOrDigit(fCurrentChar));

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
            label_devam:
            StoreCurrentCharAndReadNext();
            while (!AtEndOfSource && fCurrentChar != quoteChar)
            {
                if (fCurrentChar == '\\')
                {
                    StoreCurrentCharAndReadNext();
                    switch (fCurrentChar)
                    {
                        case '\'':
                            StoreCurrentCharAndReadNext();
                            break;
                    }
                }
                else
                    StoreCurrentCharAndReadNext();
            }
            StoreCurrentCharAndReadNext();

            if (fCurrentChar == quoteChar) // SQL ''   \' anlamına gelir..
                goto label_devam;

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
                case '@':
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
                // the symbols < <= <>
                case '<':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '=' || fCurrentChar == '>')
                        StoreCurrentCharAndReadNext();
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

        public bool Equals(string value)
        {
            return fValue.ToLowerInvariant() == value.ToLowerInvariant();
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
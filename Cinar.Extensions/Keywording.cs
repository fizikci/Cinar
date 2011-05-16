using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cinar.Extensions
{
    public class Keywording
    {
        public static MatchResult Match(string content, string keyword)
        {
            KeywordParser p = new KeywordParser();
            p.Parse(keyword);
            return p.Match(content);
        }
    }

    public class KeywordParser
    {
        private string keyword;
        Expression parsedExpression;
        private Token currentToken;

        public void Parse(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new Exception("Keyword belirtilmemiş.");

            i = -1;
            if (!keyword.Contains("<")) keyword = "<" + keyword + ">";
            this.keyword = keyword;

            currentToken = readNextToken();
            if (!(currentToken.IsWord || currentToken.Value == "("))
                throw new Exception("Keyword <kelime> veya parantez ile başlamalıdır.");

            parsedExpression = parseExpression();

        }
        private Expression parseExpression()
        {
            return parseOrExpression();
        }


        private Expression parseOrExpression()
        {
            Expression lNode = parseButNotExpression();

            if (currentToken != null)
            {
                if (currentToken.Value == "|")
                {
                    readNextToken();
                    Expression rNode = parseExpression();
                    if (rNode != null)
                        lNode = new OrExpression() { Exp1 = lNode, Exp2 = rNode };
                }
            }

            return lNode;
        }

        private Expression parseButNotExpression()
        {
            Expression lNode = parsePlusExpression();

            if (currentToken != null)
            {
                if (currentToken.Value == "^")
                {
                    readNextToken();
                    Expression rNode = parseExpression();
                    if (lNode is Word && rNode is Word)
                        lNode = new ButNotExpression() { Exp1 = lNode as Word, Exp2 = rNode as Word };
                    else
                        throw new Exception("ButNot ifadesi <word1>^<word2> şeklinde olmalıdır. Yani iki kelimeden oluşmalıdır.");
                }
            }

            return lNode;
        }

        private Expression parsePlusExpression()
        {
            Expression lNode = parseMinusExpression();

            if (currentToken != null)
            {
                if (currentToken.Value == "+")
                {
                    readNextToken();
                    Expression rNode = parseExpression();
                    if (rNode != null)
                        lNode = new PlusExpression() { Exp1 = lNode, Exp2 = rNode };
                }
            }

            return lNode;
        }

        private Expression parseMinusExpression()
        {
            Expression lNode = parseBaseExpression();

            if (currentToken != null)
            {
                if (currentToken.Value == "-")
                {
                    readNextToken();
                    Expression rNode = parseExpression();
                    if (rNode != null)
                        lNode = new MinusExpression() { Exp1 = lNode, Exp2 = rNode };
                }
            }

            return lNode;
        }

        private Expression parseBaseExpression()
        {
            if (currentToken.IsWord)
            {
                string word = currentToken.Value;
                readNextToken();
                return new Word() { TheWord = word };
            }

            if (currentToken.Value == "(")
                return parseGroupExpression();

            throw new Exception("İfade bekleniyor.");
        }

        private Expression parseGroupExpression()
        {
            readNextToken(); // skip '('

            Expression lNode = parseExpression();
            if (lNode == null)
                throw new Exception("Boş grup ifadesi!");

            readNextToken(); // skip ')'

            return lNode;
        }


        int i = -1;
        Token readNextToken()
        {
            Token res = null;

            i++;

            if (i >= keyword.Length)
            {
                currentToken = null;
                return currentToken;
            }

            char c = keyword[i];
            if (c == '<')
            {
                int wordStart = ++i;
                for (; i < keyword.Length; i++)
                    if (keyword[i] == '>')
                    {
                        res = new Token() { IsWord = true, Value = keyword.Substring(wordStart, i - wordStart) };
                        break;
                    }
            }
            else if (c == '|' || c == '+' || c == '-' || c == '^' || c == '(' || c == ')')
                res = new Token() { IsWord = false, Value = c.ToString() };
            else
                return readNextToken();

            currentToken = res;
            return res;
        }

        public MatchResult Match(string content)
        {
            MatchResult res = parsedExpression.Match(content);
            res.ParsedExpression = parsedExpression.ToString();
            return res;
        }
    }

    public class MatchResult
    {
        public bool Success { get; set; }
        public List<Match> Matches { get; set; }
        public string ParsedExpression = "";

        public MatchResult()
        {
            Matches = new List<Match>();
        }
    }

    public abstract class Expression
    {
        internal abstract MatchResult Match(string content);
    }
    public class OrExpression : Expression
    {
        public Expression Exp1 { get; set; }
        public Expression Exp2 { get; set; }

        internal override MatchResult Match(string content)
        {
            MatchResult m1 = Exp1.Match(content);
            MatchResult m2 = Exp2.Match(content);

            if (m1.Success && m2.Success)
                m1.Matches.AddRange(m2.Matches);

            if (m1.Success)
                return m1;

            if (m2.Success)
                return m2;

            return new MatchResult();
        }
        public override string ToString()
        {
            return "( " + Exp1 + " | " + Exp2 + " )";
        }
    }
    public class PlusExpression : Expression
    {
        public Expression Exp1 { get; set; }
        public Expression Exp2 { get; set; }

        internal override MatchResult Match(string content)
        {
            MatchResult m1 = Exp1.Match(content);
            MatchResult m2 = Exp2.Match(content);
            m1.Matches.AddRange(m2.Matches);

            if (m1.Success && m2.Success)
                return m1;
            else
                return new MatchResult();
        }
        public override string ToString()
        {
            return "( " + Exp1 + " + " + Exp2 + " )";
        }
    }
    public class MinusExpression : Expression
    {
        public Expression Exp1 { get; set; }
        public Expression Exp2 { get; set; }

        internal override MatchResult Match(string content)
        {
            MatchResult m1 = Exp1.Match(content);
            MatchResult m2 = Exp2.Match(content);
            if (m1.Success && !m2.Success)
                return m1;

            return new MatchResult();
        }

        public override string ToString()
        {
            return "( " + Exp1 + " - " + Exp2 + " )";
        }
    }
    public class ButNotExpression : Expression
    {
        public Word Exp1 { get; set; }
        public Word Exp2 { get; set; }

        internal override MatchResult Match(string content)
        {
            MatchResult m1 = Exp2.Match(content);
            MatchResult m2 = new Word { TheWord = Exp1.TheWord + " " + Exp2.TheWord }.Match(content);
            if (m1.Success && !m2.Success)
                return m1;

            return new MatchResult();
        }

        public override string ToString()
        {
            return "( " + Exp1 + " ^ " + Exp2 + " )";
        }
    }
    public class Word : Expression
    {
        public string TheWord { get; set; }

        internal override MatchResult Match(string content)
        {
            string reg = "\\b" + TheWord.Replace("*", "[A-Za-z0-9ğüşıöçĞÜŞİÖÇ+\\-*/!'^+%&/()=?_]*") + "\\b";
            Regex r = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection coll = r.Matches(content);

            MatchResult res = new MatchResult();
            res.Success = coll.Count > 0;
            foreach (Match match in coll)
                res.Matches.Add(match);
            return res;
        }

        public override string ToString()
        {
            return "<" + TheWord + ">";
        }
    }

    public class Token
    {
        public bool IsWord { get; set; }
        public string Value { get; set; }
    }
}

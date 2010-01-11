using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Cinar.HTMLParser
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

        public void Parse()
        {
            while (fCurrentToken != null)
            {
                if (fCurrentToken.Equals(TokenType.Symbol, "<!--"))
                {
                    string inner = "";
                    while (!fCurrentToken.Equals(TokenType.Symbol, "-->"))
                    {
                        inner += " " + fCurrentToken.Value;
                        ReadNextToken();
                    }
                    ReadNextToken();
                    statementFound(new Comment(inner));
                }
                else if (fCurrentToken.Equals(TokenType.Symbol, "<"))
                {
                    ReadNextToken();
                    string tagName = fCurrentToken.Value;
                    ReadNextToken();
                    if (fCurrentToken.Equals(TokenType.Symbol, "/>"))
                    {
                        statementFound(new StandaloneTag(tagName));
                        ReadNextToken();
                    }
                    else if (fCurrentToken.Equals(TokenType.Symbol, ">"))
                    {
                        statementFound(new OpenTag(tagName));
                        ReadNextToken();
                    }
                    else if (fCurrentToken.Type == TokenType.Word)
                    {
                        statementFound(new OpenTag(tagName));
                        string attributeName = fCurrentToken.Value;
                        ReadNextToken();
                        while (!fCurrentToken.Equals(TokenType.Symbol, ">"))
                        {
                            if (fCurrentToken.Equals(TokenType.Symbol, "/>"))
                            {
                                statementFound(new CloseTag(tagName));
                                break;
                            }
                            if (fCurrentToken.Value == "=")
                            {
                                ReadNextToken();
                                statementFound(new Attribute(attributeName, fCurrentToken.Value));
                            }
                            else
                                statementFound(new Attribute(attributeName, null));

                            ReadNextToken();
                            if (fCurrentToken.Type == TokenType.Word)
                            {
                                attributeName = fCurrentToken.Value;
                                ReadNextToken();
                                if (fCurrentToken.Equals(TokenType.Symbol, ":"))
                                {
                                    ReadNextToken();
                                    attributeName += ":" + fCurrentToken.Value;
                                    ReadNextToken();
                                }
                            }
                        }
                        ReadNextToken();
                    }
                }
                else if (fCurrentToken.Equals(TokenType.Symbol, "</"))
                {
                    ReadNextToken();
                    string tagName = fCurrentToken.Value;
                    ReadNextToken();
                    ReadNextToken();
                    statementFound(new CloseTag(tagName));
                }
                else 
                {
                    string innerText = "";
                    while (!(fCurrentToken.Equals(TokenType.Symbol, "<") || fCurrentToken.Equals(TokenType.Symbol, "</")))
                    {
                        innerText += " " + fCurrentToken.Value;
                        ReadNextToken();
                    }
                    statementFound(new InnerText(innerText));
                }
            }
        }

        public HTMLElement RootNode { get; internal set; }
       
        public HTMLElement CreateElement(string tagName)
        {
            switch (tagName)
            {
                case "IMG":
                    return new ImageElement();
                case "SPAN":
                    return new SpanElement();
                case "SCRIPT":
                    return new ScriptElement();
                default:
                    return new DivElement();
            }
        }

        Stack<HTMLElement> stack = new Stack<HTMLElement>();

        
        private void statementFound(OpenTag stm)
        {
            HTMLElement elm = CreateElement(stm.Text.ToUpperInvariant());

            elm.TagName = stm.Text;

            HTMLElement parent = null;
            if(stack.Count>0)
             parent = stack.Peek();
            elm.Parent = parent;

            if (parent == null)
                RootNode = elm;
            else
                parent.ChildNodes.Add(elm);

            stack.Push(elm);
        }
        private void statementFound(CloseTag stm)
        {
            stack.Pop();
        }
        private void statementFound(StandaloneTag stm)
        {
            HTMLElement elm = CreateElement(stm.Text.ToUpperInvariant());
            elm.TagName = stm.Text;

            HTMLElement parent = stack.Peek();
            elm.Parent = parent;

            if (parent == null)
                RootNode = elm;
            else
                parent.ChildNodes.Add(elm);
        }
        private void statementFound(Attribute stm)
        {
            HTMLElement elm = stack.Peek();
            elm.SetAttribute(stm.Name, stm.Value);
        }
        private void statementFound(InnerText stm)
        {
            InnerTextElement elm = new InnerTextElement();
            elm.TagName = "InnerText";
            elm.InnerText = stm.Text;

            HTMLElement parent = stack.Peek();
            elm.Parent = parent;

            if (parent == null)
                RootNode = elm;
            else
                parent.ChildNodes.Add(elm);
        }
        private void statementFound(Comment stm)
        { 
        }
    }

    public class Statement
    {
    }
    public class OpenTag : Statement
    {
        public string Text { get; set; }
        public OpenTag(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return "<" + Text + ">";
        }
    }
    public class CloseTag : Statement
    {
        public string Text { get; set; }
        public CloseTag(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return "</" + Text + ">";
        }
    }
    public class StandaloneTag : Statement
    {
        public string Text { get; set; }
        public StandaloneTag(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return "<" + Text + "/>";
        }
    }
    public class Attribute : Statement
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Attribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return Name + " = " + Value;
        }
    }
    public class InnerText : Statement
    {
        public string Text { get; set; }
        public InnerText(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
    }
    public class Comment : Statement
    {
        public string Text { get; set; }
        public Comment(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return "<!--" + Text + "-->";
        }
    }

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

        public Token ReadNextToken()
        {
            SkipWhitespace();

            if (AtEndOfSource)
                return null;

            // if the first character is a letter, the token is a word
            if (char.IsLetterOrDigit(fCurrentChar) || fCurrentChar=='_')
                return ReadWord();

            // if the first character is a quote, the token is a string constant
            if (fCurrentChar == '"' || fCurrentChar == '\'')
                return ReadStringConstant();

            // in all other cases, the token should be a symbol
            Token token = ReadSymbol();

            return token;
        }

        Token ReadWord()
        {
            do
            {
                StoreCurrentCharAndReadNext();
            }
            while (char.IsLetterOrDigit(fCurrentChar) || fCurrentChar == '-');

            return new Token(TokenType.Word, ExtractStoredChars());
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
                // the symbols < </ <!--
                case '<':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '/')
                        StoreCurrentCharAndReadNext();

                    if (fCurrentChar == '!')
                    {
                        StoreCurrentCharAndReadNext();
                        if (fCurrentChar == '-')
                        {
                            StoreCurrentCharAndReadNext();
                            if (fCurrentChar == '-')
                                StoreCurrentCharAndReadNext();
                        }
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols / />
                case '/':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '>')
                    {
                        StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                // the symbols - -- -->
                case '-':
                    StoreCurrentCharAndReadNext();
                    if (fCurrentChar == '-')
                    {
                        StoreCurrentCharAndReadNext();
                        if (fCurrentChar == '>')
                            StoreCurrentCharAndReadNext();
                    }
                    return new Token(TokenType.Symbol, ExtractStoredChars());
                case '>':
                case '=':
                case ':':
                default:
                    StoreCurrentCharAndReadNext();
                    return new Token(TokenType.Symbol, ExtractStoredChars());
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
        String,
        Symbol
    }

}
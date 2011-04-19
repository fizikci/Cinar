using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Reflection;
using Cinar.Database;

namespace Cinar.DBTools
{
    public class CinarSQLEditor : TextEditorControl, ICompletionDataProvider, IFoldingStrategy, ISyntaxModeFileProvider
    {
        private System.Windows.Forms.ImageList imageList;

        private enum Images {
            Class = 0,
            LocalVariable = 1,
            Method = 2,
            Property = 3,
            Struct = 4,
            Interface = 5
        }

        public CinarSQLEditor()
        {
            Document.FoldingManager.FoldingStrategy = this;
            
            HighlightingManager.Manager.AddSyntaxModeFileProvider(this); // Attach to the text editor.
            SetHighlighting("SQL");

            ActiveTextAreaControl.TextArea.KeyUp += TextArea_KeyUp;
            //ActiveTextAreaControl.TextArea.KeyDown += TextArea_KeyDown;

            imageList = new System.Windows.Forms.ImageList();
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList.ImageSize = new System.Drawing.Size(16, 16);
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.Add(SQLResources.Class);
            imageList.Images.Add(SQLResources.LocalVariable);
            imageList.Images.Add(SQLResources.Method);
            imageList.Images.Add(SQLResources.Property);
            imageList.Images.Add(SQLResources.Struct);
            imageList.Images.Add(SQLResources.Interface);
        }

        //void TextArea_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        CodeCompletionWindow.ShowCompletionWindow(this.FindForm(), this, null, this, ' ');
        //        e.Handled = true;
        //    }
        //}

        void TextArea_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue==190 || e.KeyCode == System.Windows.Forms.Keys.F1)
                CodeCompletionWindow.ShowCompletionWindow(this.FindForm(), this, null, this, '\0');
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        #region ICompletionDataProvider Members

        public int DefaultIndex
        {
            get 
            {
                return 0;
            }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            List<TextWord> words = textArea.Document.GetLineSegment(textArea.Caret.Line).Words;
            TextWord currentWord = textArea.Document.GetLineSegment(textArea.Caret.Line).GetWord(textArea.Caret.Column - 1);
            int index = words.IndexOf(currentWord);
            List<string> wordsChain = new List<string>();

            if (currentWord == null)
            {
                return getTypeList();
            }

            if (currentWord.Word == ".")
            {
                int i = index;
                while (currentWord.Word == ".")
                {
                    i--;
                    while (words[i].IsWhiteSpace) i--;
                    currentWord = words[i];
                    wordsChain.Insert(0, currentWord.Word);
                    i--;
                    if (i < 0)
                        break;
                }

                return getTypeMemberList(wordsChain.Last());
            }
            
            if (currentWord.IsWhiteSpace)
            {
                return getTypeList();
            }

            return null;
        }

        private ICompletionData[] getTypeList()
        {
            List<CinarCompletionData> list = new List<CinarCompletionData>();
            list.Add(new CinarCompletionData("insert", "", (int)Images.Struct));
            list.Add(new CinarCompletionData("update", "", (int)Images.Struct));
            list.Add(new CinarCompletionData("delete", "", (int)Images.Struct));
            list.Add(new CinarCompletionData("select", "", (int)Images.Struct));
            list.Add(new CinarCompletionData("alter", "alter table", (int)Images.Method));
            foreach (Table tbl in Provider.Database.Tables)
                list.Add(new CinarCompletionData(tbl.Name, "table", (int)Images.Class));
            return list.ToArray();
        }

        private ICompletionData[] getTypeMemberList(string varName)
        {
            List<CinarCompletionData> list = new List<CinarCompletionData>();
            Table tbl = Provider.Database.Tables[varName];
            if (tbl == null)
                return list.ToArray();

            foreach (Field f in tbl.Fields)
            {
                string desc = f.FieldType + " " + f.Table.Name + "." + f.Name;
                list.Add(new CinarCompletionData(f.Name, desc, (int)Images.Property));
            }
            return list.ToArray();
        }

        public System.Windows.Forms.ImageList ImageList
        {
            get 
            {
                return imageList;
            }
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.InsertString(data.Text);
            return true;
        }

        public string PreSelection
        {
            get 
            {
                //TextArea textArea = ActiveTextAreaControl.TextArea;
                //TextWord tw = textArea.Document.GetLineSegment(textArea.Caret.Line).GetWord(textArea.Caret.Column - 1);

                //string lastWord = tw==null ? null : tw.Word;
                //if (lastWord != ".")
                //    return lastWord;
                //else
                    return "";
            }
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            return CompletionDataProviderKeyResult.InsertionKey;
        }

        #endregion

        #region IFoldingStrategy Members

        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            //StatementCollection coll = new StatementCollection((parseInformation as Interpreter).StatementsTree, false);
            //foreach (Statement st in coll)
            //    if (st is FunctionDefinitionStatement)
            //    {
            //        LineSegment startLine = document.GetLineSegment(st.LineNumber);
            //        LineSegment endLine = document.GetLineSegment(st.LastLineNumber - 1);

            //        if (startLine.LineNumber + 3 < endLine.LineNumber)
            //            list.Add(new FoldMarker(document, startLine.LineNumber, startLine.Length - 1, endLine.LineNumber, endLine.Length));
            //    }
            return list;
        }

        #endregion

        #region ISyntaxModeFileProvider Members

        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            return new XmlTextReader(new StringReader(SQLResources.SQL));
        }

        public void UpdateSyntaxModeList()
        {
        }

        public ICollection<SyntaxMode> SyntaxModes
        {
            get
            {
                ICollection<SyntaxMode> modes = new List<SyntaxMode>();
                SyntaxMode sm = new SyntaxMode("", "SQL", ".sql");
                modes.Add(sm);
                return modes;
            }
        }

        #endregion
    }

    public class CinarCompletionData : ICompletionData
    {
        // Fields
        private string description;
        private int imageIndex;
        private double priority;
        private string text;

        // Methods
        public CinarCompletionData(string text, string description, int imageIndex)
        {
            this.text = text;
            this.description = description;
            this.imageIndex = imageIndex;
        }

        public static int Compare(ICompletionData a, ICompletionData b)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }
            if (b == null)
            {
                throw new ArgumentNullException("b");
            }
            return string.Compare(a.Text, b.Text, StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool InsertAction(TextArea textArea, char ch)
        {
            textArea.InsertString(this.text);
            return false;
        }

        // Properties
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public void SetDescription(string desc)
        {
            this.description = desc;
        }

        public int ImageIndex
        {
            get
            {
                return this.imageIndex;
            }
        }

        public double Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }


}

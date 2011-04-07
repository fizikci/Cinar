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

namespace Cinar.Scripting
{
    public class CinarScriptEditor : TextEditorControl, ICompletionDataProvider, IFoldingStrategy, ISyntaxModeFileProvider
    {
        private Interpreter interpreter;
        private System.Windows.Forms.ImageList imageList;

        private enum Images {
            Class = 0,
            LocalVariable = 1,
            Method = 2,
            Property = 3,
            Struct = 4,
            Interface = 5
        }

        public CinarScriptEditor()
        {
            Document.FoldingManager.FoldingStrategy = this;
            
            HighlightingManager.Manager.AddSyntaxModeFileProvider(this); // Attach to the text editor.
            SetHighlighting("Cinar");

            ActiveTextAreaControl.TextArea.KeyUp += TextArea_KeyUp;
            //ActiveTextAreaControl.TextArea.KeyDown += TextArea_KeyDown;

            imageList = new System.Windows.Forms.ImageList();
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList.ImageSize = new System.Drawing.Size(16, 16);
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.Add(Resources.Class);
            imageList.Images.Add(Resources.LocalVariable);
            imageList.Images.Add(Resources.Method);
            imageList.Images.Add(Resources.Property);
            imageList.Images.Add(Resources.Struct);
            imageList.Images.Add(Resources.Interface);
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

            if (Context.debugging)
                return;

            try
            {
                interpreter = new Interpreter(this.Text, null);
                interpreter.Parse();
                Document.FoldingManager.UpdateFoldings(null, interpreter);
            }
            catch { }
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
            try
            {
                List<TextWord> words = textArea.Document.GetLineSegment(textArea.Caret.Line).Words;
                TextWord currentWord = textArea.Document.GetLineSegment(textArea.Caret.Line).GetWord(textArea.Caret.Column - 1);
                string namespaceStartsWith = "";
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
                        while (words[i].IsWhiteSpace && i>0) i--;
                        if (!words[i].IsWhiteSpace) currentWord = words[i];
                        if (currentWord.Word == "using")
                        {
                            namespaceStartsWith = string.Join(".", wordsChain.ToArray());
                            return getNamespaceList(namespaceStartsWith);
                        }
                    }

                    return getTypeMemberList(wordsChain.Last());
                }

                if (currentWord.IsWhiteSpace)
                {
                    return getTypeList();
                }
            }
            catch { }
            return null;
        }

        private ICompletionData[] getNamespaceList(string namespaceStartsWith)
        { 
            List<CinarCompletionData> list = new List<CinarCompletionData>();
            foreach(string ns in Context.GetNamespaceNames(namespaceStartsWith))
                list.Add(new CinarCompletionData(ns, "namespace", (int)Images.Interface));
            return list.ToArray();
        }

        private ICompletionData[] getTypeList()
        {
            List<CinarCompletionData> list = new List<CinarCompletionData>();
            list.Add(new CinarCompletionData("debugger", "break point statement", (int)Images.Struct));
            list.Add(new CinarCompletionData("echo", "writes to output", (int)Images.Method));
            foreach (VariableDefinition varDef in Context.ParsedVariables)
                list.Add(new CinarCompletionData(varDef.Variable.Name, "local variable", (int)Images.LocalVariable));
            foreach (FunctionDefinitionStatement fDef in Context.ParsedFunctions)
                list.Add(new CinarCompletionData(fDef.Name, "local function", (int)Images.Method));
            foreach (string nameSpace in Context.ParsedUsing)
                foreach (Type t in Context.GetTypeNames(nameSpace))
                {
                    Images imgIndex = Images.Class;
                    if (t.IsInterface) imgIndex = Images.Interface;
                    if (t.IsValueType) imgIndex = Images.Struct;
                    list.Add(new CinarCompletionData(t.Name, t.FullName, (int)imgIndex));
                }
            return list.ToArray();
        }

        private ICompletionData[] getTypeMemberList(string varName)
        {
            List<CinarCompletionData> list = new List<CinarCompletionData>();
            VariableDefinition varDef = Context.ParsedVariables.Find(vd => vd.Variable.Name == varName);
            BindingFlags bf = BindingFlags.Instance;
            Type type = null;
            if (varDef == null)
            {
                type = Context.GetType(varName, Context.ParsedUsing);
                bf = BindingFlags.Static;
            }
            else if (varDef.Variable.Type == "int")
                type = typeof(int);
            else if (varDef.Variable.Type == "bool")
                type = typeof(bool);
            else if (varDef.Variable.Type == "long")
                type = typeof(long);
            else if (varDef.Variable.Type == "decimal")
                type = typeof(decimal);
            else if (varDef.Variable.Type == "string")
                type = typeof(string);
            else if (varDef.Variable.Type == "float")
                type = typeof(float);
            else
            {
                if (varDef.Value is StringConstant)
                    type = typeof(string);
                else if (varDef.Value is IntegerConstant)
                    type = typeof(int);
                else if (varDef.Value is DecimalConstant)
                    type = typeof(decimal);
                else
                    type = Context.GetType(varDef.Variable.Type, Context.ParsedUsing);
            }
            if (type != null)
            {
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | bf))
                {
                    string desc = pi.PropertyType.FullName + " " + pi.DeclaringType.Name + "." + pi.Name;
                    list.Add(new CinarCompletionData(pi.Name, desc, (int)Images.Property));
                }
                foreach (FieldInfo fi in type.GetFields(BindingFlags.Public | bf))
                {
                    string desc = fi.FieldType.FullName + " " + fi.DeclaringType.Name + "." + fi.Name;
                    list.Add(new CinarCompletionData(fi.Name, desc, (int)Images.LocalVariable));
                }
                foreach (MethodInfo mi in type.GetMethods(BindingFlags.Public | bf))
                    if (!mi.IsSpecialName)
                    {
                        List<string> pis = new List<string>();
                        foreach (ParameterInfo pi in mi.GetParameters())
                            pis.Add(pi.ParameterType.Name + " " + pi.Name);

                        string desc = (mi.ReturnType == null ? "void" : mi.ReturnType.FullName) + " " + mi.DeclaringType.Name + "." + mi.Name + "(" + string.Join(", ", pis.ToArray()) + ")";
                        CinarCompletionData item = list.Find(cd => cd.Text == mi.Name);
                        if (item == null)
                            list.Add(new CinarCompletionData(mi.Name, desc, (int)Images.Method));
                        else
                            item.SetDescription(item.Description + "\n" + desc);
                    }
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
                    return null;
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
            try
            {
                StatementCollection coll = new StatementCollection((parseInformation as Interpreter).StatementsTree, false);
                foreach (Statement st in coll)
                    if (st is FunctionDefinitionStatement)
                    {
                        LineSegment startLine = document.GetLineSegment(st.LineNumber);
                        LineSegment endLine = document.GetLineSegment(st.LastLineNumber - 1);

                        while (endLine.Words.Count == 0)
                            endLine = document.GetLineSegment(endLine.LineNumber - 1);

                        if (startLine.LineNumber + 3 < endLine.LineNumber)
                            list.Add(new FoldMarker(document, startLine.LineNumber, startLine.Length - 1, endLine.LineNumber, endLine.Length));
                    }
            }
            catch { }
            return list;
        }

        #endregion

        #region ISyntaxModeFileProvider Members

        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            return new XmlTextReader(new StringReader(Resources.CinarMode));
        }

        public void UpdateSyntaxModeList()
        {
        }

        public ICollection<SyntaxMode> SyntaxModes
        {
            get
            {
                ICollection<SyntaxMode> modes = new List<SyntaxMode>();
                SyntaxMode sm = new SyntaxMode("", "Cinar", ".csc");
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

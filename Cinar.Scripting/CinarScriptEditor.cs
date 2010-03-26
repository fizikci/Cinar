using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace Cinar.Scripting
{
    public class CinarScriptEditor : TextEditorControl
    {
        public CinarScriptEditor()
        {
            Document.FoldingManager.FoldingStrategy = new MyFolding();
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new MySyntaxModeProvider()); // Attach to the text editor.
            SetHighlighting("Cinar");
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            try
            {
                Interpreter ip = new Interpreter(this.Text, null);
                ip.Parse();

                Document.FoldingManager.UpdateFoldings(null, ip);
            }
            catch { }
        }
    }

    public class MyFolding : IFoldingStrategy
    {
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            StatementCollection coll = new StatementCollection((parseInformation as Interpreter).StatementsTree);
            foreach (Statement st in coll)
            {
                if (st.SubStatements1 != null)
                    findFolderMarkers(st.SubStatements1, list, document);
                if (st.SubStatements2 != null)
                    findFolderMarkers(st.SubStatements2, list, document);
            }
            return list;
        }

        private void findFolderMarkers(StatementCollection coll, List<FoldMarker> list, IDocument document)
        {
            if (coll.Count > 1)
            {
                int start = coll.First().LineNumber - 2;
                int end = coll.Last().LineNumber;
                if (start + 2 < end)
                    list.Add(new FoldMarker(document, start, 0, end, 0));
            }

            foreach (Statement st in coll)
            {
                if (st.SubStatements1 != null)
                    findFolderMarkers(st.SubStatements1, list, document);
                if (st.SubStatements2 != null)
                    findFolderMarkers(st.SubStatements2, list, document);
            }
        }
    }

    public class MySyntaxModeProvider : ISyntaxModeFileProvider
    {
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
    }

}

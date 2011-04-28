using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Text.RegularExpressions;

namespace Cinar.DBTools
{
    public partial class FindDialog : Form
    {
        TextEditorControl target;

        public FindDialog(TextEditorControl target)
        {
            InitializeComponent();
            this.target = target;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (target.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                ISelection sel = target.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                if(sel.SelectedText.Length<50)
                    txtFind.Text = target.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].SelectedText;
            }
        }

        int startIndex = 0;
        bool replaceAll = false;

        private void find(bool replace)
        {
            if (!txtFind.AutoCompleteCustomSource.Contains(txtFind.Text))
                txtFind.AutoCompleteCustomSource.Add(txtFind.Text);

            if (replace && target.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                ISelection sel = target.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                target.Document.Replace(sel.Offset, sel.Length, txtReplace.Text);
                target.ActiveTextAreaControl.SelectionManager.ClearSelection();
                target.ActiveTextAreaControl.Caret.Position = target.Document.OffsetToPosition(sel.Offset + txtReplace.Text.Length);
            }

            int index = findText();
            if (index == -1)
            {
                MessageBox.Show(startIndex == 0 ? "Not found any occurence." : "Search returned to the start.", "Find and Replace");
                startIndex = 0;
                target.ActiveTextAreaControl.SelectionManager.ClearSelection();
                replaceAll = false;
                return;
            }

            startIndex = index + 1;

            TextLocation start = target.Document.OffsetToPosition(index);
            TextLocation end = target.Document.OffsetToPosition(index + foundText.Length);
            target.ActiveTextAreaControl.SelectionManager.SetSelection(start, end);

            if (replaceAll)
                find(true);
        }

        string foundText = "";

        private int findText()
        {
            int index = 0;
            if (!cbUseRegEx.Checked && !cbMatchWholeWord.Checked)
            {
                index = target.Text.IndexOf(txtFind.Text, startIndex, cbMatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                foundText = txtFind.Text;
            }
            else
            {
                Regex re = new Regex((cbMatchWholeWord.Checked ? "\b" : "") + txtFind.Text + (cbMatchWholeWord.Checked ? "\b" : ""));
                Match m = re.Match(target.Text, startIndex);
                index = m.Success ? m.Index : -1;
                foundText = m.Value;
            }

            return index;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            find(false);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            find(true);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            replaceAll = true;
            find(true);
        }


        protected override void OnDeactivate(EventArgs e)
        {
            this.Opacity = .5;
        }
        protected override void OnActivated(EventArgs e)
        {
            this.Opacity = 1;
        }

    }
}

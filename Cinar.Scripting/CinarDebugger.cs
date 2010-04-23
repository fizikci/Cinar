using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Reflection;
using System.Threading;

namespace Cinar.Scripting
{
    public partial class CinarDebugger : Form
    {
        private Context debugStartContext;

        public CinarDebugger(Context context)
        {
            InitializeComponent();

            this.debugStartContext = context;
            editor.Text = Context.code;
            editor.Document.ReadOnly = true;
            editor.ActiveTextAreaControl.TextArea.KeyDown += new KeyEventHandler(TextArea_KeyDown);
        }

        public void SetMarker(Context context)
        {
            editor.Document.MarkerStrategy.RemoveAll(m => m != null);
            if (Context.CurrentStatement != null)
            {
                statusBarLabel.Text = Context.CurrentStatement.ToString().Split('\n')[0].Trim();
                txtOutput.Text = debugStartContext.RootContext.Interpreter.Output;
                LineSegment seg = editor.Document.GetLineSegment(Context.CurrentStatement.LineNumber);
                TextMarker marker = new TextMarker(seg.Offset + Context.CurrentStatement.ColumnNumber, seg.Length - Context.CurrentStatement.ColumnNumber, TextMarkerType.SolidBlock, Color.Yellow);
                editor.Document.MarkerStrategy.AddMarker(marker);
                editor.ActiveTextAreaControl.Caret.Position = editor.Document.OffsetToPosition(seg.Offset);

                tree.Nodes.Clear();
                while (context.parent != null)
                {
                    foreach (object key in context.Variables.Keys)
                        addNode(null, key.ToString(), context.Variables[key]);
                    context = context.parent;
                }

                Application.DoEvents();
                this.Refresh();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Context.debugging = false;
        }

        private void cmdStepOver()
        {
            Context.debugContinue = true;
        }
        private void cmdStepInto()
        {
            Context.debugContinue = true;
        }
        private void cmdRunToCursor()
        {
            Context.debugRunToLine = editor.ActiveTextAreaControl.Caret.Position.Line;
            Context.debugContinue = true;
        }
        private void cmdContinue()
        {
            Context.debugging = false;
            Context.debugRunToLine = 0;
            Context.debugContinue = true;
        }

        private void tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "ready-to-be-expanded-kafası")
            {
                e.Node.Nodes.Clear();
                if (e.Node.Tag is System.Collections.Hashtable)
                {
                    System.Collections.Hashtable ht = e.Node.Tag as System.Collections.Hashtable;
                    foreach (object key in ht.Keys)
                        addNode(e.Node, "[" + key + "]", ht[key]);
                    return;
                }
                if (e.Node.Tag is DataRow)
                {
                    DataRow dr = e.Node.Tag as DataRow;
                    foreach(DataColumn dc in dr.Table.Columns)
                        addNode(e.Node, "[" + dc.ColumnName + "]", dr[dc]);
                    return;
                }
                foreach (PropertyInfo pi in e.Node.Tag.GetType().GetProperties())
                {
                    object val;
                    try
                    {
                        val = pi.GetValue(e.Node.Tag, null);
                    }
                    catch (Exception ex)
                    {
                        val = "ERR: " + ex.Message;
                    }
                    addNode(e.Node, pi.Name, val);
                }
                foreach (FieldInfo fi in e.Node.Tag.GetType().GetFields())
                {
                    object val;
                    try
                    {
                        val = fi.GetValue(e.Node.Tag);
                    }
                    catch (Exception ex)
                    {
                        val = "ERR: " + ex.Message;
                    }
                    addNode(e.Node, fi.Name, val);
                }
            }
        }

        private void addNode(TreeNode parent, string name, object val)
        {
            string strVal = val == null ? "null" : val.ToString();
            if (strVal.Length > 20) strVal = strVal.Substring(0, 20) + "...";
            TreeNode node = new TreeNode(name + " (" + strVal + ")");
            if (val != null && val.GetType() != typeof(string) && !val.GetType().IsValueType)
            {
                node.Nodes.Add("ready-to-be-expanded-kafası");
                node.Collapse();
                node.Tag = val;
            }

            if (parent == null)
                tree.Nodes.Add(node);
            else
                parent.Nodes.Add(node);
        }

        void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                cmdStepOver();
            else if (e.KeyCode == Keys.F11)
                cmdStepInto();
            else if (e.KeyCode == Keys.F12)
                cmdRunToCursor();
            else if (e.KeyCode == Keys.F5)
                cmdContinue();
            else
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void btnStepOver_Click(object sender, EventArgs e)
        {
            cmdStepOver();
        }

        private void btnRunToCursor_Click(object sender, EventArgs e)
        {
            cmdRunToCursor();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            cmdContinue();
        }
    }
}

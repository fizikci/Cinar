using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using Cinar.DBTools.Tools;
using Cinar.Database;
using System.Diagnostics;

namespace Cinar.DBTools.CodeGen
{
    public class Controller
    {
        private FormMain mainForm;

        public Controller(FormMain mainForm)
        {
            this.mainForm = mainForm;

            mainForm.cmdMan.Commands.AddRange(new List<Command> { 
                     new Command {
                                     Execute = cmdNewCinarSolution,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuNewCinarSolution}
                                 },
                     new Command {
                                     Execute = cmdOpenCinarSolution,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuOpenCinarSolution}
                                 },
                     new Command {
                                     Execute = cmdSaveCinarSolution,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuSaveCinarSolution},
                                     IsEnabled = () => Solution!=null && Solution.Modified
                                 },
                     new Command {
                                     Execute = cmdAddNewItem,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuAddNewItem},
                                     IsVisible = () => tree.SelectedNode != null && tree.SelectedNode.Tag is FolderItem
                                 },
                     new Command {
                                     Execute = cmdAddExistingItems,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuAddExistingItems},
                                     IsVisible = () => tree.SelectedNode != null && tree.SelectedNode.Tag is FolderItem
                                 },
                     new Command {
                                     Execute = cmdAddNewFolder,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuAddNewFolder},
                                     IsVisible = () => tree.SelectedNode != null && tree.SelectedNode.Tag is FolderItem
                                 },
                     new Command {
                                     Execute = cmdAddExistingFolder,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuAddExistingFolder},
                                     IsVisible = () => tree.SelectedNode != null && tree.SelectedNode.Tag is FolderItem
                                 },
                     new Command {
                                     Execute = cmdDeleteItem,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuDeleteItem},
                                     IsVisible = () => tree.SelectedNode != null && !(tree.SelectedNode.Tag is Solution)
                                 },
                     new Command {
                                     Execute = cmdOpenItem,
                                     Triggers = new List<CommandTrigger>{ 
                                         new CommandTrigger{ Control = mainForm.menuOpenItem},
                                         new CommandTrigger{ Control = mainForm.treeCodeGen, Event="DoubleClick"},
                                     },
                                     IsVisible = () => tree.SelectedNode != null && tree.SelectedNode.Tag is FileItem
                                 },
                     new Command {
                                     Execute = cmdGenerateCode,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuGenerateCode}
                                 },
                     new Command {
                                     Execute = cmdShowGeneratedCode,
                                     Trigger = new CommandTrigger{ Control = mainForm.menuShowGeneratedCode}
                                 },
            });
        }

        TreeView tree { get { return mainForm.treeCodeGen; } }
        public Solution Solution { get { return tree.Nodes.Count > 0 ? tree.Nodes[0].Tag as Solution : null; } }

        #region commands
        private void cmdNewCinarSolution(string arg)
        {
            if (Solution != null && Solution.Modified)
            {
                DialogResult dr = MessageBox.Show("Would you like to save current project: \n\n" + Solution.FullPath, "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                    Solution.Save();
                else if (dr == DialogResult.Cancel)
                    return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select root folder for the new solution";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Solution s = new Solution();
                s.Name = Path.GetFileNameWithoutExtension(fbd.SelectedPath);
                s.FullPath = fbd.SelectedPath + "\\" + s.Name + ".cgs";
                s.Save();

                openProject(s.FullPath);
            }
        }
        private void cmdOpenCinarSolution(string arg)
        {
            if (Solution != null && Solution.Modified)
            {
                DialogResult dr = MessageBox.Show("Would you like to save current project: \n\n" + Solution.FullPath, "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                    Solution.Save();
                else if (dr == DialogResult.Cancel)
                    return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Cinar Solution Files|*.cgs";
            if (ofd.ShowDialog() == DialogResult.OK)
                openProject(ofd.FileName);
        }
        private void cmdSaveCinarSolution(string arg)
        {
            Solution.Save();
            MessageBox.Show("Solution saved: " + Solution.FullPath, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdGenerateCode(string arg)
        {
            if (Provider.Database == null)
            {
                MessageBox.Show("Select a database first", "Cinar Database Tools");
                return;
            }

            Item item = tree.SelectedNode.Tag as Item;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select base folder to generate the code into:";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                ListBoxDialog lbd = new ListBoxDialog();
                lbd.Message = "Select tables (or views) to generate code for:";
                lbd.ListBox.DataSource = Provider.Database.Tables;

                if (lbd.ShowDialog() == DialogResult.OK)
                {
                    item.CreateCode(fbd.SelectedPath, lbd.GetSelectedItems<Table>());
                    Process.Start(fbd.SelectedPath);
                }
            }

        }
        private void cmdShowGeneratedCode(string arg)
        {
            Item item = tree.SelectedNode.Tag as Item;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select base folder to compare generated code:";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                ListBoxDialog lbd = new ListBoxDialog();
                lbd.Message = "Select tables (or views) to generate code for:";
                lbd.ListBox.DataSource = Provider.Database.Tables;

                if (lbd.ShowDialog() == DialogResult.OK)
                {
                    List<GeneratedCode> codes = item.GenerateCode(fbd.SelectedPath, lbd.GetSelectedItems<Table>());
                    foreach (GeneratedCode gc in codes)
                    {
                        if (gc.IsFolder) continue;

                        mainForm.addFileEditor(gc.Path, gc.Code);
                        //mainForm.CurrEditor.Content = gc.Code;
                    }
                }
            }

        }

        private void cmdAddNewItem(string arg) 
        {
            FolderItem parent = tree.SelectedNode.Tag as FolderItem;
            Item item = parent.AddNewItem(new FileItem()
            {
                Name = "NewItem.cs"
            });

            addNode(tree.SelectedNode, item);
            tree.SelectedNode.Expand();

            Solution.Save();
        }
        private void cmdAddExistingItems(string arg) 
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files|*.*";
            ofd.Multiselect = true;
            FolderItem parent = tree.SelectedNode.Tag as FolderItem;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in ofd.FileNames)
                {
                    Item item = parent.AddExistingItem(new FileItem()
                    {
                        Name = Path.GetFileName(filePath)
                    }, filePath);

                    if(item!=null)
                        addNode(tree.SelectedNode, item);
                }
                tree.SelectedNode.Expand();
                Solution.Save();
            }
        }

        private void cmdAddNewFolder(string arg) 
        {
            FolderItem parent = tree.SelectedNode.Tag as FolderItem;
            FolderItem item = parent.AddNewFolder(new FolderItem()
            {
                Name = "New Folder"
            });

            addNode(tree.SelectedNode, item);
            tree.SelectedNode.Expand();
        
            Solution.Save();
        }
        private void cmdAddExistingFolder(string arg)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            FolderItem parent = tree.SelectedNode.Tag as FolderItem;
            ofd.Description = "Please select an existing subfolder:";
            ofd.SelectedPath = parent.Path;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FolderItem item = parent.AddExistingFolder(new FolderItem()
                    {
                        Name = Path.GetFileName(ofd.SelectedPath)
                    }, ofd.SelectedPath);
                    var tn = addNode(tree.SelectedNode, item);
                    populateNodes(tn, item.Items);
                    tree.SelectedNode.Expand();
                    Solution.Save();
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void cmdDeleteItem(string arg) 
        {
            if (MessageBox.Show("Do you really want to delete " + tree.SelectedNode.Name + "?", "Cinar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                (tree.SelectedNode.Tag as Item).Delete();
                tree.SelectedNode.Remove();
                Solution.Save();
            }
        }
        private void cmdOpenItem(string arg) 
        {
            Item item = tree.SelectedNode.Tag as Item;
            mainForm.addFileEditor(item.Path);
        }
        #endregion

        #region methods
        private void openProject(string path)
        {
            Solution sol = Solution.Load(path);

            tree.Nodes.Clear();

            TreeNode tnSolution = tree.Nodes.Add("Solution", "Solution '" + sol.Name + "'", "Solution", "Solution");
            tnSolution.Tag = sol;

            populateNodes(tnSolution, sol.Items);

            tree.Nodes[0].Expand();
        }
        private void populateNodes(TreeNode parentNode, List<Item> items)
        {
            foreach (Item child in items)
            {
                TreeNode tn = addNode(parentNode, child);
                if(child is FolderItem)
                    populateNodes(tn, (child as FolderItem).Items);
            }
        }
        private TreeNode addNode(TreeNode parentNode, Item child)
        {
            TreeNode tn = parentNode.Nodes.Add(child.Name, child.Name, child.GetType().Name.Replace("Item",""), child.GetType().Name.Replace("Item",""));
            tn.Tag = child;

            return tn;
        }
        #endregion
    }
}

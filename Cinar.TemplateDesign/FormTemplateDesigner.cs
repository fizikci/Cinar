using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cinar.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Drawing.Imaging;
using Cinar.UICommands;
using System.Collections;

namespace Cinar.TemplateDesign
{
    public partial class FormTemplateDesigner : DevExpress.XtraEditors.XtraForm
    {
        CommandManager cmdMan = new CommandManager();

        public FormTemplateDesigner()
        {
            InitializeComponent();
        }
        public FormTemplateDesigner(Hashtable parameters) : this()
        {
            this.Parameters = parameters;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmdMan.Commands = new CommandCollection { 
                //new Command{
                //    Execute = cmdAddNewPage,
                //    Trigger = new CommandTrigger{Control = btnAddPage}
                //},
                new Command{
                    Execute = cmdBringSelectedToFront,
                    Trigger = new CommandTrigger{Control = btnBringToFront},
                    IsEnabled = isThereSelectedElement
                },
                //new Command{
                //    Execute = cmdDeleteSelectedPage,
                //    Trigger = new CommandTrigger{Control = btnDeletePage},
                //    IsEnabled = ()=>{ return templateDesigner.template.Pages.Count>1;}
                //},
                new Command{
                    Execute = cmdExit,
                    Trigger = new CommandTrigger{Control = menuExit}
                },
                new Command{
                    Execute = cmdOpenTemplate,
                    Triggers = new List<CommandTrigger>{ 
                        new CommandTrigger{Control = menuOpen},
                        new CommandTrigger{Control = btnOpen}
                    }
                },
                new Command{
                    Execute = cmdRefreshActivePage,
                    Trigger = new CommandTrigger{Control = propertyGrid, Event="CellValueChanged"}
                },
                new Command{
                    Execute = cmdSaveToLocalAs,
                    Trigger = new CommandTrigger{Control = btnSaveToLocalAs}
                },
                new Command{
                    Execute = cmdSaveTemplate,
                    Triggers = new List<CommandTrigger>{ 
                        new CommandTrigger{Control = btnSave},
                        new CommandTrigger{Control = menuSave}
                    }
                },
                new Command{
                    Execute = cmdSaveTemplateAs,
                    Trigger = new CommandTrigger{Control = menuSaveAs}
                },
                new Command{
                    Execute = cmdSelectTool,
                    Triggers = new List<CommandTrigger>{ 
                        new CommandTrigger{Control = btnNone},
                        new CommandTrigger{Control = btnText},
                        new CommandTrigger{Control = btnRectangle},
                        new CommandTrigger{Control = btnCircle},
                        new CommandTrigger{Control = btnLine},
                        new CommandTrigger{Control = btnPicture}
                    }
                },
                new Command{
                    Execute = cmdSendSelectedBack,
                    Trigger = new CommandTrigger{Control = btnSendBack},
                    IsEnabled = isThereSelectedElement
                },
                new Command{
                    Execute = cmdZoom,
                    Triggers = new List<CommandTrigger>{
                        new CommandTrigger{Control = btnZoom10},
                        new CommandTrigger{Control = btnZoom100},
                        new CommandTrigger{Control = btnZoom150},
                        new CommandTrigger{Control = btnZoom200},
                        new CommandTrigger{Control = btnZoom25},
                        new CommandTrigger{Control = btnZoom400},
                        new CommandTrigger{Control = btnZoom400},
                        new CommandTrigger{Control = btnZoom50},
                        new CommandTrigger{Control = btnZoom75},
                        new CommandTrigger{Control = btnZoom800},
                        new CommandTrigger{Control = btnZoomHeight},
                        new CommandTrigger{Control = btnZoomWidth}
                    }
                },
                new Command{
                    Execute = cmdSetParam,
                    IsEnabled = isSelectedElementText
                }
            };

            templateDesigner.OnElementAdded = () =>
            {
                btnNone.Down = true;
                this.Cursor = Cursors.Default;
            };

            Action<Element> setPropertyGrid = (Element elm) =>
            {
                propertyGrid.SelectedObject = null;
                propertyGrid.SelectedObject = elm;
                cmdMan.SetCommandControlsEnable();
            };

            templateDesigner.OnSelectedElementChanged = setPropertyGrid;
            templateDesigner.OnSelectedElementMoved = setPropertyGrid;
            templateDesigner.OnSelectedElementResized = setPropertyGrid;

            if (templateDesigner.template == null)
            {
                templateDesigner.template = new Template();
                cmdAddNewPage(null);
            }

            // init params
            if (this.Parameters != null)
            {
                barManager1.ForceInitialize();
                Dictionary<string, BarSubItem> subItems = new Dictionary<string, BarSubItem>();
                foreach (string key in Parameters.Keys)
                {
                    BarSubItem bsi = menuParams;
                    string[] pair = key.Split('.');
                    if (subItems.ContainsKey(pair[0]))
                        bsi = subItems[pair[0]];
                    else
                    {
                        bsi = new BarSubItem(barManager1, pair[0]);
                        menuParams.AddItem(bsi);
                        subItems.Add(pair[0], bsi);
                    }

                    BarButtonItem item = new BarButtonItem(barManager1, pair[1]);
                    bsi.AddItem(item);

                    cmdMan.Commands["cmdSetParam"].Triggers.Add(new CommandTrigger
                    {
                        Control = item,
                        Argument = key
                    });
                }
            }



            cmdMan.SetCommandTriggers();
            cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();
        }

        private void cmdAddNewPage(string arg)
        {
            if (templateDesigner.template == null)
                return;

            Page page = new Page();
            templateDesigner.AddPageDesignerForPage(page);
            templateDesigner.template.Pages.Add(page);
        }

        private void cmdSelectTool(string arg)
        {
            ItemClickEventArgs e = (ItemClickEventArgs)cmdMan.LastEventArgs;
            if (templateDesigner.ActivePage == null)
                return;

            BarLargeButtonItem btn = e.Item as BarLargeButtonItem;
            string tag = btn.Tag.ToString();
            string controlType = tag; // (ControlTypes)Enum.Parse(typeof(ControlTypes), tag);

            if (controlType == "None")
                this.Cursor = Cursors.Default;
            else
                this.Cursor = Cursors.Cross;

            btn.Down = true;

            templateDesigner.ActivePage.ClickToAdd = tag; // (ControlTypes)Enum.Parse(typeof(ControlTypes), tag);
        }

        private void cmdRefreshActivePage(string arg)
        {
            templateDesigner.ActivePage.Refresh();
        }

        private void cmdOpenTemplate(string arg)
        {
            templateDesigner.Open();
        }

        private void cmdSaveTemplate(string arg)
        {
            templateDesigner.Save();
        }

        private void cmdExit(string arg)
        {
            this.Close();
        }

        private void cmdSaveTemplateAs(string arg)
        {
            templateDesigner.SaveAs();
        }

        private void cmdSendSelectedBack(string arg)
        {
            templateDesigner.ActivePage.SendSelectedBack();
        }

        private void cmdBringSelectedToFront(string arg)
        {
            templateDesigner.ActivePage.BringSelectedToFront();
        }

        private void cmdSaveToLocalAs(string arg)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TIFF Images|*.tif|HTML Files|*.html|Text Files|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        templateDesigner.template.SaveAsTiff(sfd.FileName);
                        break;
                    case 2:
                        templateDesigner.template.SaveAsHTML(sfd.FileName);
                        break;
                    case 3:
                        templateDesigner.template.SaveAsText(sfd.FileName);
                        break;
                    default:
                        break;
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void cmdDeleteSelectedPage(string arg)
        {
            templateDesigner.RemoveSelectedPageDesigner();
        }

        private void cmdZoom(string arg)
        {
            ItemClickEventArgs e = (ItemClickEventArgs)cmdMan.LastEventArgs;
            int factor = int.Parse(e.Item.Tag.ToString());
            if (factor > 0)
            {
                float scaleFactor = (float)factor / (float)100;
                templateDesigner.ScaleBy(scaleFactor);
            }
            else
                MessageBox.Show("Not implemented yet.");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (templateDesigner.Modified)
            {
                DialogResult dr = MessageBox.Show("Deðiþiklikleri kaydetmek ister misiniz?", "FinsFAX", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                    templateDesigner.Save();

                e.Cancel = (dr == DialogResult.Cancel);
            }
        }

        private bool isThereSelectedElement()
        {
            return templateDesigner.ActivePage != null && templateDesigner.ActivePage.SelectedElement != null;
        }
        private bool isSelectedElementText()
        {
            return isThereSelectedElement() && templateDesigner.ActivePage.SelectedElement is Text;
        }

        public Hashtable Parameters;
        private void cmdSetParam(string arg)
        {
            Text elm = (Text)templateDesigner.ActivePage.SelectedElement;
            elm.InnerText += (string.IsNullOrEmpty(elm.InnerText) ? "" : " ") + "@" + arg;
            templateDesigner.Refresh();
        }
    }
}
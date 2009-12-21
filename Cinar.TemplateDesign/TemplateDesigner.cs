using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace Cinar.TemplateDesign
{
    public partial class TemplateDesigner : XtraUserControl
    {
        public TemplateDesigner()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            timer.Enabled = true;
        }

        public Action OnElementAdded = null;
        public Action<Element> OnSelectedElementChanged = null;
        public Action<Element> OnSelectedElementMoved = null;
        public Action<Element> OnSelectedElementResized = null;

        public Func<KeyValuePair<string, byte[]>> OpenTemplate = null;
        public Func<byte[], string, string> SaveTemplate = null;

        public Template template = null;

        public void AddPageDesignerForPage(Page page)
        {
            page.ScaleFactor = this.ActivePage != null ? this.ActivePage.page.ScaleFactor : 1f;

            PageDesigner pd = new PageDesigner();
            pd.ClickToAdd = "None";
            pd.OnElementAdded = this.OnElementAdded;
            pd.OnSelectedElementChanged = this.OnSelectedElementChanged;
            pd.OnSelectedElementMoved = this.OnSelectedElementMoved;
            pd.OnSelectedElementResized = this.OnSelectedElementResized;
            pd.page = page;
            pd.Width = Convert.ToInt32(page.Width * page.ScaleFactor);
            pd.Height = Convert.ToInt32(page.Height * page.ScaleFactor);
            flowLayoutPanel.Controls.Add(pd);
            this.ActivePage = pd;
        }

        public void RemoveSelectedPageDesigner()
        {
            if (this.ActivePage != null)
            {
                this.template.Pages.Remove(this.ActivePage.page);
                flowLayoutPanel.Controls.Remove(this.ActivePage);
                this.ActivePage = null;
                if (this.ActivePage!=null)
                    this.ActivePage.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private PageDesigner activePage;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PageDesigner ActivePage 
        {
            get
            {
                if (activePage == null && flowLayoutPanel.Controls.Count > 0)
                    activePage = (PageDesigner)flowLayoutPanel.Controls[this.Controls.Count - 1];

                return activePage;
            }
            set
            {
                PageDesigner newPage = value;

                if (activePage == newPage)
                    return;

                if (activePage != null)
                {
                    activePage.BorderStyle = BorderStyle.None;
                    activePage.SelectedElement = null;
                    if (newPage != null)
                        newPage.ClickToAdd = activePage.ClickToAdd;
                }

                activePage = newPage;

                if (activePage != null)
                {
                    activePage.BorderStyle = BorderStyle.FixedSingle;
                    activePage.SelectedElement = null;
                }
            }
        }

        #region Open, Save
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                this.fileName = value;
                if (!String.IsNullOrEmpty(this.fileName))
                    this.FindForm().Text = "Cinar Template Designer - " + Path.GetFileNameWithoutExtension(this.fileName);
            }
        }

        public bool Modified
        {
            get
            {
                bool modified = true;
                foreach (PageDesigner pd in flowLayoutPanel.Controls)
                    modified = modified & pd.Modified;
                return modified;
            }
            set
            {
                if (value == false)
                {
                    foreach (PageDesigner pd in flowLayoutPanel.Controls)
                        pd.Modified = false;
                }
            }
        }

        private void save(string fileName)
        {
            if (this.SaveTemplate == null)
            {
                // save with standart ways
                if (String.IsNullOrEmpty(fileName))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Template Files|*.fxt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        this.FileName = sfd.FileName;
                    }
                    else
                        return;
                }

                this.template.Save(this.FileName);

                Modified = false;
            }
            else
            { 
                // save to special storage
                if (String.IsNullOrEmpty(fileName))
                {
                    string fName = this.SaveTemplate(this.template.Save(), "");
                    if (!String.IsNullOrEmpty(fName))
                    {
                        this.FileName = fName;
                    }
                    else
                        return;
                }
                else
                {
                    this.SaveTemplate(this.template.Save(), this.FileName);
                    //this.template.Save(this.FileName);
                }
                Modified = false;
            }
        }

        public void Save()
        {
            this.save(this.fileName);
        }

        public void SaveAs()
        {
            this.save(null);
        }

        public void Open()
        {
            bool opened = false;

            if (this.OpenTemplate == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Template Files|*.fxt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.fileName = ofd.FileName;
                    this.template = Template.Open(this.fileName);
                    opened = true;
                }
            }
            else
            {
                KeyValuePair<string, byte[]> file = this.OpenTemplate();
                if (!String.IsNullOrEmpty(file.Key) && file.Value != null && file.Value.Length > 0)
                {
                    this.fileName = file.Key;
                    this.template = Template.Open(file.Value);
                    opened = true;
                }
            }

            if (opened)
                ShowTemplate();
        }

        public void ShowTemplate()
        {
            flowLayoutPanel.Controls.Clear();
            if (template == null)
                return;
            foreach (Page p in template.Pages)
            {
                AddPageDesignerForPage(p);
            }

            // set first page as active
            if (flowLayoutPanel.Controls.Count > 0)
                this.ActivePage = (PageDesigner)flowLayoutPanel.Controls[0];

            this.Modified = false;
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (PageDesigner pd in flowLayoutPanel.Controls)
                pd.DoRefresh();
        }

        public void ScaleBy(float scaleFactor)
        {
            foreach (PageDesigner pd in flowLayoutPanel.Controls)
            {
                pd.page.ScaleFactor = scaleFactor;
                pd.Width = Convert.ToInt32((float)pd.page.Width * scaleFactor);
                pd.Height = Convert.ToInt32((float)pd.page.Height * scaleFactor);
                pd.PutHandleToTheCornerOfSelectedElement();
            }
            this.Refresh();
        }
    }
}

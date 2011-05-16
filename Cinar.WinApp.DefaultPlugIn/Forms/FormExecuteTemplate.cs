using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using Cinar.WinUI;
using Cinar.Scripting;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.WinApp.DefaultPlugIn.Forms
{
    [EditForm(
        RequiredRight = UserRights.ExecuteReport,
        CategoryName = "Raporlar",
        DisplayName = "Standart Raporlar",
        ImageKey = "cog")]
    public partial class FormExecuteTemplate : XtraForm, ICinarForm
    {
        public FormExecuteTemplate()
        {
            InitializeComponent();
            pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];
            this.LookAndFeel.StyleChanged += delegate
            {
                pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];
            };

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FilterExpression fExp = new FilterExpression()
            {
                Orders = new OrderList { new Order { ColumnName = "Name", Ascending = true } }
            };
            grid.DataSource = DMT.Provider.Db.ReadList<Report>(fExp);
            var columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition{Name = "Name", DisplayName = "Rapor", Width = 300}
                };
            DMT.Provider.PopulateGridColumns(
                columns,
                gridView);

            cbDocumentType.Visible = ShowRaporButton();
            btnRaporOlustur.Visible = ShowRaporButton();
        }

        private void cmdExecuteTemplate()
        {
            if (viewTemplate==null)
            {
                DMT.Provider.FeedBack("Önce bir þablon seçiniz");
                return;
            }

            #region HtmlWithSQLQuery
            if (viewTemplate.ExecutionType == ReportExecutionTypes.HtmlWithSQLQuery)
            {
                Hashtable hs = FormTemplatePreview.GetParameterValues(viewTemplate.Id);
                if (hs != null)
                {
                    ReportExecuter sqlTemplate = ReportExecuter.Load(viewTemplate, null, hs, viewTemplate.Html);

                    string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CinarDocs";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    Process.Start(dir);

                    string sonucHTML = sqlTemplate.ExecuteHTML();
                    string path = dir + "\\" + (DateTime.Now.ToString("yyyy-MM-dd") + "_" + viewTemplate.Name).MakeFileName() + ".html";
                    File.WriteAllText(path, sonucHTML, Encoding.UTF8);
                }

                return;
            }
            #endregion

            #region ReportWithSQLQuery
            if (viewTemplate.ExecutionType == ReportExecutionTypes.ReportWithSQLQuery)
            {
                if (cbDocumentType.SelectedItem == null || string.IsNullOrEmpty(cbDocumentType.SelectedItem.ToString()))
                {
                    DMT.Provider.FeedBack("Lütfen önce döküman tipini seçiniz");
                    cbDocumentType.Focus();
                    cbDocumentType.ShowPopup();
                    return;
                }

				if (string.IsNullOrEmpty(viewTemplate.SQLQuery))
                {
                    DMT.Provider.FeedBack("Geçerli þablonun sorgusu bozuk");
                    return;
                }
            	
				Hashtable hs = FormTemplatePreview.GetParameterValues(viewTemplate.Id);
				if (hs != null)
				{
                    ReportExecuter sqlTemplate = ReportExecuter.Load(viewTemplate, null, hs);
					sqlTemplate.ExecuteReport();
					
                    sqlTemplate.ExportToFile(sqlTemplate, cbDocumentType.SelectedItem.ToString(), viewTemplate.Name, DateTime.Now.ToString("yyyy-MM-dd") + "_" + viewTemplate.Name);
				}

            	return;
            }
            #endregion
		}

    	private void btnRaporOlustur_Click(object sender, EventArgs e)
        {
            cmdExecuteTemplate();
        }


        #region ICinarForm Members

        public Cinar.UICommands.CommandCollection GetCommands()
        {
            return new Cinar.UICommands.CommandCollection();
        }

        public string GetTitle()
        {
            return "Raporlar";
        }

        public void Initialize(Cinar.UICommands.CommandManager cmdMan)
        {
            
        }

        #endregion

        Report viewTemplate = null;

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTemplate = (Report)gridView.GetRow(e.FocusedRowHandle);

            cbDocumentType.Visible = (viewTemplate.ExecutionType != ReportExecutionTypes.HtmlWithSQLQuery);

        }

        public virtual bool ShowRaporButton()
        {
            return true;
        }
    }

}
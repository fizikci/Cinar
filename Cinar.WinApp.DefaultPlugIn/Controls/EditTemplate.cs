using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.UICommands;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Cinar.WinApp.DefaultPlugIn.Forms;
using Cinar.WinUI;
using System.IO;
using System.Data;
using Cinar.Entities.Standart;
using Cinar.Database;
using System.Collections;


namespace Cinar.WinApp.DefaultPlugIn.Controls
{
	[EditForm(
		RequiredRight = UserRights.OpenReport,
		EntityType = typeof(Report),
		CategoryName = "Sistem Yönetimi",
		DisplayName = "Þablonlar / Raporlar",
        ImageKey = "cog")]
	public partial class EditTemplate : XtraUserControl, IEntityEditControl
	{
		public EditTemplate()
		{
			InitializeComponent();

            this.editName.EntityType = typeof(Report);

            layoutControlItemName.Image = FamFamFam.cog;
		}

		public CommandCollection GetCommands()
		{
			return new CommandCollection() { 
                   new Command(){
                                    Execute = (arg)=>{ShowEntity((BaseEntity)editName.SelectedItem);},
                                    Trigger = new CommandTrigger(){Control=editName, Event="ItemSelected"},
                                },
                   new Command(){
                                    Execute = cmdAddNewTemplate,
                                    Trigger = new CommandTrigger(){Control=btnAddTemplate}
                                },
                   new Command(){
                                    Execute = cmdCopy,
                                    Trigger = new CommandTrigger(){Control=btnCopy},
                                    IsEnabled = () => CurrentTemplate.Id != -1
                                },
                   new Command(){
                                    Execute = cmdSaveTemplate,
                                    Trigger = new CommandTrigger(){Control=btnSaveTemplate}
                                },
                   new Command(){
                                    Execute = cmdDeleteTemplate,
                                    Trigger = new CommandTrigger(){Control=btnDeleteTemplate},
                                    IsEnabled = () => CurrentTemplate.Id != -1
                                },
                   new Command(){
                                    Execute = cmdEditTemplateDesign,
                                    Trigger = new CommandTrigger(){Control=btnEditTemplateDesign},
                                },
                   new Command(){
                                    Execute = cmdPreview,
                                    Trigger = new CommandTrigger(){Control=btnPreview},
                                },
			   };
		}
		public void Initialize(CommandManager cmdMan)
		{
			setupListEntityTemplateParam();
			cmdMan.Commands.AddRange(listEntityTemplateParam.GetCommands());
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.LookAndFeel.StyleChanged += delegate
			{
				listEntityTemplateParam.WhenStyleChanged();
			};
		}

		private void setupListEntityTemplateParam()
		{

			listEntityTemplateParam.EntityType = typeof(ReportParam);
			listEntityTemplateParam.GetVisibleColumns = () => new ColumnDefinitionList{
					new ColumnDefinition(){Name="Name", DisplayName="Parametre Adý", Width=150},
					new ColumnDefinition(){Name="PType", DisplayName="Tipi", Width=150},
					new ColumnDefinition(){Name="PModuleName", DisplayName="Modül Adý", Width=150},
					new ColumnDefinition(){Name="PEntityName", DisplayName="Entity Adý", Width=150},
			 };
			listEntityTemplateParam.GetFilter = () => FilterExpression.Create("ReportId", CriteriaTypes.Eq, CurrentTemplate.Id);
			listEntityTemplateParam.CreateEntityForm = (entity) =>
			                                           	{
                                                            ReportParam tp = (ReportParam)entity;
                                                            if (tp == null) tp = new ReportParam();
			                                           		tp.ReportId = CurrentTemplate.Id;
			                                           		return new FormTemplateParam(tp);
			                                           	};
		}
		public Type GetEntityType()
		{
			return typeof(Report);
		}

		public List<ColumnDefinition> GetVisibleColumns()
		{
			return new List<ColumnDefinition>(){
                                                   new ColumnDefinition(){Name="Name", DisplayName="Rapor Adý", Width=200},
                                                   new ColumnDefinition(){Name="InsertDate", DisplayName="Oluþ. Tarihi", Width=100},
                                               };
		}

		public BaseEntity CurrentEntity
		{
			get
			{
				return CurrentTemplate;
			}
		}

		public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
		{
            return DMT.Provider.Db.ReadList<Report>(fExp, pageNo, pageSize);
		}

		private Report currentTemplate;
        public Report CurrentTemplate
		{
			get
			{
				if (currentTemplate == null)
                    currentTemplate = new Report();
				return currentTemplate;
			}
			set
			{
				currentTemplate = value;
			}
		}

		private ListEntity listForm;
		public ListEntity ListForm
		{
			get
			{
				return listForm;
			}
			set
			{
				listForm = value;
			}
		}

		public FilterExpression GetFilter()
		{
			return null;
		}

		public Control GetControl(string fieldName)
		{
			return layoutEntity.GetControlByName("edit" + fieldName);
		}
        public string GetControlLabel(string fieldName)
        {
            return layoutEntity.GetItemByControl(layoutEntity.GetControlByName("edit" + fieldName)).Text;
        }

		public void ShowEntity(BaseEntity entity)
		{
			if (entity == null) return;

            CurrentTemplate = (Report)entity;

			if (CurrentTemplate.Id > 0)
			{
				DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentTemplate);
				listEntityTemplateParam.BindData();
				using (System.IO.Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(CurrentTemplate.ReportLayout)))
				{
					report = new XtraReport();
					report.LoadLayout(stream);
				}
				panelDetail.Visible = true;
			}
			else
			{
				DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentTemplate);
				panelDetail.Visible = false;

			}
			
		}

        private void cmdAddNewTemplate(string arg)
        {
            report = new XtraReport();
            ShowEntity(new Report());
            editName.Focus();
        }

        private void cmdCopy(string arg)
        {
            Report t = (Report)CurrentTemplate.Clone();
            t.Name += " 2";
            t.Id = 0;
            t.Save();
            listForm.BindGrid();
        }

		private void cmdSaveTemplate(string arg)
		{
			DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentTemplate);

			using (MemoryStream stream = new MemoryStream())
			{
				report.SaveLayout(stream);
				CurrentTemplate.ReportLayout = Encoding.UTF8.GetString(stream.ToArray());
			}

            CurrentTemplate.Save();
            DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
		private void cmdDeleteTemplate(string arg)
		{
			if (DMT.Provider.Confirm("Rapor kaydý silinecek. Onaylýyor musunuz?"))
			{
				CurrentTemplate.Delete();
				listForm.BindGrid();
				listForm.FocusFirstEntity();
			}
		}
		private void cmdEditTemplateDesign(string arg)
		{
			report.DataSource = getReportEmptyDataSource();
			report.ShowDesigner();
		}
		private void cmdPreview(string arg)
		{
			report.DataSource = getReportRealDataSource();
            if (report.DataSource != null)
                report.ShowPreview();
		}

		private XtraReport report = new XtraReport();

		private object getReportEmptyDataSource()
		{
			if (editSQLQuery.EditValue == null || editSQLQuery.EditValue.ToString().Trim() != "")
			{
				string query = editSQLQuery.Text;
                IDbCommand cmd = DMT.Provider.Db.CreateCommand(query);

                if(listEntityTemplateParam.DataSource!=null)
                    foreach (ReportParam tp in listEntityTemplateParam.DataSource)
                    {
                        object paramVal = null;
                        switch (tp.PType)
                        {
                            case ReportParamTypes.TamSayý:
                                paramVal = 0;
                                break;
                            case ReportParamTypes.Tarih:
                                paramVal = DateTime.Now;
                                break;
                            case ReportParamTypes.Metin:
                                paramVal = "";
                                break;
                            case ReportParamTypes.OndalikliSayi:
                                paramVal = 0M;
                                break;
                            case ReportParamTypes.EvetHayir:
                                paramVal = false;
                                break;
                            case ReportParamTypes.Entity:
                                paramVal = 0L;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        cmd.Parameters.Add(DMT.Provider.Db.CreateParameter("@" + tp.PName, paramVal));
                    }
                IDataAdapter da = DMT.Provider.Db.CreateDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
				return ds;
			}

            return null;
		}

		private object getReportRealDataSource()
		{
			if (editSQLQuery.EditValue != null && editSQLQuery.EditValue.ToString().Trim() != "")
			{
                List<ReportParam> prms = listEntityTemplateParam.DataSource.Cast<ReportParam>().ToList();
                Hashtable ht = FormTemplatePreview.GetParameterValues(prms);

                IDbCommand cmd = DMT.Provider.Db.CreateCommand(editSQLQuery.Text);
                foreach (ReportParam tp in prms)
                {
                    if (ht == null)
                        return null;

                    object paramVal = ht[tp.PName];
                    if (paramVal is BaseEntity)
                        paramVal = paramVal.GetMemberValue(tp.PValueField);
                    cmd.Parameters.Add(DMT.Provider.Db.CreateParameter(tp.PName, paramVal));
                }
                IDataAdapter da = DMT.Provider.Db.CreateDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
			}

            return null;
		}


		public string GetTitle()
		{
			return "Þablon" + (!string.IsNullOrEmpty(CurrentTemplate.Name) ? " - " + CurrentTemplate.Name : "");
		}

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }
	}
}
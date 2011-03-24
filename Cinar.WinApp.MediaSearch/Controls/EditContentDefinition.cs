using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.UICommands;
using DevExpress.XtraGrid.Views.Grid;
using Cinar.WinUI;
using Cinar.Entities.Standart;
using Cinar.Database;
using Cinar.Entities.MediaSearch;

namespace Cinar.WinApp.MediaSearch.Controls
{
    [EditForm(
        RequiredRight = UserRights.OpenContentDefinition,
        EntityType = typeof(ContentDefinition), 
        CategoryName = "Media Search", 
        DisplayName = "RSS Tanýmlarý",
        ImageKey = "feed")]
    public partial class EditContentDefinition : XtraUserControl, IEntityEditControl
    {
        public EditContentDefinition()
        {
            InitializeComponent();

            this.editCategory.EntityType = typeof(Category);
            this.editMedia.EntityType = typeof(Media);
            editContentType.BindTo(Enum.GetValues(typeof(ContentType)));
            layoutControlItemName.Image = Cinar.WinUI.Properties.Resources.feed;
            btnAddContentDefinition.Image = Cinar.WinUI.Properties.Resources.add;
            btnDeleteContentDefinition.Image = Cinar.WinUI.Properties.Resources.delete;
            btnSaveContentDefinition.Image = Cinar.WinUI.Properties.Resources.disk;
            btnTest.Image = Cinar.WinUI.Properties.Resources.television;
            listEntityContent.ImageForEntity = Cinar.WinUI.Properties.Resources.newspaper;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection() { 
                       new Command(){
                                Execute = cmdAddNewContentDefinition,
                                Trigger = new CommandTrigger(){Control=btnAddContentDefinition},
                                IsEnabled = () => CurrentContentDefinition.Id != -1
                            },
                       new Command(){
                                Execute = cmdSaveContentDefinition,
                                Trigger = new CommandTrigger(){Control=btnSaveContentDefinition}
                            },
                       new Command(){
                                Execute = cmdDeleteContentDefinition,
                                Trigger = new CommandTrigger(){Control=btnDeleteContentDefinition},
                                IsEnabled = () => CurrentContentDefinition.Id != -1
                            },
                       new Command(){
                                Execute = cmdTestSelector,
                                Triggers = new List<CommandTrigger>{
                                    new CommandTrigger(){Control=editTitleSelector, Event="ButtonClick", Argument="Baþlýk"},
                                    new CommandTrigger(){Control=editContentSelector, Event="ButtonClick", Argument="Ýçerik"},
                                    new CommandTrigger(){Control=editDateSelector, Event="ButtonClick", Argument="Tarih"},
                                    new CommandTrigger(){Control=editAuthorSelector, Event="ButtonClick", Argument="Yazar"},
                                    new CommandTrigger(){Control=editImageSelector, Event="ButtonClick", Argument="Resim"},
                                }
                            },
                       new Command(){
                                Execute = cmdTest,
                                Trigger = new CommandTrigger(){Control=btnTest},
                                IsEnabled = () => CurrentContentDefinition.Id != -1
                            },
                   };
        }
        public void Initialize(CommandManager cmdMan)
        {
            setupListEntityContent();

            cmdMan.Commands.AddRange(listEntityContent.GetCommands());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LookAndFeel.StyleChanged += delegate
                                 {
                                     listEntityContent.WhenStyleChanged();
                                 };
        }

        private void setupListEntityContent()
        {
            listEntityContent.EntityType = typeof(Content);
            listEntityContent.GetVisibleColumns = () => new ColumnDefinitionList{
                new ColumnDefinition(){Name = "Title", DisplayName="Haber Baþlýðý", Width=150},
            };
            listEntityContent.GetFilter = () => FilterExpression.Create("ContentDefinitionId", CriteriaTypes.Eq, CurrentContentDefinition.Id);
        }
        public Type GetEntityType()
        {
            return typeof(ContentDefinition);
        }

        public List<ColumnDefinition> GetVisibleColumns()
        {
            return new List<ColumnDefinition>(){
                                   new ColumnDefinition(){Name="Media", DisplayName="Medya", Width=150},
                                   new ColumnDefinition(){Name="RSSUrl", DisplayName="RSS Adresi", Width=250}
                               };
        }

        public BaseEntity CurrentEntity
        {
            get {
                return CurrentContentDefinition;
            }
        }

        public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
        {
            return DMT.Provider.Db.ReadList<ContentDefinition>(fExp, pageNo, pageSize);
        }

        private ContentDefinition currentContentDefinition;
        public ContentDefinition CurrentContentDefinition
        {
            get
            {
                if (currentContentDefinition == null)
                    currentContentDefinition = new ContentDefinition();
                return currentContentDefinition;
            }
            set
            {
                currentContentDefinition = value;
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
            CurrentContentDefinition = (ContentDefinition)entity;

            if (CurrentContentDefinition.Id > 0)
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentContentDefinition);
                listEntityContent.BindData();
                panelDetail.Visible = true;
            }
            else
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentContentDefinition);
                panelDetail.Visible = false;
            }
        }

        private void cmdAddNewContentDefinition(string arg)
        {
            ShowEntity(new ContentDefinition());
            editCategory.Focus();
        }
        private void cmdSaveContentDefinition(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentContentDefinition);

            CurrentContentDefinition.Save();
            DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
        private void cmdDeleteContentDefinition(string arg)
        {
            if (DMT.Provider.Confirm("RSS kaydý silinecek. Onaylýyor musunuz?"))
            {
                CurrentContentDefinition.Delete();
                listForm.BindGrid();
                listForm.FocusFirstEntity();
            }
        }

        public string GetTitle()
        {
            return "RSS Tanýmý" + (!string.IsNullOrEmpty(CurrentContentDefinition.RSSUrl) ? " - " + CurrentContentDefinition.RSSUrl : "");
        }

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }

        private void cmdTestSelector(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentContentDefinition);
			ButtonEdit edit = null;
            switch(arg)
            {
                case "Baþlýk":
                    edit = editTitleSelector;
                    break;
                case "Ýçerik":
                    edit = editContentSelector;
                    break;
                case "Tarih":
                    edit = editDateSelector;
                    break;
                case "Yazar":
                    edit = editAuthorSelector;
                    break;
                case "Resim":
                    edit = editImageSelector;
                    break;
            }

            if (edit == null)
                return;

            string selector = Forms.FormCSSSelector.GetSelector(arg + " öðesinin bulunduðu bölgeyi iþaretleyiniz", CurrentContentDefinition.RSSUrl, edit.EditValue==null ? "" : edit.EditValue.ToString());
            if (selector != null)
                edit.EditValue = selector;
        }

        private void cmdTest(string arg)
        {
            new Forms.FormTest(CurrentContentDefinition).ShowDialog();
        }
    }
}
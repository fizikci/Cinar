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
        RequiredRight = UserRights.OpenCategory,
        EntityType = typeof(Category), 
        CategoryName = "Media Search", 
        DisplayName = "Haber Kategorileri",
        ImageKey = "folder")]
    public partial class EditCategory : XtraUserControl, IEntityEditControl
    {
        public EditCategory()
        {
            InitializeComponent();

            this.editName.EntityType = typeof(Category);
            layoutControlItemName.Image = Cinar.WinUI.Properties.Resources.folder;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection() { 
                       new Command(){
                                Execute = (arg)=>{ShowEntity((BaseEntity)editName.SelectedItem);},
                                Trigger = new CommandTrigger(){Control=editName, Event="ItemSelected"},
                            },
                       new Command(){
                                Execute = cmdAddNewCategory,
                                Trigger = new CommandTrigger(){Control=btnAddCategory},
                                IsEnabled = () => CurrentCategory.Id != -1
                            },
                       new Command(){
                                Execute = cmdSaveCategory,
                                Trigger = new CommandTrigger(){Control=btnSaveCategory}
                            },
                       new Command(){
                                Execute = cmdDeleteCategory,
                                Trigger = new CommandTrigger(){Control=btnDeleteCategory},
                                IsEnabled = () => CurrentCategory.Id != -1
                            },
                   };
        }
        public void Initialize(CommandManager cmdMan)
        {
            setupListEntityContentDefinition();

            cmdMan.Commands.AddRange(listEntityContentDefinition.GetCommands());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LookAndFeel.StyleChanged += delegate
                                 {
                                     listEntityContentDefinition.WhenStyleChanged();
                                 };
        }

        private void setupListEntityContentDefinition()
        {
            listEntityContentDefinition.EntityType = typeof(ContentDefinition);
            listEntityContentDefinition.GetVisibleColumns = () => new ColumnDefinitionList{
                      new ColumnDefinition(){Name="Media", DisplayName="Medya", Width=150},
                      new ColumnDefinition(){Name="RSSUrl", DisplayName="RSS Url", Width=250},
                  };
            listEntityContentDefinition.GetFilter = () => FilterExpression.Create("CategoryId", CriteriaTypes.Eq, CurrentCategory.Id);
        }
        public Type GetEntityType()
        {
            return typeof(Category);
        }

        public List<ColumnDefinition> GetVisibleColumns()
        {
            return new List<ColumnDefinition>(){
                                                   new ColumnDefinition(){Name="Name", DisplayName="Kategori", Width=150}
                                               };
        }

        public BaseEntity CurrentEntity
        {
            get {
                return CurrentCategory;
            }
        }

        public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
        {
            return DMT.Provider.Db.ReadList<Category>(fExp, pageNo, pageSize);
        }

        private Category currentCategory;
        public Category CurrentCategory
        {
            get
            {
                if (currentCategory == null)
                    currentCategory = new Category();
                return currentCategory;
            }
            set
            {
                currentCategory = value;
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
            CurrentCategory = (Category)entity;

            if (CurrentCategory.Id > 0)
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentCategory);
                listEntityContentDefinition.BindData();
                panelDetail.Visible = true;
            }
            else
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentCategory);
                panelDetail.Visible = false;
            }
        }

        private void cmdAddNewCategory(string arg)
        {
            ShowEntity(new Category());
            editName.Focus();
        }
        private void cmdSaveCategory(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentCategory);

            CurrentCategory.Save();
            DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
        private void cmdDeleteCategory(string arg)
        {
            if (DMT.Provider.Confirm("Kategori kaydý silinecek. Onaylýyor musunuz?"))
            {
                CurrentCategory.Delete();
                listForm.BindGrid();
                listForm.FocusFirstEntity();
            }
        }

        public string GetTitle()
        {
            return "Kategori" + (!string.IsNullOrEmpty(CurrentCategory.Name) ? " - " + CurrentCategory.Name : "");
        }

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }


    }
}
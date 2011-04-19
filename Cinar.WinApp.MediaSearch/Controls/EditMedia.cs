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
        RequiredRight = UserRights.OpenMedia,
        EntityType = typeof(Media), 
        CategoryName = "Media Search", 
        DisplayName = "Medya Kurumlarý",
        ImageKey = "newspaper")]
    public partial class EditMedia : XtraUserControl, IEntityEditControl
    {
        public EditMedia()
        {
            InitializeComponent();

            this.editName.EntityType = typeof(Media);
            layoutControlItemName.Image = FamFamFam.newspaper;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection() { 
                       new Command(){
                                Execute = (arg)=>{ShowEntity((BaseEntity)editName.SelectedItem);},
                                Trigger = new CommandTrigger(){Control=editName, Event="ItemSelected"},
                            },
                       new Command(){
                                Execute = cmdAddNewMedia,
                                Trigger = new CommandTrigger(){Control=btnAddMedia},
                                IsEnabled = () => CurrentMedia.Id != -1
                            },
                       new Command(){
                                Execute = cmdSaveMedia,
                                Trigger = new CommandTrigger(){Control=btnSaveMedia}
                            },
                       new Command(){
                                Execute = cmdDeleteMedia,
                                Trigger = new CommandTrigger(){Control=btnDeleteMedia},
                                IsEnabled = () => CurrentMedia.Id != -1
                            },
                   };
        }
        public void Initialize(CommandManager cmdMan)
        {
            setupListEntityAuthor();
            setupListEntityContentDefinition();

            cmdMan.Commands.AddRange(listEntityAuthor.GetCommands());
            cmdMan.Commands.AddRange(listEntityContentDefinition.GetCommands());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LookAndFeel.StyleChanged += delegate
                                 {
                                     listEntityAuthor.WhenStyleChanged();
                                     listEntityContentDefinition.WhenStyleChanged();
                                 };
        }

        private void setupListEntityAuthor()
        {
            listEntityAuthor.EntityType = typeof(Author);
            listEntityAuthor.GetVisibleColumns = () => new ColumnDefinitionList{
                new ColumnDefinition(){Name = "Name", DisplayName="Yazar Adý", Width=150},
            };
            listEntityAuthor.GetFilter = () => FilterExpression.Create("MediaId", CriteriaTypes.Eq, CurrentMedia.Id);
        }
        private void setupListEntityContentDefinition()
        {
            listEntityContentDefinition.EntityType = typeof(ContentDefinition);
            listEntityContentDefinition.GetVisibleColumns = () => new ColumnDefinitionList{
                      new ColumnDefinition(){Name="Category", DisplayName="Kategori", Width=150},
                      new ColumnDefinition(){Name="RSSUrl", DisplayName="RSS Url", Width=250},
                  };
            listEntityContentDefinition.GetFilter = () => FilterExpression.Create("MediaId", CriteriaTypes.Eq, CurrentMedia.Id);
        }
        public Type GetEntityType()
        {
            return typeof(Media);
        }

        public List<ColumnDefinition> GetVisibleColumns()
        {
            return new List<ColumnDefinition>(){
                                                   new ColumnDefinition(){Name="Name", DisplayName="Rol", Width=150}
                                               };
        }

        public BaseEntity CurrentEntity
        {
            get {
                return CurrentMedia;
            }
        }

        public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
        {
            return DMT.Provider.Db.ReadList<Media>(fExp, pageNo, pageSize);
        }

        private Media currentMedia;
        public Media CurrentMedia
        {
            get
            {
                if (currentMedia == null)
                    currentMedia = new Media();
                return currentMedia;
            }
            set
            {
                currentMedia = value;
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
            CurrentMedia = (Media)entity;

            if (CurrentMedia.Id > 0)
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentMedia);
                listEntityAuthor.BindData();
                listEntityContentDefinition.BindData();
                panelDetail.Visible = true;
            }
            else
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentMedia);
                panelDetail.Visible = false;
            }
        }

        private void cmdAddNewMedia(string arg)
        {
            ShowEntity(new Media());
            editName.Focus();
        }
        private void cmdSaveMedia(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentMedia);

            CurrentMedia.Save();
            DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
        private void cmdDeleteMedia(string arg)
        {
            if (DMT.Provider.Confirm("Medya kaydý silinecek. Onaylýyor musunuz?"))
            {
                CurrentMedia.Delete();
                listForm.BindGrid();
                listForm.FocusFirstEntity();
            }
        }

        public string GetTitle()
        {
            return "Medya" + (!string.IsNullOrEmpty(CurrentMedia.Name) ? " - " + CurrentMedia.Name : "");
        }

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }


    }
}
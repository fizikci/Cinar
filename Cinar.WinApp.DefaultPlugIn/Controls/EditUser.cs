using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.UICommands;
using DevExpress.XtraGrid.Views.Grid;
using Cinar.WinApp.DefaultPlugIn.Forms;
using Cinar.WinUI;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.WinApp.DefaultPlugIn.Controls
{
    [EditForm(
        RequiredRight = UserRights.OpenUser, 
        EntityType = typeof(User), 
        CategoryName = "Sistem Yönetimi", 
        DisplayName = "Kullanýcýlar",
        ImageKey = "user")]
    public partial class EditUser : XtraUserControl, IEntityEditControl
    {
        public EditUser()
        {
            InitializeComponent();

            this.editName.EntityType = typeof(User);
            layoutControlItemName.Image = FamFamFam.user;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection() { 
                                               new Command(){
                                                                Execute = (arg)=>{ShowEntity((BaseEntity)editName.SelectedItem);},
                                                                Trigger = new CommandTrigger(){Control=editName, Event="ItemSelected"},
                                                            },
                                               new Command(){
                                                                Execute = cmdAddNewDbtUser,
                                                                Trigger = new CommandTrigger(){Control=btnAddDbtUser},
                                                                IsEnabled = () => CurrentDbtUser.Id != -1
                                                            },
                                               new Command(){
                                                                Execute = cmdSaveDbtUser,
                                                                Trigger = new CommandTrigger(){Control=btnSaveDbtUser}
                                                            },
                                               new Command(){
                                                                Execute = cmdDeleteDbtUser,
                                                                Trigger = new CommandTrigger(){Control=btnDeleteDbtUser},
                                                                IsEnabled = () => CurrentDbtUser.Id != -1
                                                            },
                                           };
        }
        public void Initialize(CommandManager cmdMan)
        {
            setupListEntityKeyword();

            cmdMan.Commands.AddRange(listEntityRoleUser.GetCommands());

            editName.EntityType = typeof(User);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LookAndFeel.StyleChanged += delegate
                                                 {
                                                     listEntityRoleUser.WhenStyleChanged();
                                                 };
        }

        private void setupListEntityKeyword()
        {
            listEntityRoleUser.EntityType = typeof(RoleUser);
            listEntityRoleUser.GetVisibleColumns = () => new ColumnDefinitionList{
                 new ColumnDefinition(){Name="Role", DisplayName="Kullanýcý Rolleri", Width=150},
             };
            listEntityRoleUser.GetFilter = () => FilterExpression.Create("UserId", CriteriaTypes.Eq, CurrentDbtUser.Id);
            listEntityRoleUser.CreateEntityForm = (entity) => new FormRoleUser(-1, CurrentDbtUser.Id, (RoleUser)entity);
            listEntityRoleUser.DoubleClick = () =>
            {
                RoleUser entity = listEntityRoleUser.SelectedEntity as RoleUser;
                if (entity != null)
                    DMT.Provider.UIMetaData.ShowDialog(entity.Role);
            };
        }
        public Type GetEntityType()
        {
            return typeof(User);
        }

        public List<ColumnDefinition> GetVisibleColumns()
        {
            return new List<ColumnDefinition>(){
               new ColumnDefinition(){Name="Name", DisplayName="Ad Soyad", Width=150},
               new ColumnDefinition(){Name="UserName", DisplayName="Kullanýcý Adý", Width=100},
               new ColumnDefinition(){Name="Password", DisplayName="Þifre", Width=100},
           };
        }

        public BaseEntity CurrentEntity
        {
            get {
                return CurrentDbtUser;
            }
        }

        public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
        {
            return DMT.Provider.Db.ReadList<User>(fExp, pageNo, pageSize);
        }

        private User currentDbtUser;
        public User CurrentDbtUser
        {
            get
            {
                if (currentDbtUser == null)
                    currentDbtUser = new User();
                return currentDbtUser;
            }
            set
            {
                currentDbtUser = value;
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
            return FilterExpression
                .Where("Name", CriteriaTypes.Like, "")
                .OrderBy("Name");
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
            CurrentDbtUser = (User)entity;

            if (CurrentDbtUser.Id > 0)
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentDbtUser);
                listEntityRoleUser.BindData();
                panelDetail.Visible = true;
            }
            else
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentDbtUser);
                panelDetail.Visible = false;
            }
        }

        private void cmdAddNewDbtUser(string arg)
        {
            ShowEntity(new User());
            editName.Focus();
        }
        private void cmdSaveDbtUser(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentDbtUser);

                CurrentDbtUser.Save();
                DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
        private void cmdDeleteDbtUser(string arg)
        {
            if (DMT.Provider.Confirm("Kullanýcý kaydý silinecek. Onaylýyor musunuz?"))
            {
                CurrentDbtUser.Delete();
                listForm.BindGrid();
                listForm.FocusFirstEntity();
            }
        }

        public string GetTitle()
        {
            return "Kullanýcý" + (!string.IsNullOrEmpty(CurrentDbtUser.Name) ? " - " + CurrentDbtUser.Name : "");
        }

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }

    }
}
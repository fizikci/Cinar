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
        RequiredRight = UserRights.OpenRole,
        EntityType = typeof(Role), 
        CategoryName = "Sistem Yönetimi", 
        DisplayName = "Roller",
        ImageKey = "shield")]
    public partial class EditRole : XtraUserControl, IEntityEditControl
    {
        public EditRole()
        {
            InitializeComponent();

            this.editRight.EntityType = typeof(Right);
            this.editName.EntityType = typeof(Role);

            layoutControlItemName.Image = FamFamFam.shield;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection() { 
                                               new Command(){
                                                                Execute = (arg)=>{ShowEntity((BaseEntity)editName.SelectedItem);},
                                                                Trigger = new CommandTrigger(){Control=editName, Event="ItemSelected"},
                                                            },
                                               new Command(){
                                                                Execute = cmdAddNewRole,
                                                                Trigger = new CommandTrigger(){Control=btnAddRole},
                                                                IsEnabled = () => CurrentRole.Id != -1
                                                            },
                                               new Command(){
                                                                Execute = cmdSaveRole,
                                                                Trigger = new CommandTrigger(){Control=btnSaveRole}
                                                            },
                                               new Command(){
                                                                Execute = cmdDeleteRole,
                                                                Trigger = new CommandTrigger(){Control=btnDeleteRole},
                                                                IsEnabled = () => CurrentRole.Id != -1
                                                            },
                                               new Command(){
                                                                Execute = cmdAddRight,
                                                                Trigger = new CommandTrigger(){Control=btnAddRight},
                                                                IsEnabled = () => editRight.EditValue!=null
                                                            },
                                               new Command(){
                                                                Execute = cmdRemoveRight,
                                                                Trigger = new CommandTrigger(){Control=btnRemoveRight},
                                                                IsEnabled = () => listEntityRoleRight.SelectedEntity!=null
                                                            },
                                           };
        }
        public void Initialize(CommandManager cmdMan)
        {
            setupListEntityRoleUser();
            setupListEntityRoleRight();

            cmdMan.Commands.AddRange(listEntityRoleUser.GetCommands());
            cmdMan.Commands.AddRange(listEntityRoleRight.GetCommands());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LookAndFeel.StyleChanged += delegate
                                                 {
                                                     listEntityRoleUser.WhenStyleChanged();
                                                     listEntityRoleRight.WhenStyleChanged();
                                                 };
        }

        private void setupListEntityRoleUser()
        {
            listEntityRoleUser.EntityType = typeof(RoleUser);
            listEntityRoleUser.GetVisibleColumns = () => new ColumnDefinitionList{
                new ColumnDefinition(){Name = "User", DisplayName="Kullanýcýlar", Width=150},
            };
            listEntityRoleUser.GetFilter = () => FilterExpression.Create("RoleId", CriteriaTypes.Eq, CurrentRole.Id);
            listEntityRoleUser.CreateEntityForm = (entity) => new FormRoleUser(CurrentRole.Id, -1, (RoleUser)entity);
            listEntityRoleUser.DoubleClick = () =>
            {
                RoleUser entity = listEntityRoleUser.SelectedEntity as RoleUser;
                if (entity != null)
                    DMT.Provider.UIMetaData.ShowDialog(entity.User);
            };
        }
        private void setupListEntityRoleRight()
        {
            listEntityRoleRight.EntityType = typeof(RoleRight);
            listEntityRoleRight.GetVisibleColumns = () => new ColumnDefinitionList{
                      new ColumnDefinition(){Name="Right", DisplayName="Yetki Adý", Width=150},
                  };
            listEntityRoleRight.GetFilter = () => FilterExpression.Create("RoleId", CriteriaTypes.Eq, CurrentRole.Id);
            listEntityRoleRight.CreateEntityForm = (entity) => new FormRoleRight(-1, CurrentRole.Id, (RoleRight)entity);
        }
        public Type GetEntityType()
        {
            return typeof(Role);
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
                return CurrentRole;
            }
        }

        public object GetEntityList(FilterExpression fExp, int pageNo, int pageSize)
        {
            return DMT.Provider.Db.ReadList<Role>(fExp, pageNo, pageSize);
        }

        private Role currentRole;
        public Role CurrentRole
        {
            get
            {
                if (currentRole == null)
                    currentRole = new Role();
                return currentRole;
            }
            set
            {
                currentRole = value;
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
            CurrentRole = (Role)entity;

            if (CurrentRole.Id > 0)
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentRole);
                listEntityRoleUser.BindData();
                listEntityRoleRight.BindData();
                panelDetail.Visible = true;
            }
            else
            {
                DMT.Provider.UpdateEditControlValuesWithEntity(layoutEntity, CurrentRole);
                panelDetail.Visible = false;
            }
        }

        private void cmdAddNewRole(string arg)
        {
            ShowEntity(new Role());
            editName.Focus();
        }
        private void cmdSaveRole(string arg)
        {
            DMT.Provider.UpdateEntityWithEditControlValues(layoutEntity, CurrentRole);

            CurrentRole.Save();
            DMT.Provider.FeedBack("Kaydedildi.");

            if (listForm != null)
            {
                listForm.BindGrid();
                listForm.FocusCurrentEntity();
            }
        }
        private void cmdDeleteRole(string arg)
        {
            if (DMT.Provider.Confirm("Rol kaydý silinecek. Onaylýyor musunuz?"))
            {
                CurrentRole.Delete();
                listForm.BindGrid();
                listForm.FocusFirstEntity();
            }
        }

        private void cmdAddRight(string arg)
        {
            Right r = null;
            if ((editRight.EditValue as Right) == null)
            {
                r = new Right { Name = editRight.EditValue.ToString() };
                r.Save();
            }
            else
                r = editRight.EditValue as Right;

            RoleRight rr = new RoleRight
            {
                RightId = r.Id,
                RoleId = CurrentRole.Id
            };

            rr.Save();
            listEntityRoleRight.BindData();

            editRight.EditValue = null;
        }
        private void cmdRemoveRight(string arg)
        {
            if (DMT.Provider.Confirm("Seçilen yetki bu rolden kaldýrýlacak. Onaylýyor musunuz?"))
            {
                listEntityRoleRight.SelectedEntity.Delete();
                listEntityRoleRight.BindData();
            }
        }

        public string GetTitle()
        {
            return "Rol" + (!string.IsNullOrEmpty(CurrentRole.Name) ? " - " + CurrentRole.Name : "");
        }

        public void SetStyleOfGridCell(object data, RowStyleEventArgs args)
        {
        }


    }
}
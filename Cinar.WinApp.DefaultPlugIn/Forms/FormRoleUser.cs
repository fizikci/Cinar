using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cinar.WinUI;
using Cinar.Entities.Standart;

namespace Cinar.WinApp.DefaultPlugIn.Forms
{
    public partial class FormRoleUser : DevExpress.XtraEditors.XtraForm, IFormEntity
    {
        int role_id;
        int user_id;
        RoleUser entity;

        public RoleUser Entity { get { return entity; } set { entity = value; } }

        public FormRoleUser(int role_id, int user_id, RoleUser entity)
        {
            InitializeComponent();

            this.editUser.EntityType = typeof(User);
            this.editRole.EntityType = typeof(Role);


            this.role_id = role_id;
            this.user_id = user_id;
            this.entity = entity == null ? new RoleUser() : (RoleUser)DMT.Provider.CloneObject(entity);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            editUser.DrawItem = (item) =>
            {
                item.Appearance.ForeColor = (item.Item as User).Deleted ? Color.Gray : Color.Black;
            };
            editRole.DrawItem = (item) =>
            {
                item.Appearance.ForeColor = (item.Item as Role).Deleted ? Color.Gray : Color.Black;
            };

            if (user_id > -1)
            {
                editUser.EditValue = DMT.Provider.Db.Read<User>(user_id);
                layoutControl.Height -= layoutDbtUser.Height;
                layoutControlGroup1.Remove(layoutDbtUser);
            }

            if (role_id > -1)
            {
                editRole.EditValue = DMT.Provider.Db.Read<Role>(role_id);
                layoutControl.Height -= layoutRole.Height;
                layoutControlGroup1.Remove(layoutRole);
            }

            DMT.Provider.UpdateEditControlValuesWithEntity(layoutControl, entity);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                DMT.Provider.UpdateEntityWithEditControlValues(layoutControl, entity);

                if (role_id > -1)
                    entity.Role = DMT.Provider.Db.Read<Role>(role_id);

                if (user_id > -1)
                    entity.User = DMT.Provider.Db.Read<User>(user_id);
            }

            base.OnClosing(e);
        }

        #region IFormEntity Members

        public BaseEntity CurrentEntity
        {
            get { return this.Entity; }
        }

        #endregion
    }
}
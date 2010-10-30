using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cinar.WinUI;
using Cinar.Entities.Standart;

namespace Cinar.WinApp.DefaultPlugIn.Forms
{
    public partial class FormRoleRight : DevExpress.XtraEditors.XtraForm, IFormEntity
    {
        int role_id;
        int right_id;
        RoleRight entity;

        public RoleRight Entity { get { return entity; } set { entity = value; } }

        public FormRoleRight(int right_id, int role_id, RoleRight entity)
        {
            InitializeComponent();

            this.editRight.EntityType = typeof(Right);
            this.editRole.EntityType = typeof(Role);

            this.role_id = role_id;
            this.right_id = right_id;
            this.entity = entity == null ? new RoleRight() : (RoleRight)DMT.Provider.CloneObject(entity);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            editRole.DrawItem = (item) =>
            {
                item.Appearance.ForeColor = (item.Item as Role).Deleted ? Color.Gray : Color.Black;
            };

            if (right_id > -1)
            {
                editRight.EditValue = DMT.Provider.Db.Read<Right>(right_id);
                layoutControl.Height -= layoutRight.Height;
                layoutControlGroup1.Remove(layoutRight);
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

                if (right_id > -1)
                    entity.Right = DMT.Provider.Db.Read<Right>(right_id);
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
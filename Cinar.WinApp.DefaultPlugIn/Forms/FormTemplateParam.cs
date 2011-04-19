using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.UICommands;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using DevExpress.XtraLayout.Utils;
using Cinar.WinUI;
using Cinar.Entities.Standart;

namespace Cinar.WinApp.DefaultPlugIn.Forms
{
    public partial class FormTemplateParam : DevExpress.XtraEditors.XtraForm, IFormEntity
    {
        ReportParam entity;
        public ReportParam Entity { get { return entity; } set { entity = value; } }

        public FormTemplateParam(ReportParam entity)
        {
            InitializeComponent();

            this.entity = entity == null ? new ReportParam() : (ReportParam)DMT.Provider.CloneObject(entity);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			editPType.EditValueChanged += new EventHandler(editPType_EditValueChanged);

            editPType.BindTo(Enum.GetValues(typeof(ReportParamTypes)));
            editPModuleName.BindTo(typeof(BaseEntity).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseEntity))).Select(t => t.Namespace.SplitAndGetLast('.')).OrderBy(s => s).Distinct());
            editPModuleName.EditValueChanged += new EventHandler(editPModuleName_EditValueChanged);
            editPEntityName.EditValueChanged += new EventHandler(editPEntityName_EditValueChanged);

            DMT.Provider.UpdateEditControlValuesWithEntity(layoutControl, entity);
        }

        void editPModuleName_EditValueChanged(object sender, EventArgs e)
        {
            editPEntityName.BindTo(typeof(BaseEntity).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseEntity)) && t.Namespace.EndsWith(editPModuleName.Text)).Select(t => t.Name).OrderBy(s => s).Distinct());
        }

        void editPEntityName_EditValueChanged(object sender, EventArgs e)
        {
            editPDisplayField.BindTo(typeof(BaseEntity).Assembly.GetTypes().First(t => t.FullName.EndsWith(editPModuleName.Text + "." + editPEntityName.Text)).GetProperties().Select(p => p.Name).OrderBy(s => s));
            editPValueField.BindTo(typeof(BaseEntity).Assembly.GetTypes().First(t => t.FullName.EndsWith(editPModuleName.Text + "." + editPEntityName.Text)).GetProperties().Select(p => p.Name).OrderBy(s => s));
        }

		void editPType_EditValueChanged(object sender, EventArgs e)
		{
			if (editPType.Text == "Entity")
				layoutEntityGrup.Enabled = true;
			else
				layoutEntityGrup.Enabled = false;
		}


        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                DMT.Provider.UpdateEntityWithEditControlValues(layoutControl, entity);
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
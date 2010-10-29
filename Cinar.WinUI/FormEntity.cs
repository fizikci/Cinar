using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cinar.UICommands;
using DevExpress.XtraEditors;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public partial class FormEntity : XtraForm
    {
        public IEntityEditControl EditControl;
        private BaseEntity entity;
        CommandManager cmdMan = new CommandManager();
        public Dictionary<string, object> DefaultValues = new Dictionary<string, object>();

        public FormEntity(IEntityEditControl editControl, BaseEntity entity, string title)
        {
            InitializeComponent();

            this.Text = title;

            this.entity = entity;
            EditControl = editControl;
            EditControl.Initialize(cmdMan);
            cmdMan.Commands.AddRange(EditControl.GetCommands());

            Control c = editControl as Control;
            c.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            c.Location = new Point(3, 3);
            c.Size = new Size(663, c.Size.Height);
            panelControl1.Controls.Add(c);

            panelControl1.AutoScroll = true;


            cmdMan.SetCommandTriggers();
            cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();
        }

        public BaseEntity Entity 
        {
            get { return EditControl.CurrentEntity; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(DefaultValues.Count>0)
                foreach (var item in DefaultValues)
                    entity.SetMemberValue(item.Key, item.Value);

            EditControl.ShowEntity(entity);
        }
    }
}
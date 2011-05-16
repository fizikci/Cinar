using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public partial class PromptDialog : DevExpress.XtraEditors.XtraForm
    {
        public PromptDialog()
        {
            InitializeComponent();
        }

        public static T Prompt<T>(string question, T defaultAnswer)
        {
            PromptDialog pd = new PromptDialog();
            pd.lblQuestion.Text = question;
            BaseEdit edit = null;
            switch (typeof(T).Name)
            {
                case "DateTime":
                    edit = new DateEdit();
                    break;
                case "Boolean":
                    edit = new ComboBoxEdit();
                    (edit as ComboBoxEdit).BindTo(new[] { new ObjectStringPair { Value = true, Display = "Evet" }, new ObjectStringPair { Value = false, Display = "Hayýr" } });
                    break;
                default:
                    if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                    {
                        LookUp lookUp = new LookUp();
                        lookUp.EntityType = typeof(T);
                        lookUp.DisplayFieldName = ((BaseEntity)Activator.CreateInstance(typeof(T))).GetNameColumn();
                        edit = lookUp;
                    }
                    else
                        edit = new TextEdit();
                    break;
            }
            edit.EditValue = defaultAnswer;
            edit.Dock = DockStyle.Fill;
            pd.panel.Controls.Add(edit);

            if (pd.ShowDialog() == DialogResult.OK)
            {
                if(typeof(T).Name=="Boolean")
                    return (T)(edit.EditValue as ObjectStringPair).Value;

                return (T)edit.EditValue;
            }

            throw new Exception("Ýptal edildi.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.WinUI
{
	public partial class FormTemplatePreview : DevExpress.XtraEditors.XtraForm
	{
        private FormTemplatePreview(List<ReportParam> prms)
		{
			InitializeComponent();

            foreach (ReportParam tp in prms)
            {
                BaseEdit ctrl = null;
                switch (tp.PType)
                {
                    case ReportParamTypes.TamSayý:
                        SpinEdit se = new SpinEdit();
                        ctrl = se;
                        break;
                    case ReportParamTypes.Tarih:
                        ctrl = new DateEdit();
                        break;
                    case ReportParamTypes.Metin:
                        ctrl = new TextEdit();
                        break;
                    case ReportParamTypes.OndalikliSayi:
                        ctrl = new SpinEdit();
                        break;
                    case ReportParamTypes.EvetHayir:
                        ctrl = new CheckEdit();
                        break;
                    case ReportParamTypes.Entity:
                        LookUp lu = new LookUp();
                        lu.EntityType = DMT.Provider.GetEntityType(tp.PModuleName, tp.PEntityName);
                        lu.DisplayFieldName = tp.PDisplayField;
                        lu.ValueFieldName = tp.PValueField;
                        ctrl = lu;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ctrl.Name = tp.PName;
                LayoutControl.AddItem(tp.Name, ctrl);
            }

		}

        public static Hashtable GetParameterValues(List<ReportParam> templateParams)
        {
            if (templateParams.Count == 0)
                return new Hashtable();

            FormTemplatePreview ftp = new FormTemplatePreview(templateParams);

            if (ftp.ShowDialog() == DialogResult.OK)
            {
                Hashtable hs = new Hashtable();
                foreach (ReportParam tp in templateParams)
                {
                    object paramVal = (ftp.LayoutControl.GetControlByName(tp.PName) as BaseEdit).EditValue;
                    if (paramVal is BaseEntity)
                        paramVal = paramVal.GetMemberValue(tp.PValueField);
                    hs.Add(tp.PName, paramVal);
                }
                return hs;
            }

            return null;
        }
        public static Hashtable GetParameterValues(long reportId)
        {
            return GetParameterValues(
                DMT.Provider.Db.ReadList<ReportParam>(
                    FilterExpression.Create("ReportId", CriteriaTypes.Eq, reportId)
                )
            );
        }
    }
}
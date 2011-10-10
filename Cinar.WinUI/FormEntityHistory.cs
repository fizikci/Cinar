using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.WinUI
{
    public partial class FormEntityHistory : DevExpress.XtraEditors.XtraForm
    {
        public FormEntityHistory(string entityName, long entityId)
        {
            InitializeComponent();

            DMT.Provider.PopulateGridColumns(new List<ColumnDefinition> { 
                new ColumnDefinition{Name=EntityHistoryFields.InsertDate, DisplayName="Tarih", Width=120, FormatType=FormatType.DateTime, FormatString="dd.MM.yyyy HH:mm"},
                new ColumnDefinition{Name=EntityHistoryFields.Operation, DisplayName="Ýþlem", Width=80},
                //new ColumnDefinition{Name=EntityHistoryFields.UserName, DisplayName="Kullanýcý", Width=140},
            }, gridView);

            grid.DataSource = DMT.Provider.Db.ReadList<EntityHistory>(
                FilterExpression
                .Where(EntityHistoryFields.EntityName, CriteriaTypes.Eq, entityName)
                .And(EntityHistoryFields.EntityId, CriteriaTypes.Eq, entityId));
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EntityHistory eh = (EntityHistory)gridView.GetRow(e.FocusedRowHandle);
            editDetails.EditValue = eh.Details;
        }


    }
}
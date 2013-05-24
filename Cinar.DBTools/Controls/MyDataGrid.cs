using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Cinar.DBTools.Controls
{
    public class MyDataGrid : DataGridView
    {
        public MyDataGrid()
        {
            CausesValidation = false;
            RowTemplate.Height = 18;
        }

        public int RowNumberOffset { get; set; }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + RowNumberOffset + 1).ToString();

            while (strRowNumber.Length < this.RowCount.ToString().Length) strRowNumber = "0" + strRowNumber;

            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);

            if (this.RowHeadersWidth < (int)(size.Width + 20)) this.RowHeadersWidth = (int)(size.Width + 20);

            Brush b = ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected) ? SystemBrushes.HighlightText : SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

            base.OnRowPostPaint(e);
        }
    }
}

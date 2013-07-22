using System;
using System.Text;
using System.Data;
using Cinar.Database;
using System.Xml.Serialization;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class SQLDataList : TableView
    {
        public SQLDataList()
        {
            PictureHeight = 0;
            PictureWidth = 0;
            DataTemplate = "";
            SQL = "";
        }

        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SQL { get; set; }

        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string DataTemplate { get; set; }

        public int PictureWidth { get; set; }

        public int PictureHeight { get; set; }

        public bool CropPicture { get; set; }

        DataTable data = null;

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (this.SQL == "")
                return "";

            Interpreter engine = Provider.GetInterpreter(SQL, this);
            engine.Parse();
            engine.Execute();
            SQL = engine.Output;

            data = Provider.Database.GetDataTable(SQL);

            if (data==null || data.Rows.Count == 0)
                return "";
            else
                sb.Append(base.show());

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override int rowCount
        {
            get { return (int)Math.Ceiling((double)data.Rows.Count / (double)this.cols); }
        }

        DataRow dr;
        Interpreter engineCell = null;

        protected override string getCellHTML(int row, int col)
        {
            string html = this.DataTemplate;

            if (html.Trim() == "")
                return "";

            int index = row * this.cols + col;

            if (data.Rows.Count <= index)
                return String.Empty;

            dr = data.Rows[index];

            if (engineCell == null)
            {
                engineCell = Provider.GetInterpreter(html, this);
                engineCell.Parse();
            }
            engineCell.SetAttribute("row", dr);
            engineCell.SetAttribute("index", index + 1);
            engineCell.Execute();
            html = engineCell.Output;

            return html;
        }

        [XmlIgnore]
        public string ThumbPicture
        {
            get
            {
                if (dr.Table.Columns.Contains("Picture") && !dr.IsNull("Picture"))
                    return Provider.GetThumbPath(dr["Picture"].ToString(), this.PictureWidth, this.PictureHeight, CropPicture);
                else if (dr.Table.Columns.Contains("FileName") && !dr.IsNull("FileName"))
                    return Provider.GetThumbPath(dr["FileName"].ToString(), this.PictureWidth, this.PictureHeight, CropPicture);
                else
                    return "Could not find a picture field - Try something else";
            }
        }
    }
}
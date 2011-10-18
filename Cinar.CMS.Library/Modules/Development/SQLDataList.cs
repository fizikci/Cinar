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
        private string sql = "";
        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SQL
        {
            get { return sql; }
            set { sql = value; }
        }

        private string dataTemplate = "";
        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string DataTemplate
        {
            get { return dataTemplate; }
            set { dataTemplate = value; }
        }

        protected int pictureWidth = 0;
        public int PictureWidth
        {
            get { return pictureWidth; }
            set { pictureWidth = value; }
        }

        protected int pictureHeight = 0;
        public int PictureHeight
        {
            get { return pictureHeight; }
            set { pictureHeight = value; }
        }

        DataTable data = null;

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (this.sql == "")
                return "";

            Interpreter engine = Provider.GetInterpreter(sql, this);
            engine.Parse();
            engine.Execute();
            sql = engine.Output;

            data = Provider.Database.GetDataTable(sql);

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
            string html = this.dataTemplate;

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
                return Provider.GetThumbPath(dr["Picture"].ToString(), this.pictureWidth, this.pictureHeight);
            }
        }
    }
}
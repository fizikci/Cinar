using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    public abstract class TableView : Module
    {
        private bool displayAsTable;
        public bool DisplayAsTable
        {
            get { return displayAsTable; }
            set { displayAsTable = value; }
        }

        protected int cols = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        private bool encloseWithDiv;
        public bool EncloseWithDiv
        {
            get { return encloseWithDiv; }
            set { encloseWithDiv = value; }
        }

        private string divClassName = "extraItemDiv";
        public string DivClassName
        {
            get { return divClassName; }
            set { divClassName = value; }
        }

        protected abstract int rowCount { get; }

        protected abstract string getCellHTML(int row, int col);

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (rowCount == 0)
                return ""; //***

            if (displayAsTable) // table layout
            {
                sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\n");

                for (int i = 0; i < this.rowCount; i++)
                {
                    sb.Append("<tr>\n");
                    for (int j = 0; j < this.cols; j++)
                    {
                        string cellHtml = this.getCellHTML(i, j);
                        if (this.rowCount==1 && string.IsNullOrWhiteSpace(cellHtml)) break;

                        sb.Append("<td>\n");
                        sb.Append(cellHtml);
                        sb.Append("</td>\n");
                    }
                    sb.Append("</tr>\n");
                }
                sb.Append("</table>\n");
            }
            else                // flow layout
            {
                for (int i = 0; i < this.rowCount; i++)
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        if(encloseWithDiv)
                            sb.AppendFormat("<div class=\"{0}\">\n", DivClassName);
                        sb.Append(this.getCellHTML(i, j));
                        if(encloseWithDiv)
                            sb.Append("</div>\n");
                    }
                }
            }

            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (this.cols == 0)
                throw new Exception(Provider.GetResource("Columns number cannot be zero"));
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.{2} {{}}\n", this.Name, this.Id, DivClassName);
            return sb.ToString();
        }

    }
}
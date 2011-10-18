using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class Table : ModuleContainer
    {
        protected int cols = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        [EditFormFieldProps()]
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        private int rows = 3;
        [ColumnDetail(IsNotNull = true, DefaultValue = "3")]
        [EditFormFieldProps()]
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        private List<Module> getCellModules(string regionId) 
        {
            return this.ChildModules.FindAll(delegate(Module mdl) { return mdl.Region == regionId; });
        }

        protected string getCellHTML(int row, int col)
        {
            string regionId = "tblCell" + this.Id + "_" + row + "_" + col;
            return String.Format("<div id=\"{0}\" class=\"tblCell Region\">{1}</div>", regionId, Provider.GetRegionInnerHtml(this.getCellModules(regionId)));
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (Provider.DesignMode)
                sb.Append("Table<br/>\n");

            if (this.rows == 0 && Provider.DesignMode)
                return Provider.GetResource("Empty content"); //***

            sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\n");

            for (int i = 0; i < this.rows; i++)
            {
                sb.Append("<tr>\n");
                for (int j = 0; j < this.cols; j++)
                {
                    sb.Append("<td>\n");
                    sb.Append(this.getCellHTML(i, j));
                    sb.Append("</td>\n");
                }
                sb.Append("</tr>\n");
            }
            sb.Append("</table>\n");

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (this.rows < 1 || this.rows > 30 || this.cols < 1 || this.cols > 30)
                throw new Exception(Provider.GetResource("Row and column numbers must be between 1 and 30"));

            string regionId = "tblCell" + this.Id;

            //TODO: aşağıdaki fasilite yanlış çalışıyor.. Fazla modül siliyor.
            if (isUpdate)
            {
                // silinecek cell'leri bulalım
                Module[] modules = Module.Read(Provider.Database.GetDataTable("select * from [Module] where Region > '" + (regionId + "_" + this.Rows) + "_' UNION ALL select * from [Module] where Region like '" + regionId + "_%' and substring(Region, " + (regionId.Length + 4) + ", 1)>='" + this.Cols + "' and Template='" + this.Template + "';"));
                // ve tek tek silelim
                foreach (Module module in modules)
                    module.Delete();
            }
        }

    }
}
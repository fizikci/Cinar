using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Data;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class TagCloud : Module
    {
        protected string useTemplate = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string UseTemplate
        {
            get { return useTemplate; }
            set { useTemplate = value; }
        }

        private int maxFontSize = 30;
        [ColumnDetail(IsNotNull = true)]
        public int MaxFontSize
        {
            get { return maxFontSize; }
            set { maxFontSize = value; }
        }

        protected int minFontSize = 10;
        [ColumnDetail(IsNotNull = true),]
        public int MinFontSize
        {
            get { return minFontSize; }
            set { minFontSize = value; }
        }

        protected int minContentCount = 0;
        [ColumnDetail(IsNotNull = true),]
        public int MinContentCount
        {
            get { return minContentCount; }
            set { minContentCount = value; }
        }

        protected int maxContentCount = Int32.MaxValue;
        [ColumnDetail(IsNotNull = true),]
        public int MaxContentCount
        {
            get { return maxContentCount; }
            set { maxContentCount = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            Tag[] tags = (Tag[])Provider.Database.ReadList(typeof(Tag), "select Id, Name, ContentCount from Tag where ContentCount>{0} AND ContentCount<{1} order by Name", this.minContentCount, this.maxContentCount);
            Provider.Translate(tags);

            DataRow drMaxMin = Provider.Database.GetDataRow("select max(ContentCount) as ccMax, min(ContentCount) as ccMin from Tag where ContentCount>{0} AND ContentCount<{1}", this.minContentCount, this.maxContentCount);
            decimal ccMax = (decimal)(int)drMaxMin["ccMax"];
            decimal ccMin = (decimal)(int)drMaxMin["ccMin"];
            if (ccMin == ccMax) ccMax = ccMin + 1;

            foreach (Tag tag in tags)
            {
                decimal fontSize = (decimal)minFontSize + ((decimal)tag.ContentCount - ccMin) * (decimal)(maxFontSize - minFontSize) / (ccMax - ccMin);
                sb.AppendFormat("{4}<a href=\"{0}?tag={1}\" style=\"font-size:{2}px\">{3}</a><!--{5}-->",
                    useTemplate,
                    Provider.Server.UrlEncode(tag.Name),
                    fontSize.ToString().Replace(",","."),
                    tag.Name,
                    Environment.NewLine,
                    tag.ContentCount);
            }

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} a {{text-transform:capitalize}}\n", this.Name, this.Id);
            return sb.ToString();
        }

    }

}

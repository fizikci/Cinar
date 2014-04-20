using System;
using System.Text;
using Cinar.Database;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class Ajanda : Module
    {
        private int kategoriId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0", References = typeof(Entities.Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int KategoriId
        {
            get { return kategoriId; }
            set { kategoriId = value; }
        }

        private string useTemplate = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string UseTemplate
        {
            get { return useTemplate; }
            set { useTemplate = value; }
        }

        protected int howManyChars = 2;
        public int HowManyChars
        {
            get { return howManyChars; }
            set { if (value > 3) howManyChars = 3; else if (value < 1) howManyChars = 1; else howManyChars = value; }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable eventHash = new Hashtable();

            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            int dayIndex = (int)firstDayOfMonth.DayOfWeek == 0 ? 6 : (int)firstDayOfMonth.DayOfWeek - 1;
            DateTime startDate = firstDayOfMonth.AddDays(Convert.ToDouble(-1 * dayIndex));
            DateTime cellDate;

            //TODO: buraya ContentListByFilter'daki gibi hier muhabbetini uygula
            if (kategoriId == 0) kategoriId = 1;

            IDatabaseEntity[] events =
                Provider.Database.ReadList(typeof(Entities.Content), @"
                    select top 31
                        Id,
                        Title,
                        PublishDate,
                        ShowInPage,
                        ClassName,
                        Hierarchy
                    from 
                        Content
                    where
                        Visible = 1 and 
                        Hierarchy like '" + Provider.GetHierarchyLike(kategoriId) + "%'");


            sb.Append("<table>");
            sb.Append(WriteRow(WriteCell(
                "colspan=7 align=center " + WriteClass("title"), 
                today.ToString("Y"))));

            // kýsaltma gün isimleri
            DateTime d = new DateTime(2008, 6, 15); // bu pazar :)
            if (howManyChars > d.ToString("ddd").Length) howManyChars = d.ToString("ddd").Length;
            sb.Append(WriteRow(WriteCells(WriteClass("dayNames"),
                d.AddDays(1).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(2).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(3).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(4).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(5).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(6).ToString("ddd").Substring(0, howManyChars),
                d.AddDays(7).ToString("ddd").Substring(0, howManyChars)
            )));
            for (int i = 0; i < Math.Ceiling((lastDayOfMonth.Day+dayIndex)/7d); i++)
            {
                string rowStr = "";
                for (int j = 0; j < 7; j++)
                {
                    string attr = "";
                    string cellValue = "";
                    cellDate = startDate + TimeSpan.FromDays(i * 7 + j);
                    
                    foreach (Entities.Content calenderEvent in events)
                    {
                        string template = Provider.GetTemplate(calenderEvent, useTemplate);
                        if (calenderEvent.PublishDate.Date.Equals(cellDate))
                        {
                            //?
                            attr += WriteClass("eventDays");
                            cellValue = WriteLink(Provider.GetPageUrl(template, calenderEvent.Id, calenderEvent.Category.Title, calenderEvent.Title), cellDate.Day.ToString(), calenderEvent.Title);
                            break;
                        }
                    }
                    if(cellDate==today)
                        attr += WriteClass("today");
                    else if(cellDate < firstDayOfMonth || cellDate > lastDayOfMonth) 
                        attr += WriteClass("otherDays");

                    if (string.IsNullOrEmpty(cellValue)) 
                        cellValue = cellDate.Day.ToString();
 
                    rowStr += WriteCell(attr, cellValue);
                }
                sb.Append(WriteRow(rowStr));
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        private string WriteLink(string page, string label, string title)
        {
            return "<a href=\"" + page + "\" title=\"" + title + "\">" + label + "</a>";
        }

        private string WriteClass(string className)
        {
            return " class=" + className + " ";
        }

        private string WriteStyle(string value)
        {
            return "style=\"" + value + "\"";
        }

        private string WriteCells(string attr, params string[] values)
        {
            string s = "";
            foreach (string value in values)
            {
                s += WriteCell(attr, value);
            }
            return s;
        }

        private string WriteCell(string value)
        {
            return WriteCell("", value);
        }

        private string WriteCell(string attr, string value)
        {
            return "<td " + attr + " >" + value + "</td>";
        }

        private string WriteRow(string value)
        {
            return "<tr>" + value + "</tr>";
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} td {{text-align:center}}\n", getCSSId());
            sb.AppendFormat("#{0} td.title {{font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} td.dayNames {{font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} td.eventDays {{font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} td.emptyDays {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.today {{background: orange; color:white}}\n", getCSSId());
            sb.AppendFormat("#{0} td.otherDays {{color: gray;}}\n", getCSSId());
            return sb.ToString();
        }
    }
}

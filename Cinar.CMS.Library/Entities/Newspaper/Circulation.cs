using System;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, WeekStartDate as [Circulation.WeekStartDate],  DailyName as [Circulation.DailyName],  AvgDailySale as [Circulation.AvgDailySale] from [Circulation]", QueryOrderBy = "Id")]
    public class Circulation : BaseEntity
    {
        private DateTime weekStartDate;
        public DateTime WeekStartDate
        {
            get { return weekStartDate; }
            set { weekStartDate = value; }
        }

        private string dailyName;
        public string DailyName
        {
            get { return dailyName; }
            set { dailyName = value; }
        }

        private int avgDailySale;
        public int AvgDailySale
        {
            get { return avgDailySale; }
            set { avgDailySale = value; }
        }


        public override string GetNameValue()
        {
            return this.Id.ToString();
        }
        public override string GetNameColumn()
        {
            return "Id";
        }


    }
}

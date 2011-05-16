using Cinar.Database;

namespace Cinar.Entities.Standart
{
    public class Report : NamedEntity
    {
        private string reportLayout = "";
    	private string sqlQuery = "";
        private string html = "";

        [ColumnDetail(ColumnType=DbType.Text)]
        public string ReportLayout
        {
            get { return reportLayout; }
            set { reportLayout = value; }
        }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string SQLQuery
		{
			get { return sqlQuery; }
			set { sqlQuery = value; }
		}

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Html 
        {
            get { return html; }
            set { html = value; }
        }

        public virtual ReportExecutionTypes ExecutionType
        {
            get {
                if (!string.IsNullOrEmpty(SQLQuery) && !string.IsNullOrEmpty(Html))
                    return ReportExecutionTypes.HtmlWithSQLQuery;

                if (!string.IsNullOrEmpty(SQLQuery))
                    return ReportExecutionTypes.ReportWithSQLQuery;

                return ReportExecutionTypes.Undefined;
            }
        }
    }

    public enum ReportExecutionTypes
    {
        Undefined,
        ReportWithSQLQuery,
        HtmlWithSQLQuery
    }
}

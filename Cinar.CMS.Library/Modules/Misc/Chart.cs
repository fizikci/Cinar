using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class Chart : Module
    {
        private bool htmlChart;
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:[[false,'Image'],[true,'HTML']]")]
        public bool HTMLChart
        {
            get { return htmlChart; }
            set { htmlChart = value; }
        }

        // cht
        private string chartType = "lc";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_CHARTTYPES_")]
        public string ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        private string sql = "";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SQL
        {
            get { return sql; }
            set { sql = value; }
        }

        // chdl
        private string series = ""; // if sql enter column name, otherwise enter legend names seperated with pipe (|)
        public string Series
        {
            get { return series; }
            set { series = value; }
        }

        // pie: chl   other: chxt=y&chxl=0:|label1|label2|&chxr=axis1,minVal1,maxVal1|...
        private string labels = ""; // if sql enter column name, otherwise enter label names seperated with pipe (|)
        public string Labels
        {
            get { return labels; }
            set { labels = value; }
        }

        // pie: chd=t:10,20,30   other: chd ve chxt ile chxr'yi ekle
        private string values = ""; // if sql enter column name, otherwise enter values seperated with comma (,) and pipe (|)
        public string Values
        {
            get { return values; }
            set { values = value; }
        }

        private string labelsFormat = "";
        public string LabelsFormat
        {
            get { return labelsFormat; }
            set { labelsFormat = value; }
        }

        // chdlp
        private string legendPosition = "r"; // r,l,b,t
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_LEGENDPOSITIONS_")]
        public string LegendPosition
        {
            get { return legendPosition; }
            set { legendPosition = value; }
        }

        // chs=widthxheight
        protected int width = 500;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        // chs
        protected int height = 300;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        // chco
        private string colors = "ff9900,6699ff,669933,3333ff,ff66cc,ff3300,000000,666666";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        // chf=bg,bgFill
        private string bgFill = "s,EFEFEF";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string BgFill
        {
            get { return bgFill; }
            set { bgFill = value; }
        }

        // chf=c,chartFill
        private string chartFill = "lg,0,76A4FB,1,FFFFFF,0";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string ChartFill
        {
            get { return chartFill; }
            set { chartFill = value; }
        }

        /// <summary>
        /// chma
        ///     <left margin>,<right margin>,<top margin>,<bottom margin>|<legend width>,<legend height>
        /// </summary>
        private string margins = "";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Margins
        {
            get { return margins; }
            set { margins = value; }
        }

        // chtt
        private string title = "Title";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        // chts=titleColor,titleFontSize
        private string titleStyle = "ff9900,14";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string TitleStyle
        {
            get { return titleStyle; }
            set { titleStyle = value; }
        }

        /// <summary>
        /// chg
        ///     <x axis step size>,<y axis step size>,<length of line segment>,<length of blank segment>
        /// </summary>
        private string gridLines = "";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string GridLines
        {
            get { return gridLines; }
            set { gridLines = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            Pair data = this.getChartData();

            if (this.htmlChart)
            {
                sb.AppendFormat("<div id=\"chart{0}\"></div>\n", this.Id);

                sb.Append("<script type=\"text/javascript\">\n");
                sb.AppendFormat("document.observe('dom:loaded', function(){{\n");
                sb.AppendFormat("    var c = new Chart('chart{0}', {{\n", this.Id);
                sb.AppendFormat("        chartType:'{0}',\n", this.chartType);
                sb.AppendFormat("        data:[[{0}]],\n", data.Values.Replace("|", "],["));
                sb.AppendFormat("        width:{0},\n", this.width);
                sb.AppendFormat("        height:{0},\n", this.height);
                sb.AppendFormat("        series:['{0}'],\n", data.Series.Replace("|", "','"));
                sb.AppendFormat("        labels:['{0}'],\n", data.Labels.Replace(",", "','"));
                sb.AppendFormat("        minValue:{0},\n", data.MinValue.ToString().Replace(",", "."));
                sb.AppendFormat("        maxValue:{0},\n", data.MaxValue.ToString().Replace(",", "."));
                if (this.legendPosition != "")
                    sb.AppendFormat("        legendPosition:'{0}',\n", this.legendPosition);
                if (this.colors != "")
                    sb.AppendFormat("        colors:['{0}'],\n", this.colors.Replace(",", "','"));
                if (this.bgFill != "" && this.bgFill.StartsWith("s,"))
                    sb.AppendFormat("        bgColor:'{0}',\n", this.bgFill.Split(',')[1]);
                if (this.chartFill != "" && this.chartFill.StartsWith("s,"))
                    sb.AppendFormat("        chartBgColor:'{0}',\n", this.chartFill.Split(',')[1]);
                if (this.margins != "")
                    sb.AppendFormat("        margins:[{0}],\n", this.margins.Replace("|", ","));
                if (this.title != "")
                    sb.AppendFormat("        title:'{0}',\n", this.title.Replace("'", "\\'"));
                if (this.titleStyle != "")
                    sb.AppendFormat("        titleColor:'{0}',\n", this.titleStyle.Split(',')[0]);
                if (this.titleStyle != "" && this.titleStyle.Contains(","))
                    sb.AppendFormat("        titleFontSize:{0},\n", this.titleStyle.Split(',')[1]);
                if (this.gridLines != "")
                    sb.AppendFormat("        gridLines:[{0}],\n", this.gridLines);
                sb.Remove(sb.Length - 2, 1);

                sb.Append("    });\nc.show();});\n</script>\n");
            }
            else
            {
                sb.Append("<img src=\"");

                sb.AppendFormat("http://chart.apis.google.com/chart?cht={0}", this.chartType);
                if ("p,p3,pc".Contains(this.chartType))
                    sb.AppendFormat("&chd=t:{0}", data.Values.Replace("|", ","));
                else
                    sb.AppendFormat("&chd=t:{0}", data.Values);
                sb.AppendFormat("&chs={0}x{1}", this.width, this.height);
                if ("p,p3,pc".Contains(this.chartType))
                {
                    sb.AppendFormat("&chl={0}", data.Series);
                }
                else
                {
                    sb.AppendFormat("&chdl={0}", data.Series);
                    sb.Append("&chxt=x,y");
                    sb.AppendFormat("&chxl=0:|{0}|", data.Labels.Replace(",", "|"));
                    data.MinValue = 0;
                    sb.AppendFormat("&chxr=1,{0},{1}", data.MinValue, data.MaxValue);
                }
                if ("bvs,bhs,bvg,bhg".Contains(this.ChartType))
                    sb.Append("&chbh=a");
                if (this.legendPosition != "")
                    sb.AppendFormat("&chdlp={0}", this.legendPosition);
                if (this.colors != "")
                    sb.AppendFormat("&chco={0}", this.colors);
                if (this.bgFill != "" || this.chartFill != "")
                {
                    sb.Append("&chf=");
                    if (this.bgFill != "")
                        sb.AppendFormat("bg,{0}", this.bgFill);
                    if (this.bgFill != "" && this.chartFill != "")
                        sb.Append("|");
                    if (this.chartFill != "")
                        sb.AppendFormat("c,{0}", this.chartFill);
                }
                if (this.margins != "")
                    sb.AppendFormat("&chma={0}", this.margins);
                if (this.title != "")
                    sb.AppendFormat("&chtt={0}", this.title);
                if (this.titleStyle != "")
                    sb.AppendFormat("&chts={0}", this.titleStyle);
                if (this.gridLines != "")
                    sb.AppendFormat("&chg={0}", this.gridLines);

                sb.AppendFormat("\" width=\"{0}\" height=\"{1}\"/>", this.width, this.height);
            }

            return sb.ToString();
        }

        private Pair getChartData()
        {
            Pair data = new Pair();
            if (this.sql != "")
            {
                Interpreter engine = Provider.GetInterpreter(sql, this);
                engine.Parse();
                engine.Execute();
                sql = engine.Output;

                DataTable dt = Provider.Database.GetDataTable(this.sql);

                Dictionary<string, DataTable> list = new Dictionary<string, DataTable>();
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dtSerie = null;
                    string legend = dr[this.series].ToString();
                    if (list.ContainsKey(legend))
                        dtSerie = list[legend];
                    else
                    {
                        dtSerie = new DataTable(legend);
                        dtSerie.Columns.Add(this.labels, dt.Columns[this.labels].DataType);
                        dtSerie.Columns.Add(this.values, dt.Columns[this.values].DataType);
                        list[legend] = dtSerie;
                    }
                    DataRow drNew = dtSerie.NewRow();
                    drNew[this.labels] = dr[this.labels];
                    drNew[this.values] = dr[this.values];
                    dtSerie.Rows.Add(drNew);

                    double val = Convert.ToDouble(dr[this.values]);
                    if (data.MaxValue < val) data.MaxValue = val;
                    if (data.MinValue > val) data.MinValue = val;
                }

                DataSet ds = new DataSet();
                ArrayList al = new ArrayList();
                foreach (string s in list.Keys) al.Add(s);
                al.Sort();
                foreach (string s in al) ds.Tables.Add(list[s]);

                foreach (DataTable _dt in ds.Tables)
                    foreach (DataRow dr in _dt.Rows)
                    {
                        double val = Convert.ToDouble(dr[this.values]);
                        dr[this.values] = val * 100 / data.MaxValue;
                    }

                foreach (DataTable dtSerie in ds.Tables)
                {
                    if (data.Values != "")
                        data.Values += "|";
                    data.Values += getValues(dtSerie, this.values);
                    if (data.Series != "")
                        data.Series += "|";
                    data.Series += dtSerie.TableName;
                }

                data.Labels = getValues(ds.Tables[0], this.labels);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("value", typeof(double));
                foreach (string dataValue in this.values.Replace('|', ',').SplitWithTrim(','))
                {
                    DataRow dr = dt.NewRow();
                    double val = Convert.ToDouble(dataValue);
                    if (data.MaxValue < val) data.MaxValue = val;
                    if (data.MinValue > val) data.MinValue = val;
                    dr[0] = val;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    double val = Convert.ToDouble(dr[0]);
                    dr[0] = val * 100 / data.MaxValue;
                }
                data.Values = this.values;
                data.Labels = this.labels;
                data.Series = this.Series;
            }

            return data;
        }
        private string getValues(DataTable dt, string colName)
        {
            string[] values = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (colName == this.Labels)
                    values[i] = String.Format("{0" + (labelsFormat != "" ? ":" + labelsFormat : "") + "}", dr[colName]).Replace(",", ".");
                else
                    values[i] = String.Format("{0:0}", dr[colName]).Replace(",", ".");
            }

            return String.Join(",", values);
        }

        private class Pair
        {
            public double MaxValue = Double.MinValue;
            public double MinValue = Double.MaxValue;
            public string Values = "";
            public string Series = "";
            public string Labels = "";
        }

        public class SQLParams
        {
            private ArrayList alParams = new ArrayList();
            public string this[string paramName]
            {
                get
                {
                    if (!alParams.Contains(paramName))
                        alParams.Add(paramName);
                    return "{" + alParams.IndexOf(paramName) + "}";
                }
            }
        }

        SQLParams _sqlParams = new SQLParams();
        public SQLParams Params
        {
            get
            {
                return _sqlParams;
            }
        }
    }
}

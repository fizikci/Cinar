using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using Cinar.Scripting;
using DevExpress.XtraReports.UI;
using Cinar.Entities;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Cinar.WinUI
{
	public class ReportExecuter
	{
		private Report musTemp;
		private string customSQL;
		private Hashtable sqlParams;
        private string htmlTemplate;
        private DataSet customDataSource;

        private ReportExecuter()
		{
			
		}

        public static ReportExecuter Load(Report musTemp, string customSQL, Hashtable sqlParams)
        {
            ReportExecuter t = new ReportExecuter();
            t.musTemp = musTemp;
            t.customSQL = customSQL;
            t.sqlParams = sqlParams;
            return t;
        }
        public static ReportExecuter Load(Report musTemp, string customSQL, Hashtable sqlParams, string htmlTemplate)
        {
            ReportExecuter t = new ReportExecuter();
            t.musTemp = musTemp;
            t.customSQL = customSQL;
            t.sqlParams = sqlParams;
            t.htmlTemplate = htmlTemplate;
            return t;
        }
        public static ReportExecuter Load(Report musTemp, DataSet customDataSource)
        {
            ReportExecuter t = new ReportExecuter();
            t.musTemp = musTemp;
            t.customDataSource = customDataSource;
            return t;
        }

		public void ExecuteReport()
		{
			this.report = new XtraReport();
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(musTemp.ReportLayout)))
			{
				this.report = new XtraReport();
				string sql = customSQL;
				if (string.IsNullOrEmpty(sql))
					sql = musTemp.SQLQuery;
				this.report.DataSource = GetDataSource(sql, sqlParams);
				this.report.LoadLayout(stream);
			}
		}

        public DataSet GetDataSource(string sql, Hashtable sqlParams)
        {
            if (customDataSource != null)
                return customDataSource;


            IDbCommand cmd = DMT.Provider.Db.CreateCommand(sql);

            if (sqlParams != null)
                foreach (DictionaryEntry key in sqlParams)
                    cmd.Parameters.Add(DMT.Provider.Db.CreateParameter("@" + key.Key.ToString(), key.Value));

            IDbDataAdapter da = DMT.Provider.Db.CreateDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public string ExecuteHTML()
        {
            Interpreter pret = new Interpreter(htmlTemplate, null);
            pret.SetAttribute("ds", GetDataSource(customSQL ?? musTemp.SQLQuery, sqlParams));
            pret.Parse();
            pret.Execute();
            return pret.Output;
        }


        public static void ORNEK_KOD()
        {
            ReportExecuter t = ReportExecuter.Load(new Report(), new DataSet());
            t.ExecuteReport();
            t.Preview();
        }

        
        internal XtraReport report = null;


        public void ExportToCsv(Stream stream, CsvExportOptions options)
        {
            report.ExportToCsv(stream, options);
        }

        public void ExportToHtml(Stream stream, HtmlExportOptions options)
        {
            report.ExportToHtml(stream, options);
        }

        public void ExportToImage(Stream stream, ImageExportOptions options)
        {
            report.ExportToImage(stream, options);
        }

        public void ExportToMht(Stream stream, MhtExportOptions options)
        {
            report.ExportToMht(stream, options);
        }

        public void ExportToPdf(Stream stream, PdfExportOptions options)
        {
            report.ExportToPdf(stream, options);
        }

        public void ExportToRtf(Stream stream, RtfExportOptions options)
        {
            report.ExportToRtf(stream, options);
        }

        public void ExportToText(Stream stream, TextExportOptions options)
        {
            report.ExportToText(stream, options);
        }

        public void ExportToXls(Stream stream, XlsExportOptions options)
        {
            report.ExportToXls(stream, options);
        }

        public void ExportToFile(ReportExecuter template, string docType, string title, string fileName)
        {
            if (docType == "EKRAN")
            {
                report.ShowPreview();
                return;
            }

            if (string.IsNullOrEmpty(fileName))
                fileName = title.MakeFileName();
            else
                fileName = fileName.MakeFileName();

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CinarDocs";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            Process.Start(dir);

            using (MemoryStream st = new MemoryStream())
            {
                switch (docType)
                {
                    case "CSV":
                        CsvExportOptions csvOptions = new CsvExportOptions();
                        csvOptions.Encoding = Encoding.GetEncoding(1254);
                        template.ExportToCsv(st, csvOptions);
                        break;
                    case "HTM":
                        template.ExportToHtml(st, new HtmlExportOptions("UTF8", title, true));
                        break;
                    case "JPG":
                        template.ExportToImage(st, new ImageExportOptions(ImageFormat.Jpeg));
                        break;
                    case "MHT":
                        template.ExportToMht(st, new MhtExportOptions("UTF8"));
                        break;
                    case "PDF":
                        template.ExportToPdf(st, new PdfExportOptions());
                        break;
                    case "RTF":
                        template.ExportToRtf(st, new RtfExportOptions());
                        break;
                    case "TXT":
                        template.ExportToText(st, new TextExportOptions("", Encoding.UTF8));
                        break;
                    case "XLS":
                        template.ExportToXls(st, new XlsExportOptions(true, true, true));
                        break;
                }

                string path = dir + "\\" + fileName + "." + docType;
                File.WriteAllBytes(path, st.ToArray());
            }
        }

        public void Preview()
        {
            report.ShowPreview();
        }

    }

}

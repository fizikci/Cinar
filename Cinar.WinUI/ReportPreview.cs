using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace Cinar.WinUI
{
    public partial class ReportPreview : DevExpress.XtraEditors.XtraUserControl
    {
        public ReportPreview()
        {
            InitializeComponent();	
            printingSystem.SetCommandVisibility(PrintingSystemCommand.ClosePreview, DevExpress.XtraPrinting.CommandVisibility.None);
            fPrintBarManager = CreatePrintBarManager(printControl);
        }

        protected XtraReport fReport;
        protected string fileName = "";

        public virtual void Activate() {
            System.ComponentModel.DXDisplayNameAttribute.UseResourceManager = true;
            Report = CreateReport();	
            File.Delete(fileName);
        }
        protected virtual XtraReport CreateReport() {
            return null;
        }

        public XtraReport Report { 
            get { return fReport; } 
            set {
                if (fReport != value) {
                    if (fReport != null)
                        fReport.Dispose();
                    fReport = value;
                    if (fReport == null) 
                        return;
                    printingSystem.ClearContent();
                    Invalidate();
                    Update();
                    fileName = ReportsHelper.GetReportPath(fReport, "repx");
                    //fReport.PrintingSystem = printingSystem;

                    fReport.CreateDocument(true);
                } 
            }
        }

        protected PrintBarManager CreatePrintBarManager(PrintControl pc)
        {
            PrintBarManager printBarManager = new PrintBarManager();
            printBarManager.Form = printControl;
            printBarManager.Initialize(pc);
            printBarManager.MainMenu.Visible = false;
            printBarManager.AllowCustomization = false;
            return printBarManager;
        }
        private void ShowDesignerForm(Form designForm, Form parentForm)
        {
            designForm.MinimumSize = parentForm.MinimumSize;
            if (parentForm.WindowState == FormWindowState.Normal)
                designForm.Bounds = parentForm.Bounds;
            designForm.WindowState = parentForm.WindowState;
            parentForm.Visible = false;
            designForm.ShowDialog();
            parentForm.Visible = true;
        }
        protected virtual void InitializeControls()
        {
        }
        public void ShowDesigner()
        {
            string saveFileName = ReportsHelper.GetReportPath(fReport, "sav");
            fReport.PrintingSystem.ExecCommand(PrintingSystemCommand.StopPageBuilding);
            fReport.SaveLayout(saveFileName);
            using (XtraReport newReport = XtraReport.FromFile(saveFileName, true))
            {
                XRDesignFormExBase designForm = new CustomDesignForm();
                newReport.DataSource = fReport.DataSource;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                ShowDesignerForm(designForm, this.FindForm());
                if (designForm.FileName != fileName && File.Exists(designForm.FileName))
                    File.Copy(designForm.FileName, fileName, true);

                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }
            if (File.Exists(fileName))
            {
                fReport.LoadLayout(fileName);
                File.Delete(fileName);
                fReport.CreateDocument(true);
            }

            ShowParameters();
            File.Delete(saveFileName);
            InitializeControls();
        }

        protected void ShowParameters()
        {
            this.printingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
        }
    }

    public class ReportsHelper
    {
        public const int ICC_USEREX_CLASSES = 0x00000200;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class INITCOMMONCONTROLSEX
        {
            public int dwSize = 8; //ndirect.DllLib.sizeOf(this);
            public int dwICC;
        }
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string libname);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static string GetReportPath(DevExpress.XtraReports.UI.XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
             string repName = fReport.Name;
            if (repName.Length == 0) repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName, repName + "." + ext);
        }
        public static Image LoadImage(string name)
        {
            Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XtraReportsDemos." + name));
            bmp.MakeTransparent(Color.Magenta);
            return bmp;
        }
        /*
        public static void PatchConnections(string startupPath) {
            string ignore = global::XtraReportsDemos.Properties.Settings.Default.nwindConnectionString;
            ignore = global::XtraReportsDemos.Properties.Settings.Default.countriesDBConnectionString;
            ignore = global::XtraReportsDemos.Properties.Settings.Default.CarsDBConnectionString;
            ignore = global::XtraReportsDemos.Properties.Settings.Default.StyleSheetLavenderPath;
            ignore = global::XtraReportsDemos.Properties.Settings.Default.BiolifeTxtPath;

            string[] connections = new String[] { "nwindConnectionString", "countriesDBConnectionString", "CarsDBConnectionString" };
            foreach(string s in connections)
                PatchConnection(s, startupPath);
			string[] pathes = new String[] { "StyleSheetLavenderPath", "BiolifeTxtPath" };
            foreach(string s in pathes)
                PatchPath(s, startupPath);
        }
        static void PatchPath(string propertyName, string startupPath) {
            string path = (string)global::XtraReportsDemos.Properties.Settings.Default[propertyName];
            string fileName = System.IO.Path.GetFileName(path);
            string newPath = DevExpress.Utils.FilesHelper.FindingFileName(startupPath, "Data\\" + fileName, false);
            if(System.IO.File.Exists(newPath))
                global::XtraReportsDemos.Properties.Settings.Default[propertyName] = newPath;
        }
        static void PatchConnection(string propertyName, string startupPath) {
            string s = (string)global::XtraReportsDemos.Properties.Settings.Default[propertyName];
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(s, @".*Data Source=(?<dataSource>[^;]*)");
            System.Text.RegularExpressions.Group matchGroup = m.Groups["dataSource"];
            if(matchGroup.Success) {
                string fileName = System.IO.Path.GetFileName(matchGroup.Value);
                string path = DevExpress.Utils.FilesHelper.FindingFileName(startupPath, "Data\\" + fileName, false);
                if(File.Exists(path))
                    global::XtraReportsDemos.Properties.Settings.Default[propertyName] = s.Replace(matchGroup.Value, path);
            }
        }
         */
    }
}
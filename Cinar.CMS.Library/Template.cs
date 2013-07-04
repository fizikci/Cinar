using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library
{
    [DefaultData(ColumnList = "FileName, HTMLCode", ValueList = @"'Default.aspx','<!DOCTYPE html>
<html>
<head>
$=this.HeadSection$
</head>
<body>
    <div id=""page"">
        <div id=""Header"" class=""Region"">$=this.Header$</div>
        <div id=""Content"" class=""Region"">$=this.Content$</div>
        <div id=""Footer"" class=""Region"">$=this.Footer$</div>
    </div>
</body>
</html>'")]
    public class Template : BaseEntity
    {
        public static string defaultTemplate = @"<!DOCTYPE html>
<html>
<head>
$=this.HeadSection$
</head>
<body>
    <div id=""page"">
        <div id=""Header"" class=""Region"">$=this.Header$</div>
        <div id=""Content"" class=""Region"">$=this.Content$</div>
        <div id=""Footer"" class=""Region"">$=this.Footer$</div>
    </div>
</body>
</html>";

        private string fileName;
        [ColumnDetail(Length = 50)]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string htmlCode;
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        public string HTMLCode
        {
            get {
                if (string.IsNullOrEmpty(htmlCode))
                    htmlCode = defaultTemplate;
                return htmlCode; 
            }
            set { htmlCode = value; }
        }

    }

}

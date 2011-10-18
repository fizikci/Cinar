using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library
{
    [DefaultData(ColumnList = "FileName, HTMLCode", ValueList = @"'Main.aspx','<html>
<head>
$=this.HeadSection$
</head>
<body>
    <div id=""page"">
        <div id=""Header"" class=""Region Header"">$=this.Header$</div>
        <div id=""Content"" class=""Region Content"">$=this.Content$</div>
        <div id=""Footer"" class=""Region Footer"">$=this.Footer$</div>
    </div>
</body>
</html>'")]
    public class Template : BaseEntity
    {
        public static string defaultTemplate = @"<html>
<head>
$=this.HeadSection$
</head>
<body>
    <div id=""page"">
        <div id=""Header"" class=""Region Header"">$=this.Header$</div>
        <div id=""Content"" class=""Region Content"">$=this.Content$</div>
        <div id=""Footer"" class=""Region Footer"">$=this.Footer$</div>
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

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
    <div class=""navbar navbar-inverse navbar-fixed-top"">
      <div id=""topNav"" class=""Region container"">$=this.topNav$</div>
    </div>
    
    <div id=""jumbo"" class=""Region container"">
        $=this.jumbo$
    </div>
    
    <div id=""content"" class=""Region container"">
        $=this.content$
    </div>
    
    <footer id=""footer"" class=""Region container"">
        $=this.footer$
    </footer>
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
    <div class=""navbar navbar-inverse navbar-fixed-top"">
      <div id=""topNav"" class=""Region container"">$=this.topNav$</div>
    </div>
    
    <div id=""jumbo"" class=""Region container"">
        $=this.jumbo$
    </div>
    
    <div id=""content"" class=""Region container"">
        $=this.content$
    </div>
    
    <footer id=""footer"" class=""Region container"">
        $=this.footer$
    </footer>
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

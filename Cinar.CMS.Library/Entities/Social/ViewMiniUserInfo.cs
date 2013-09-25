using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    [TableDetail(IsView = true, ViewSQL = "select Id, concat(Name,' ',Surname) as FullName, Nick, Avatar, About from User")]
    public class ViewMiniUserInfo : DatabaseEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nick { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
    }
}

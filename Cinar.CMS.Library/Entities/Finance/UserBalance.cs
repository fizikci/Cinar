using System;
using Cinar.Database;
using System.Text.RegularExpressions;
using System.Data;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, InsertDate, InsertUserId, USD, EUR, TRY from UserBalance")]
    public class UserBalance : BaseEntity
    {
        public int USD { get; set; }
        public int EUR { get; set; }
        public int TRY { get; set; }
    }

}

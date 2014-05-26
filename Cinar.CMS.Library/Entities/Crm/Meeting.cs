using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Meeting.Id as [Meeting.Id], Meeting.MeetingType as [Meeting.MeetingType] from Meeting", QueryOrderBy = "Meeting.Id desc")]
    public class Meeting : BaseEntity
    {
        public int TaskId { get; set; }
        public int ContactId { get; set; }
        [ColumnDetail(ColumnType=DbType.VarChar, Length=16)]
        public MeetingTypes MeetingType { get; set; }

        public string Details { get; set; }
    }

    public enum MeetingTypes
    {
        FaceToFace,
        PhoneCall,
        Email
    }
}

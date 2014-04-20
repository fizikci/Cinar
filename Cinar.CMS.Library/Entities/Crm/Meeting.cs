using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class Meeting : BaseEntity
    {
        public int TaskId { get; set; }
        public int ContactId { get; set; }
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

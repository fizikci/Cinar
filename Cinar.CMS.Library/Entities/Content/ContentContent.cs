using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false)]
    public class ContentContent : BaseEntity
    {
        private int parentContentId;
        [ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int ParentContentId
        {
            get { return parentContentId; }
            set { parentContentId = value; }
        }

        private Content _parentContent;
        [XmlIgnore]
        public Content ParentContent
        {
            get
            {
                if (_parentContent == null)
                    _parentContent = (Content)Provider.Database.Read(typeof(Content), this.parentContentId);
                return _parentContent;
            }
        }

        private int childContentId;
        [ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int ChildContentId
        {
            get { return childContentId; }
            set { childContentId = value; }
        }

        private Content _childContent;
        [XmlIgnore]
        public Content ChildContent
        {
            get
            {
                if (_childContent == null)
                    _childContent = (Content)Provider.Database.Read(typeof(Content), this.childContentId);
                return _childContent;
            }
        }

    }

}

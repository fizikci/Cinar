using System;
using Cinar.Database;
using System.Drawing;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public abstract class NamedEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Picture2 { get; set; }

    }
}

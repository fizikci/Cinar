using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.WinUI
{
    public class EditFormAttribute : Attribute
    {
        public string RequiredRight { get; set; }
        public string DisplayName { get; set; }
        public string CategoryName { get; set; }
        public Type EntityType { get; set; }
        public Type FormType { get; set; }
        public string ImageKey { get; set; }

        public EditFormAttribute()
        {
        }
    }
}
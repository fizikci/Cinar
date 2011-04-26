using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.DBTools.Controls
{
    public interface IEditor
    {
        bool Modified { get; }

        bool Save();
    }
}

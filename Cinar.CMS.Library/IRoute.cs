using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library
{
    public interface IRoute
    {
        string WriteUrl(string url);
        string ReWriteUrl(string url);
    }
}

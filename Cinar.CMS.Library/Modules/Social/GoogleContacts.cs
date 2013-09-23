using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Contacts;
using Google.GData.Apps;
using Google.GData.Client;
using Cinar.CMS.Library.Entities;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class LastPrivateMessages : StaticHtml
    {
        public LastPrivateMessages()
        {
            this.InnerHtml = @"";

            this.TopHtml = "<h1>$=Provider.TR(\"Mesajlaşma\")$</h1>";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Entities.Workflows
{
    public enum WFS_AracTalep
    {
        New,
        Canceled,
        Confirmed
    }

    public class WFAracTalep : WorkflowEntity<WFS_AracTalep>
    {
        public string Adres { get; set; }
        public string Saat { get; set; }
    }
}

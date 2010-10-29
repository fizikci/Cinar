using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cinar.Entities
{
    public class UsageReport : BaseEntity
    {
        private string usageType1 = "";
        public virtual string UsageType1
        {
            get { return usageType1; }
            set { usageType1 = value; }
        }

        private string usageType2 = "";
        public virtual string UsageType2
        {
            get { return usageType2; }
            set { usageType2 = value; }
        }

        private string usageType3 = "";
        public virtual string UsageType3
        {
            get { return usageType3; }
            set { usageType3 = value; }
        }

        private int intVal = 0;
        public virtual int IntVal
        {
            get { return intVal; }
            set { intVal = value; }
        }
    }

    public class UsageReportFields
    {
        public const string IntVal = "IntVal";
        public const string UsageType1 = "UsageType1";
        public const string UsageType2 = "UsageType2";
        public const string UsageType3 = "UsageType3";
    }
}


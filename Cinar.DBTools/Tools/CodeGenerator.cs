using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.DBTools.Tools
{
    [Serializable]
    public class CodeGenerator
    {
        public CodeGenerator()
        {
            this.Templates = new List<Template>();
        }

        public List<Template> Templates { get; set; }
    }

    [Serializable]
    public class Template
    {
        public string Name { get; set; }
        public string FileNameFormat { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
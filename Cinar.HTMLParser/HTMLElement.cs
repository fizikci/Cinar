using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.HTMLParser
{
    public class HTMLElement
    {
        public HTMLElement()
        {
            this.Attributes = new Dictionary<string, string>();
            this.Style = new Dictionary<string, string>();
            this.ChildNodes = new List<HTMLElement>();
        }

        public string TagName { get; internal set; }
        public Dictionary<string,string> Attributes { get; internal set; }
        public Dictionary<string, string> Style { get; internal set; }

        public List<HTMLElement> ChildNodes { get; internal set; }
        public HTMLElement Parent { get; internal set; }

        private string innerText;
        public string InnerText 
        {
            get
            {
                if (innerText == null)
                {
                    string res = "";
                    foreach (HTMLElement elm in this.ChildNodes)
                        res += elm.InnerText;
                    return res;
                }
                else
                    return innerText + Environment.NewLine;
            }
            set
            {
                if (this.TagName == "InnerText")
                    this.innerText = value;
                else
                {
                    this.ChildNodes.Clear();
                    HTMLElement elm = new HTMLElement();
                    elm.TagName = "InnerText";
                    elm.InnerText = value;
                    elm.Parent = this;
                    this.ChildNodes.Add(elm);
                }
            }
        }
    }
}

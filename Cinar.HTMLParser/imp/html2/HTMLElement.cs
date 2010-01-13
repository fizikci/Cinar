using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom.html2;

namespace Cinar.HTMLParser.imp.html2
{
    public class HTMLElement : Element, IHTMLElement
    {
        private string _id;

        private string _title;

        private string _lang;

        private string _dir;

        private string _className;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string lang
        {
            get { return _lang; }
            set { _lang = value; }
        }

        public string dir
        {
            get { return _dir; }
            set { _dir = value; }
        }

        public string className
        {
            get { return _className; }
            set { _className = value; }
        }
    }
}

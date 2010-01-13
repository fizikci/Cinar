﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom.html2;

namespace Cinar.HTMLParser.imp.html2
{
    public class HTMLDivElement : HTMLElement, IHTMLDivElement
    {
        private string _align;
        public string align
        {
            get { return _align; }
            set 
            { 
                _align = value;
                if (domReady) {
                    // if called by JavaScript code after domReady, do repainting
                }
            }
        }
    }
}

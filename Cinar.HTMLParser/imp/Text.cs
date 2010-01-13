using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class Text : CharacterData, IText
    {
        public IText splitText(int offset)
        {
            if (this.length > offset)
            {
                string rest = this.data.Substring(offset);
                this.deleteData(offset, rest.Length);
                return (IText)this.parentNode.appendChild(this.ownerDocument.createTextNode(rest));
            }
            else
                return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class Text : CharacterData
    {
        public Text splitText(int offset)
        {
            if (this.length > offset)
            {
                string rest = this.data.Substring(offset);
                this.deleteData(offset, rest.Length);
                return (Text)this.parentNode.appendChild(this.ownerDocument.createTextNode(rest));
            }
            else
                return null;
        }
    }
}

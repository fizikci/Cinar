using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class Attr : Node, IAttr
    {
        #region IAttr Members

        public string name
        {
            get { throw new NotImplementedException(); }
        }

        public bool specified
        {
            get { throw new NotImplementedException(); }
        }

        public string value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IElement ownerElement
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}

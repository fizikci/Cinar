using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class DocumentType : Node, IDocumentType
    {
        public string name
        {
            get { throw new NotImplementedException(); }
        }

        public INamedNodeMap entities
        {
            get { throw new NotImplementedException(); }
        }

        public INamedNodeMap notations
        {
            get { throw new NotImplementedException(); }
        }

        public string publicId
        {
            get { throw new NotImplementedException(); }
        }

        public string systemId
        {
            get { throw new NotImplementedException(); }
        }

        public string internalSubset
        {
            get { throw new NotImplementedException(); }
        }
    }
}

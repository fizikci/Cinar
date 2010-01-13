using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class DOMImplementation : IDOMImplementation
    {
        public bool hasFeature(string feature, string version)
        {
            throw new NotImplementedException();
        }

        public IDocumentType createDocumentType(string qualifiedName, string publicId, string systemId)
        {
            throw new NotImplementedException();
        }

        public IDocument createDocument(string namespaceURI, string qualifiedName, IDocumentType doctype)
        {
            throw new NotImplementedException();
        }
    }
}

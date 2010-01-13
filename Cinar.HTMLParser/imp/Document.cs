using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class Document : Node, IDocument
    {
        public IDocumentType doctype
        {
            get { throw new NotImplementedException(); }
        }

        public IDOMImplementation implementation
        {
            get { throw new NotImplementedException(); }
        }

        public IElement documentElement
        {
            get { throw new NotImplementedException(); }
        }

        public IElement createElement(string tagName)
        {
            throw new NotImplementedException();
        }

        public IDocumentFragment createDocumentFragment()
        {
            throw new NotImplementedException();
        }

        public IText createTextNode(string data)
        {
            throw new NotImplementedException();
        }

        public IComment createComment(string data)
        {
            throw new NotImplementedException();
        }

        public ICDATASection createCDATASection(string data)
        {
            throw new NotImplementedException();
        }

        public IProcessingInstruction createProcessingInstruction(string target, string data)
        {
            throw new NotImplementedException();
        }

        public IAttr createAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public IEntityReference createEntityReference(string name)
        {
            throw new NotImplementedException();
        }

        public INodeList getElementsByTagName(string tagname)
        {
            throw new NotImplementedException();
        }

        public INode importNode(INode importedNode, bool deep)
        {
            throw new NotImplementedException();
        }

        public IElement createElementNS(string namespaceURI, string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public IAttr createAttributeNS(string namespaceURI, string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public INodeList getElementsByTagNameNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        public IElement getElementById(string elementId)
        {
            throw new NotImplementedException();
        }
    }
}

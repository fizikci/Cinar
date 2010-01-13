using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class Element : Node, IElement
    {
        public string tagName
        {
            get { throw new NotImplementedException(); }
        }

        public string getAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public void setAttribute(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void removeAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public IAttr getAttributeNode(string name)
        {
            throw new NotImplementedException();
        }

        public IAttr setAttributeNode(IAttr newAttr)
        {
            throw new NotImplementedException();
        }

        public IAttr removeAttributeNode(IAttr oldAttr)
        {
            throw new NotImplementedException();
        }

        public INodeList getElementsByTagName(string name)
        {
            throw new NotImplementedException();
        }

        public string getAttributeNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        public void setAttributeNS(string namespaceURI, string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }

        public void removeAttributeNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        public IAttr getAttributeNodeNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        public IAttr setAttributeNodeNS(IAttr newAttr)
        {
            throw new NotImplementedException();
        }

        public INodeList getElementsByTagNameNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        public bool hasAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public bool hasAttributeNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }
    }
}

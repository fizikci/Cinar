using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class Node : INode
    {
        public string nodeName
        {
            get { throw new NotImplementedException(); }
        }

        public string nodeValue
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public NodeType nodeType
        {
            get { throw new NotImplementedException(); }
        }

        public INode parentNode
        {
            get { throw new NotImplementedException(); }
        }

        public INodeList childNodes
        {
            get { throw new NotImplementedException(); }
        }

        public INode firstChild
        {
            get { throw new NotImplementedException(); }
        }

        public INode lastChild
        {
            get { throw new NotImplementedException(); }
        }

        public INode previousSibling
        {
            get { throw new NotImplementedException(); }
        }

        public INode nextSibling
        {
            get { throw new NotImplementedException(); }
        }

        public INamedNodeMap attributes
        {
            get { throw new NotImplementedException(); }
        }

        public IDocument ownerDocument
        {
            get { throw new NotImplementedException(); }
        }

        public INode insertBefore(INode newChild, INode refChild)
        {
            throw new NotImplementedException();
        }

        public INode replaceChild(INode newChild, INode oldChild)
        {
            throw new NotImplementedException();
        }

        public INode removeChild(INode oldChild)
        {
            throw new NotImplementedException();
        }

        public INode appendChild(INode newChild)
        {
            throw new NotImplementedException();
        }

        public bool hasChildNodes()
        {
            throw new NotImplementedException();
        }

        public INode cloneNode(bool deep)
        {
            throw new NotImplementedException();
        }

        public void normalize()
        {
            throw new NotImplementedException();
        }

        public bool isSupported(string feature, string version)
        {
            throw new NotImplementedException();
        }

        public string getNamespaceURI()
        {
            throw new NotImplementedException();
        }

        public string prefix
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string getLocalName()
        {
            throw new NotImplementedException();
        }

        public bool hasAttributes()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class NamedNodeMap : List<Node>
    {
        /// <summary>Retrieves a node specified by name.</summary>
        /// <param name="name">The nodeName of a node to retrieve.</param>
        /// <returns>A INode (of any type) with the specified 
        ///   nodeName, or null if it does not identify 
        ///   any node in this map.</returns>
        public Node getNamedItem(string name)
        {
            return this.Find(n => n.nodeName == name);
        }

        /// <summary>Adds a node using its nodeName attribute. If a node with 
        /// that name is already present in this map, it is replaced by the new 
        /// one.
        /// As the nodeName attribute is used to derive the name 
        /// which the node must be stored under, multiple nodes of certain types 
        /// (those that have a "special" string value) cannot be stored as the 
        /// names would clash. This is seen as preferable to allowing nodes to be 
        /// aliased.</summary>
        /// <param name="arg">A node to store in this map. The node will later be 
        ///   accessible using the value of its nodeName attribute.</param>
        /// <returns>If the new INode replaces an existing node the 
        ///   replaced INode is returned, otherwise null 
        ///   is returned.</returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if arg was created from a 
        ///   different document than the one that created this map.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this map is readonly.
        ///   INUSE_ATTRIBUTE_ERR: Raised if arg is an 
        ///   Attr that is already an attribute of another 
        ///   IElement object. The DOM user must explicitly clone 
        ///   Attr nodes to re-use them in other elements.
        /// </exception>
        public Node setNamedItem(Node arg)
        {
            Node node = getNamedItem(arg.nodeName);
            if (node == null)
            {
                this.Add(arg);
            }
            else
            {
                this[this.IndexOf(node)] = arg;
            }

            return node;
        }

        /// <summary>Removes a node specified by name. When this map contains the attributes 
        /// attached to an element, if the removed attribute is known to have a 
        /// default value, an attribute immediately appears containing the 
        /// default value as well as the corresponding namespace URI, local name, 
        /// and prefix when applicable.</summary>
        /// <param name="name">The nodeName of the node to remove.</param>
        /// <returns>The node removed from this map if a node with such a name 
        ///   exists.</returns>
        /// <exception cref="DOMException">
        ///   NOT_FOUND_ERR: Raised if there is no node named name in 
        ///   this map.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this map is readonly.
        /// </exception>
        public Node removeNamedItem(string name)
        {
            Node node = getNamedItem(name);
            if(node==null)
                throw ErrorMessages.Get(DOMExceptionCodes.NOT_FOUND_ERR);
            this.Remove(node);
            return node;
        }

        /// <summary>Returns the indexth item in the map. If index 
        /// is greater than or equal to the number of nodes in this map, this 
        /// returns null.</summary>
        /// <param name="indexIndex"> into this map.</param>
        /// <returns>The node at the indexth position in the map, or 
        ///   null if that is not a valid index.
        /// </returns>
        public Node item(int index)
        {
            if (index < 0 || index >= this.Count)
                return null;
            return this[index];
        }

        /// <summary>The number of nodes in this map. The range of valid child node indices 
        /// is 0 to length-1 inclusive. 
        /// </summary>
        public int length
        {
            get { return this.Count; }
        }

        /// <summary>Retrieves a node specified by local name and namespace URI. HTML-only 
        /// DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the node to retrieve.</param>
        /// <param name="localName">The local name of the node to retrieve.</param>
        /// <returns>A INode (of any type) with the specified local 
        ///   name and namespace URI, or null if they do not 
        ///   identify any node in this map.</returns>
        public Node getNamedItemNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Adds a node using its namespaceURI and 
        /// localName. If a node with that namespace URI and that 
        /// local name is already present in this map, it is replaced by the new 
        /// one.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="arg">A node to store in this map. The node will later be 
        ///   accessible using the value of its namespaceURI and 
        ///   localName attributes.</param>
        /// <returns>If the new INode replaces an existing node the 
        ///   replaced INode is returned, otherwise null 
        ///   is returned.</returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if arg was created from a 
        ///   different document than the one that created this map.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this map is readonly.
        ///   INUSE_ATTRIBUTE_ERR: Raised if arg is an 
        ///   Attr that is already an attribute of another 
        ///   IElement object. The DOM user must explicitly clone 
        ///   Attr nodes to re-use them in other elements.</exception>
        public Node setNamedItemNS(Node arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>Removes a node specified by local name and namespace URI. A removed 
        /// attribute may be known to have a default value when this map contains 
        /// the attributes attached to an element, as returned by the attributes 
        /// attribute of the INode interface. If so, an attribute 
        /// immediately appears containing the default value as well as the 
        /// corresponding namespace URI, local name, and prefix when applicable.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the node to remove.</param>
        /// <param name="localName">The local name of the node to remove.</param>
        /// <returns>The node removed from this map if a node with such a local 
        ///   name and namespace URI exists.</returns>
        /// <exception cref="DOMException">
        ///   NOT_FOUND_ERR: Raised if there is no node with the specified 
        ///   namespaceURI and localName in this map.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this map is readonly.</exception>
        public Node removeNamedItemNS(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }
    }
}

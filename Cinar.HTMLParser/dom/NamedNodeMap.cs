/*
 * Copyright (c) 2000 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

namespace org.w3c.dom
{

    /// <summary>Objects implementing the NamedNodeMap interface are used to 
    /// represent collections of nodes that can be accessed by name. Note that 
    /// NamedNodeMap does not inherit from NodeList; 
    /// NamedNodeMaps are not maintained in any particular order. 
    /// Objects contained in an object implementing NamedNodeMap may 
    /// also be accessed by an ordinal index, but this is simply to allow 
    /// convenient enumeration of the contents of a NamedNodeMap, 
    /// and does not imply that the DOM specifies an order to these Nodes. 
    /// NamedNodeMap objects in the DOM are live.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface INamedNodeMap
    {
        /// <summary>Retrieves a node specified by name.</summary>
        /// <param name="name">The nodeName of a node to retrieve.</param>
        /// <returns>A INode (of any type) with the specified 
        ///   nodeName, or null if it does not identify 
        ///   any node in this map.</returns>
        INode getNamedItem(string name);

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
        INode setNamedItem(INode arg)
                                 ; // throws DOMException;

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
        INode removeNamedItem(string name)
                                    ; // throws DOMException;

        /// <summary>Returns the indexth item in the map. If index 
        /// is greater than or equal to the number of nodes in this map, this 
        /// returns null.</summary>
        /// <param name="indexIndex"> into this map.</param>
        /// <returns>The node at the indexth position in the map, or 
        ///   null if that is not a valid index.
        /// </returns>
        INode item(int index);

        /// <summary>The number of nodes in this map. The range of valid child node indices 
        /// is 0 to length-1 inclusive. 
        /// </summary>
        int length { get; }

        /// <summary>Retrieves a node specified by local name and namespace URI. HTML-only 
        /// DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the node to retrieve.</param>
        /// <param name="localName">The local name of the node to retrieve.</param>
        /// <returns>A INode (of any type) with the specified local 
        ///   name and namespace URI, or null if they do not 
        ///   identify any node in this map.</returns>
        INode getNamedItemNS(string namespaceURI,
                                   string localName);

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
        INode setNamedItemNS(INode arg)
                                   ; // throws DOMException;

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
        INode removeNamedItemNS(string namespaceURI,
                                      string localName)
                                      ; // throws DOMException;

    }
}
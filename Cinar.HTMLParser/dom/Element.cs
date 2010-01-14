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

    /// <summary>The IElement interface represents an element in an HTML or XML 
    /// document. Elements may have attributes associated with them; since the 
    /// IElement interface inherits from INode, the 
    /// generic INode interface attribute attributes may 
    /// be used to retrieve the set of all attributes for an element. There are 
    /// methods on the IElement interface to retrieve either an 
    /// Attr object by name or an attribute value by name. In XML, 
    /// where an attribute value may contain entity references, an 
    /// Attr object should be retrieved to examine the possibly 
    /// fairly complex sub-tree representing the attribute value. On the other 
    /// hand, in HTML, where all attributes have simple string values, methods to 
    /// directly access an attribute value can safely be used as a convenience.In 
    /// DOM Level 2, the method normalize is inherited from the 
    /// INode interface where it was moved.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IElement : INode
    {
        /// <summary>The name of the element. For example, in: 
        /// <pre> &lt;elementExample 
        /// id="demo"&gt; ... &lt;/elementExample&gt; , </pre>
        ///  tagName has 
        /// the value "elementExample". Note that this is 
        /// case-preserving in XML, as are all of the operations of the DOM. The 
        /// HTML DOM returns the tagName of an HTML element in the 
        /// canonical uppercase form, regardless of the case in the source HTML 
        /// document. 
        /// </summary>
        string tagName { get; }

        /// <summary>Retrieves an attribute value by name.</summary>
        /// <param name="name">The name of the attribute to retrieve.</param>
        /// <returns>The Attr value as a string, or the empty string 
        ///   if that attribute does not have a specified or default value.
        /// </returns>
        string getAttribute(string name);

        /// <summary>Adds a new attribute. If an attribute with that name is already present 
        /// in the element, its value is changed to be that of the value 
        /// parameter. This value is a simple string; it is not parsed as it is 
        /// being set. So any markup (such as syntax to be recognized as an 
        /// entity reference) is treated as literal text, and needs to be 
        /// appropriately escaped by the implementation when it is written out. 
        /// In order to assign an attribute value that contains entity 
        /// references, the user must create an Attr node plus any 
        /// Text and EntityReference nodes, build the 
        /// appropriate subtree, and use setAttributeNode to assign 
        /// it as the value of an attribute.
        /// To set an attribute with a qualified name and namespace URI, use 
        /// the setAttributeNS method.</summary>
        /// <param name="name">The name of the attribute to create or alter.</param>
        /// <param name="valueValue"> to set in string form.</param>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified name contains an 
        ///   illegal character.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void setAttribute(string name, string value); // throws DOMException;

        /// <summary>Removes an attribute by name. If the removed attribute is known to have 
        /// a default value, an attribute immediately appears containing the 
        /// default value as well as the corresponding namespace URI, local name, 
        /// and prefix when applicable.
        /// To remove an attribute by local name and namespace URI, use the 
        /// removeAttributeNS method.</summary>
        /// <param name="name">The name of the attribute to remove.</param>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void removeAttribute(string name); // throws DOMException;

        /// <summary>Retrieves an attribute node by name.
        /// To retrieve an attribute node by qualified name and namespace URI, 
        /// use the getAttributeNodeNS method.</summary>
        /// <param name="name">The name (nodeName) of the attribute to 
        ///   retrieve.</param>
        /// <returns>The Attr node with the specified name (
        ///   nodeName) or null if there is no such 
        ///   attribute.</returns>
        IAttr getAttributeNode(string name);

        /// <summary>Adds a new attribute node. If an attribute with that name (
        /// nodeName) is already present in the element, it is 
        /// replaced by the new one.
        /// To add a new attribute node with a qualified name and namespace 
        /// URI, use the setAttributeNodeNS method.</summary>
        /// <param name="newAttr">The Attr node to add to the attribute list.</param>
        /// <returns>If the newAttr attribute replaces an existing 
        ///   attribute, the replaced Attr node is returned, 
        ///   otherwise null is returned.</returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if newAttr was created from a 
        ///   different document than the one that created the element.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   INUSE_ATTRIBUTE_ERR: Raised if newAttr is already an 
        ///   attribute of another IElement object. The DOM user must 
        ///   explicitly clone Attr nodes to re-use them in other 
        ///   elements.
        /// </exception>
        IAttr setAttributeNode(IAttr newAttr); // throws DOMException;

        /// <summary>Removes the specified attribute node. If the removed Attr 
        /// has a default value it is immediately replaced. The replacing 
        /// attribute has the same namespace URI and local name, as well as the 
        /// original prefix, when applicable.</summary>
        /// <param name="oldAttr">The Attr node to remove from the attribute 
        ///   list.</param>
        /// <returns>The Attr node that was removed.</returns>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   NOT_FOUND_ERR: Raised if oldAttr is not an attribute 
        ///   of the element.
        /// </exception>
        IAttr removeAttributeNode(IAttr oldAttr); // throws DOMException;

        /// <summary>Returns a NodeList of all descendant Elements 
        /// with a given tag name, in the order in which they are encountered in 
        /// a preorder traversal of this IElement tree.</summary>
        /// <param name="name">The name of the tag to match on. The special value "*" 
        ///   matches all tags.</param>
        /// <returns>A list of matching IElement nodes.</returns>
        INodeList getElementsByTagName(string name);

        /// <summary>Retrieves an attribute value by local name and namespace URI. HTML-only 
        /// DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to retrieve.</param>
        /// <param name="localName">The local name of the attribute to retrieve.</param>
        /// <returns>The Attr value as a string, or the empty string 
        ///   if that attribute does not have a specified or default value.</returns>
        string getAttributeNS(string namespaceURI, string localName);

        /// <summary>Adds a new attribute. If an attribute with the same local name and 
        /// namespace URI is already present on the element, its prefix is 
        /// changed to be the prefix part of the qualifiedName, and 
        /// its value is changed to be the value parameter. This 
        /// value is a simple string; it is not parsed as it is being set. So any 
        /// markup (such as syntax to be recognized as an entity reference) is 
        /// treated as literal text, and needs to be appropriately escaped by the 
        /// implementation when it is written out. In order to assign an 
        /// attribute value that contains entity references, the user must create 
        /// an Attr node plus any Text and 
        /// EntityReference nodes, build the appropriate subtree, 
        /// and use setAttributeNodeNS or 
        /// setAttributeNode to assign it as the value of an 
        /// attribute.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to create or 
        ///   alter.</param>
        /// <param name="qualifiedName">The qualified name of the attribute to create or 
        ///   alter.</param>
        /// <param name="value">The value to set in string form.</param>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified qualified name 
        ///   contains an illegal character.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   NAMESPACE_ERR: Raised if the qualifiedName is 
        ///   malformed, if the qualifiedName has a prefix and the 
        ///   namespaceURI is null, if the 
        ///   qualifiedName has a prefix that is "xml" and the 
        ///   namespaceURI is different from "
        ///   http://www.w3.org/XML/1998/namespace", or if the 
        ///   qualifiedName is "xmlns" and the 
        ///   namespaceURI is different from "
        ///   http://www.w3.org/2000/xmlns/".
        /// </exception>
        void setAttributeNS(string namespaceURI, string qualifiedName, string value); // throws DOMException;

        /// <summary>Removes an attribute by local name and namespace URI. If the removed 
        /// attribute has a default value it is immediately replaced. The 
        /// replacing attribute has the same namespace URI and local name, as 
        /// well as the original prefix.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to remove.</param>
        /// <param name="localName">The local name of the attribute to remove.</param>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void removeAttributeNS(string namespaceURI,
                                      string localName)
                                      ; // throws DOMException;

        /// <summary>Retrieves an Attr node by local name and namespace URI. 
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to retrieve.</param>
        /// <param name="localName">The local name of the attribute to retrieve.</param>
        /// <returns>The Attr node with the specified attribute local 
        ///   name and namespace URI or null if there is no such 
        ///   attribute.</returns>
        IAttr getAttributeNodeNS(string namespaceURI,
                                       string localName);

        /// <summary>Adds a new attribute. If an attribute with that local name and that 
        /// namespace URI is already present in the element, it is replaced by 
        /// the new one.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="newAttr">The Attr node to add to the attribute list.</param>
        /// <returns>If the newAttr attribute replaces an existing 
        ///   attribute with the same local name and namespace URI, the replaced 
        ///   Attr node is returned, otherwise null is 
        ///   returned.</returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if newAttr was created from a 
        ///   different document than the one that created the element.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   INUSE_ATTRIBUTE_ERR: Raised if newAttr is already an 
        ///   attribute of another IElement object. The DOM user must 
        ///   explicitly clone Attr nodes to re-use them in other 
        ///   elements.</exception>
        IAttr setAttributeNodeNS(IAttr newAttr)
                                       ; // throws DOMException;

        /// <summary>Returns a NodeList of all the descendant 
        /// Elements with a given local name and namespace URI in 
        /// the order in which they are encountered in a preorder traversal of 
        /// this IElement tree.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the elements to match on. The 
        ///   special value "*" matches all namespaces.</param>
        /// <param name="localName">The local name of the elements to match on. The 
        ///   special value "*" matches all local names.</param>
        /// <returns>A new NodeList object containing all the matched 
        ///   Elements.</returns>
        INodeList getElementsByTagNameNS(string namespaceURI,
                                               string localName);

        /// <summary>Returns true when an attribute with a given name is 
        /// specified on this element or has a default value, false 
        /// otherwise.</summary>
        /// <param name="name">The name of the attribute to look for.</param>
        /// <returns>true if an attribute with the given name is 
        ///   specified on this element or has a default value, false
        ///    otherwise.</returns>
        bool hasAttribute(string name);

        /// <summary>Returns true when an attribute with a given local name and 
        /// namespace URI is specified on this element or has a default value, 
        /// false otherwise. HTML-only DOM implementations do not 
        /// need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to look for.</param>
        /// <param name="localName">The local name of the attribute to look for.</param>
        /// <returns>true if an attribute with the given local name 
        ///   and namespace URI is specified or has a default value on this 
        ///   element, false otherwise.</returns>
        bool hasAttributeNS(string namespaceURI,
                                      string localName);

    }
}
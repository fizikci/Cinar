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

    /// <summary>The IDocument interface represents the entire HTML or XML 
    /// document. Conceptually, it is the root of the document tree, and provides 
    /// the primary access to the document's data.
    /// Since elements, text nodes, comments, processing instructions, etc. 
    /// cannot exist outside the context of a IDocument, the 
    /// IDocument interface also contains the factory methods needed 
    /// to create these objects. The INode objects created have a 
    /// ownerDocument attribute which associates them with the 
    /// IDocument within whose context they were created.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IDocument : INode
    {
        /// <summary>The IDocument Type Declaration (see DocumentType) 
        /// associated with this document. For HTML documents as well as XML 
        /// documents without a document type declaration this returns 
        /// null. The DOM Level 2 does not support editing the 
        /// IDocument Type Declaration. docType cannot be altered in 
        /// any way, including through the use of methods inherited from the 
        /// INode interface, such as insertNode or 
        /// removeNode.
        /// </summary>
        IDocumentType doctype { get; }

        /// <summary>The DOMImplementation object that handles this document. A 
        /// DOM application may use objects from multiple implementations.
        /// </summary>
        IDOMImplementation implementation { get; }

        /// <summary>This is a convenience attribute that allows direct access to the child 
        /// node that is the root element of the document. For HTML documents, 
        /// this is the element with the tagName "HTML".
        /// </summary>
        IElement documentElement { get; }

        /// <summary>Creates an element of the type specified. Note that the instance 
        /// returned implements the IElement interface, so attributes 
        /// can be specified directly on the returned object.
        /// In addition, if there are known attributes with default values, 
        /// Attr nodes representing them are automatically created 
        /// and attached to the element.
        /// To create an element with a qualified name and namespace URI, use 
        /// the createElementNS method.</summary>
        /// <param name="tagName">The name of the element type to instantiate. For XML, 
        ///   this is case-sensitive. For HTML, the tagName 
        ///   parameter may be provided in any case, but it must be mapped to the 
        ///   canonical uppercase form by the DOM implementation. </param>
        /// <returns>A new IElement object with the 
        ///   nodeName attribute set to tagName, and 
        ///   localName, prefix, and 
        ///   namespaceURI set to null.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified name contains an 
        ///   illegal character.
        /// </exception>
        IElement createElement(string tagName)
                                     ; // throws DOMException;

        /// <summary>Creates an empty DocumentFragment object. </summary>
        /// <returns>A new DocumentFragment.</returns>
        IDocumentFragment createDocumentFragment();

        /// <summary>Creates a Text node given the specified string.</summary>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new Text object.</returns>
        IText createTextNode(string data);

        /// <summary>Creates a Comment node given the specified string.</summary>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new Comment object.</returns>
        IComment createComment(string data);

        /// <summary>Creates a CDATASection node whose value is the specified 
        /// string.</summary>
        /// <param name="data">The data for the CDATASection contents.</param>
        /// <returns>The new CDATASection object.</returns>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if this document is an HTML document.
        /// </exception>
        ICDATASection createCDATASection(string data)
                                               ; // throws DOMException;

        /// <summary>Creates a ProcessingInstruction node given the specified 
        /// name and data strings.</summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new ProcessingInstruction object.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified target contains an 
        ///   illegal character.
        ///   NOT_SUPPORTED_ERR: Raised if this document is an HTML document.
        /// </exception>
        IProcessingInstruction createProcessingInstruction(string target,
                                                                 string data)
                                                                 ; // throws DOMException;

        /// <summary>Creates an Attr of the given name. Note that the 
        /// Attr instance can then be set on an IElement 
        /// using the setAttributeNode method. 
        /// To create an attribute with a qualified name and namespace URI, use 
        /// the createAttributeNS method.</summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>A new Attr object with the nodeName 
        ///   attribute set to name, and localName, 
        ///   prefix, and namespaceURI set to 
        ///   null. The value of the attribute is the empty string.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified name contains an 
        ///   illegal character.
        /// </exception>
        IAttr createAttribute(string name); // throws DOMException;

        /// <summary>Creates an EntityReference object. In addition, if the 
        /// referenced entity is known, the child list of the 
        /// EntityReference node is made the same as that of the 
        /// corresponding Entity node.If any descendant of the 
        /// Entity node has an unbound namespace prefix, the 
        /// corresponding descendant of the created EntityReference 
        /// node is also unbound; (its namespaceURI is 
        /// null). The DOM Level 2 does not support any mechanism to 
        /// resolve namespace prefixes.</summary>
        /// <param name="name">The name of the entity to reference. </param>
        /// <returns>The new EntityReference object.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified name contains an 
        ///   illegal character.
        ///   NOT_SUPPORTED_ERR: Raised if this document is an HTML document.
        /// </exception>
        IEntityReference createEntityReference(string name); // throws DOMException;

        /// <summary>Returns a NodeList of all the Elements with a 
        /// given tag name in the order in which they are encountered in a 
        /// preorder traversal of the IDocument tree.</summary>
        /// <param name="tagname">The name of the tag to match on. The special value "*" 
        ///   matches all tags.</param>
        /// <returns>A new NodeList object containing all the matched 
        ///   Elements.
        /// </returns>
        INodeList getElementsByTagName(string tagname);

        /// <summary>Imports a node from another document to this document. The returned 
        /// node has no parent; (parentNode is null). 
        /// The source node is not altered or removed from the original document; 
        /// this method creates a new copy of the source node.
        /// For all nodes, importing a node creates a node object owned by the 
        /// importing document, with attribute values identical to the source 
        /// node's nodeName and nodeType, plus the 
        /// attributes related to namespaces (prefix, 
        /// localName, and namespaceURI). As in the 
        /// cloneNode operation on a INode, the source 
        /// node is not altered.
        /// Additional information is copied as appropriate to the 
        /// nodeType, attempting to mirror the behavior expected if 
        /// a fragment of XML or HTML source was copied from one document to 
        /// another, recognizing that the two documents may have different DTDs 
        /// in the XML case. The following list describes the specifics for each 
        /// type of node. 
        /// <dl>
        /// <dt>ATTRIBUTE_NODE</dt>
        /// <dd>The ownerElement attribute 
        /// is set to null and the specified flag is 
        /// set to true on the generated Attr. The 
        /// descendants of the source Attr are recursively imported 
        /// and the resulting nodes reassembled to form the corresponding subtree.
        /// Note that the deep parameter has no effect on 
        /// Attr nodes; they always carry their children with them 
        /// when imported.</dd>
        /// <dt>DOCUMENT_FRAGMENT_NODE</dt>
        /// <dd>If the deep option 
        /// was set to true, the descendants of the source element 
        /// are recursively imported and the resulting nodes reassembled to form 
        /// the corresponding subtree. Otherwise, this simply generates an empty 
        /// DocumentFragment.</dd>
        /// <dt>DOCUMENT_NODE</dt>
        /// <dd>IDocument 
        /// nodes cannot be imported.</dd>
        /// <dt>DOCUMENT_TYPE_NODE</dt>
        /// <dd>DocumentType 
        /// nodes cannot be imported.</dd>
        /// <dt>ELEMENT_NODE</dt>
        /// <dd>Specified attribute nodes of the 
        /// source element are imported, and the generated Attr 
        /// nodes are attached to the generated IElement. Default 
        /// attributes are not copied, though if the document being imported into 
        /// defines default attributes for this element name, those are assigned. 
        /// If the importNode deep parameter was set to 
        /// true, the descendants of the source element are 
        /// recursively imported and the resulting nodes reassembled to form the 
        /// corresponding subtree.</dd>
        /// <dt>ENTITY_NODE</dt>
        /// <dd>Entity nodes can be 
        /// imported, however in the current release of the DOM the 
        /// DocumentType is readonly. Ability to add these imported 
        /// nodes to a DocumentType will be considered for addition 
        /// to a future release of the DOM.On import, the publicId, 
        /// systemId, and notationName attributes are 
        /// copied. If a deep import is requested, the descendants 
        /// of the the source Entity are recursively imported and 
        /// the resulting nodes reassembled to form the corresponding subtree.</dd>
        /// <dt>
        /// ENTITY_REFERENCE_NODE</dt>
        /// <dd>Only the EntityReference itself is 
        /// copied, even if a deep import is requested, since the 
        /// source and destination documents might have defined the entity 
        /// differently. If the document being imported into provides a 
        /// definition for this entity name, its value is assigned.</dd>
        /// <dt>NOTATION_NODE</dt>
        /// <dd>
        /// Notation nodes can be imported, however in the current 
        /// release of the DOM the DocumentType is readonly. Ability 
        /// to add these imported nodes to a DocumentType will be 
        /// considered for addition to a future release of the DOM.On import, the 
        /// publicId and systemId attributes are copied.
        /// Note that the deep parameter has no effect on 
        /// Notation nodes since they never have any children.</dd>
        /// <dt>
        /// PROCESSING_INSTRUCTION_NODE</dt>
        /// <dd>The imported node copies its 
        /// target and data values from those of the 
        /// source node.</dd>
        /// <dt>TEXT_NODE, CDATA_SECTION_NODE, COMMENT_NODE</dt>
        /// <dd>These three 
        /// types of nodes inheriting from CharacterData copy their 
        /// data and length attributes from those of 
        /// the source node.</dd>
        ///  
        /// <param name="importedNode">The node to import.
        /// <param name="deepIf"> true, recursively import the subtree under 
        ///   the specified node; if false, import only the node 
        ///   itself, as explained above. This has no effect on Attr
        ///   , EntityReference, and Notation nodes.
        /// <returns>The imported node that belongs to this IDocument.
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if the type of node being imported is not 
        ///   supported.
        /// </summary>
        INode importNode(INode importedNode, bool deep); // throws DOMException;

        /// <summary>Creates an element of the given qualified name and namespace URI. 
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the element to create.</param>
        /// <param name="qualifiedName">The qualified name of the element type to 
        ///   instantiate.</param>
        /// <returns>A new IElement object with the following 
        ///   attributes:AttributeValueINode.nodeName
        ///   qualifiedNameINode.namespaceURI
        ///   namespaceURIINode.prefixprefix, extracted 
        ///   from qualifiedName, or null if there is 
        ///   no prefixINode.localNamelocal name, extracted from 
        ///   qualifiedNameIElement.tagName
        ///   qualifiedName</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified qualified name 
        ///   contains an illegal character.
        ///   NAMESPACE_ERR: Raised if the qualifiedName is 
        ///   malformed, if the qualifiedName has a prefix and the 
        ///   namespaceURI is null, or if the 
        ///   qualifiedName has a prefix that is "xml" and the 
        ///   namespaceURI is different from "http://www.w3.org/XML/1998/namespace" .
        /// </exception>
        IElement createElementNS(string namespaceURI, string qualifiedName); // throws DOMException;

        /// <summary>Creates an attribute of the given qualified name and namespace URI. 
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the attribute to create.</param>
        /// <param name="qualifiedName">The qualified name of the attribute to instantiate.</param>
        /// <returns>A new Attr object with the following attributes:
        ///   AttributeValueINode.nodeNamequalifiedName
        ///   INode.namespaceURInamespaceURI
        ///   INode.prefixprefix, extracted from 
        ///   qualifiedName, or null if there is no 
        ///   prefixINode.localNamelocal name, extracted from 
        ///   qualifiedNameAttr.name
        ///   qualifiedNameINode.nodeValuethe empty 
        ///   string</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified qualified name 
        ///   contains an illegal character.
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
        IAttr createAttributeNS(string namespaceURI, string qualifiedName); // throws DOMException;

        /// <summary>Returns a NodeList of all the Elements with a 
        /// given local name and namespace URI in the order in which they are 
        /// encountered in a preorder traversal of the IDocument tree.</summary>
        /// <param name="namespaceURI">The namespace URI of the elements to match on. The 
        ///   special value "*" matches all namespaces.</param>
        /// <param name="localName">The local name of the elements to match on. The 
        ///   special value "*" matches all local names.</param>
        /// <returns>A new NodeList object containing all the matched 
        ///   Elements.
        /// </returns>
        INodeList getElementsByTagNameNS(string namespaceURI, string localName);

        /// <summary>Returns the IElement whose ID is given by 
        /// elementId. If no such element exists, returns 
        /// null. Behavior is not defined if more than one element 
        /// has this ID. The DOM implementation must have 
        /// information that says which attributes are of type ID. Attributes 
        /// with the name "ID" are not of type ID unless so defined. 
        /// Implementations that do not know whether attributes are of type ID or 
        /// not are expected to return null.</summary>
        /// <param name="elementId">The unique id value for an element.</param>
        /// <returns>The matching element.</returns>
        IElement getElementById(string elementId);

    }
}
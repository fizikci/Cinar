using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class Node
    {
        internal string _nodeName;
        private string _nodeValue;
        internal NodeType _nodeType;
        internal Node _parentNode;
        internal NodeList _childNodes;
        internal Node _firstChild;
        internal Node _lastChild;
        internal Node _previousSibling;
        internal Node _nextSibling;
        internal NamedNodeMap _attributes;
        internal Document _ownerDocument;
        private string _prefix;

        /// <summary>The name of this node, depending on its type; see the table above. 
        /// </summary>
        public string nodeName
        {
            get { return _nodeName; }
        }

        /// <summary>The value of this node, depending on its type; see the table above. 
        /// When it is defined to be null, setting it has no effect.</summary>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised when the node is readonly.</exception>
        /// <exception cref="DOMException">
        ///   DOMSTRING_SIZE_ERR: Raised when it would return more characters than 
        ///   fit in a DOMString variable on the implementation 
        ///   platform.
        /// </summary>
        public string nodeValue
        {
            get { return _nodeValue; }
            set { _nodeValue = value; }
        }

        /// <summary>A code representing the type of the underlying object, as defined above.
        /// </summary>
        public NodeType nodeType
        {
            get { return _nodeType; }
        }

        /// <summary>The parent of this node. All nodes, except Attr, 
        /// IDocument, DocumentFragment, 
        /// Entity, and Notation may have a parent. 
        /// However, if a node has just been created and not yet added to the 
        /// tree, or if it has been removed from the tree, this is 
        /// null.
        /// </summary>
        public Node parentNode
        {
            get { return _parentNode; }
        }

        /// <summary>A NodeList that contains all children of this node. If 
        /// there are no children, this is a NodeList containing no 
        /// nodes.
        /// </summary>
        public NodeList childNodes
        {
            get { return _childNodes; }
        }

        /// <summary>The first child of this node. If there is no such node, this returns 
        /// null.
        /// </summary>
        public Node firstChild
        {
            get { return _firstChild; }
        }

        /// <summary>The last child of this node. If there is no such node, this returns 
        /// null.
        /// </summary>
        public Node lastChild
        {
            get { return _lastChild; }
        }

        /// <summary>The node immediately preceding this node. If there is no such node, 
        /// this returns null.
        /// </summary>
        public Node previousSibling
        {
            get { return _previousSibling; }
        }

        /// <summary>The node immediately following this node. If there is no such node, 
        /// this returns null.
        /// </summary>
        public Node nextSibling
        {
            get { return _nextSibling; }
        }

        /// <summary>A NamedNodeMap containing the attributes of this node (if 
        /// it is an IElement) or null otherwise. 
        /// </summary>
        public NamedNodeMap attributes
        {
            get { return _attributes; }
        }

        /// <summary>The IDocument object associated with this node. This is 
        /// also the IDocument object used to create new nodes. When 
        /// this node is a IDocument or a DocumentType 
        /// which is not used with any IDocument yet, this is 
        /// null.
        /// @version DOM Level 2
        /// </summary>
        public virtual Document ownerDocument
        {
            get { return _ownerDocument; }
        }

        /// <summary>Inserts the node newChild before the existing child node 
        /// refChild. If refChild is null, 
        /// insert newChild at the end of the list of children.
        /// If newChild is a DocumentFragment object, 
        /// all of its children are inserted, in the same order, before 
        /// refChild. If the newChild is already in the 
        /// tree, it is first removed.</summary>
        /// <param name="newChild">The node to insert.</param>
        /// <param name="refChild">The reference node, i.e., the node before which the new 
        ///   node must be inserted.</param>
        /// <returns>The node being inserted.</returns>
        /// <exception cref="DOMException">
        ///   HIERARCHY_REQUEST_ERR: Raised if this node is of a type that does not 
        ///   allow children of the type of the newChild node, or if 
        ///   the node to insert is one of this node's ancestors.
        ///   WRONG_DOCUMENT_ERR: Raised if newChild was created 
        ///   from a different document than the one that created this node.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly or 
        ///   if the parent of the node being inserted is readonly.
        ///   NOT_FOUND_ERR: Raised if refChild is not a child of 
        ///   this node.
        /// </exception>
        public Node insertBefore(Node newChild, Node refChild)
        {
            throw new NotImplementedException();
        }

        /// <summary>Replaces the child node oldChild with newChild
        ///  in the list of children, and returns the oldChild node.
        /// If newChild is a DocumentFragment object, 
        /// oldChild is replaced by all of the 
        /// DocumentFragment children, which are inserted in the 
        /// same order. If the newChild is already in the tree, it 
        /// is first removed.</summary>
        /// <param name="newChild">The new node to put in the child list.</param>
        /// <param name="oldChild">The node being replaced in the list.</param>
        /// <returns>The node replaced.</returns>
        /// <exception cref="DOMException">
        ///   HIERARCHY_REQUEST_ERR: Raised if this node is of a type that does not 
        ///   allow children of the type of the newChild node, or if 
        ///   the node to put in is one of this node's ancestors.
        ///   WRONG_DOCUMENT_ERR: Raised if newChild was created 
        ///   from a different document than the one that created this node.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node or the parent of 
        ///   the new node is readonly.
        ///   NOT_FOUND_ERR: Raised if oldChild is not a child of 
        ///   this node.
        /// </exception>
        public Node replaceChild(Node newChild, Node oldChild)
        {
            throw new NotImplementedException();
        }

        /// <summary>Removes the child node indicated by oldChild from the list 
        /// of children, and returns it.</summary>
        /// <param name="oldChild">The node being removed.</param>
        /// <returns>The node removed.</returns>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   NOT_FOUND_ERR: Raised if oldChild is not a child of 
        ///   this node.
        /// </exception>
        public Node removeChild(Node oldChild)
        {
            throw new NotImplementedException();
        }

        /// <summary>Adds the node newChild to the end of the list of children 
        /// of this node. If the newChild is already in the tree, it 
        /// is first removed.</summary>
        /// <param name="newChild">The node to add.If it is a DocumentFragment
        ///    object, the entire contents of the document fragment are moved 
        ///   into the child list of this node</param>
        /// <returns>The node added.</returns>
        /// <exception cref="DOMException">
        ///   HIERARCHY_REQUEST_ERR: Raised if this node is of a type that does not 
        ///   allow children of the type of the newChild node, or if 
        ///   the node to append is one of this node's ancestors.
        ///   WRONG_DOCUMENT_ERR: Raised if newChild was created 
        ///   from a different document than the one that created this node.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        public Node appendChild(Node newChild)
        {
            throw new NotImplementedException();
        }

        /// <summary>Returns whether this node has any children.</summary>
        /// <returns> true if this node has any children, 
        ///   false otherwise.
        /// </returns>
        public bool hasChildNodes()
        {
            throw new NotImplementedException();
        }

        /// <summary>Returns a duplicate of this node, i.e., serves as a generic copy 
        /// constructor for nodes. The duplicate node has no parent; (
        /// parentNode is null.).
        /// Cloning an IElement copies all attributes and their 
        /// values, including those generated by the XML processor to represent 
        /// defaulted attributes, but this method does not copy any text it 
        /// contains unless it is a deep clone, since the text is contained in a 
        /// child Text node. Cloning an Attribute 
        /// directly, as opposed to be cloned as part of an IElement 
        /// cloning operation, returns a specified attribute (
        /// specified is true). Cloning any other type 
        /// of node simply returns a copy of this node.
        /// Note that cloning an immutable subtree results in a mutable copy, 
        /// but the children of an EntityReference clone are readonly
        /// . In addition, clones of unspecified Attr nodes are 
        /// specified. And, cloning IDocument, 
        /// DocumentType, Entity, and 
        /// Notation nodes is implementation dependent.</summary>
        /// <param name="deepIf"> true, recursively clone the subtree under 
        ///   the specified node; if false, clone only the node 
        ///   itself (and its attributes, if it is an IElement).</param>
        /// <returns>The duplicate node.</returns>
        public Node cloneNode(bool deep)
        {
            throw new NotImplementedException();
        }

        /// <summary>Puts all Text nodes in the full depth of the sub-tree 
        /// underneath this INode, including attribute nodes, into a 
        /// "normal" form where only structure (e.g., elements, comments, 
        /// processing instructions, CDATA sections, and entity references) 
        /// separates Text nodes, i.e., there are neither adjacent 
        /// Text nodes nor empty Text nodes. This can 
        /// be used to ensure that the DOM view of a document is the same as if 
        /// it were saved and re-loaded, and is useful when operations (such as 
        /// XPointer  lookups) that depend on a particular document tree 
        /// structure are to be used.In cases where the document contains 
        /// CDATASections, the normalize operation alone may not be 
        /// sufficient, since XPointers do not differentiate between 
        /// Text nodes and CDATASection nodes.
        /// </summary>
        public void normalize()
        {
            throw new NotImplementedException();
        }

        /// <summary>Tests whether the DOM implementation implements a specific feature and 
        /// that feature is supported by this node.</summary>
        /// <param name="feature">The name of the feature to test. This is the same name 
        ///   which can be passed to the method hasFeature on 
        ///   DOMImplementation.</param>
        /// <param name="version">This is the version number of the feature to test. In 
        ///   Level 2, version 1, this is the string "2.0". If the version is not 
        ///   specified, supporting any version of the feature will cause the 
        ///   method to return true.</param>
        /// <returns>Returns true if the specified feature is 
        ///   supported on this node, false otherwise.</returns>
        public bool isSupported(string feature, string version)
        {
            throw new NotImplementedException();
        }

        /// <summary>The namespace URI of this node, or null if it is 
        /// unspecified.
        /// This is not a computed value that is the result of a namespace 
        /// lookup based on an examination of the namespace declarations in 
        /// scope. It is merely the namespace URI given at creation time.
        /// For nodes of any type other than ELEMENT_NODE and 
        /// ATTRIBUTE_NODE and nodes created with a DOM Level 1 
        /// method, such as createElement from the 
        /// IDocument interface, this is always null.Per 
        /// the Namespaces in XML Specification  an attribute does not inherit 
        /// its namespace from the element it is attached to. If an attribute is 
        /// not explicitly given a namespace, it simply has no namespace.
        /// </summary>
        public string getNamespaceURI()
        {
            throw new NotImplementedException();
        }

        /// <summary>The namespace prefix of this node, or null if it is 
        /// unspecified.
        /// Note that setting this attribute, when permitted, changes the 
        /// nodeName attribute, which holds the qualified name, as 
        /// well as the tagName and name attributes of 
        /// the IElement and Attr interfaces, when 
        /// applicable.
        /// Note also that changing the prefix of an attribute that is known to 
        /// have a default value, does not make a new attribute with the default 
        /// value and the original prefix appear, since the 
        /// namespaceURI and localName do not change.
        /// For nodes of any type other than ELEMENT_NODE and 
        /// ATTRIBUTE_NODE and nodes created with a DOM Level 1 
        /// method, such as createElement from the 
        /// IDocument interface, this is always null.</summary>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified prefix contains an 
        ///   illegal character.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        ///   NAMESPACE_ERR: Raised if the specified prefix is 
        ///   malformed, if the namespaceURI of this node is 
        ///   null, if the specified prefix is "xml" and the 
        ///   namespaceURI of this node is different from "
        ///   http://www.w3.org/XML/1998/namespace", if this node is an attribute 
        ///   and the specified prefix is "xmlns" and the 
        ///   namespaceURI of this node is different from "
        ///   http://www.w3.org/2000/xmlns/", or if this node is an attribute and 
        ///   the qualifiedName of this node is "xmlns" .
        /// </exception>
        public string prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        /// <summary>Returns the local part of the qualified name of this node.
        /// For nodes of any type other than ELEMENT_NODE and 
        /// ATTRIBUTE_NODE and nodes created with a DOM Level 1 
        /// method, such as createElement from the 
        /// IDocument interface, this is always null.
        /// </summary>
        public string getLocalName()
        {
            throw new NotImplementedException();
        }

        /// <summary>Returns whether this node (if it is an element) has any attributes.</summary>
        /// <returns>true if this node has any attributes, 
        ///   false otherwise.
        /// </returns>
        public bool hasAttributes()
        {
            throw new NotImplementedException();
        }
    }
    public enum NodeType
    {
        /// <summary>The node is an IElement.
        /// </summary>
        ELEMENT_NODE = 1,
        /// <summary>The node is an Attr.
        /// </summary>
        ATTRIBUTE_NODE = 2,
        /// <summary>The node is a Text node.
        /// </summary>
        TEXT_NODE = 3,
        /// <summary>The node is a CDATASection.
        /// </summary>
        CDATA_SECTION_NODE = 4,
        /// <summary>The node is an EntityReference.
        /// </summary>
        ENTITY_REFERENCE_NODE = 5,
        /// <summary>The node is an Entity.
        /// </summary>
        ENTITY_NODE = 6,
        /// <summary>The node is a ProcessingInstruction.
        /// </summary>
        PROCESSING_INSTRUCTION_NODE = 7,
        /// <summary>The node is a Comment.
        /// </summary>
        COMMENT_NODE = 8,
        /// <summary>The node is a IDocument.
        /// </summary>
        DOCUMENT_NODE = 9,
        /// <summary>The node is a DocumentType.
        /// </summary>
        DOCUMENT_TYPE_NODE = 10,
        /// <summary>The node is a DocumentFragment.
        /// </summary>
        DOCUMENT_FRAGMENT_NODE = 11,
        /// <summary>The node is a Notation.
        /// </summary>
        NOTATION_NODE = 12
    }

}

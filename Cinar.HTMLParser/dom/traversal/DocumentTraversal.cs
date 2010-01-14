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

namespace org.w3c.dom.traversal
{


    /// <summary>DocumentTraversal contains methods that create iterators and 
    /// tree-walkers to traverse a node and its children in document order (depth 
    /// first, pre-order traversal, which is equivalent to the order in which the 
    /// start tags occur in the text representation of the document). In DOMs 
    /// which support the Traversal feature, DocumentTraversal will 
    /// be implemented by the same objects that implement the IDocument interface.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IDocumentTraversal
    {
        /// <summary>Create a new NodeIterator over the subtree rooted at the 
        /// specified node.</summary>
        /// <param name="root">The node which will be iterated together with its children. 
        ///   The iterator is initially positioned just before this node. The 
        ///   whatToShow flags and the filter, if any, are not 
        ///   considered when setting this position. The root must not be 
        ///   null.</param>
        /// <param name="whatToShowThis"> flag specifies which node types may appear in 
        ///   the logical view of the tree presented by the iterator. See the 
        ///   description of NodeFilter for the set of possible 
        ///   SHOW_ values.These flags can be combined using 
        ///   OR.</param>
        /// <param name="filter">The NodeFilter to be used with this 
        ///   TreeWalker, or null to indicate no filter.</param>
        /// <param name="entityReferenceExpansion">The value of this flag determines 
        ///   whether entity reference nodes are expanded.</param>
        /// <returns>The newly created NodeIterator.</returns>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if the specified root is 
        ///   null.
        /// </exception>
        INodeIterator createNodeIterator(INode root,
                                               int whatToShow,
                                               INodeFilter filter,
                                               bool entityReferenceExpansion); // throws DOMException;

        /// <summary>Create a new TreeWalker over the subtree rooted at the 
        /// specified node.</summary>
        /// <param name="root">The node which will serve as the root for the 
        ///   TreeWalker. The whatToShow flags and the 
        ///   NodeFilter are not considered when setting this value; 
        ///   any node type will be accepted as the root. The 
        ///   currentNode of the TreeWalker is 
        ///   initialized to this node, whether or not it is visible. The 
        ///   root functions as a stopping point for traversal 
        ///   methods that look upward in the document structure, such as 
        ///   parentNode and nextNode. The root must 
        ///   not be null.</param>
        /// <param name="whatToShow">This flag specifies which node types may appear in 
        ///   the logical view of the tree presented by the tree-walker. See the 
        ///   description of NodeFilter for the set of possible 
        ///   SHOW_ values.These flags can be combined using OR.</param>
        /// <param name="filter">The NodeFilter to be used with this 
        ///   TreeWalker, or null to indicate no filter.</param>
        /// <param name="entityReferenceExpansion">If this flag is false, the contents of 
        ///   EntityReference nodes are not presented in the logical 
        ///   view.</param>
        /// <returns>The newly created TreeWalker.</returns>
        /// <exception cref="DOMException">
        ///    NOT_SUPPORTED_ERR: Raised if the specified root is 
        ///   null.
        /// </exception>
        ITreeWalker createTreeWalker(INode root,
                                           int whatToShow,
                                           INodeFilter filter,
                                           bool entityReferenceExpansion); // throws DOMException;

    }
}
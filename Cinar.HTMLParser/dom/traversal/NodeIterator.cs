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

using System;

namespace org.w3c.dom.traversal
{

    /// <summary>Iterators are used to step through a set of nodes, e.g. the 
    /// set of nodes in a NodeList, the document subtree governed by 
    /// a particular INode, the results of a query, or any other set 
    /// of nodes. The set of nodes to be iterated is determined by the 
    /// implementation of the NodeIterator. DOM Level 2 specifies a 
    /// single NodeIterator implementation for document-order 
    /// traversal of a document subtree. Instances of these iterators are created 
    /// by calling DocumentTraversal
    /// .createNodeIterator().
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class NodeIterator
    {
        /// <summary>The root node of the NodeIterator, as specified when it 
        /// was created.
        /// </summary>
        public Node root { get; internal set; }

        /// <summary>This attribute determines which node types are presented via the 
        /// iterator. The available set of constants is defined in the 
        /// NodeFilter interface.  Nodes not accepted by 
        /// whatToShow will be skipped, but their children may still 
        /// be considered. Note that this skip takes precedence over the filter, 
        /// if any. 
        /// </summary>
        public NodeFilterType whatToShow { get; internal set; }

        /// <summary>The NodeFilter used to screen nodes.
        /// </summary>
        public NodeFilter filter { get; internal set; }

        /// <summary> The value of this flag determines whether the children of entity 
        /// reference nodes are visible to the iterator. If false, they  and 
        /// their descendants will be rejected. Note that this rejection takes 
        /// precedence over whatToShow and the filter. Also note 
        /// that this is currently the only situation where 
        /// NodeIterators may reject a complete subtree rather than 
        /// skipping individual nodes. 
        /// 
        ///  To produce a view of the document that has entity references 
        /// expanded and does not expose the entity reference node itself, use 
        /// the whatToShow flags to hide the entity reference node 
        /// and set expandEntityReferences to true when creating the 
        /// iterator. To produce a view of the document that has entity reference 
        /// nodes but no entity expansion, use the whatToShow flags 
        /// to show the entity reference node and set 
        /// expandEntityReferences to false.
        /// </summary>
        public bool expandEntityReferences { get; internal set; }

        /// <summary>Returns the next node in the set and advances the position of the 
        /// iterator in the set. After a NodeIterator is created, 
        /// the first call to nextNode() returns the first node in 
        /// the set.</summary>
        /// <returns>The next INode in the set being iterated over, or 
        ///   null if there are no more members in that set.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if this method is called after the 
        ///   detach method was invoked.
        /// </exception>
        public Node nextNode() { throw new NotImplementedException(); }

        /// <summary>Returns the previous node in the set and moves the position of the 
        /// NodeIterator backwards in the set.</summary>
        /// <returns>The previous INode in the set being iterated over, 
        ///   or null if there are no more members in that set.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if this method is called after the 
        ///   detach method was invoked.
        /// </exception>
        public Node previousNode() { throw new NotImplementedException(); }

        /// <summary>Detaches the NodeIterator from the set which it iterated 
        /// over, releasing any computational resources and placing the iterator 
        /// in the INVALID state. After detach has been invoked, 
        /// calls to nextNode or previousNode will 
        /// raise the exception INVALID_STATE_ERR.
        /// </summary>
        public void detach()
        {
            throw new NotImplementedException();
        }


    }
}
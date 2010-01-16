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

    /// <summary>Filters are objects that know how to "filter out" nodes. If a 
    /// NodeIterator or TreeWalker is given a 
    /// NodeFilter, it applies the filter before it returns the next 
    /// node. If the filter says to accept the node, the traversal logic returns 
    /// it; otherwise, traversal looks for the next node and pretends that the 
    /// node that was rejected was not there.
    /// The DOM does not provide any filters. NodeFilter is just an 
    /// interface that users can implement to provide their own filters. 
    /// NodeFilters do not need to know how to traverse from node 
    /// to node, nor do they need to know anything about the data structure that 
    /// is being traversed. This makes it very easy to write filters, since the 
    /// only thing they have to know how to do is evaluate a single node. One 
    /// filter may be used with a number of different kinds of traversals, 
    /// encouraging code reuse.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class NodeFilter
    {

        /// <summary>Test whether a specified node is visible in the logical view of a 
        /// TreeWalker or NodeIterator. This function 
        /// will be called by the implementation of TreeWalker and 
        /// NodeIterator; it is not normally called directly from 
        /// user code. (Though you could do so if you wanted to use the same 
        /// filter to guide your own application logic.)</summary>
        /// <param name="n">The node to check to see if it passes the filter or not.</param>
        /// <returns>a constant to determine whether the node is accepted, .
        ///   rejected, or skipped, as defined above.
        /// </returns>
        NodeFilterType acceptNode(Node n)
        {
            throw new NotImplementedException();
        }


    }

    public enum NodeFilterType : uint
    { 
        /// <summary>Accept the node. Navigation methods defined for 
        /// NodeIterator or TreeWalker will return this 
        /// node.
        /// </summary>
        FILTER_ACCEPT = 1,
        /// <summary>Reject the node. Navigation methods defined for 
        /// NodeIterator or TreeWalker will not return 
        /// this node. For TreeWalker, the children of this node 
        /// will also be rejected. NodeIterators treat this as a 
        /// synonym for FILTER_SKIP.
        /// </summary>
        FILTER_REJECT = 2,
        /// <summary>Skip this single node. Navigation methods defined for 
        /// NodeIterator or TreeWalker will not return 
        /// this node. For both NodeIterator and 
        /// TreeWalker, the children of this node will still be 
        /// considered. 
        /// </summary>
        FILTER_SKIP = 3,

        // Constants for whatToShow
        /// <summary>Show all Nodes.
        /// </summary>
        SHOW_ALL = 0xFFFFFFFF,
        /// <summary>Show IElement nodes.
        /// </summary>
        SHOW_ELEMENT = 0x00000001,
        /// <summary>Show Attr nodes. This is meaningful only when creating an 
        /// iterator or tree-walker with an attribute node as its 
        /// root; in this case, it means that the attribute node 
        /// will appear in the first position of the iteration or traversal. 
        /// Since attributes are never children of other nodes, they do not 
        /// appear when traversing over the document tree.
        /// </summary>
        SHOW_ATTRIBUTE = 0x00000002,
        /// <summary>Show Text nodes.
        /// </summary>
        SHOW_TEXT = 0x00000004,
        /// <summary>Show CDATASection nodes.
        /// </summary>
        SHOW_CDATA_SECTION = 0x00000008,
        /// <summary>Show EntityReference nodes.
        /// </summary>
        SHOW_ENTITY_REFERENCE = 0x00000010,
        /// <summary>Show Entity nodes. This is meaningful only when creating 
        /// an iterator or tree-walker with an Entity node as its 
        /// root; in this case, it means that the Entity
        ///  node will appear in the first position of the traversal. Since 
        /// entities are not part of the document tree, they do not appear when 
        /// traversing over the document tree.
        /// </summary>
        SHOW_ENTITY = 0x00000020,
        /// <summary>Show ProcessingInstruction nodes.
        /// </summary>
        SHOW_PROCESSING_INSTRUCTION = 0x00000040,
        /// <summary>Show Comment nodes.
        /// </summary>
        SHOW_COMMENT = 0x00000080,
        /// <summary>Show IDocument nodes.
        /// </summary>
        SHOW_DOCUMENT = 0x00000100,
        /// <summary>Show DocumentType nodes.
        /// </summary>
        SHOW_DOCUMENT_TYPE = 0x00000200,
        /// <summary>Show DocumentFragment nodes.
        /// </summary>
        SHOW_DOCUMENT_FRAGMENT = 0x00000400,
        /// <summary>Show Notation nodes. This is meaningful only when creating 
        /// an iterator or tree-walker with a Notation node as its 
        /// root; in this case, it means that the 
        /// Notation node will appear in the first position of the 
        /// traversal. Since notations are not part of the document tree, they do 
        /// not appear when traversing over the document tree.
        /// </summary>
        SHOW_NOTATION = 0x00000800
    }
}
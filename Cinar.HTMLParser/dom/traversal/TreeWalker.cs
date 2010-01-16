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

    /// <summary>TreeWalker objects are used to navigate a document tree or 
    /// subtree using the view of the document defined by their 
    /// whatToShow flags and filter (if any). Any function which 
    /// performs navigation using a TreeWalker will automatically 
    /// support any view defined by a TreeWalker.
    /// Omitting nodes from the logical view of a subtree can result in a 
    /// structure that is substantially different from the same subtree in the 
    /// complete, unfiltered document. Nodes that are siblings in the 
    /// TreeWalker view may be children of different, widely 
    /// separated nodes in the original view. For instance, consider a 
    /// NodeFilter that skips all nodes except for Text nodes and 
    /// the root node of a document. In the logical view that results, all text 
    /// nodes will be siblings and appear as direct children of the root node, no 
    /// matter how deeply nested the structure of the original document.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class TreeWalker
    {
        /// <summary>The root node of the TreeWalker, as specified 
        /// when it was created.
        /// </summary>
        public Node root { get; internal set; }

        /// <summary>This attribute determines which node types are presented via the 
        /// TreeWalker. The available set of constants is defined in 
        /// the NodeFilter interface.  Nodes not accepted by 
        /// whatToShow will be skipped, but their children may still 
        /// be considered. Note that this skip takes precedence over the filter, 
        /// if any. 
        /// </summary>
        public NodeFilterType whatToShow { get; internal set; }

        /// <summary>The filter used to screen nodes.
        /// </summary>
        public NodeFilter filter { get; internal set; }

        /// <summary>The value of this flag determines whether the children of entity 
        /// reference nodes are visible to the TreeWalker. If false, 
        /// they  and their descendants will be rejected. Note that this 
        /// rejection takes precedence over whatToShow and the 
        /// filter, if any. 
        ///  To produce a view of the document that has entity references 
        /// expanded and does not expose the entity reference node itself, use 
        /// the whatToShow flags to hide the entity reference node 
        /// and set expandEntityReferences to true when creating the 
        /// TreeWalker. To produce a view of the document that has 
        /// entity reference nodes but no entity expansion, use the 
        /// whatToShow flags to show the entity reference node and 
        /// set expandEntityReferences to false.
        /// </summary>
        public bool expandEntityReferences { get; internal set; }

        /// <summary>The node at which the TreeWalker is currently positioned.
        /// Alterations to the DOM tree may cause the current node to no longer 
        /// be accepted by the TreeWalker's associated filter. 
        /// currentNode may also be explicitly set to any node, 
        /// whether or not it is within the subtree specified by the 
        /// root node or would be accepted by the filter and 
        /// whatToShow flags. Further traversal occurs relative to 
        /// currentNode even if it is not part of the current view, 
        /// by applying the filters in the requested direction; if no traversal 
        /// is possible, currentNode is not changed. </summary>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if an attempt is made to set 
        ///   currentNode to null.
        /// </exception>
        public Node currentNode { get; set;} // throws DOMException;

        /// <summary>Moves to and returns the closest visible ancestor node of the current 
        /// node. If the search for parentNode attempts to step 
        /// upward from the TreeWalker's root node, or 
        /// if it fails to find a visible ancestor node, this method retains the 
        /// current position and returns null.</summary>
        /// <returns>The new parent node, or null if the current node 
        ///   has no parent  in the TreeWalker's logical view.  
        /// </returns>
        public Node parentNode()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the first visible child of the 
        /// current node, and returns the new node. If the current node has no 
        /// visible children, returns null, and retains the current 
        /// node.</summary>
        /// <returns>The new node, or null if the current node has no 
        ///   visible children  in the TreeWalker's logical view.  
        /// </returns>
        public Node firstChild()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the last visible child of the 
        /// current node, and returns the new node. If the current node has no 
        /// visible children, returns null, and retains the current 
        /// node.</summary>
        /// <returns>The new node, or null if the current node has no 
        ///   children  in the TreeWalker's logical view.  
        /// </returns>
        public Node lastChild()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the previous sibling of the 
        /// current node, and returns the new node. If the current node has no 
        /// visible previous sibling, returns null, and retains the 
        /// current node.</summary>
        /// <returns>The new node, or null if the current node has no 
        ///   previous sibling.  in the TreeWalker's logical view.  
        /// </returns>
        public Node previousSibling()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the next sibling of the current 
        /// node, and returns the new node. If the current node has no visible 
        /// next sibling, returns null, and retains the current node.</summary>
        /// <returns>The new node, or null if the current node has no 
        ///   next sibling.  in the TreeWalker's logical view.  
        /// </returns>
        public Node nextSibling()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the previous visible node in 
        /// document order relative to the current node, and returns the new 
        /// node. If the current node has no previous node,  or if the search for 
        /// previousNode attempts to step upward from the 
        /// TreeWalker's root node,  returns 
        /// null, and retains the current node.</summary> 
        /// <returns>The new node, or null if the current node has no 
        ///   previous node  in the TreeWalker's logical view.  
        /// </returns>
        public Node previousNode()
        {
            throw new NotImplementedException();
        }


        /// <summary>Moves the TreeWalker to the next visible node in document 
        /// order relative to the current node, and returns the new node. If the 
        /// current node has no next node, or if the search for nextNode attempts 
        /// to step upward from the TreeWalker's root 
        /// node, returns null, and retains the current node.</summary>
        /// <returns>The new node, or null if the current node has no 
        ///   next node  in the TreeWalker's logical view.  
        /// </returns>
        public Node nextNode()
        {
            throw new NotImplementedException();
        }


    }
}
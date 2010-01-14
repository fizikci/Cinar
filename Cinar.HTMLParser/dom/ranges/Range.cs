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

namespace org.w3c.dom.ranges
{


    /// <summary>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </exception>
    public interface IRange
    {
        /// <summary>INode within which the IRange begins</summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        INode startContainer { get; } // throws DOMException;

        /// <summary>Offset within the starting node of the IRange. </summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        int startOffset { get; } // throws DOMException;

        /// <summary>INode within which the IRange ends</summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        INode endContainer { get; } // throws DOMException;

        /// <summary>Offset within the ending node of the IRange.</summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        int endOffset { get; } // throws DOMException;

        /// <summary>TRUE if the IRange is collapsed </summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        bool collapsed { get; } // throws DOMException;

        /// <summary>The deepest common ancestor container of the IRange's two 
        /// boundary-points.</summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        INode commonAncestorContainer { get; } // throws DOMException;

        /// <summary>Sets the attributes describing the start of the IRange. </summary>
        /// <param name="refNode">The refNode value. This parameter must be 
        ///   different from null.</param>
        /// <param name="offset">The startOffset value. 
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if refNode or an ancestor 
        ///   of refNode is an Entity, Notation, or DocumentType 
        ///   node.</exception>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if offset is negative or greater 
        ///   than the number of child units in refNode. Child units 
        ///   are 16-bit units if refNode is a type of CharacterData 
        ///   node (e.g., a Text or Comment node) or a ProcessingInstruction 
        ///   node. Child units are Nodes in all other cases.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        void setStart(INode refNode, int offset); // throws RangeException, DOMException;

        /// <summary>Sets the attributes describing the end of a IRange.</summary>
        /// <param name="refNode">The refNode value. This parameter must be 
        ///   different from null.</param>
        /// <param name="offset">The endOffset value. </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if refNode or an ancestor 
        ///   of refNode is an Entity, Notation, or DocumentType 
        ///   node.</exception>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if offset is negative or greater 
        ///   than the number of child units in refNode. Child units 
        ///   are 16-bit units if refNode is a type of CharacterData 
        ///   node (e.g., a Text or Comment node) or a ProcessingInstruction 
        ///   node. Child units are Nodes in all other cases.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        void setEnd(INode refNode, int offset); // throws RangeException, DOMException;

        /// <summary>Sets the start position to be before a node</summary>
        /// <param name="refNodeRange"> starts before refNode </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if the root container of 
        ///   refNode is not an Attr, IDocument, or DocumentFragment 
        ///   node or if refNode is a IDocument, DocumentFragment, 
        ///   Attr, Entity, or Notation node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void setStartBefore(INode refNode); // throws RangeException, DOMException;

        /// <summary>Sets the start position to be after a node</summary>
        /// <param name="refNodeRange"> starts after refNode </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if the root container of 
        ///   refNode is not an Attr, IDocument, or DocumentFragment 
        ///   node or if refNode is a IDocument, DocumentFragment, 
        ///   Attr, Entity, or Notation node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void setStartAfter(INode refNode); // throws RangeException, DOMException;

        /// <summary>Sets the end position to be before a node. </summary>
        /// <param name="refNodeRange"> ends before refNode </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if the root container of 
        ///   refNode is not an Attr, IDocument, or DocumentFragment 
        ///   node or if refNode is a IDocument, DocumentFragment, 
        ///   Attr, Entity, or Notation node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void setEndBefore(INode refNode); // throws RangeException, DOMException;

        /// <summary>Sets the end of a IRange to be after a node </summary>
        /// <param name="refNodeRange"> ends after refNode. </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if the root container of 
        ///   refNode is not an Attr, IDocument or DocumentFragment 
        ///   node or if refNode is a IDocument, DocumentFragment, 
        ///   Attr, Entity, or Notation node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void setEndAfter(INode refNode); // throws RangeException, DOMException;

        /// <summary>Collapse a IRange onto one of its boundary-points </summary>
        /// <param name="toStartIf"> TRUE, collapses the IRange onto its start; if FALSE, 
        ///   collapses it onto its end. </param>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void collapse(bool toStart); // throws DOMException;

        /// <summary>Select a node and its contents </summary>
        /// <param name="refNode">The node to select. </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if an ancestor of refNode 
        ///   is an Entity, Notation or DocumentType node or if 
        ///   refNode is a IDocument, DocumentFragment, Attr, Entity, 
        ///   or Notation node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void selectNode(INode refNode); // throws RangeException, DOMException;

        /// <summary>Select the contents within a node </summary>
        /// <param name="refNodeNode"> to select from </param>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if refNode or an ancestor 
        ///   of refNode is an Entity, Notation or DocumentType node.</exception>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void selectNodeContents(INode refNode); // throws RangeException, DOMException;

        /// <summary>Compare the boundary-points of two Ranges in a document.</summary>
        /// <param name="howA"> code representing the type of comparison, as defined above.</param>
        /// <param name="sourceRange">The IRange on which this current 
        ///   IRange is compared to.</param>
        /// <returns> -1, 0 or 1 depending on whether the corresponding 
        ///   boundary-point of the IRange is respectively before, equal to, or 
        ///   after the corresponding boundary-point of sourceRange. </returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if the two Ranges are not in the same 
        ///   IDocument or DocumentFragment.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        short compareBoundaryPoints(CompareHow how, IRange sourceRange); // throws DOMException;

        /// <summary>Removes the contents of a IRange from the containing document or 
        /// document fragment without returning a reference to the removed 
        /// content.</summary>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if any portion of the content of 
        ///   the IRange is read-only or any of the nodes that contain any of the 
        ///   content of the IRange are read-only.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        void deleteContents(); // throws DOMException;

        /// <summary>Moves the contents of a IRange from the containing document or document 
        /// fragment to a new DocumentFragment.</summary>
        /// <returns>A DocumentFragment containing the extracted contents. </returns>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if any portion of the content of 
        ///   the IRange is read-only or any of the nodes which contain any of the 
        ///   content of the IRange are read-only.
        ///   HIERARCHY_REQUEST_ERR: Raised if a DocumentType node would be 
        ///   extracted into the new DocumentFragment.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        IDocumentFragment extractContents(); // throws DOMException;

        /// <summary>Duplicates the contents of a IRange </summary>
        /// <returns>A DocumentFragment that contains content equivalent to this 
        ///   IRange.</returns>
        /// <exception cref="DOMException">
        ///   HIERARCHY_REQUEST_ERR: Raised if a DocumentType node would be 
        ///   extracted into the new DocumentFragment.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.
        /// </exception>
        IDocumentFragment cloneContents(); // throws DOMException;

        /// <summary>Inserts a node into the IDocument or DocumentFragment at the start of 
        /// the IRange. If the container is a Text node, this will be split at the 
        /// start of the IRange (as if the Text node's splitText method was 
        /// performed at the insertion point) and the insertion will occur 
        /// between the two resulting Text nodes. Adjacent Text nodes will not be 
        /// automatically merged. If the node to be inserted is a 
        /// DocumentFragment node, the children will be inserted rather than the 
        /// DocumentFragment node itself.</summary>
        /// <param name="newNode">The node to insert at the start of the IRange </param>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if an ancestor container of the 
        ///   start of the IRange is read-only.
        ///   WRONG_DOCUMENT_ERR: Raised if newNode and the 
        ///   container of the start of the IRange were not created from the same 
        ///   document.
        ///   HIERARCHY_REQUEST_ERR: Raised if the container of the start of 
        ///   the IRange is of a type that does not allow children of the type of 
        ///   newNode or if newNode is an ancestor of 
        ///   the container.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.</exception>
        /// <exception cref="RangeException">
        ///   INVALID_NODE_TYPE_ERR: Raised if newNode is an Attr, 
        ///   Entity, Notation, or IDocument node.
        /// </exception>
        void insertNode(INode newNode); // throws DOMException, RangeException;

        /// <summary>Reparents the contents of the IRange to the given node and inserts the 
        /// node at the position of the start of the IRange.</summary>
        /// <param name="newParent">The node to surround the contents with. </param>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if an ancestor container of 
        ///   either boundary-point of the IRange is read-only.
        ///   WRONG_DOCUMENT_ERR: Raised if  newParent and the 
        ///   container of the start of the IRange were not created from the same 
        ///   document.
        ///   HIERARCHY_REQUEST_ERR: Raised if the container of the start of 
        ///   the IRange is of a type that does not allow children of the type of 
        ///   newParent or if newParent is an ancestor 
        ///   of the container or if node would end up with a child 
        ///   node of a type not allowed by the type of node.
        ///   INVALID_STATE_ERR: Raised if detach() has already 
        ///   been invoked on this object.</exception>
        /// <exception cref="RangeException">
        ///   BAD_BOUNDARYPOINTS_ERR: Raised if the IRange partially selects a 
        ///   non-text node.
        ///   INVALID_NODE_TYPE_ERR: Raised if  node is an Attr, 
        ///   Entity, DocumentType, Notation, IDocument, or DocumentFragment node.
        /// </exception>
        void surroundContents(INode newParent); // throws DOMException, RangeException;

        /// <summary>Produces a new IRange whose boundary-points are equal to the 
        /// boundary-points of the IRange. </summary>
        /// <returns>The duplicated IRange. </returns>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        IRange cloneRange(); // throws DOMException;

        /// <summary>Returns the contents of a IRange as a string. This string contains only 
        /// the data characters, not any markup. </summary>
        /// <returns>The contents of the IRange.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        string toString(); // throws DOMException;

        /// <summary>Called to indicate that the IRange is no longer in use and that the 
        /// implementation may relinquish any resources associated with this 
        /// IRange. Subsequent calls to any methods or attribute getters on this 
        /// IRange will result in a DOMException being thrown with an 
        /// error code of INVALID_STATE_ERR.</summary>
        /// <exception cref="DOMException">
        ///   INVALID_STATE_ERR: Raised if detach() has already been 
        ///   invoked on this object.
        /// </exception>
        void detach(); // throws DOMException;

    }

    public enum CompareHow
    {
        /// <summary>Compare start boundary-point of sourceRange to start 
        /// boundary-point of IRange on which compareBoundaryPoints 
        /// is invoked.
        /// </exception>
        START_TO_START = 0,
        /// <summary>Compare start boundary-point of sourceRange to end 
        /// boundary-point of IRange on which compareBoundaryPoints 
        /// is invoked.
        /// </exception>
        START_TO_END = 1,
        /// <summary>Compare end boundary-point of sourceRange to end 
        /// boundary-point of IRange on which compareBoundaryPoints 
        /// is invoked.
        /// </exception>
        END_TO_END = 2,
        /// <summary>Compare end boundary-point of sourceRange to start 
        /// boundary-point of IRange on which compareBoundaryPoints 
        /// is invoked.
        /// </exception>
        END_TO_START = 3
    }
}
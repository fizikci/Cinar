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


    /**
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
     * @since DOM Level 2
     */
    public interface IRange
    {
        /**
         * INode within which the IRange begins 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        INode startContainer { get; } // throws DOMException;

        /**
         * Offset within the starting node of the IRange. 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        int startOffset { get; } // throws DOMException;

        /**
         * INode within which the IRange ends 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        INode endContainer { get; } // throws DOMException;

        /**
         * Offset within the ending node of the IRange. 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        int endOffset { get; } // throws DOMException;

        /**
         * TRUE if the IRange is collapsed 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        bool collapsed { get; } // throws DOMException;

        /**
         * The deepest common ancestor container of the IRange's two 
         * boundary-points.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        INode commonAncestorContainer() { get; } // throws DOMException;

        /**
         * Sets the attributes describing the start of the IRange. 
         * @param refNodeThe <code>refNode</code> value. This parameter must be 
         *   different from <code>null</code>.
         * @param offsetThe <code>startOffset</code> value. 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if <code>refNode</code> or an ancestor 
         *   of <code>refNode</code> is an Entity, Notation, or DocumentType 
         *   node.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if <code>offset</code> is negative or greater 
         *   than the number of child units in <code>refNode</code>. Child units 
         *   are 16-bit units if <code>refNode</code> is a type of CharacterData 
         *   node (e.g., a Text or Comment node) or a ProcessingInstruction 
         *   node. Child units are Nodes in all other cases.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        void setStart(INode refNode, int offset); // throws RangeException, DOMException;

        /**
         * Sets the attributes describing the end of a IRange.
         * @param refNodeThe <code>refNode</code> value. This parameter must be 
         *   different from <code>null</code>.
         * @param offsetThe <code>endOffset</code> value. 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if <code>refNode</code> or an ancestor 
         *   of <code>refNode</code> is an Entity, Notation, or DocumentType 
         *   node.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if <code>offset</code> is negative or greater 
         *   than the number of child units in <code>refNode</code>. Child units 
         *   are 16-bit units if <code>refNode</code> is a type of CharacterData 
         *   node (e.g., a Text or Comment node) or a ProcessingInstruction 
         *   node. Child units are Nodes in all other cases.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        void setEnd(INode refNode, int offset); // throws RangeException, DOMException;

        /**
         * Sets the start position to be before a node
         * @param refNodeRange starts before <code>refNode</code> 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if the root container of 
         *   <code>refNode</code> is not an Attr, IDocument, or DocumentFragment 
         *   node or if <code>refNode</code> is a IDocument, DocumentFragment, 
         *   Attr, Entity, or Notation node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void setStartBefore(INode refNode); // throws RangeException, DOMException;

        /**
         * Sets the start position to be after a node
         * @param refNodeRange starts after <code>refNode</code> 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if the root container of 
         *   <code>refNode</code> is not an Attr, IDocument, or DocumentFragment 
         *   node or if <code>refNode</code> is a IDocument, DocumentFragment, 
         *   Attr, Entity, or Notation node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void setStartAfter(INode refNode); // throws RangeException, DOMException;

        /**
         * Sets the end position to be before a node. 
         * @param refNodeRange ends before <code>refNode</code> 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if the root container of 
         *   <code>refNode</code> is not an Attr, IDocument, or DocumentFragment 
         *   node or if <code>refNode</code> is a IDocument, DocumentFragment, 
         *   Attr, Entity, or Notation node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void setEndBefore(INode refNode); // throws RangeException, DOMException;

        /**
         * Sets the end of a IRange to be after a node 
         * @param refNodeRange ends after <code>refNode</code>. 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if the root container of 
         *   <code>refNode</code> is not an Attr, IDocument or DocumentFragment 
         *   node or if <code>refNode</code> is a IDocument, DocumentFragment, 
         *   Attr, Entity, or Notation node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void setEndAfter(INode refNode); // throws RangeException, DOMException;

        /**
         * Collapse a IRange onto one of its boundary-points 
         * @param toStartIf TRUE, collapses the IRange onto its start; if FALSE, 
         *   collapses it onto its end. 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void collapse(bool toStart); // throws DOMException;

        /**
         * Select a node and its contents 
         * @param refNodeThe node to select. 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if an ancestor of <code>refNode</code> 
         *   is an Entity, Notation or DocumentType node or if 
         *   <code>refNode</code> is a IDocument, DocumentFragment, Attr, Entity, 
         *   or Notation node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void selectNode(INode refNode); // throws RangeException, DOMException;

        /**
         * Select the contents within a node 
         * @param refNodeNode to select from 
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if <code>refNode</code> or an ancestor 
         *   of <code>refNode</code> is an Entity, Notation or DocumentType node.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void selectNodeContents(INode refNode); // throws RangeException, DOMException;

        /**
         * Compare the boundary-points of two Ranges in a document.
         * @param howA code representing the type of comparison, as defined above.
         * @param sourceRangeThe <code>IRange</code> on which this current 
         *   <code>IRange</code> is compared to.
         * @return  -1, 0 or 1 depending on whether the corresponding 
         *   boundary-point of the IRange is respectively before, equal to, or 
         *   after the corresponding boundary-point of <code>sourceRange</code>. 
         * @exception DOMException
         *   WRONG_DOCUMENT_ERR: Raised if the two Ranges are not in the same 
         *   IDocument or DocumentFragment.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        short compareBoundaryPoints(CompareHow how, IRange sourceRange); // throws DOMException;

        /**
         * Removes the contents of a IRange from the containing document or 
         * document fragment without returning a reference to the removed 
         * content.  
         * @exception DOMException
         *   NO_MODIFICATION_ALLOWED_ERR: Raised if any portion of the content of 
         *   the IRange is read-only or any of the nodes that contain any of the 
         *   content of the IRange are read-only.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        void deleteContents(); // throws DOMException;

        /**
         * Moves the contents of a IRange from the containing document or document 
         * fragment to a new DocumentFragment. 
         * @return A DocumentFragment containing the extracted contents. 
         * @exception DOMException
         *   NO_MODIFICATION_ALLOWED_ERR: Raised if any portion of the content of 
         *   the IRange is read-only or any of the nodes which contain any of the 
         *   content of the IRange are read-only.
         *   <br>HIERARCHY_REQUEST_ERR: Raised if a DocumentType node would be 
         *   extracted into the new DocumentFragment.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        IDocumentFragment extractContents(); // throws DOMException;

        /**
         * Duplicates the contents of a IRange 
         * @return A DocumentFragment that contains content equivalent to this 
         *   IRange.
         * @exception DOMException
         *   HIERARCHY_REQUEST_ERR: Raised if a DocumentType node would be 
         *   extracted into the new DocumentFragment.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         */
        IDocumentFragment cloneContents(); // throws DOMException;

        /**
         * Inserts a node into the IDocument or DocumentFragment at the start of 
         * the IRange. If the container is a Text node, this will be split at the 
         * start of the IRange (as if the Text node's splitText method was 
         * performed at the insertion point) and the insertion will occur 
         * between the two resulting Text nodes. Adjacent Text nodes will not be 
         * automatically merged. If the node to be inserted is a 
         * DocumentFragment node, the children will be inserted rather than the 
         * DocumentFragment node itself.
         * @param newNodeThe node to insert at the start of the IRange 
         * @exception DOMException
         *   NO_MODIFICATION_ALLOWED_ERR: Raised if an ancestor container of the 
         *   start of the IRange is read-only.
         *   <br>WRONG_DOCUMENT_ERR: Raised if <code>newNode</code> and the 
         *   container of the start of the IRange were not created from the same 
         *   document.
         *   <br>HIERARCHY_REQUEST_ERR: Raised if the container of the start of 
         *   the IRange is of a type that does not allow children of the type of 
         *   <code>newNode</code> or if <code>newNode</code> is an ancestor of 
         *   the container.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         * @exception RangeException
         *   INVALID_NODE_TYPE_ERR: Raised if <code>newNode</code> is an Attr, 
         *   Entity, Notation, or IDocument node.
         */
        void insertNode(INode newNode); // throws DOMException, RangeException;

        /**
         * Reparents the contents of the IRange to the given node and inserts the 
         * node at the position of the start of the IRange. 
         * @param newParentThe node to surround the contents with. 
         * @exception DOMException
         *   NO_MODIFICATION_ALLOWED_ERR: Raised if an ancestor container of 
         *   either boundary-point of the IRange is read-only.
         *   <br>WRONG_DOCUMENT_ERR: Raised if <code> newParent</code> and the 
         *   container of the start of the IRange were not created from the same 
         *   document.
         *   <br>HIERARCHY_REQUEST_ERR: Raised if the container of the start of 
         *   the IRange is of a type that does not allow children of the type of 
         *   <code>newParent</code> or if <code>newParent</code> is an ancestor 
         *   of the container or if <code>node</code> would end up with a child 
         *   node of a type not allowed by the type of <code>node</code>.
         *   <br>INVALID_STATE_ERR: Raised if <code>detach()</code> has already 
         *   been invoked on this object.
         * @exception RangeException
         *   BAD_BOUNDARYPOINTS_ERR: Raised if the IRange partially selects a 
         *   non-text node.
         *   <br>INVALID_NODE_TYPE_ERR: Raised if <code> node</code> is an Attr, 
         *   Entity, DocumentType, Notation, IDocument, or DocumentFragment node.
         */
        void surroundContents(INode newParent); // throws DOMException, RangeException;

        /**
         * Produces a new IRange whose boundary-points are equal to the 
         * boundary-points of the IRange. 
         * @return The duplicated IRange. 
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        IRange cloneRange(); // throws DOMException;

        /**
         * Returns the contents of a IRange as a string. This string contains only 
         * the data characters, not any markup. 
         * @return The contents of the IRange.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        string toString(); // throws DOMException;

        /**
         * Called to indicate that the IRange is no longer in use and that the 
         * implementation may relinquish any resources associated with this 
         * IRange. Subsequent calls to any methods or attribute getters on this 
         * IRange will result in a <code>DOMException</code> being thrown with an 
         * error code of <code>INVALID_STATE_ERR</code>.
         * @exception DOMException
         *   INVALID_STATE_ERR: Raised if <code>detach()</code> has already been 
         *   invoked on this object.
         */
        void detach(); // throws DOMException;

    }

    public enum CompareHow
    {
        // CompareHow
        /**
         * Compare start boundary-point of <code>sourceRange</code> to start 
         * boundary-point of IRange on which <code>compareBoundaryPoints</code> 
         * is invoked.
         */
        START_TO_START = 0,
        /**
         * Compare start boundary-point of <code>sourceRange</code> to end 
         * boundary-point of IRange on which <code>compareBoundaryPoints</code> 
         * is invoked.
         */
        START_TO_END = 1,
        /**
         * Compare end boundary-point of <code>sourceRange</code> to end 
         * boundary-point of IRange on which <code>compareBoundaryPoints</code> 
         * is invoked.
         */
        END_TO_END = 2,
        /**
         * Compare end boundary-point of <code>sourceRange</code> to start 
         * boundary-point of IRange on which <code>compareBoundaryPoints</code> 
         * is invoked.
         */
        END_TO_START = 3
    }
}
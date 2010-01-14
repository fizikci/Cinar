/*
 * Copyright (c) 2002 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

namespace org.w3c.dom.ls
{


    /// <summary>DOMBuilderFilters provide applications the ability to examine 
    /// nodes as they are being constructed during a parse. As each node is 
    /// examined, it may be modified or removed, or the entire parse may be 
    /// terminated early. 
    /// At the time any of the filter methods are called by the parser, the 
    /// owner IDocument and DOMImplementation objects exist and are accessible. 
    /// The document element is never passed to the DOMBuilderFilter 
    /// methods, i.e. it is not possible to filter out the document element. The 
    /// IDocument, DocumentType, Notation, 
    /// and Entity nodes are not passed to the filter.
    /// All validity checking while reading a document occurs on the source 
    /// document as it appears on the input stream, not on the DOM document as it 
    /// is built in memory. With filters, the document in memory may be a subset 
    /// of the document on the stream, and its validity may have been affected by 
    /// the filtering.
    ///  All default content, including default attributes, must be passed to 
    /// the filter methods. 
    ///  Any exception raised in the filter are ignored by the 
    /// DOMBuilder. The description of these methods is not complete
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public interface IDOMBuilderFilter
    {
        /// <summary>This method will be called by the parser after each IElement
        ///  start tag has been scanned, but before the remainder of the 
        /// IElement is processed. The intent is to allow the 
        /// element, including any children, to be efficiently skipped. Note that 
        /// only element nodes are passed to the startElement 
        /// function.
        /// The element node passed to startElement for filtering 
        /// will include all of the IElement's attributes, but none of the 
        /// children nodes. The IElement may not yet be in place in the document 
        /// being constructed (it may not have a parent node.) 
        /// A startElement filter function may access or change 
        /// the attributes for the IElement. Changing Namespace declarations will 
        /// have no effect on namespace resolution by the parser.
        /// For efficiency, the IElement node passed to the filter may not be 
        /// the same one as is actually placed in the tree if the node is 
        /// accepted. And the actual node (node object identity) may be reused 
        /// during the process of reading in and filtering a document.</summary>
        /// <param name="elt"> The newly encountered element. At the time this method is 
        ///   called, the element is incomplete - it will have its attributes, 
        ///   but no children.</param>
        /// <returns> FILTER_ACCEPT if this IElement 
        ///   should be included in the DOM document being built.  
        ///   FILTER_REJECT if the IElement and all of 
        ///   its children should be rejected.  FILTER_SKIP if the 
        ///   IElement should be rejected. All of its children are 
        ///   inserted in place of the rejected IElement node.  
        ///   FILTER_INTERRUPT if the filter wants to stop the 
        ///   processing of the document. Interrupting the processing of the 
        ///   document does no longer guarantee that the entire is XML well-formed
        ///   .  Returning any other values will result in unspecified behavior. 
        /// </returns>
        DOMBuilderFilterType startElement(IElement elt);

        /// <summary>This method will be called by the parser at the completion of the 
        /// parsing of each node. The node and all of its descendants will exist 
        /// and be complete. The parent node will also exist, although it may be 
        /// incomplete, i.e. it may have additional children that have not yet 
        /// been parsed. Attribute nodes are never passed to this function.
        /// From within this method, the new node may be freely modified - 
        /// children may be added or removed, text nodes modified, etc. The state 
        /// of the rest of the document outside this node is not defined, and the 
        /// affect of any attempt to navigate to, or to modify any other part of 
        /// the document is undefined. 
        /// For validating parsers, the checks are made on the original 
        /// document, before any modification by the filter. No validity checks 
        /// are made on any document modifications made by the filter.
        /// If this new node is rejected, the parser might reuse the new node 
        /// or any of its descendants.</summary>
        /// <param name="enode"> The newly constructed element. At the time this method is 
        ///   called, the element is complete - it has all of its children (and 
        ///   their children, recursively) and attributes, and is attached as a 
        ///   child to its parent.</param>
        /// <returns> FILTER_ACCEPT if this INode should 
        ///   be included in the DOM document being built.  
        ///   FILTER_REJECT if the INode and all of its 
        ///   children should be rejected.  FILTER_SKIP if the 
        ///   INode should be skipped and the INode 
        ///   should be replaced by all the children of the INode.  
        ///   FILTER_INTERRUPT if the filter wants to stop the 
        ///   processing of the document. Interrupting the processing of the 
        ///   document does no longer guarantee that the entire is XML well-formed
        /// </returns>
        DOMBuilderFilterType acceptNode(INode enode);

        /// <summary> Tells the DOMBuilder what types of nodes to show to the 
        /// filter. See NodeFilter for definition of the constants. 
        /// The constant SHOW_ATTRIBUTE is meaningless here, 
        /// attribute nodes will never be passed to a 
        /// DOMBuilderFilter. 
        /// </summary>
        int getWhatToShow();

    }

    public enum DOMBuilderFilterType
    {
        /// <summary>Accept the node.
        /// </summary>
        FILTER_ACCEPT = 1,
        /// <summary>Reject the node abd its children.
        /// </summary>
        FILTER_REJECT = 2,
        /// <summary>Skip this single node. The children of this node will still be 
        /// considered. 
        /// </summary>
        FILTER_SKIP = 3,
        /// <summary> Interrupt the normal processing of the document. 
        /// </summary>
        FILTER_INTERRUPT = 4
    }
}
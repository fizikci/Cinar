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

using org.w3c.dom.traversal;

namespace org.w3c.dom.ls
{

    /// <summary> DOMWriterFilters provide applications the ability to examine 
    /// nodes as they are being serialized. DOMWriterFilter lets the 
    /// application decide what nodes should be serialized or not. 
    ///  The IDocument, DocumentType, 
    /// Notation, and Entity nodes are not passed to 
    /// the filter. 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public class DOMWriterFilter : NodeFilter
    {
        /// <summary> Tells the DOMWriter what types of nodes to show to the 
        /// filter. See NodeFilter for definition of the constants. 
        /// The constants SHOW_ATTRIBUTE, SHOW_DOCUMENT
        /// , SHOW_DOCUMENT_TYPE, SHOW_NOTATION, and 
        /// SHOW_DOCUMENT_FRAGMENT are meaningless here, those nodes 
        /// will never be passed to a DOMWriterFilter. 
        /// Entity nodes are not passed to the filter. 
        /// </summary>
        public int whatToShow { get; internal set; }

    }
}
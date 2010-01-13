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

    /**
     *  <code>DOMWriterFilter</code>s provide applications the ability to examine 
     * nodes as they are being serialized. <code>DOMWriterFilter</code> lets the 
     * application decide what nodes should be serialized or not. 
     * <p> The <code>IDocument</code>, <code>DocumentType</code>, 
     * <code>Notation</code>, and <code>Entity</code> nodes are not passed to 
     * the filter. 
     * <p>See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load
    and Save Specification</a>.
     */
    public interface IDOMWriterFilter : INodeFilter
    {
        /**
         *  Tells the <code>DOMWriter</code> what types of nodes to show to the 
         * filter. See <code>NodeFilter</code> for definition of the constants. 
         * The constants <code>SHOW_ATTRIBUTE</code>, <code>SHOW_DOCUMENT</code>
         * , <code>SHOW_DOCUMENT_TYPE</code>, <code>SHOW_NOTATION</code>, and 
         * <code>SHOW_DOCUMENT_FRAGMENT</code> are meaningless here, those nodes 
         * will never be passed to a <code>DOMWriterFilter</code>. 
         * <code>Entity</code> nodes are not passed to the filter. 
         */
        int whatToShow { get; }

    }
}
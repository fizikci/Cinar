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

namespace org.w3c.dom.views
{

    /// <summary>The DocumentView interface is implemented by 
    /// IDocument objects in DOM implementations supporting DOM 
    /// Views. It provides an attribute to retrieve the default view of a 
    /// document.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Views-20001113'>IDocument Object Model (DOM) Level 2 Views Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class DocumentView
    {
        /// <summary>The default IAbstractView for this IDocument, 
        /// or null if none available.
        /// </summary>
        public AbstractView defaultView { get; internal set; }

    }
}
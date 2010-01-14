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

namespace org.w3c.dom.stylesheets
{

    /// <summary> The LinkStyle interface provides a mechanism by which a style 
    /// sheet can be retrieved from the node responsible for linking it into a 
    /// document. An instance of the LinkStyle interface can be 
    /// obtained using binding-specific casting methods on an instance of a 
    /// linking node (HTMLLinkElement, HTMLStyleElement 
    /// or ProcessingInstruction in DOM Level 2). 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class LinkStyle
    {
        /// <summary> The style sheet. 
        /// </summary>
        public StyleSheet sheet { get; internal set; }

    }
}
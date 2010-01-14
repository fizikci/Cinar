/*
 * Copyright (c) 2003 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

namespace org.w3c.dom.html2
{

    /// <summary>Local change to font. See the FONT element definition in HTML 4.01. This 
    /// element is deprecated in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLFontElement : HTMLElement
    {
        /// <summary>Font color. See the color attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        public string color { get; set; }


        /// <summary>Font face identifier. See the face attribute definition in HTML 4.01. 
        /// This attribute is deprecated in HTML 4.01.
        /// </summary>
        public string face { get; set; }


        /// <summary>Font size. See the size attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        public string size { get; set; }


    }
}
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

    /// <summary>Client-side image map area definition. See the AREA element definition in 
    /// HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLAreaElement : HTMLElement
    {
        /// <summary>A single character access key to give access to the form control. See 
        /// the accesskey attribute definition in HTML 4.01.
        /// </summary>
        public string accessKey { get; set; }


        /// <summary>Alternate text for user agents not rendering the normal content of this 
        /// element. See the alt attribute definition in HTML 4.01.
        /// </summary>
        public string alt { get; set; }


        /// <summary>Comma-separated list of lengths, defining an active region geometry. 
        /// See also shape for the shape of the region. See the 
        /// coords attribute definition in HTML 4.01.
        /// </summary>
        public string coords { get; set; }


        /// <summary>The URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string href { get; set; }


        /// <summary>Specifies that this area is inactive, i.e., has no associated action. 
        /// See the nohref attribute definition in HTML 4.01.
        /// </summary>
        public bool noHref { get; set; }


        /// <summary>The shape of the active area. The coordinates are given by 
        /// coords. See the shape attribute definition in HTML 4.01.
        /// </summary>
        public string shape { get; set; }


        /// <summary>Index that represents the element's position in the tabbing order. See 
        /// the tabindex attribute definition in HTML 4.01.
        /// </summary>
        public int tabIndex { get; set; }


        /// <summary>Frame to render the resource in. See the target attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string target { get; set; }


    }
}
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

using System;

namespace org.w3c.dom.html2
{

    /**
     * The anchor element. See the A element definition in HTML 4.01.
     * See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public class HTMLAnchorElement : HTMLElement
    {
        /// <summary>A single character access key to give access to the form control. See 
        /// the accesskey attribute definition in HTML 4.01.
        /// </summary>
        public string accessKey { get; set; }


        /// <summary>The character encoding of the linked resource. See the charset 
        /// attribute definition in HTML 4.01.
        /// </summary>
        public string charset { get; set; }


        /// <summary>Comma-separated list of lengths, defining an active region geometry. 
        /// See also shape for the shape of the region. See the 
        /// coords attribute definition in HTML 4.01.
        /// </summary>
        public string coords { get; set; }


        /// <summary>The absolute URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string href { get; set; }


        /// <summary>Language code of the linked resource. See the hreflang attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string hreflang { get; set; }


        /// <summary>Anchor name. See the name attribute definition in HTML 4.01.
        /// </summary>
        public string name { get; set; }


        /// <summary>Forward link type. See the rel attribute definition in HTML 4.01.
        /// </summary>
        public string rel { get; set; }


        /// <summary>Reverse link type. See the rev attribute definition in HTML 4.01.
        /// </summary>
        public string rev { get; set; }


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


        /// <summary>Advisory content type. See the type attribute definition in HTML 4.01.
        /// </summary>
        public string type { get; set; }


        /// <summary>Removes keyboard focus from this element.
        /// </summary>
        public void blur()
        {
            throw new NotImplementedException();
        }


        /// <summary>Gives keyboard focus to this element.
        /// </summary>
        public void focus()
        {
            throw new NotImplementedException();
        }


    }
}
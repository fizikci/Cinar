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


    /// <summary>Inline subwindows. See the IFRAME element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLIFrameElement : HTMLElement
    {
        /// <summary>Aligns this object (vertically or horizontally) with respect to its 
        /// surrounding text. See the align attribute definition in HTML 4.01. 
        /// This attribute is deprecated in HTML 4.01.
        /// </summary>
        public string align { get; set; }


        /// <summary>Request frame borders. See the frameborder attribute definition in HTML 
        /// 4.01.
        /// </summary>
        public string frameBorder { get; set; }


        /// <summary>Frame height. See the height attribute definition in HTML 4.01.
        /// </summary>
        public string height { get; set; }


        /// <summary>URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
        /// longdesc attribute definition in HTML 4.01.
        /// </summary>
        public string longDesc { get; set; }


        /// <summary>Frame margin height, in pixels. See the marginheight attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string marginHeight { get; set; }


        /// <summary>Frame margin width, in pixels. See the marginwidth attribute definition 
        /// in HTML 4.01.
        /// </summary>
        public string marginWidth { get; set; }


        /// <summary>The frame name (object of the target attribute). See the 
        /// name attribute definition in HTML 4.01.
        /// </summary>
        public string name { get; set; }


        /// <summary>Specify whether or not the frame should have scrollbars. See the 
        /// scrolling attribute definition in HTML 4.01.
        /// </summary>
        public string scrolling { get; set; }


        /// <summary>A URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the initial frame contents. See the src attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string src { get; set; }


        /// <summary>Frame width. See the width attribute definition in HTML 4.01.
        /// </summary>
        public string Width { get; set; }


        /// <summary>The document this frame contains, if there is any and it is available, 
        /// or null otherwise.
        /// @since DOM Level 2
        /// </summary>
        public Document ContentDocument { get; set; }

    }
}
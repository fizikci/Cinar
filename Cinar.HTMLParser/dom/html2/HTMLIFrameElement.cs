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


    /**
     * Inline subwindows. See the IFRAME element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLIFrameElement : IHTMLElement
    {
        /**
         * Aligns this object (vertically or horizontally) with respect to its 
         * surrounding text. See the align attribute definition in HTML 4.01. 
         * This attribute is deprecated in HTML 4.01.
         */
        string align { get; set; }


        /**
         * Request frame borders. See the frameborder attribute definition in HTML 
         * 4.01.
         */
        string frameBorder { get; set; }


        /**
         * Frame height. See the height attribute definition in HTML 4.01.
         */
        string height { get; set; }


        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
         * longdesc attribute definition in HTML 4.01.
         */
        string longDesc { get; set; }


        /**
         * Frame margin height, in pixels. See the marginheight attribute 
         * definition in HTML 4.01.
         */
        string marginHeight { get; set; }


        /**
         * Frame margin width, in pixels. See the marginwidth attribute definition 
         * in HTML 4.01.
         */
        string marginWidth { get; set; }


        /**
         * The frame name (object of the <code>target</code> attribute). See the 
         * name attribute definition in HTML 4.01.
         */
        string name { get; set; }


        /**
         * Specify whether or not the frame should have scrollbars. See the 
         * scrolling attribute definition in HTML 4.01.
         */
        string scrolling { get; set; }


        /**
         * A URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the initial frame contents. See the src attribute 
         * definition in HTML 4.01.
         */
        string src { get; set; }


        /**
         * Frame width. See the width attribute definition in HTML 4.01.
         */
        string Width { get; set; }


        /**
         * The document this frame contains, if there is any and it is available, 
         * or <code>null</code> otherwise.
         * @since DOM Level 2
         */
        IDocument ContentDocument { get; set; }

    }
}
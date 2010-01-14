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

    /// <summary>Create a frame. See the FRAME element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLFrameElement : IHTMLElement
    {
        /// <summary>Request frame borders. See the frameborder attribute definition in HTML 
        /// 4.01.
        /// </summary>
        string frameBorder { get; set; }


        /// <summary>URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
        /// longdesc attribute definition in HTML 4.01.
        /// </summary>
        string longDesc { get; set; }


        /// <summary>Frame margin height, in pixels. See the marginheight attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string marginHeight { get; set; }


        /// <summary>Frame margin width, in pixels. See the marginwidth attribute definition 
        /// in HTML 4.01.
        /// </summary>
        string marginWidth { get; set; }


        /// <summary>The frame name (object of the target attribute). See the 
        /// name attribute definition in HTML 4.01.
        /// </summary>
        string name { get; set; }


        /// <summary>When true, forbid user from resizing frame. See the noresize attribute 
        /// definition in HTML 4.01.
        /// </summary>
        bool noResize { get; set; }


        /// <summary>Specify whether or not the frame should have scrollbars. See the 
        /// scrolling attribute definition in HTML 4.01.
        /// </summary>
        string scrolling { get; set; }


        /// <summary>A URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the initial frame contents. See the src attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string src { get; set; }


        /// <summary>The document this frame contains, if there is any and it is available, 
        /// or null otherwise.
        /// @since DOM Level 2
        /// </summary>
        IDocument ContentDocument { get; set; }

    }
}
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

    /// <summary>The HTML document body. This element is always present in the DOM API, even 
    /// if the tags are not present in the source document. See the BODY element 
    /// definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLBodyElement : IHTMLElement
    {
        /// <summary>Color of active links (after mouse-button down, but before mouse-button 
        /// up). See the alink attribute definition in HTML 4.01. This attribute 
        /// is deprecated in HTML 4.01.
        /// </summary>
        string aLink { get; set; }


        /// <summary>URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the background texture tile image. See the background attribute 
        /// definition in HTML 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        string background { get; set; }


        /// <summary>IDocument background color. See the bgcolor attribute definition in HTML 
        /// 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        string bgColor { get; set; }


        /// <summary>Color of links that are not active and unvisited. See the link 
        /// attribute definition in HTML 4.01. This attribute is deprecated in 
        /// HTML 4.01.
        /// </summary>
        string link { get; set; }


        /// <summary>IDocument text color. See the text attribute definition in HTML 4.01. 
        /// This attribute is deprecated in HTML 4.01.
        /// </summary>
        string text { get; set; }


        /// <summary>Color of links that have been visited by the user. See the vlink 
        /// attribute definition in HTML 4.01. This attribute is deprecated in 
        /// HTML 4.01.
        /// </summary>
        string vLink { get; set; }


    }
}
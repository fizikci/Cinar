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

    /// <summary>The LINK element specifies a link to an external resource, and 
    /// defines this document's relationship to that resource (or vice versa). 
    /// See the LINK element definition in HTML 4.01 (see also the 
    /// LinkStyle interface in the IStyleSheet module [<a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>DOM Level 2 Style Sheets and CSS</a>]).
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLLinkElement : IHTMLElement
    {
        /// <summary>Enables/disables the link. This is currently only used for style sheet 
        /// links, and may be used to activate or deactivate style sheets. 
        /// </summary>
        bool disabled { get; set; }


        /// <summary>The character encoding of the resource being linked to. See the charset 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string charset { get; set; }


        /// <summary>The URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string href { get; set; }


        /// <summary>Language code of the linked resource. See the hreflang attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string hreflang { get; set; }


        /// <summary>Designed for use with one or more target media. See the media attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string media { get; set; }


        /// <summary>Forward link type. See the rel attribute definition in HTML 4.01.
        /// </summary>
        string rel { get; set; }


        /// <summary>Reverse link type. See the rev attribute definition in HTML 4.01.
        /// </summary>
        string rev { get; set; }


        /// <summary>Frame to render the resource in. See the target attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string target { get; set; }


        /// <summary>Advisory content type. See the type attribute definition in HTML 4.01.
        /// </summary>
        string type { get; set; }


    }
}
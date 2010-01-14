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

    /// <summary>This contains generic meta-information about the document. See the META 
    /// element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLMetaElement : IHTMLElement
    {
        /// <summary>Associated information. See the content attribute definition in HTML 
        /// 4.01.
        /// </summary>
        string content { get; set; }


        /// <summary>HTTP response header name [<a href='http://www.ietf.org/rfc/rfc2616.txt'>IETF RFC 2616</a>]. See the http-equiv attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string httpEquiv { get; set; }


        /// <summary>Meta information name. See the name attribute definition in HTML 4.01.
        /// </summary>
        string name { get; set; }


        /// <summary>Select form of content. See the scheme attribute definition in HTML 
        /// 4.01.
        /// </summary>
        string scheme { get; set; }


    }
}
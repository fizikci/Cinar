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

    /// <summary>Script statements. See the SCRIPT element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLScriptElement : HTMLElement
    {
        /// <summary>The script content of the element. 
        /// </summary>
        public string text { get; set; }


        /// <summary>Reserved for future use. 
        /// </summary>
        public string htmlFor { get; set; }


        /// <summary>Reserved for future use. 
        /// </summary>
        public string IEvent { get; set; }


        /// <summary>The character encoding of the linked resource. See the charset 
        /// attribute definition in HTML 4.01.
        /// </summary>
        public string charset { get; set; }


        /// <summary>Indicates that the user agent can defer processing of the script. See 
        /// the defer attribute definition in HTML 4.01.
        /// </summary>
        public bool defer { get; set; }


        /// <summary>URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating an external script. See the src attribute definition 
        /// in HTML 4.01.
        /// </summary>
        public string src { get; set; }


        /// <summary>The content type of the script language. See the type attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string type { get; set; }


    }
}
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
     * Script statements. See the SCRIPT element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLScriptElement : IHTMLElement
    {
        /**
         * The script content of the element. 
         */
        string text { get; set; }


        /**
         * Reserved for future use. 
         */
        string htmlFor { get; set; }


        /**
         * Reserved for future use. 
         */
        string IEvent { get; set; }


        /**
         * The character encoding of the linked resource. See the charset 
         * attribute definition in HTML 4.01.
         */
        string charset { get; set; }


        /**
         * Indicates that the user agent can defer processing of the script. See 
         * the defer attribute definition in HTML 4.01.
         */
        bool defer { get; set; }


        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating an external script. See the src attribute definition 
         * in HTML 4.01.
         */
        string src { get; set; }


        /**
         * The content type of the script language. See the type attribute 
         * definition in HTML 4.01.
         */
        string type { get; set; }


    }
}
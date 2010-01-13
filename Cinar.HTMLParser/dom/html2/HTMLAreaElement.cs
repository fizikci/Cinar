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
     * Client-side image map area definition. See the AREA element definition in 
     * HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLAreaElement : IHTMLElement
    {
        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in HTML 4.01.
         */
        string accessKey { get; set; }


        /**
         * Alternate text for user agents not rendering the normal content of this 
         * element. See the alt attribute definition in HTML 4.01.
         */
        string alt { get; set; }


        /**
         * Comma-separated list of lengths, defining an active region geometry. 
         * See also <code>shape</code> for the shape of the region. See the 
         * coords attribute definition in HTML 4.01.
         */
        string coords { get; set; }


        /**
         * The URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute definition in 
         * HTML 4.01.
         */
        string href { get; set; }


        /**
         * Specifies that this area is inactive, i.e., has no associated action. 
         * See the nohref attribute definition in HTML 4.01.
         */
        bool noHref { get; set; }


        /**
         * The shape of the active area. The coordinates are given by 
         * <code>coords</code>. See the shape attribute definition in HTML 4.01.
         */
        string shape { get; set; }


        /**
         * Index that represents the element's position in the tabbing order. See 
         * the tabindex attribute definition in HTML 4.01.
         */
        int tabIndex { get; set; }


        /**
         * Frame to render the resource in. See the target attribute definition in 
         * HTML 4.01.
         */
        string target { get; set; }


    }
}
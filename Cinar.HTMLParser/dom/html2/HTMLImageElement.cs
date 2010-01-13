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
     * Embedded image. See the IMG element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLImageElement : IHTMLElement
    {
        /**
         * The name of the element (for backwards compatibility). 
         */
        string name { get; set; }


        /**
         * Aligns this object (vertically or horizontally) with respect to its 
         * surrounding text. See the align attribute definition in HTML 4.01. 
         * This attribute is deprecated in HTML 4.01.
         */
        string align { get; set; }


        /**
         * Alternate text for user agents not rendering the normal content of this 
         * element. See the alt attribute definition in HTML 4.01.
         */
        string alt { get; set; }


        /**
         * Width of border around image. See the  border attribute definition in 
         * HTML 4.01. This attribute is deprecated in HTML 4.01. Note that the 
         * type of this attribute was <code>DOMString</code> in DOM Level 1 HTML [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>]
         * .
         */
        string border { get; set; }


        /**
         * Height of the image in pixels. See the height attribute definition in 
         * HTML 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 HTML [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        int height { get; set; }


        /**
         * Horizontal space to the left and right of this image in pixels. See the 
         * hspace attribute definition in HTML 4.01. This attribute is 
         * deprecated in HTML 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 HTML [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        int hspace { get; set; }


        /**
         * Use server-side image map. See the ismap attribute definition in HTML 
         * 4.01.
         */
        bool isMap { get; set; }


        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
         * longdesc attribute definition in HTML 4.01.
         */
        string longDesc { get; set; }


        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the source of this image. See the src attribute 
         * definition in HTML 4.01.
         */
        string src { get; set; }


        /**
         * Use client-side image map. See the usemap attribute definition in HTML 
         * 4.01.
         */
        string useMap { get; set; }


        /**
         * Vertical space above and below this image in pixels. See the vspace 
         * attribute definition in HTML 4.01. This attribute is deprecated in 
         * HTML 4.01. Note that the type of this attribute was "DOMString" in 
         * DOM Level 1 HTML [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        int vspace { get; set; }


        /**
         * The width of the image in pixels. See the width attribute definition in 
         * HTML 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 HTML [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        int width { get; set; }


    }
}
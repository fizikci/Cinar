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

namespace org.w3c.dom.html2 {

/**
 * An embedded Java applet. See the APPLET element definition in HTML 4.01. 
 * This element is deprecated in HTML 4.01.
 * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
 */
public interface IHTMLAppletElement : IHTMLElement {
    /**
     * Aligns this object (vertically or horizontally) with respect to its 
     * surrounding text. See the align attribute definition in HTML 4.01. 
     * This attribute is deprecated in HTML 4.01.
     */
    string align {get; set;}


    /**
     * Alternate text for user agents not rendering the normal content of this 
     * element. See the alt attribute definition in HTML 4.01. This 
     * attribute is deprecated in HTML 4.01.
     */
    string alt {get; set;}


    /**
     * Comma-separated archive list. See the archive attribute definition in 
     * HTML 4.01. This attribute is deprecated in HTML 4.01.
     */
    string archive {get; set;}


    /**
     * Applet class file. See the code attribute definition in HTML 4.01. This 
     * attribute is deprecated in HTML 4.01.
     */
    string code {get; set;}


    /**
     * Optional base URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] for applet. See the codebase attribute definition in 
     * HTML 4.01. This attribute is deprecated in HTML 4.01.
     */
    string codeBase {get; set;}


    /**
     * Override height. See the height attribute definition in HTML 4.01. This 
     * attribute is deprecated in HTML 4.01.
     */
    string height {get; set;}


    /**
     * Horizontal space, in pixels, to the left and right of this image, 
     * applet, or object. See the hspace attribute definition in HTML 4.01. 
     * This attribute is deprecated in HTML 4.01.
     * @version DOM Level 2
     */
    int hspace {get; set;}


    /**
     * The name of the applet. See the name attribute definition in HTML 4.01. 
     * This attribute is deprecated in HTML 4.01.
     */
    string name {get; set;}


    /**
     * The value of the "object" attribute. See the object attribute definition
     *  in HTML 4.01. This attribute is deprecated in HTML 4.01. 
     * @version DOM Level 2
     */
    string Object {get; set;}


    /**
     * Vertical space, in pixels, above and below this image, applet, or 
     * object. See the vspace attribute definition in HTML 4.01. This 
     * attribute is deprecated in HTML 4.01.
     * @version DOM Level 2
     */
    int vspace {get; set;}


    /**
     * Override width. See the width attribute definition in HTML 4.01. This 
     * attribute is deprecated in HTML 4.01.
     */
    string width {get;}


}
}
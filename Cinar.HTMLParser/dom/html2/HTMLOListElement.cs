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
     * Ordered list. See the OL element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLOListElement : IHTMLElement
    {
        /**
         * Reduce spacing between list items. See the compact attribute definition 
         * in HTML 4.01. This attribute is deprecated in HTML 4.01.
         */
        bool compact { get; set; }


        /**
         * Starting sequence number. See the start attribute definition in HTML 
         * 4.01. This attribute is deprecated in HTML 4.01.
         */
        int start { get; set; }


        /**
         * Numbering style. See the type attribute definition in HTML 4.01. This 
         * attribute is deprecated in HTML 4.01.
         */
        string type { get; set; }


    }
}
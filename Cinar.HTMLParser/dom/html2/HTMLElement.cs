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

using System;

namespace org.w3c.dom.html2
{


    /// <summary> All HTML element interfaces derive from this class. Elements that only 
    /// expose the HTML core attributes are represented by the base 
    /// HTMLElement interface. These elements are as follows: 
    /// special: SUB, SUP, SPAN, BDOfont: TT, I, B, U, S, STRIKE, BIG, SMALL
    /// phrase: EM, STRONG, DFN, CODE, SAMP, KBD, VAR, CITE, ACRONYM, ABBRlist: 
    /// DD, DTNOFRAMES, NOSCRIPTADDRESS, CENTERThe style attribute 
    /// of an HTML element is accessible through the 
    /// ElementCSSInlineStyle interface which is defined in the CSS 
    /// module [<a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>DOM Level 2 Style Sheets and CSS</a>]. 
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLElement : Element
    {
        /// <summary>The element's identifier. See the id attribute definition in HTML 4.01.
        /// </summary>
        public string id { get; set; }


        /// <summary>The element's advisory title. See the title attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string title { get; set; }


        /// <summary>Language code defined in RFC 1766. See the lang attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string lang { get; set; }


        /// <summary>Specifies the base direction of directionally neutral text and the 
        /// directionality of tables. See the dir attribute definition in HTML 
        /// 4.01.
        /// </summary>
        public string dir { get; set; }


        /// <summary>The class attribute of the element. This attribute has been renamed due 
        /// to conflicts with the "class" keyword exposed by many languages. See 
        /// the class attribute definition in HTML 4.01.
        /// </summary>
        public string className { get; set; }


        public override string getDefaultAttributeValue(string name)
        {
            return String.Empty;
        }
    }
}
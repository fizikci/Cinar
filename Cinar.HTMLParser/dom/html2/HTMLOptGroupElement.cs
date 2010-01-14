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

    /// <summary>Group options together in logical subdivisions. See the OPTGROUP element 
    /// definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLOptGroupElement : IHTMLElement
    {
        /// <summary>The control is unavailable in this context. See the disabled attribute 
        /// definition in HTML 4.01.
        /// </summary>
        bool disabled { get; set; }


        /// <summary>Assigns a label to this option group. See the label attribute definition
        ///  in HTML 4.01.
        /// </summary>
        string label { get; set; }


    }
}
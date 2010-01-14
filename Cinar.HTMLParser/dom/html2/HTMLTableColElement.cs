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

    /// <summary>Regroups the COL and COLGROUP elements. See the 
    /// COL element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLTableColElement : IHTMLElement
    {
        /// <summary>Horizontal alignment of cell data in column. See the align attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string align { get; set; }


        /// <summary>Alignment character for cells in a column. See the char attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string ch { get; set; }


        /// <summary>Offset of alignment character. See the charoff attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string chOff { get; set; }


        /// <summary>Indicates the number of columns in a group or affected by a grouping. 
        /// See the span attribute definition in HTML 4.01.
        /// </summary>
        int span { get; set; }


        /// <summary>Vertical alignment of cell data in column. See the valign attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string vAlign { get; set; }


        /// <summary>Default column width. See the width attribute definition in HTML 4.01.
        /// </summary>
        string Width { get; set; }


    }
}
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

    /// <summary>The object used to represent the TH and TD 
    /// elements. See the TD element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLTableCellElement : IHTMLElement
    {
        /// <summary>The index of this cell in the row, starting from 0. This index is in 
        /// document tree order and not display order.
        /// </summary>
        int cellIndex { get; set; }

        /// <summary>Abbreviation for header cells. See the abbr attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string abbr { get; set; }


        /// <summary>Horizontal alignment of data in cell. See the align attribute definition
        ///  in HTML 4.01.
        /// </summary>
        string align { get; set; }


        /// <summary>Names group of related headers. See the axis attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string axis { get; set; }


        /// <summary>Cell background color. See the bgcolor attribute definition in HTML 
        /// 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        string bgColor { get; set; }


        /// <summary>Alignment character for cells in a column. See the char attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string ch { get; set; }


        /// <summary>Offset of alignment character. See the charoff attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string chOff { get; set; }


        /// <summary>Number of columns spanned by cell. See the colspan attribute definition 
        /// in HTML 4.01.
        /// </summary>
        int colSpan { get; set; }


        /// <summary>List of id attribute values for header cells. See the 
        /// headers attribute definition in HTML 4.01.
        /// </summary>
        string headers { get; set; }


        /// <summary>Cell height. See the height attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        string height { get; set; }


        /// <summary>Suppress word wrapping. See the nowrap attribute definition in HTML 
        /// 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        bool noWrap { get; set; }


        /// <summary>Number of rows spanned by cell. See the rowspan attribute definition in 
        /// HTML 4.01.
        /// </summary>
        int rowSpan { get; set; }


        /// <summary>Scope covered by header cells. See the scope attribute definition in 
        /// HTML 4.01.
        /// </summary>
        string scope { get; set; }


        /// <summary>Vertical alignment of data in cell. See the valign attribute definition 
        /// in HTML 4.01.
        /// </summary>
        string vAlign { get; set; }


        /// <summary>Cell width. See the width attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        string Width { get; set; }


    }
}
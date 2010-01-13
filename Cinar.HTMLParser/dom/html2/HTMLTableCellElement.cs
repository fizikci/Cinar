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
     * The object used to represent the <code>TH</code> and <code>TD</code> 
     * elements. See the TD element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLTableCellElement : IHTMLElement
    {
        /**
         * The index of this cell in the row, starting from 0. This index is in 
         * document tree order and not display order.
         */
        int cellIndex { get; set; }

        /**
         * Abbreviation for header cells. See the abbr attribute definition in 
         * HTML 4.01.
         */
        string abbr { get; set; }


        /**
         * Horizontal alignment of data in cell. See the align attribute definition
         *  in HTML 4.01.
         */
        string align { get; set; }


        /**
         * Names group of related headers. See the axis attribute definition in 
         * HTML 4.01.
         */
        string axis { get; set; }


        /**
         * Cell background color. See the bgcolor attribute definition in HTML 
         * 4.01. This attribute is deprecated in HTML 4.01.
         */
        string bgColor { get; set; }


        /**
         * Alignment character for cells in a column. See the char attribute 
         * definition in HTML 4.01.
         */
        string ch { get; set; }


        /**
         * Offset of alignment character. See the charoff attribute definition in 
         * HTML 4.01.
         */
        string chOff { get; set; }


        /**
         * Number of columns spanned by cell. See the colspan attribute definition 
         * in HTML 4.01.
         */
        int colSpan { get; set; }


        /**
         * List of <code>id</code> attribute values for header cells. See the 
         * headers attribute definition in HTML 4.01.
         */
        string headers { get; set; }


        /**
         * Cell height. See the height attribute definition in HTML 4.01. This 
         * attribute is deprecated in HTML 4.01.
         */
        string height { get; set; }


        /**
         * Suppress word wrapping. See the nowrap attribute definition in HTML 
         * 4.01. This attribute is deprecated in HTML 4.01.
         */
        bool noWrap { get; set; }


        /**
         * Number of rows spanned by cell. See the rowspan attribute definition in 
         * HTML 4.01.
         */
        int rowSpan { get; set; }


        /**
         * Scope covered by header cells. See the scope attribute definition in 
         * HTML 4.01.
         */
        string scope { get; set; }


        /**
         * Vertical alignment of data in cell. See the valign attribute definition 
         * in HTML 4.01.
         */
        string vAlign { get; set; }


        /**
         * Cell width. See the width attribute definition in HTML 4.01. This 
         * attribute is deprecated in HTML 4.01.
         */
        string Width { get; set; }


    }
}
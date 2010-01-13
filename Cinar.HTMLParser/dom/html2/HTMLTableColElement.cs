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
     * Regroups the <code>COL</code> and <code>COLGROUP</code> elements. See the 
     * COL element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLTableColElement : IHTMLElement
    {
        /**
         * Horizontal alignment of cell data in column. See the align attribute 
         * definition in HTML 4.01.
         */
        string align { get; set; }


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
         * Indicates the number of columns in a group or affected by a grouping. 
         * See the span attribute definition in HTML 4.01.
         */
        int span { get; set; }


        /**
         * Vertical alignment of cell data in column. See the valign attribute 
         * definition in HTML 4.01.
         */
        string vAlign { get; set; }


        /**
         * Default column width. See the width attribute definition in HTML 4.01.
         */
        string Width { get; set; }


    }
}
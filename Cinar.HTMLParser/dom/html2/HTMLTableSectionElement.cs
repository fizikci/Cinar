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

    /// <summary>The THEAD, TFOOT, and TBODY 
    /// elements. 
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLTableSectionElement : HTMLElement
    {
        /// <summary>Horizontal alignment of data in cells. See the align 
        /// attribute for HTMLTheadElement for details. 
        /// </summary>
        public string align { get; set; }


        /// <summary>Alignment character for cells in a column. See the char attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string ch { get; set; }


        /// <summary>Offset of alignment character. See the charoff attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string chOff { get; set; }


        /// <summary>Vertical alignment of data in cells. See the valign 
        /// attribute for HTMLTheadElement for details. 
        /// </summary>
        public string vAlign { get; set; }


        /// <summary>The collection of rows in this table section. 
        /// </summary>
        public HTMLCollection Rows { get; set; }

        /// <summary>Insert a row into this section. The new row is inserted immediately 
        /// before the current indexth row in this section. If 
        /// index is -1 or equal to the number of rows in this 
        /// section, the new row is appended.
        /// <param name="index"> The row number where to insert a new row. This index 
        ///   starts from 0 and is relative only to the rows contained inside 
        ///   this section, not all the rows in the table.
        /// <returns>The newly created row.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater than the 
        ///   number of rows of if the index is a negative number other than -1.
        /// @version DOM Level 2
        /// </summary>
        public HTMLElement insertRow(int index); //; // throws DOMException;

        /// <summary>Delete a row from this section.
        /// <param name="index"> The index of the row to be deleted, or -1 to delete the 
        ///   last row. This index starts from 0 and is relative only to the rows 
        ///   contained inside this section, not all the rows in the table.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater than or 
        ///   equal to the number of rows or if the index is a negative number 
        ///   other than -1.
        /// @version DOM Level 2
        /// </summary>
        public void deleteRow(int index); // ; // throws DOMException;

    }
}
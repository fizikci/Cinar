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


    /// <summary>A row in a table. See the TR element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLTableRowElement : IHTMLElement
    {
        /// <summary>This is in logical order and not in document order. The 
        /// rowIndex does take into account sections (
        /// THEAD, TFOOT, or TBODY) within 
        /// the table, placing THEAD rows first in the index, 
        /// followed by TBODY rows, followed by TFOOT 
        /// rows.
        /// @version DOM Level 2
        /// </summary>
        int rowIndex { get; set; }

        /// <summary>The index of this row, relative to the current section (
        /// THEAD, TFOOT, or TBODY), 
        /// starting from 0.
        /// @version DOM Level 2
        /// </summary>
        int sectionRowIndex { get; set; }

        /// <summary>The collection of cells in this row. 
        /// @version DOM Level 2
        /// </summary>
        IHTMLCollection Cells { get; set; }

        /// <summary>Horizontal alignment of data within cells of this row. See the align 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string align { get; set; }


        /// <summary>Background color for rows. See the bgcolor attribute definition in HTML 
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


        /// <summary>Vertical alignment of data within cells of this row. See the valign 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string vAlign { get; set; }


        /// <summary>Insert an empty TD cell into this row. If 
        /// index is -1 or equal to the number of cells, the new 
        /// cell is appended.
        /// <param name="index"> The place to insert the cell, starting from 0.
        /// <returns>The newly created cell.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater 
        ///   than the number of cells or if the index is a negative number other 
        ///   than -1.
        /// @version DOM Level 2
        /// </summary>
        IHTMLElement insertCell(int index); //; // throws DOMException;

        /// <summary>Delete a cell from the current row.
        /// <param name="index"> The index of the cell to delete, starting from 0. If the 
        ///   index is -1 the last cell in the row is deleted.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater 
        ///   than or equal to the number of cells or if the index is a negative 
        ///   number other than -1.
        /// @version DOM Level 2
        /// </summary>
        void deleteCell(int index); // ; // throws DOMException;

    }
}
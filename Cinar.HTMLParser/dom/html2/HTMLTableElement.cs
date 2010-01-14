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


    /// <summary>The create* and delete* methods on the table allow authors to construct and 
    /// modify tables. [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>HTML 4.01</a>] specifies that only one of each of the 
    /// CAPTION, THEAD, and TFOOT elements 
    /// may exist in a table. Therefore, if one exists, and the createTHead() or 
    /// createTFoot() method is called, the method returns the existing THead or 
    /// TFoot element. See the TABLE element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLTableElement : IHTMLElement
    {
        /// <summary>Returns the table's CAPTION, or void if none exists. 
        /// @version DOM Level 2
        /// </summary>
        IHTMLTableCaptionElement Caption { get; set; }
        /// <summary>Returns the table's CAPTION, or void if none exists. 
        /// <exception cref="DOMException">
        ///    HIERARCHY_REQUEST_ERR: if the element is not a CAPTION. 
        /// @version DOM Level 2
        /// </summary>
        void setCaption(IHTMLTableCaptionElement caption); //; // throws DOMException;

        /// <summary>Returns the table's THEAD, or null if none 
        /// exists. 
        /// @version DOM Level 2
        /// </summary>
        IHTMLTableSectionElement THead { get; set; }
        /// <summary>Returns the table's THEAD, or null if none 
        /// exists. 
        /// <exception cref="DOMException">
        ///    HIERARCHY_REQUEST_ERR: if the element is not a THEAD. 
        /// @version DOM Level 2
        /// </summary>
        void setTHead(IHTMLTableSectionElement tHead); //; // throws DOMException;

        /// <summary>Returns the table's TFOOT, or null if none 
        /// exists. 
        /// @version DOM Level 2
        /// </summary>
        IHTMLTableSectionElement TFoot { get; set; }
        /// <summary>Returns the table's TFOOT, or null if none 
        /// exists. 
        /// <exception cref="DOMException">
        ///    HIERARCHY_REQUEST_ERR: if the element is not a TFOOT. 
        /// @version DOM Level 2
        /// </summary>
        void setTFoot(IHTMLTableSectionElement tFoot); //; // throws DOMException;

        /// <summary>Returns a collection of all the rows in the table, including all in 
        /// THEAD, TFOOT, all TBODY 
        /// elements. 
        /// </summary>
        IHTMLCollection Rows { get; set; }

        /// <summary>Returns a collection of the table bodies (including implicit ones).
        /// </summary>
        IHTMLCollection TBodies { get; set; }

        /// <summary>Specifies the table's position with respect to the rest of the 
        /// document. See the align attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        string align { get; set; }


        /// <summary>Cell background color. See the bgcolor attribute definition in HTML 
        /// 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        string bgColor { get; set; }


        /// <summary>The width of the border around the table. See the border attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string border { get; set; }


        /// <summary>Specifies the horizontal and vertical space between cell content and 
        /// cell borders. See the cellpadding attribute definition in HTML 4.01.
        /// </summary>
        string cellPadding { get; set; }


        /// <summary>Specifies the horizontal and vertical separation between cells. See the 
        /// cellspacing attribute definition in HTML 4.01.
        /// </summary>
        string cellSpacing { get; set; }


        /// <summary>Specifies which external table borders to render. See the frame 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string frame { get; set; }


        /// <summary>Specifies which internal table borders to render. See the rules 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string rules { get; set; }


        /// <summary>Description about the purpose or structure of a table. See the summary 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string summary { get; set; }


        /// <summary>Specifies the desired table width. See the width attribute definition 
        /// in HTML 4.01.
        /// </summary>
        string Width { get; set; }


        /// <summary>Create a table header row or return an existing one.
        /// <returns>A new table header element (THEAD).
        /// </summary>
        IHTMLElement createTHead();

        /// <summary>Delete the header from the table, if one exists.
        /// </summary>
        void deleteTHead();

        /// <summary>Create a table footer row or return an existing one.
        /// <returns>A footer element (TFOOT).
        /// </summary>
        IHTMLElement createTFoot();

        /// <summary>Delete the footer from the table, if one exists.
        /// </summary>
        void deleteTFoot();

        /// <summary>Create a new table caption object or return an existing one.
        /// <returns>A CAPTION element.
        /// </summary>
        IHTMLElement createCaption();

        /// <summary>Delete the table caption, if one exists.
        /// </summary>
        void deleteCaption();

        /// <summary>Insert a new empty row in the table. The new row is inserted 
        /// immediately before and in the same section as the current 
        /// indexth row in the table. If index is -1 or 
        /// equal to the number of rows, the new row is appended. In addition, 
        /// when the table is empty the row is inserted into a TBODY 
        /// which is created and inserted into the table.A table row cannot be 
        /// empty according to [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>HTML 4.01</a>].
        /// <param name="index"> The row number where to insert a new row. This index 
        ///   starts from 0 and is relative to the logical order (not document 
        ///   order) of all the rows contained inside the table.
        /// <returns>The newly created row.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater than the 
        ///   number of rows or if the index is a negative number other than -1.
        /// @version DOM Level 2
        /// </summary>
        IHTMLElement insertRow(int index); //; // throws DOMException;

        /// <summary>Delete a table row.
        /// <param name="index"> The index of the row to be deleted. This index starts 
        ///   from 0 and is relative to the logical order (not document order) of 
        ///   all the rows contained inside the table. If the index is -1 the 
        ///   last row in the table is deleted.
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index is greater than or 
        ///   equal to the number of rows or if the index is a negative number 
        ///   other than -1.
        /// @version DOM Level 2
        /// </summary>
        void deleteRow(int index); // ; // throws DOMException;

    }
}
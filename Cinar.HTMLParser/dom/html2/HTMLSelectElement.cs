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

    /// <summary>The select element allows the selection of an option. The contained options 
    /// can be directly accessed through the select element as a collection. See 
    /// the SELECT element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLSelectElement : IHTMLElement
    {
        /// <summary>The type of this form control. This is the string "select-multiple" 
        /// when the multiple attribute is true and the string 
        /// "select-one" when false.
        /// </summary>
        string type { get; set; }

        /// <summary>The ordinal index of the selected option, starting from 0. The value -1 
        /// is returned if no element is selected. If multiple options are 
        /// selected, the index of the first selected option is returned. 
        /// </summary>
        int selectedIndex { get; set; }


        /// <summary> The current form control value (i.e. the value of the currently 
        /// selected option), if multiple options are selected this is the value 
        /// of the first selected option. 
        /// </summary>
        string value { get; set; }


        /// <summary> The number of options in this SELECT. 
        /// @version DOM Level 2
        /// </summary>
        int length { get; set; }
        /// <summary> The number of options in this SELECT. 
        /// <exception cref="DOMException">
        ///    NOT_SUPPORTED_ERR: if setting the length is not allowed by the 
        ///   implementation. 
        /// @version DOM Level 2
        /// </summary>
        void setLength(int length); //; // throws DOMException;

        /// <summary>Returns the FORM element containing this control. Returns 
        /// null if this control is not within the context of a 
        /// form. 
        /// </summary>
        IHTMLFormElement Form { get; set; }

        /// <summary>The collection of OPTION elements contained by this 
        /// element. 
        /// @version DOM Level 2
        /// </summary>
        IHTMLOptionsCollection Options { get; set; }

        /// <summary>The control is unavailable in this context. See the disabled attribute 
        /// definition in HTML 4.01.
        /// </summary>
        bool disabled { get; set; }


        /// <summary>If true, multiple OPTION elements may be selected in this 
        /// SELECT. See the multiple attribute definition in HTML 
        /// 4.01.
        /// </summary>
        bool multiple { get; set; }


        /// <summary>Form control or object name when submitted with a form. See the name 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string name { get; set; }


        /// <summary>Number of visible rows. See the size attribute definition in HTML 4.01.
        /// </summary>
        int size { get; set; }


        /// <summary>Index that represents the element's position in the tabbing order. See 
        /// the tabindex attribute definition in HTML 4.01.
        /// </summary>
        int tabIndex { get; set; }


        /// <summary>Add a new element to the collection of OPTION elements for 
        /// this SELECT. This method is the equivalent of the 
        /// appendChild method of the INode interface if 
        /// the before parameter is null. It is 
        /// equivalent to the insertBefore method on the parent of 
        /// before in all other cases. This method may have no 
        /// effect if the new element is not an OPTION or an 
        /// OPTGROUP.
        /// <param name="element"> The element to add.
        /// <param name="before"> The element to insert before, or null for 
        ///   the tail of the list.
        /// <exception cref="DOMException">
        ///   NOT_FOUND_ERR: Raised if before is not a descendant of 
        ///   the SELECT element. 
        /// </summary>
        void add(IHTMLElement element, IHTMLElement before); //; // throws DOMException;

        /// <summary>Remove an element from the collection of OPTION elements 
        /// for this SELECT. Does nothing if no element has the 
        /// given index.
        /// <param name="index"> The index of the item to remove, starting from 0.
        /// </summary>
        void remove(int index);

        /// <summary>Removes keyboard focus from this element.
        /// </summary>
        void blur();

        /// <summary>Gives keyboard focus to this element.
        /// </summary>
        void focus();

    }
}
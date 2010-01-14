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

    /// <summary>Multi-line text field. See the TEXTAREA element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLTextAreaElement : HTMLElement
    {
        /// <summary>Represents the contents of the element. The value of this attribute 
        /// does not change if the contents of the corresponding form control, in 
        /// an interactive user agent, changes.
        /// @version DOM Level 2
        /// </summary>
        public string defaultValue { get; set; }


        /// <summary>Returns the FORM element containing this control. Returns 
        /// null if this control is not within the context of a 
        /// form. 
        /// </summary>
        public HTMLFormElement Form { get; set; }

        /// <summary>A single character access key to give access to the form control. See 
        /// the accesskey attribute definition in HTML 4.01.
        /// </summary>
        public string accessKey { get; set; }


        /// <summary>Width of control (in characters). See the cols attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public int cols { get; set; }


        /// <summary>The control is unavailable in this context. See the disabled attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public bool disabled { get; set; }


        /// <summary>Form control or object name when submitted with a form. See the name 
        /// attribute definition in HTML 4.01.
        /// </summary>
        public string name { get; set; }


        /// <summary>This control is read-only. See the readonly attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public bool readOnly { get; set; }


        /// <summary>Number of text rows. See the rows attribute definition in HTML 4.01.
        /// </summary>
        public int rows { get; set; }


        /// <summary>Index that represents the element's position in the tabbing order. See 
        /// the tabindex attribute definition in HTML 4.01.
        /// </summary>
        public int tabIndex { get; set; }


        /// <summary>The type of this form control. This the string "textarea".
        /// </summary>
        public string type { get; set; }

        /// <summary>Represents the current contents of the corresponding form control, in 
        /// an interactive user agent. Changing this attribute changes the 
        /// contents of the form control, but does not change the contents of the 
        /// element. If the entirety of the data can not fit into a single 
        /// DOMString, the implementation may truncate the data.
        /// </summary>
        public string value { get; set; }


        /// <summary>Removes keyboard focus from this element.
        /// </summary>
        public void blur();

        /// <summary>Gives keyboard focus to this element.
        /// </summary>
        public void focus();

        /// <summary>Select the contents of the TEXTAREA.
        /// </summary>
        public void select();

    }
}
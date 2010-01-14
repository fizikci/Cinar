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

    /// <summary>A selectable choice. See the OPTION element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLOptionElement : HTMLElement
    {
        /// <summary>Returns the FORM element containing this control. Returns 
        /// null if this control is not within the context of a 
        /// form. 
        /// </summary>
        public HTMLFormElement Form { get; set; }

        /// <summary>Represents the value of the HTML selected attribute. The value of this 
        /// attribute does not change if the state of the corresponding form 
        /// control, in an interactive user agent, changes. See the selected 
        /// attribute definition in HTML 4.01.
        /// @version DOM Level 2
        /// </summary>
        public bool defaultSelected { get; set; }


        /// <summary>The text contained within the option element. 
        /// </summary>
        public string text { get; set; }

        /// <summary>The index of this OPTION in its parent SELECT
        /// , starting from 0.
        /// @version DOM Level 2
        /// </summary>
        public int index { get; set; }

        /// <summary>The control is unavailable in this context. See the disabled attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public bool disabled { get; set; }


        /// <summary>Option label for use in hierarchical menus. See the label attribute 
        /// definition in HTML 4.01.
        /// </summary>
        public string label { get; set; }


        /// <summary>Represents the current state of the corresponding form control, in an 
        /// interactive user agent. Changing this attribute changes the state of 
        /// the form control, but does not change the value of the HTML selected 
        /// attribute of the element.
        /// </summary>
        public bool selected { get; set; }


        /// <summary>The current form control value. See the value attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string value { get; set; }


    }
}
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
     * Push button. See the BUTTON element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLButtonElement : IHTMLElement
    {
        /**
         * Returns the <code>FORM</code> element containing this control. Returns 
         * <code>null</code> if this control is not within the context of a 
         * form. 
         */
        IHTMLFormElement Form { get; set; }

        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in HTML 4.01.
         */
        string accessKey { get; set; }


        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in HTML 4.01.
         */
        bool disabled { get; set; }


        /**
         * Form control or object name when submitted with a form. See the name 
         * attribute definition in HTML 4.01.
         */
        string name { get; set; }


        /**
         * Index that represents the element's position in the tabbing order. See 
         * the tabindex attribute definition in HTML 4.01.
         */
        int tabIndex { get; set; }


        /**
         * The type of button (all lower case). See the type attribute definition 
         * in HTML 4.01.
         */
        string type { get; set; }

        /**
         * The current form control value. See the value attribute definition in 
         * HTML 4.01.
         */
        string value { get; set; }


    }
}
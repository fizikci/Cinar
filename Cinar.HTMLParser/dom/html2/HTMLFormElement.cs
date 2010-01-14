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

    /// <summary>The FORM element encompasses behavior similar to a collection 
    /// and an element. It provides direct access to the contained form controls 
    /// as well as the attributes of the form element. See the FORM element 
    /// definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public class HTMLFormElement : HTMLElement
    {
        /// <summary>Returns a collection of all form control elements in the form. 
        /// </summary>
        public HTMLCollection Elements { get; set; }

        /// <summary>The number of form controls in the form.
        /// </summary>
        public int length { get; set; }

        /// <summary>Names the form. 
        /// </summary>
        public string name { get; set; }


        /// <summary>List of character sets supported by the server. See the accept-charset 
        /// attribute definition in HTML 4.01.
        /// </summary>
        public string acceptCharset { get; set; }


        /// <summary>Server-side form handler. See the action attribute definition in HTML 
        /// 4.01.
        /// </summary>
        public string action { get; set; }


        /// <summary>The content type of the submitted form, generally 
        /// "application/x-www-form-urlencoded". See the enctype attribute 
        /// definition in HTML 4.01. The onsubmit even handler is not guaranteed 
        /// to be triggered when invoking this method. The behavior is 
        /// inconsistent for historical reasons and authors should not rely on a 
        /// particular one. 
        /// </summary>
        public string enctype { get; set; }


        /// <summary>HTTP method [<a href='http://www.ietf.org/rfc/rfc2616.txt'>IETF RFC 2616</a>] used to submit form. See the method attribute definition 
        /// in HTML 4.01.
        /// </summary>
        public string method { get; set; }


        /// <summary>Frame to render the resource in. See the target attribute definition in 
        /// HTML 4.01.
        /// </summary>
        public string target { get; set; }


        /// <summary>Submits the form. It performs the same action as a submit button.
        /// </summary>
        public void submit();

        /// <summary>Restores a form element's default values. It performs the same action 
        /// as a reset button.
        /// </summary>
        public void reset();

    }
}
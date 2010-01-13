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
     * Provides a caption for a <code>FIELDSET</code> grouping. See the LEGEND 
     * element definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLLegendElement : IHTMLElement
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
         * Text alignment relative to <code>FIELDSET</code>. See the align 
         * attribute definition in HTML 4.01. This attribute is deprecated in 
         * HTML 4.01.
         */
        string align { get; set; }


    }
}
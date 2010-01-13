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
     * Parameters fed to the <code>OBJECT</code> element. See the PARAM element 
     * definition in HTML 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
     */
    public interface IHTMLParamElement : IHTMLElement
    {
        /**
         * The name of a run-time parameter. See the name attribute definition in 
         * HTML 4.01.
         */
        string name { get; set; }


        /**
         * Content type for the <code>value</code> attribute when 
         * <code>valuetype</code> has the value "ref". See the type attribute 
         * definition in HTML 4.01.
         */
        string type { get; set; }


        /**
         * The value of a run-time parameter. See the value attribute definition 
         * in HTML 4.01.
         */
        string value { get; set; }


        /**
         * Information about the meaning of the <code>value</code> attribute 
         * value. See the valuetype attribute definition in HTML 4.01.
         */
        string valueType { get; set; }


    }
}
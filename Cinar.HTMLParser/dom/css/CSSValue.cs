/*
 * Copyright (c) 2000 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

namespace org.w3c.dom.css
{

    /**
     *  The <code>CSSValue</code> interface represents a simple or a complex 
     * value. A <code>CSSValue</code> object only occurs in a context of a CSS 
     * property. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
     * @since DOM Level 2
     */
    public interface ICSSValue
    {
        /**
         *  A string representation of the current value. 
         * @exception DOMException
         *    SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
         *   error (according to the attached property) or is unparsable. 
         *   <br>INVALID_MODIFICATION_ERR: Raised if the specified CSS string 
         *   value represents a different type of values than the values allowed 
         *   by the CSS property.
         *   <br> NO_MODIFICATION_ALLOWED_ERR: Raised if this value is readonly. 
         */
        string cssText { get; set; } // throws DOMException;

        /**
         *  A code defining the type of the value as defined above. 
         */
        CSSValueType cssValueType { get; }

    }

    public enum CSSValueType
    { 
        // UnitTypes
        /**
         * The value is inherited and the <code>cssText</code> contains "inherit".
         */
        CSS_INHERIT = 0,
        /**
         * The value is a primitive value and an instance of the 
         * <code>ICSSPrimitiveValue</code> interface can be obtained by using 
         * binding-specific casting methods on this instance of the 
         * <code>CSSValue</code> interface.
         */
        CSS_PRIMITIVE_VALUE = 1,
        /**
         * The value is a <code>CSSValue</code> list and an instance of the 
         * <code>CSSValueList</code> interface can be obtained by using 
         * binding-specific casting methods on this instance of the 
         * <code>CSSValue</code> interface.
         */
        CSS_VALUE_LIST = 2,
        /**
         * The value is a custom value.
         */
        CSS_CUSTOM = 3
    }
}
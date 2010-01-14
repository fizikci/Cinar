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

    /// <summary> The CSSValue interface represents a simple or a complex 
    /// value. A CSSValue object only occurs in a context of a CSS 
    /// property. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class CSSValue
    {
        /// <summary> A string representation of the current value.</summary>
        /// <exception cref="DOMException">
        ///    SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
        ///   error (according to the attached property) or is unparsable. 
        ///   INVALID_MODIFICATION_ERR: Raised if the specified CSS string 
        ///   value represents a different type of values than the values allowed 
        ///   by the CSS property.
        ///    NO_MODIFICATION_ALLOWED_ERR: Raised if this value is readonly. 
        /// </exception>
        public string cssText { get; set; } // throws DOMException;

        /// <summary> A code defining the type of the value as defined above. 
        /// </summary>
        public CSSValueType cssValueType { get; internal set; }

    }

    public enum CSSValueType
    { 
        /// <summary>The value is inherited and the cssText contains "inherit".
        /// </summary>
        CSS_INHERIT = 0,
        /// <summary>The value is a primitive value and an instance of the 
        /// ICSSPrimitiveValue interface can be obtained by using 
        /// binding-specific casting methods on this instance of the 
        /// CSSValue interface.
        /// </summary>
        CSS_PRIMITIVE_VALUE = 1,
        /// <summary>The value is a CSSValue list and an instance of the 
        /// CSSValueList interface can be obtained by using 
        /// binding-specific casting methods on this instance of the 
        /// CSSValue interface.
        /// </summary>
        CSS_VALUE_LIST = 2,
        /// <summary>The value is a custom value.
        /// </summary>
        CSS_CUSTOM = 3
    }
}
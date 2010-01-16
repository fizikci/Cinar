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

using System;

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

        public static CSSValue parseCSSValue(string cssValueText)
        {
            cssValueText = cssValueText.Trim().ToLowerInvariant();
            float fVal;

            if (cssValueText == "inherit")
                return new CSSValue
                {
                    cssText = cssValueText,
                    cssValueType = CSSValueType.CSS_INHERIT
                };
            else if (cssValueText.StartsWith("url"))
                return new CSSPrimitiveValue
                {
                    cssText = cssValueText,
                    cssValueType = CSSValueType.CSS_PRIMITIVE_VALUE,
                    primitiveType = UnitType.CSS_URI,
                    stringValue = cssValueText.Substring(3).Trim('(', ')', '\'', '"')
                };
            else if (cssValueText.StartsWith("#"))
                return new CSSPrimitiveValue
                {
                    cssText = cssValueText,
                    cssValueType = CSSValueType.CSS_PRIMITIVE_VALUE,
                    primitiveType = UnitType.CSS_RGBCOLOR,
                    stringValue = cssValueText
                };
            else if (cssValueText.StartsWith("rgb"))
                return new CSSPrimitiveValue
                {
                    cssText = cssValueText,
                    cssValueType = CSSValueType.CSS_PRIMITIVE_VALUE,
                    primitiveType = UnitType.CSS_RGBCOLOR,
                    rgbColorValue = parseRGBColorValue(cssValueText)
                };
            else if (float.TryParse(cssValueText, out fVal))
                return new CSSPrimitiveValue
                {
                    cssText = cssValueText,
                    cssValueType = CSSValueType.CSS_PRIMITIVE_VALUE,
                    stringValue = fVal.ToString(),
                    primitiveType = UnitType.CSS_NUMBER
                };
            else if (cssValueText.EndsWith("%"))
            {
                CSSPrimitiveValue val = (CSSPrimitiveValue)parseCSSValue(cssValueText.Substring(0, cssValueText.Length - 1));
                val.primitiveType = UnitType.CSS_PERCENTAGE;
                return val;
            }
            else if (Char.IsDigit(cssValueText[0]) && Char.IsLetter(cssValueText[cssValueText.Length - 1]))
            {
                return parseValueAndUnit(cssValueText);
            }

            throw ErrorMessages.Get(DOMExceptionCodes.SYNTAX_ERR);
        }

        public static CSSValue parseValueAndUnit(string cssValueText)
        {
            foreach (string name in Enum.GetNames(typeof(UnitType)))
            {
                string unit = name.Substring(4).ToLowerInvariant();
                string number = cssValueText.Substring(0, cssValueText.Length - unit.Length);
                float fVal;
                if (float.TryParse(number, out fVal))
                {
                    return new CSSPrimitiveValue
                    {
                        cssText = cssValueText,
                        cssValueType = CSSValueType.CSS_PRIMITIVE_VALUE,
                        primitiveType = (UnitType)Enum.Parse(typeof(UnitType), name),
                        stringValue = fVal.ToString()
                    };
                }
            }
            throw ErrorMessages.Get(DOMExceptionCodes.SYNTAX_ERR);
        }

        public static RGBColor parseRGBColorValue(string cssValueText)
        {
            string[] parts = cssValueText.Substring(3).Trim('(', ')').Split(',');
            return new RGBColor
            {
                red = (CSSPrimitiveValue)parseCSSValue(parts[0]),
                green = (CSSPrimitiveValue)parseCSSValue(parts[1]),
                blue = (CSSPrimitiveValue)parseCSSValue(parts[2])
            };
        }

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
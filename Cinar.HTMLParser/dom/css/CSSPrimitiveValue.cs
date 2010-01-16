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

    /// <summary> The ICSSPrimitiveValue interface represents a single CSS value
    /// . This interface may be used to determine the value of a specific style 
    /// property currently set in a block or to set a specific style property 
    /// explicitly within the block. An instance of this interface might be 
    /// obtained from the getPropertyCSSValue method of the 
    /// ICSSStyleDeclaration interface. A 
    /// ICSSPrimitiveValue object only occurs in a context of a CSS 
    /// property. 
    ///  Conversions are allowed between absolute values (from millimeters to 
    /// centimeters, from degrees to radians, and so on) but not between relative 
    /// values. (For example, a pixel value cannot be converted to a centimeter 
    /// value.) Percentage values can't be converted since they are relative to 
    /// the parent value (or another property value). There is one exception for 
    /// color percentage values: since a color percentage value is relative to 
    /// the range 0-255, a color percentage value can be converted to a number; 
    /// (see also the RGBColor interface). 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class CSSPrimitiveValue : CSSValue
    {

        /// <summary>The type of the value as defined by the constants specified above.
        /// </summary>
        public UnitType primitiveType { get; internal set; }

        /// <summary> A method to set the float value with a specified unit. If the property 
        /// attached with this value can not accept the specified unit or the 
        /// float value, the value will be unchanged and a 
        /// DOMException will be raised. </summary>
        /// <param name="unitType"> A unit code as defined above. The unit code can only 
        ///   be a float unit type (i.e. CSS_NUMBER, 
        ///   CSS_PERCENTAGE, CSS_EMS, 
        ///   CSS_EXS, CSS_PX, CSS_CM, 
        ///   CSS_MM, CSS_IN, CSS_PT, 
        ///   CSS_PC, CSS_DEG, CSS_RAD, 
        ///   CSS_GRAD, CSS_MS, CSS_S, 
        ///   CSS_HZ, CSS_KHZ, 
        ///   CSS_DIMENSION).</param>
        /// <param name="floatValue"> The new float value.</param>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the attached property doesn't support 
        ///   the float value or the unit type.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public void setFloatValue(UnitType unitType,
                                  float floatValue)
        {
            throw new NotImplementedException();
        }

        /// <summary> This method is used to get a float value in a specified unit. If this 
        /// CSS value doesn't contain a float value or can't be converted into 
        /// the specified unit, a DOMException is raised. </summary>
        /// <param name="unitType"> A unit code to get the float value. The unit code can 
        ///   only be a float unit type (i.e. CSS_NUMBER, 
        ///   CSS_PERCENTAGE, CSS_EMS, 
        ///   CSS_EXS, CSS_PX, CSS_CM, 
        ///   CSS_MM, CSS_IN, CSS_PT, 
        ///   CSS_PC, CSS_DEG, CSS_RAD, 
        ///   CSS_GRAD, CSS_MS, CSS_S, 
        ///   CSS_HZ, CSS_KHZ, 
        ///   CSS_DIMENSION).</param>
        /// <returns> The float value in the specified unit. </returns>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the CSS value doesn't contain a float 
        ///   value or if the float value can't be converted into the specified 
        ///   unit. 
        /// </exception>
        public float getFloatValue(UnitType unitType)
        {
            throw new NotImplementedException();
        }

        /// <summary> A method to set the string value with the specified unit. If the 
        /// property attached to this value can't accept the specified unit or 
        /// the string value, the value will be unchanged and a 
        /// DOMException will be raised.</summary>
        /// <param name="stringType"> A string code as defined above. The string code can 
        ///   only be a string unit type (i.e. CSS_STRING, 
        ///   CSS_URI, CSS_IDENT, and 
        ///   CSS_ATTR).</param>
        /// <param name="stringValue"> The new string value.</param>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the CSS value doesn't contain a string 
        ///   value or if the string value can't be converted into the specified 
        ///   unit.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public void setStringValue(UnitType stringType,
                                   string stringValue)
        {
            throw new NotImplementedException();
        }

        /// <summary> This method is used to get the string value. If the CSS value doesn't 
        /// contain a string value, a DOMException is raised.  Some 
        /// properties (like 'font-family' or 'voice-family') convert a 
        /// whitespace separated list of idents to a string. </summary>
        /// <returns> The string value in the current unit. The current 
        ///   primitiveType can only be a string unit type (i.e. 
        ///   CSS_STRING, CSS_URI, 
        ///   CSS_IDENT and CSS_ATTR). </returns>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the CSS value doesn't contain a string 
        ///   value. 
        /// </exception>
        public string stringValue { get; internal set; } // throws DOMException;

        /// <summary> This method is used to get the Counter value. If this CSS value 
        /// doesn't contain a counter value, a DOMException is 
        /// raised. Modification to the corresponding style property can be 
        /// achieved using the Counter interface.</summary>
        /// <returns>The Counter value.</returns>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the CSS value doesn't contain a 
        ///   Counter value (e.g. this is not CSS_COUNTER). 
        /// </exception>
        public Counter counterValue { get; internal set; } // throws DOMException;

        /// <summary> This method is used to get the Rect value. If this CSS value doesn't 
        /// contain a rect value, a DOMException is raised. 
        /// Modification to the corresponding style property can be achieved 
        /// using the Rect interface.</summary>
        /// <returns>The Rect value.</returns>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the CSS value doesn't contain a Rect 
        ///   value. (e.g. this is not CSS_RECT). 
        /// </exception>
        public Rect rectValue { get; internal set; } // throws DOMException;

        /// <summary> This method is used to get the RGB color. If this CSS value doesn't 
        /// contain a RGB color value, a DOMException is raised. 
        /// Modification to the corresponding style property can be achieved 
        /// using the RGBColor interface.</summary>
        /// <returns>the RGB color value.</returns>
        /// <exception cref="DOMException">
        ///    INVALID_ACCESS_ERR: Raised if the attached property can't return a 
        ///   RGB color value (e.g. this is not CSS_RGBCOLOR). 
        /// </exception>
        public RGBColor rgbColorValue { get; internal set; } // throws DOMException;

    }

    public enum UnitType
    {
        /// <summary>The value is not a recognized CSS2 value. The value can only be 
        /// obtained by using the cssText attribute.
        /// </summary>
        CSS_UNKNOWN = 0,
        /// <summary>The value is a simple number. The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_NUMBER = 1,
        /// <summary>The value is a percentage. The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_PERCENTAGE = 2,
        /// <summary>The value is a length (ems). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_EMS = 3,
        /// <summary>The value is a length (exs). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_EXS = 4,
        /// <summary>The value is a length (px). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_PX = 5,
        /// <summary>The value is a length (cm). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_CM = 6,
        /// <summary>The value is a length (mm). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_MM = 7,
        /// <summary>The value is a length (in). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_IN = 8,
        /// <summary>The value is a length (pt). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_PT = 9,
        /// <summary>The value is a length (pc). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_PC = 10,
        /// <summary>The value is an angle (deg). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_DEG = 11,
        /// <summary>The value is an angle (rad). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_RAD = 12,
        /// <summary>The value is an angle (grad). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_GRAD = 13,
        /// <summary>The value is a time (ms). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_MS = 14,
        /// <summary>The value is a time (s). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_S = 15,
        /// <summary>The value is a frequency (Hz). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_HZ = 16,
        /// <summary>The value is a frequency (kHz). The value can be obtained by using the 
        /// getFloatValue method.
        /// </summary>
        CSS_KHZ = 17,
        /// <summary>The value is a number with an unknown dimension. The value can be 
        /// obtained by using the getFloatValue method.
        /// </summary>
        CSS_DIMENSION = 18,
        /// <summary>The value is a STRING. The value can be obtained by using the 
        /// getStringValue method.
        /// </summary>
        CSS_STRING = 19,
        /// <summary>The value is a URI. The value can be obtained by using the 
        /// getStringValue method.
        /// </summary>
        CSS_URI = 20,
        /// <summary>The value is an identifier. The value can be obtained by using the 
        /// getStringValue method.
        /// </summary>
        CSS_IDENT = 21,
        /// <summary>The value is a attribute function. The value can be obtained by using 
        /// the getStringValue method.
        /// </summary>
        CSS_ATTR = 22,
        /// <summary>The value is a counter or counters function. The value can be obtained 
        /// by using the getCounterValue method.
        /// </summary>
        CSS_COUNTER = 23,
        /// <summary>The value is a rect function. The value can be obtained by using the 
        /// getRectValue method.
        /// </summary>
        CSS_RECT = 24,
        /// <summary>The value is a RGB color. The value can be obtained by using the 
        /// getRGBColorValue method.
        /// </summary>
        CSS_RGBCOLOR = 25
    }
}
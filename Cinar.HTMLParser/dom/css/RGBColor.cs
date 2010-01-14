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

    /// <summary> The RGBColor interface is used to represent any RGB color 
    /// value. This interface reflects the values in the underlying style 
    /// property. Hence, modifications made to the ICSSPrimitiveValue 
    /// objects modify the style property. 
    ///  A specified RGB color is not clipped (even if the number is outside the 
    /// range 0-255 or 0%-100%). A computed RGB color is clipped depending on the 
    /// device. 
    ///  Even if a style sheet can only contain an integer for a color value, 
    /// the internal storage of this integer is a float, and this can be used as 
    /// a float in the specified or the computed style. 
    ///  A color percentage value can always be converted to a number and vice 
    /// versa. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IRGBColor
    {
        /// <summary> This attribute is used for the red value of the RGB color. 
        /// </summary>
        ICSSPrimitiveValue red { get; }

        /// <summary> This attribute is used for the green value of the RGB color. 
        /// </summary>
        ICSSPrimitiveValue green { get; }

        /// <summary> This attribute is used for the blue value of the RGB color. 
        /// </summary>
        ICSSPrimitiveValue blue { get; }

    }
}
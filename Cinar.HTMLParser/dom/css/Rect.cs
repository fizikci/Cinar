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

    /// <summary> The Rect interface is used to represent any rect value. This 
    /// interface reflects the values in the underlying style property. Hence, 
    /// modifications made to the ICSSPrimitiveValue objects modify 
    /// the style property. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IRect
    {
        /// <summary> This attribute is used for the top of the rect. 
        /// </summary>
        ICSSPrimitiveValue top { get; }

        /// <summary> This attribute is used for the right of the rect. 
        /// </summary>
        ICSSPrimitiveValue right { get; }

        /// <summary> This attribute is used for the bottom of the rect. 
        /// </summary>
        ICSSPrimitiveValue bottom { get; }

        /// <summary> This attribute is used for the left of the rect. 
        /// </summary>
        ICSSPrimitiveValue left { get; }

    }
}
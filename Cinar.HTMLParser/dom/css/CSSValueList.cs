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

    /// <summary>The CSSValueList interface provides the abstraction of an 
    /// ordered collection of CSS values.
    ///  Some properties allow an empty list into their syntax. In that case, 
    /// these properties take the none identifier. So, an empty list 
    /// means that the property has the value none. 
    ///  The items in the CSSValueList are accessible via an 
    /// integral index, starting from 0. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface ICSSValueList : ICSSValue
    {
        /// <summary>The number of CSSValues in the list. The range of valid 
        /// values of the indices is 0 to length-1 
        /// inclusive.
        /// </summary>
        int length { get; }

        /// <summary>Used to retrieve a CSSValue by ordinal index. The order in 
        /// this collection represents the order of the values in the CSS style 
        /// property. If index is greater than or equal to the number of values 
        /// in the list, this returns null.</summary>
        /// <param name="indexIndex"> into the collection.</param>
        /// <returns>The CSSValue at the index position 
        ///   in the CSSValueList, or null if that is 
        ///   not a valid index.
        /// </returns>
        ICSSValue item(int index);

    }
}
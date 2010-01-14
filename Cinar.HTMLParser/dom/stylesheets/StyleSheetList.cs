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

namespace org.w3c.dom.stylesheets
{

    /// <summary>The IStyleSheetList interface provides the abstraction of an 
    /// ordered collection of style sheets. 
    ///  The items in the IStyleSheetList are accessible via an 
    /// integral index, starting from 0. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IStyleSheetList
    {
        /// <summary> The number of StyleSheets in the list. The range of valid 
        /// child stylesheet indices is 0 to length-1 
        /// inclusive. 
        /// </summary>
        int length { get; }

        /// <summary> Used to retrieve a style sheet by ordinal index. If index is greater 
        /// than or equal to the number of style sheets in the list, this returns 
        /// null. </summary>
        /// <param name="index">Index into the collection</param>
        /// <returns>The style sheet at the index position in the 
        ///   IStyleSheetList, or null if that is not a 
        ///   valid index. 
        /// </returns>
        IStyleSheet item(int index);

    }
}
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

namespace org.w3c.dom.ranges
{

    /// <summary>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class DocumentRange
    {
        /// <summary>This interface can be obtained from the object implementing the 
        /// IDocument interface using binding-specific casting 
        /// methods.</summary>
        /// <returns>The initial state of the IRange returned from this method is 
        ///   such that both of its boundary-points are positioned at the 
        ///   beginning of the corresponding IDocument, before any content. The 
        ///   IRange returned can only be used to select content associated with 
        ///   this IDocument, or with DocumentFragments and Attrs for which this 
        ///   IDocument is the ownerDocument.
        /// </returns>
        public Range createRange();

    }
}
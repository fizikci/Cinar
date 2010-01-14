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

namespace org.w3c.dom
{

    /// <summary>The Text interface inherits from CharacterData 
    /// and represents the textual content (termed character data in XML) of an 
    /// IElement or Attr. If there is no markup inside 
    /// an element's content, the text is contained in a single object 
    /// implementing the Text interface that is the only child of 
    /// the element. If there is markup, it is parsed into the information items 
    /// (elements, comments, etc.) and Text nodes that form the list 
    /// of children of the element.
    /// When a document is first made available via the DOM, there is only one 
    /// Text node for each block of text. Users may create adjacent 
    /// Text nodes that represent the contents of a given element 
    /// without any intervening markup, but should be aware that there is no way 
    /// to represent the separations between these nodes in XML or HTML, so they 
    /// will not (in general) persist between DOM editing sessions. The 
    /// normalize() method on INode merges any such 
    /// adjacent Text objects into a single node for each block of 
    /// text.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IText : ICharacterData
    {
        /// <summary>Breaks this node into two nodes at the specified offset, 
        /// keeping both in the tree as siblings. After being split, this node 
        /// will contain all the content up to the offset point. A 
        /// new node of the same type, which contains all the content at and 
        /// after the offset point, is returned. If the original 
        /// node had a parent node, the new node is inserted as the next sibling 
        /// of the original node. When the offset is equal to the 
        /// length of this node, the new node has no data.</summary>
        /// <param name="offset">The 16-bit unit offset at which to split, starting from 
        ///   0.</param>
        /// <returns>The new node, of the same type as this node.</returns>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified offset is negative or greater 
        ///   than the number of 16-bit units in data.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        IText splitText(int offset)
                              ; // throws DOMException;

    }
}
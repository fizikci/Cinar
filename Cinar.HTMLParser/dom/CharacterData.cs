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

    /// <summary>The CharacterData interface : INode with a set of 
    /// attributes and methods for accessing character data in the DOM. For 
    /// clarity this set is defined here rather than on each object that uses 
    /// these attributes and methods. No DOM objects correspond directly to 
    /// CharacterData, though Text and others do 
    /// inherit the interface from it. All offsets in this interface 
    /// start from 0.
    /// As explained in the DOMString interface, text strings in 
    /// the DOM are represented in UTF-16, i.e. as a sequence of 16-bit units. In 
    /// the following, the term 16-bit units is used whenever necessary to 
    /// indicate that indexing on CharacterData is done in 16-bit units.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface ICharacterData : INode
    {
        /// <summary>The character data of the node that implements this interface. The DOM 
        /// implementation may not put arbitrary limits on the amount of data 
        /// that may be stored in a CharacterData node. However, 
        /// implementation limits may mean that the entirety of a node's data may 
        /// not fit into a single DOMString. In such cases, the user 
        /// may call substringData to retrieve the data in 
        /// appropriately sized pieces.</summary>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised when the node is readonly.</exception>
        /// <exception cref="DOMException">
        ///   DOMSTRING_SIZE_ERR: Raised when it would return more characters than 
        ///   fit in a DOMString variable on the implementation 
        ///   platform.
        /// </exception>
        string data{get; set;}

        /// <summary>The number of 16-bit units that are available through data 
        /// and the substringData method below. This may have the 
        /// value zero, i.e., CharacterData nodes may be empty.
        /// </summary>
        int length { get; }

        /// <summary>Extracts a range of data from the node.</summary>
        /// <param name="offset">Start offset of substring to extract.</param>
        /// <param name="count">The number of 16-bit units to extract.</param>
        /// <returns>The specified substring. If the sum of offset and 
        ///   count exceeds the length, then all 16-bit 
        ///   units to the end of the data are returned.</returns>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified offset is 
        ///   negative or greater than the number of 16-bit units in 
        ///   data, or if the specified count is 
        ///   negative.
        ///   DOMSTRING_SIZE_ERR: Raised if the specified range of text does 
        ///   not fit into a DOMString.
        /// </exception>
        string substringData(int offset,
                                    int count)
                                    ; // throws DOMException;

        /// <summary>Append the string to the end of the character data of the node. Upon 
        /// success, data provides access to the concatenation of 
        /// data and the DOMString specified.</summary>
        /// <param name="arg">The DOMString to append.</param>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void appendData(string arg)
                               ; // throws DOMException;

        /// <summary>Insert a string at the specified 16-bit unit offset.</summary>
        /// <param name="offset">The character offset at which to insert.</param>
        /// <param name="arg">The DOMString to insert.</param>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified offset is 
        ///   negative or greater than the number of 16-bit units in 
        ///   data.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void insertData(int offset,
                               string arg)
                               ; // throws DOMException;

        /// <summary>Remove a range of 16-bit units from the node. Upon success, 
        /// data and length reflect the change.</summary>
        /// <param name="offset">The offset from which to start removing.</param>
        /// <param name="count">The number of 16-bit units to delete. If the sum of 
        ///   offset and count exceeds 
        ///   length then all 16-bit units from offset 
        ///   to the end of the data are deleted.</param>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified offset is 
        ///   negative or greater than the number of 16-bit units in 
        ///   data, or if the specified count is 
        ///   negative.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void deleteData(int offset,
                               int count)
                               ; // throws DOMException;

        /// <summary>Replace the characters starting at the specified 16-bit unit offset 
        /// with the specified string.</summary>
        /// <param name="offset">The offset from which to start replacing.</param>
        /// <param name="count">The number of 16-bit units to replace. If the sum of 
        ///   offset and count exceeds 
        ///   length, then all 16-bit units to the end of the data 
        ///   are replaced; (i.e., the effect is the same as a remove
        ///    method call with the same range, followed by an append
        ///    method invocation).</param>
        /// <param name="arg">The DOMString with which the range must be 
        ///   replaced.</param>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified offset is 
        ///   negative or greater than the number of 16-bit units in 
        ///   data, or if the specified count is 
        ///   negative.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this node is readonly.
        /// </exception>
        void replaceData(int offset,
                                int count,
                                string arg)
                                ; // throws DOMException;

    }
}
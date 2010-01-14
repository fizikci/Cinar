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

    /// <summary> The IMediaList interface provides the abstraction of an 
    /// ordered collection of media, without defining or constraining how this 
    /// collection is implemented. An empty list is the same as a list that 
    /// contains the medium "all". 
    ///  The items in the IMediaList are accessible via an integral 
    /// index, starting from 0. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IMediaList
    {
        /// <summary> The parsable textual representation of the media list. This is a 
        /// comma-separated list of media.</summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified string value has a syntax error 
        ///   and is unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this media list is 
        ///   readonly.
        /// </exception>
        string mediaText { get; set; } // throws DOMException;

        /// <summary> The number of media in the list. The range of valid media is 
        /// 0 to length-1 inclusive. 
        /// </summary>
        int length { get; }

        /// <summary> Returns the indexth in the list. If index is 
        /// greater than or equal to the number of media in the list, this 
        /// returns null. </summary>
        /// <param name="index"> Index into the collection. </param>
        /// <returns> The medium at the indexth position in the 
        ///   IMediaList, or null if that is not a valid 
        ///   index. 
        /// </returns>
        string item(int index);

        /// <summary> Deletes the medium indicated by oldMedium from the list. </summary>
        /// <param name="oldMedium">The medium to delete in the media list.</param>
        /// <exception cref="DOMException">
        ///    NO_MODIFICATION_ALLOWED_ERR: Raised if this list is readonly. 
        ///    NOT_FOUND_ERR: Raised if oldMedium is not in the 
        ///   list. 
        /// </exception>
        void deleteMedium(string oldMedium); // throws DOMException;

        /// <summary> Adds the medium newMedium to the end of the list. If the 
        /// newMedium is already used, it is first removed.</summary>
        /// <param name="newMedium">The new medium to add.</param>
        /// <exception cref="DOMException">
        ///    INVALID_CHARACTER_ERR: If the medium contains characters that are 
        ///   invalid in the underlying style language. 
        ///    NO_MODIFICATION_ALLOWED_ERR: Raised if this list is readonly. 
        /// </exception>
        void appendMedium(string newMedium); // throws DOMException;

    }
}
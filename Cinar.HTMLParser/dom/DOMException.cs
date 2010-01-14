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

namespace org.w3c.dom
{

    /// <summary>DOM operations only raise exceptions in "exceptional" circumstances, i.e., 
    /// when an operation is impossible to perform (either for logical reasons, 
    /// because data is lost, or because the implementation has become unstable). 
    /// In general, DOM methods return specific error values in ordinary 
    /// processing situations, such as out-of-bound errors when using 
    /// NodeList. 
    /// Implementations should raise other exceptions under other circumstances. 
    /// For example, implementations should raise an implementation-dependent 
    /// exception if a null argument is passed. 
    /// Some languages and object systems do not support the concept of 
    /// exceptions. For such systems, error conditions may be indicated using 
    /// native error reporting mechanisms. For some bindings, for example, 
    /// methods may return error codes similar to those listed in the 
    /// corresponding method descriptions.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public class DOMException : Exception
    {
        DOMException(DOMExceptionCodes code, string message)
            : base(message)
        {
            this.code = code;
        }
        DOMExceptionCodes code;

    }

    public enum DOMExceptionCodes
    {
        /// <summary>If index or size is negative, or greater than the allowed value
        /// </summary>
        INDEX_SIZE_ERR = 1,
        /// <summary>If the specified range of text does not fit into a DOMString
        /// </summary>
        DOMSTRING_SIZE_ERR = 2,
        /// <summary>If any node is inserted somewhere it doesn't belong
        /// </summary>
        HIERARCHY_REQUEST_ERR = 3,
        /// <summary>If a node is used in a different document than the one that created it 
        /// (that doesn't support it)
        /// </summary>
        WRONG_DOCUMENT_ERR = 4,
        /// <summary>If an invalid or illegal character is specified, such as in a name. See 
        /// production 2 in the XML specification for the definition of a legal 
        /// character, and production 5 for the definition of a legal name 
        /// character.
        /// </summary>
        INVALID_CHARACTER_ERR = 5,
        /// <summary>If data is specified for a node which does not support data
        /// </summary>
        NO_DATA_ALLOWED_ERR = 6,
        /// <summary>If an attempt is made to modify an object where modifications are not 
        /// allowed
        /// </summary>
        NO_MODIFICATION_ALLOWED_ERR = 7,
        /// <summary>If an attempt is made to reference a node in a context where it does 
        /// not exist
        /// </summary>
        NOT_FOUND_ERR = 8,
        /// <summary>If the implementation does not support the requested type of object or 
        /// operation.
        /// </summary>
        NOT_SUPPORTED_ERR = 9,
        /// <summary>If an attempt is made to add an attribute that is already in use 
        /// elsewhere
        /// </summary>
        INUSE_ATTRIBUTE_ERR = 10,
        /// <summary>If an attempt is made to use an object that is not, or is no longer, 
        /// usable.
        /// @since DOM Level 2
        /// </summary>
        INVALID_STATE_ERR = 11,
        /// <summary>If an invalid or illegal string is specified.
        /// @since DOM Level 2
        /// </summary>
        SYNTAX_ERR = 12,
        /// <summary>If an attempt is made to modify the type of the underlying object.
        /// @since DOM Level 2
        /// </summary>
        INVALID_MODIFICATION_ERR = 13,
        /// <summary>If an attempt is made to create or change an object in a way which is 
        /// incorrect with regard to namespaces.
        /// @since DOM Level 2
        /// </summary>
        NAMESPACE_ERR = 14,
        /// <summary>If a parameter or an operation is not supported by the underlying 
        /// object.
        /// @since DOM Level 2
        /// </summary>
        INVALID_ACCESS_ERR = 15
    }
}
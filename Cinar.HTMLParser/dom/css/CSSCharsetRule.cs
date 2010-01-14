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

    /// <summary> The CSSCharsetRule interface represents a @charset rule in a 
    /// CSS style sheet. The value of the encoding attribute does 
    /// not affect the encoding of text data in the DOM objects; this encoding is 
    /// always UTF-16. After a stylesheet is loaded, the value of the 
    /// encoding attribute is the value found in the 
    /// @charset rule. If there was no @charset in the 
    /// original document, then no CSSCharsetRule is created. The 
    /// value of the encoding attribute may also be used as a hint 
    /// for the encoding used on serialization of the style sheet. 
    ///  The value of the @charset rule (and therefore of the 
    /// CSSCharsetRule) may not correspond to the encoding the 
    /// document actually came in; character encoding information e.g. in an HTTP 
    /// header, has priority (see CSS document representation) but this is not 
    /// reflected in the CSSCharsetRule. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface ICSSCharsetRule : ICSSRule
    {
        /// <summary> The encoding information used in this @charset rule.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified encoding value has a syntax error 
        ///   and is unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this encoding rule is 
        ///   readonly.
        /// </exception>
        string encoding { get; set; } // throws DOMException;

    }
}
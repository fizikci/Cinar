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


    /// <summary> The CSSStyleRule interface represents a single rule set in a 
    /// CSS style sheet. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface ICSSStyleRule : ICSSRule
    {
        /// <summary> The textual representation of the selector for the rule set. The 
        /// implementation may have stripped out insignificant whitespace while 
        /// parsing the selector.</summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
        ///   error and is unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this rule is readonly.
        /// </exception>
        string selectorText { get; set; } // throws DOMException;

        /// <summary> The declaration-block of this rule set. 
        /// </summary>
        ICSSStyleDeclaration style { get; }

    }
}
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

namespace org.w3c.dom.css
{


    /// <summary> This interface allows the DOM user to create a CSSStyleSheet 
    /// outside the context of a document. There is no way to associate the new 
    /// CSSStyleSheet with a document in DOM Level 2. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM   Level 2
    /// </summary>
    public class DOMImplementationCSS : DOMImplementation
    {
        /// <summary>Creates a new CSSStyleSheet.</summary>
        /// <param name="title"> The advisory title. See also the  section.</param>
        /// <param name="media"> The comma-separated list of media associated with the new 
        ///   style sheet. See also the  section.</param>
        /// <returns>A new CSS style sheet.</returns>
        /// <exception cref="DOMException">
        ///    SYNTAX_ERR: Raised if the specified media string value has a syntax 
        ///   error and is unparsable. 
        /// </exception>
        public CSSStyleSheet createCSSStyleSheet(string title,
                                                 string media)
        {
            throw new NotImplementedException();
        }
    }
}
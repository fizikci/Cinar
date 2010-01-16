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

    /// <summary> Inline style information attached to elements is exposed through the 
    /// style attribute. This represents the contents of the STYLE 
    /// attribute for HTML elements (or elements in other schemas or DTDs which 
    /// use the STYLE attribute in the same way). The expectation is that an 
    /// instance of the ElementCSSInlineStyle interface can be obtained by using 
    /// binding-specific casting methods on an instance of the IElement interface 
    /// when the element supports inline CSS style informations. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IElementCSSInlineStyle
    {
        /// <summary> The style attribute. 
        /// </summary>
        CSSStyleDeclaration style { get; }

    }
}
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

using org.w3c.dom.stylesheets;

namespace org.w3c.dom.css
{

    /// <summary> The CSSImportRule interface represents a @import rule within 
    /// a CSS style sheet. The @import rule is used to import style 
    /// rules from other style sheets. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface ICSSImportRule : ICSSRule
    {
        /// <summary> The location of the style sheet to be imported. The attribute will not 
        /// contain the "url(...)" specifier around the URI. 
        /// </summary>
        string href { get; }

        /// <summary> A list of media types for which this style sheet may be used. 
        /// </summary>
        IMediaList media { get;}

        /// <summary>The style sheet referred to by this rule, if it has been loaded. The 
        /// value of this attribute is null if the style sheet has 
        /// not yet been loaded or if it will not be loaded (e.g. if the style 
        /// sheet is for a media type not supported by the user agent). 
        /// </summary>
        ICSSStyleSheet styleSheet { get; }

    }
}
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

    /// <summary> The CSSRule interface is the abstract base interface for any 
    /// type of CSS statement. This includes both rule sets and at-rules. An 
    /// implementation is expected to preserve all rules specified in a CSS style 
    /// sheet, even if the rule is not recognized by the parser. Unrecognized 
    /// rules are represented using the CSSUnknownRule interface. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface ICSSRule
    {
        /// <summary> The type of the rule, as defined above. The expectation is that 
        /// binding-specific casting methods can be used to cast down from an 
        /// instance of the CSSRule interface to the specific 
        /// derived interface implied by the type. 
        /// </summary>
        CSSRuleType type { get; }

        /// <summary> The parsable textual representation of the rule. This reflects the 
        /// current state of the rule and not its initial value. </summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
        ///   error and is unparsable.
        ///   INVALID_MODIFICATION_ERR: Raised if the specified CSS string 
        ///   value represents a different type of rule than the current one.
        ///   HIERARCHY_REQUEST_ERR: Raised if the rule cannot be inserted at 
        ///   this point in the style sheet.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if the rule is readonly.
        /// </exception>
        string cssText { get; set; } // throws DOMException;

        /// <summary> The style sheet that contains this rule. 
        /// </summary>
        ICSSStyleSheet parentStyleSheet { get; }

        /// <summary> If this rule is contained inside another rule (e.g. a style rule 
        /// inside an @media block), this is the containing rule. If this rule is 
        /// not nested inside any other rules, this returns null. 
        /// </summary>
        ICSSRule parentRule { get; }

    }

    public enum CSSRuleType
    {
        /// <summary>The rule is a CSSUnknownRule.
        /// </summary>
        UNKNOWN_RULE = 0,
        /// <summary>The rule is a CSSStyleRule.
        /// </summary>
        STYLE_RULE = 1,
        /// <summary>The rule is a CSSCharsetRule.
        /// </summary>
        CHARSET_RULE = 2,
        /// <summary>The rule is a CSSImportRule.
        /// </summary>
        IMPORT_RULE = 3,
        /// <summary>The rule is a CSSMediaRule.
        /// </summary>
        MEDIA_RULE = 4,
        /// <summary>The rule is a CSSFontFaceRule.
        /// </summary>
        FONT_FACE_RULE = 5,
        /// <summary>The rule is a CSSPageRule.
        /// </summary>
        PAGE_RULE = 6
    }
}
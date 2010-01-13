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

    /**
     *  The <code>CSSRule</code> interface is the abstract base interface for any 
     * type of CSS statement. This includes both rule sets and at-rules. An 
     * implementation is expected to preserve all rules specified in a CSS style 
     * sheet, even if the rule is not recognized by the parser. Unrecognized 
     * rules are represented using the <code>CSSUnknownRule</code> interface. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
     * @since DOM Level 2
     */
    public interface ICSSRule
    {
        /**
         *  The type of the rule, as defined above. The expectation is that 
         * binding-specific casting methods can be used to cast down from an 
         * instance of the <code>CSSRule</code> interface to the specific 
         * derived interface implied by the <code>type</code>. 
         */
        CSSRuleType type { get; }

        /**
         *  The parsable textual representation of the rule. This reflects the 
         * current state of the rule and not its initial value. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
         *   error and is unparsable.
         *   <br>INVALID_MODIFICATION_ERR: Raised if the specified CSS string 
         *   value represents a different type of rule than the current one.
         *   <br>HIERARCHY_REQUEST_ERR: Raised if the rule cannot be inserted at 
         *   this point in the style sheet.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if the rule is readonly.
         */
        string cssText { get; set; } // throws DOMException;

        /**
         *  The style sheet that contains this rule. 
         */
        ICSSStyleSheet parentStyleSheet { get; }

        /**
         *  If this rule is contained inside another rule (e.g. a style rule 
         * inside an @media block), this is the containing rule. If this rule is 
         * not nested inside any other rules, this returns <code>null</code>. 
         */
        ICSSRule parentRule { get; }

    }

    public enum CSSRuleType
    {
        // RuleType
        /**
         * The rule is a <code>CSSUnknownRule</code>.
         */
        UNKNOWN_RULE = 0,
        /**
         * The rule is a <code>CSSStyleRule</code>.
         */
        STYLE_RULE = 1,
        /**
         * The rule is a <code>CSSCharsetRule</code>.
         */
        CHARSET_RULE = 2,
        /**
         * The rule is a <code>CSSImportRule</code>.
         */
        IMPORT_RULE = 3,
        /**
         * The rule is a <code>CSSMediaRule</code>.
         */
        MEDIA_RULE = 4,
        /**
         * The rule is a <code>CSSFontFaceRule</code>.
         */
        FONT_FACE_RULE = 5,
        /**
         * The rule is a <code>CSSPageRule</code>.
         */
        PAGE_RULE = 6
    }
}
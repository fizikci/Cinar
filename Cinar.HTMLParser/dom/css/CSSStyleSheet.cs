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


    /// <summary> The CSSStyleSheet interface is a concrete interface used to 
    /// represent a CSS style sheet i.e., a style sheet whose content type is 
    /// "text/css". 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class CSSStyleSheet : StyleSheet
    {
        /// <summary> If this style sheet comes from an @import rule, the 
        /// ownerRule attribute will contain the 
        /// CSSImportRule. In that case, the ownerNode 
        /// attribute in the IStyleSheet interface will be 
        /// null. If the style sheet comes from an element or a 
        /// processing instruction, the ownerRule attribute will be 
        /// null and the ownerNode attribute will 
        /// contain the INode. 
        /// </summary>
        public CSSRule ownerRule { get; internal set; }

        /// <summary> The list of all CSS rules contained within the style sheet. This 
        /// includes both rule sets and at-rules. 
        /// </summary>
        public CSSRuleList cssRules { get; internal set; }

        /// <summary> Used to insert a new rule into the style sheet. The new rule now 
        /// becomes part of the cascade.</summary>
        /// <param name="rule"> The parsable text representing the rule. For rule sets 
        ///   this contains both the selector and the style declaration. For 
        ///   at-rules, this specifies both the at-identifier and the rule 
        ///   content.</param>
        /// <param name="index"> The index within the style sheet's rule list of the rule 
        ///   before which to insert the specified rule. If the specified index 
        ///   is equal to the length of the style sheet's rule collection, the 
        ///   rule will be added to the end of the style sheet.</param>
        /// <returns> The index within the style sheet's rule collection of the 
        ///   newly inserted rule.</returns>
        /// <exception cref="DOMException">
        ///   HIERARCHY_REQUEST_ERR: Raised if the rule cannot be inserted at the 
        ///   specified index e.g. if an @import rule is inserted 
        ///   after a standard rule set or other at-rule.
        ///   INDEX_SIZE_ERR: Raised if the specified index is not a valid 
        ///   insertion point.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this style sheet is 
        ///   readonly.
        ///   SYNTAX_ERR: Raised if the specified rule has a syntax error and 
        ///   is unparsable.
        /// </exception>
        public int insertRule(string strRule, int index)
        {
            if (index < 0 || index >= cssRules.length)
                throw ErrorMessages.Get(DOMExceptionCodes.INDEX_SIZE_ERR);

            string[] parts = strRule.Trim().Split('{');
            string selector = parts[0].Trim();
            string styleDec = parts[1].Trim('}').Trim();
            CSSStyleRule rule = new CSSStyleRule
            {
                cssText = strRule,
                parentStyleSheet = this,
                selectorText = selector,
            };
            rule.style = new CSSStyleDeclaration { cssText = styleDec, parentRule = rule };
            this.cssRules.Insert(index, rule);
            return this.cssRules.length - 1;
        }

        /// <summary> Used to delete a rule from the style sheet.</summary>
        /// <param name="index"> The index within the style sheet's rule list of the rule 
        ///   to remove.</param>
        /// <exception cref="DOMException">
        ///   INDEX_SIZE_ERR: Raised if the specified index does not correspond to 
        ///   a rule in the style sheet's rule list.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this style sheet is 
        ///   readonly.
        /// </exception>
        public void deleteRule(int index)
        {
            if (index < 0 || index >= cssRules.length)
                throw ErrorMessages.Get(DOMExceptionCodes.INDEX_SIZE_ERR);
            this.cssRules.RemoveAt(index);
        }

    }
}
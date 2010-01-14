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


    /// <summary>This interface represents a document with a CSS view.
    ///  The getOverrideStyle method provides a mechanism through 
    /// which a DOM author could effect immediate change to the style of an 
    /// element without modifying the explicitly linked style sheets of a 
    /// document or the inline style of elements in the style sheets. This style 
    /// sheet comes after the author style sheet in the cascade algorithm and is 
    /// called override style sheet. The override style sheet takes precedence 
    /// over author style sheets. An "!important" declaration still takes 
    /// precedence over a normal declaration. Override, author, and user style 
    /// sheets all may contain "!important" declarations. User "!important" rules 
    /// take precedence over both override and author "!important" rules, and 
    /// override "!important" rules take precedence over author "!important" 
    /// rules. 
    ///  The expectation is that an instance of the DocumentCSS 
    /// interface can be obtained by using binding-specific casting methods on an 
    /// instance of the IDocument interface. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class DocumentCSS : DocumentStyle
    {
        /// <summary> This method is used to retrieve the override style declaration for a 
        /// specified element and a specified pseudo-element.</summary>
        /// <param name="elt"> The element whose style is to be modified. This parameter 
        ///   cannot be null.</param>
        /// <param name="pseudoElt"> The pseudo-element or null if none.</param>
        /// <returns> The override style declaration. 
        /// </returns>
        public CSSStyleDeclaration getOverrideStyle(Element elt,
                                                    string pseudoElt);

    }
}
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

using org.w3c.dom.views;

namespace org.w3c.dom.css
{


    /// <summary> This interface represents a CSS view. The getComputedStyle 
    /// method provides a read only access to the computed values of an element. 
    ///  The expectation is that an instance of the ViewCSS 
    /// interface can be obtained by using binding-specific casting methods on an 
    /// instance of the IAbstractView interface. 
    ///  Since a computed style is related to an IElement node, if 
    /// this element is removed from the document, the associated 
    /// ICSSStyleDeclaration and CSSValue related to 
    /// this declaration are no longer valid. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public interface IViewCSS : IAbstractView
    {
        /// <summary> This method is used to get the computed style as it is defined in.</summary>
        /// <param name="elt"> The element whose style is to be computed. This parameter 
        ///   cannot be null.</param>
        /// <param name="pseudoElt"> The pseudo-element or null if none.</param>
        /// <returns> The computed style. The ICSSStyleDeclaration is 
        ///   read-only and contains only absolute values.</returns>
        ICSSStyleDeclaration getComputedStyle(IElement elt,
                                                    string pseudoElt);

    }
}
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

    /// <summary> The CSS2Properties interface represents a convenience 
    /// mechanism for retrieving and setting properties within a 
    /// ICSSStyleDeclaration. The attributes of this interface 
    /// correspond to all the properties specified in CSS2. Getting an attribute 
    /// of this interface is equivalent to calling the 
    /// getPropertyValue method of the 
    /// ICSSStyleDeclaration interface. Setting an attribute of this 
    /// interface is equivalent to calling the setProperty method of 
    /// the ICSSStyleDeclaration interface. 
    ///  A conformant implementation of the CSS module is not required to 
    /// implement the CSS2Properties interface. If an implementation 
    /// does implement this interface, the expectation is that language-specific 
    /// methods can be used to cast from an instance of the 
    /// ICSSStyleDeclaration interface to the 
    /// CSS2Properties interface. 
    ///  If an implementation does implement this interface, it is expected to 
    /// understand the specific syntax of the shorthand properties, and apply 
    /// their semantics; when the margin property is set, for 
    /// example, the marginTop, marginRight, 
    /// marginBottom and marginLeft properties are 
    /// actually being set by the underlying implementation. 
    ///  When dealing with CSS "shorthand" properties, the shorthand properties 
    /// should be decomposed into their component longhand properties as 
    /// appropriate, and when querying for their value, the form returned should 
    /// be the shortest form exactly equivalent to the declarations made in the 
    /// ruleset. However, if there is no shorthand declaration that could be 
    /// added to the ruleset without changing in any way the rules already 
    /// declared in the ruleset (i.e., by adding longhand rules that were 
    /// previously not declared in the ruleset), then the empty string should be 
    /// returned for the shorthand property. 
    ///  For example, querying for the font property should not 
    /// return "normal normal normal 14pt/normal Arial, sans-serif", when "14pt 
    /// Arial, sans-serif" suffices. (The normals are initial values, and are 
    /// implied by use of the longhand property.) 
    ///  If the values for all the longhand properties that compose a particular 
    /// string are the initial values, then a string consisting of all the 
    /// initial values should be returned (e.g. a border-width value 
    /// of "medium" should be returned as such, not as ""). 
    ///  For some shorthand properties that take missing values from other 
    /// sides, such as the margin, padding, and 
    /// border-[width|style|color] properties, the minimum number of 
    /// sides possible should be used; i.e., "0px 10px" will be returned instead 
    /// of "0px 10px 0px 10px". 
    ///  If the value of a shorthand property can not be decomposed into its 
    /// component longhand properties, as is the case for the font 
    /// property with a value of "menu", querying for the values of the component 
    /// longhand properties should return the empty string. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </exception>
    public interface ICSS2Properties
    {
        /// <summary> See the azimuth property definition in CSS2.</summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string azimuth { get; set; }

        /// <summary> See the background property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string background { get; set; }

        /// <summary> See the background-attachment property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string backgroundAttachment { get; set; } // throws DOMException;

        /// <summary> See the background-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string backgroundColor { get; set; } // throws DOMException;

        /// <summary> See the background-image property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string backgroundImage { get; set; } // throws DOMException;

        /// <summary> See the background-position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string backgroundPosition { get; set; } // throws DOMException;

        /// <summary> See the background-repeat property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string backgroundRepeat { get; set; } // throws DOMException;

        /// <summary> See the border property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string border { get; set; } // throws DOMException;

        /// <summary> See the border-collapse property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderCollapse { get; set; } // throws DOMException;

        /// <summary> See the border-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderColor { get; set; } // throws DOMException;

        /// <summary> See the border-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderSpacing { get; set; } // throws DOMException;

        /// <summary> See the border-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderStyle { get; set; } // throws DOMException;

        /// <summary> See the border-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderTop { get; set; } // throws DOMException;

        /// <summary> See the border-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderRight { get; set; } // throws DOMException;

        /// <summary> See the border-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderBottom { get; set; } // throws DOMException;

        /// <summary> See the border-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderLeft { get; set; } // throws DOMException;

        /// <summary> See the border-top-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderTopColor { get; set; } // throws DOMException;

        /// <summary> See the border-right-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderRightColor { get; set; } // throws DOMException;

        /// <summary> See the border-bottom-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderBottomColor { get; set; } // throws DOMException;

        /// <summary> See the border-left-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderLeftColor { get; set; } // throws DOMException;

        /// <summary> See the border-top-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderTopStyle { get; set; } // throws DOMException;

        /// <summary> See the border-right-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderRightStyle { get; set; } // throws DOMException;

        /// <summary> See the border-bottom-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderBottomStyle { get; set; } // throws DOMException;

        /// <summary> See the border-left-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderLeftStyle { get; set; } // throws DOMException;

        /// <summary> See the border-top-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderTopWidth { get; set; } // throws DOMException;

        /// <summary> See the border-right-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderRightWidth { get; set; } // throws DOMException;

        /// <summary> See the border-bottom-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderBottomWidth { get; set; } // throws DOMException;

        /// <summary> See the border-left-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderLeftWidth { get; set; } // throws DOMException;

        /// <summary> See the border-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string borderWidth { get; set; } // throws DOMException;

        /// <summary> See the bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string bottom { get; set; } // throws DOMException;

        /// <summary> See the caption-side property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string captionSide { get; set; } // throws DOMException;

        /// <summary> See the clear property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string clear { get; set; } // throws DOMException;

        /// <summary> See the clip property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string clip { get; set; } // throws DOMException;

        /// <summary> See the color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string color { get; set; } // throws DOMException;

        /// <summary> See the content property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string content { get; set; } // throws DOMException;

        /// <summary> See the counter-increment property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string counterIncrement { get; set; } // throws DOMException;

        /// <summary> See the counter-reset property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string counterReset { get; set; } // throws DOMException;

        /// <summary> See the cue property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string cue { get; set; } // throws DOMException;

        /// <summary> See the cue-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string cueAfter { get; set; } // throws DOMException;

        /// <summary> See the cue-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string cueBefore { get; set; } // throws DOMException;

        /// <summary> See the cursor property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string cursor { get; set; } // throws DOMException;

        /// <summary> See the direction property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string direction { get; set; } // throws DOMException;

        /// <summary> See the display property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string display { get; set; } // throws DOMException;

        /// <summary> See the elevation property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string elevation { get; set; } // throws DOMException;

        /// <summary> See the empty-cells property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string emptyCells { get; set; } // throws DOMException;

        /// <summary> See the float property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string cssFloat { get; set; } // throws DOMException;

        /// <summary> See the font property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string font { get; set; } // throws DOMException;

        /// <summary> See the font-family property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontFamily { get; set; } // throws DOMException;

        /// <summary> See the font-size property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontSize { get; set; } // throws DOMException;

        /// <summary> See the font-size-adjust property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontSizeAdjust { get; set; } // throws DOMException;

        /// <summary> See the font-stretch property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontStretch { get; set; } // throws DOMException;

        /// <summary> See the font-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontStyle { get; set; } // throws DOMException;

        /// <summary> See the font-variant property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontVariant { get; set; } // throws DOMException;

        /// <summary> See the font-weight property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string fontWeight { get; set; } // throws DOMException;

        /// <summary> See the height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string height { get; set; } // throws DOMException;

        /// <summary> See the left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string left { get; set; } // throws DOMException;

        /// <summary> See the letter-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string letterSpacing { get; set; } // throws DOMException;

        /// <summary> See the line-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string lineHeight { get; set; } // throws DOMException;

        /// <summary> See the list-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string listStyle { get; set; } // throws DOMException;

        /// <summary> See the list-style-image property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string listStyleImage { get; set; } // throws DOMException;

        /// <summary> See the list-style-position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string listStylePosition { get; set; } // throws DOMException;

        /// <summary> See the list-style-type property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string listStyleType { get; set; } // throws DOMException;

        /// <summary> See the margin property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string margin { get; set; } // throws DOMException;

        /// <summary> See the margin-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string marginTop { get; set; } // throws DOMException;

        /// <summary> See the margin-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string marginRight { get; set; } // throws DOMException;

        /// <summary> See the margin-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string marginBottom { get; set; } // throws DOMException;

        /// <summary> See the margin-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string marginLeft { get; set; } // throws DOMException;

        /// <summary> See the marker-offset property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string markerOffset { get; set; } // throws DOMException;

        /// <summary> See the marks property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string marks { get; set; } // throws DOMException;

        /// <summary> See the max-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string maxHeight { get; set; } // throws DOMException;

        /// <summary> See the max-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string maxWidth { get; set; } // throws DOMException;

        /// <summary> See the min-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string minHeight { get; set; } // throws DOMException;

        /// <summary> See the min-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string minWidth { get; set; } // throws DOMException;

        /// <summary> See the orphans property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string orphans { get; set; } // throws DOMException;

        /// <summary> See the outline property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string outline { get; set; } // throws DOMException;

        /// <summary> See the outline-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string outlineColor { get; set; } // throws DOMException;

        /// <summary> See the outline-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string outlineStyle { get; set; } // throws DOMException;

        /// <summary> See the outline-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string outlineWidth { get; set; } // throws DOMException;

        /// <summary> See the overflow property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string overflow { get; set; } // throws DOMException;

        /// <summary> See the padding property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string padding { get; set; } // throws DOMException;

        /// <summary> See the padding-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string paddingTop { get; set; } // throws DOMException;

        /// <summary> See the padding-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string paddingRight { get; set; } // throws DOMException;

        /// <summary> See the padding-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string paddingBottom { get; set; } // throws DOMException;

        /// <summary> See the padding-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string paddingLeft { get; set; } // throws DOMException;

        /// <summary> See the page property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string page { get; set; } // throws DOMException;

        /// <summary> See the page-break-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pageBreakAfter { get; set; } // throws DOMException;

        /// <summary> See the page-break-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pageBreakBefore { get; set; } // throws DOMException;

        /// <summary> See the page-break-inside property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pageBreakInside { get; set; } // throws DOMException;

        /// <summary> See the pause property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pause { get; set; } // throws DOMException;

        /// <summary> See the pause-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pauseAfter { get; set; } // throws DOMException;

        /// <summary> See the pause-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pauseBefore { get; set; } // throws DOMException;

        /// <summary> See the pitch property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pitch { get; set; } // throws DOMException;

        /// <summary> See the pitch-range property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string pitchRange { get; set; } // throws DOMException;

        /// <summary> See the play-during property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string playDuring { get; set; } // throws DOMException;

        /// <summary> See the position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string position { get; set; } // throws DOMException;

        /// <summary> See the quotes property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string quotes { get; set; } // throws DOMException;

        /// <summary> See the richness property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string richness { get; set; } // throws DOMException;

        /// <summary> See the right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string right { get; set; } // throws DOMException;

        /// <summary> See the size property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string size { get; set; } // throws DOMException;

        /// <summary> See the speak property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string speak { get; set; } // throws DOMException;

        /// <summary> See the speak-header property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string speakHeader { get; set; } // throws DOMException;

        /// <summary> See the speak-numeral property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string speakNumeral { get; set; } // throws DOMException;

        /// <summary> See the speak-punctuation property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string speakPunctuation { get; set; } // throws DOMException;

        /// <summary> See the speech-rate property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string speechRate { get; set; } // throws DOMException;

        /// <summary> See the stress property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string stress { get; set; } // throws DOMException;

        /// <summary> See the table-layout property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string tableLayout { get; set; } // throws DOMException;

        /// <summary> See the text-align property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string textAlign { get; set; } // throws DOMException;

        /// <summary> See the text-decoration property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string textDecoration { get; set; } // throws DOMException;

        /// <summary> See the text-indent property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string textIndent { get; set; } // throws DOMException;

        /// <summary> See the text-shadow property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string textShadow { get; set; } // throws DOMException;

        /// <summary> See the text-transform property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string textTransform { get; set; } // throws DOMException;

        /// <summary> See the top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string top { get; set; } // throws DOMException;

        /// <summary> See the unicode-bidi property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string unicodeBidi { get; set; } // throws DOMException;

        /// <summary> See the vertical-align property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string verticalAlign { get; set; } // throws DOMException;

        /// <summary> See the visibility property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string visibility { get; set; } // throws DOMException;

        /// <summary> See the voice-family property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string voiceFamily { get; set; } // throws DOMException;

        /// <summary> See the volume property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string volume { get; set; } // throws DOMException;

        /// <summary> See the white-space property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string whiteSpace { get; set; } // throws DOMException;

        /// <summary> See the widows property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string widows { get; set; } // throws DOMException;

        /// <summary> See the width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string width { get; set; } // throws DOMException;

        /// <summary> See the word-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string wordSpacing { get; set; } // throws DOMException;

        /// <summary> See the z-index property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        string zIndex { get; set; } // throws DOMException;

    }
}
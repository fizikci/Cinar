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
using System.Collections.Generic;

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
    public class CSS2Properties
    {
        internal Dictionary<string, CSSValue> styles = new Dictionary<string, CSSValue>();

        internal string getStyle(string propertyName)
        {
            return styles[propertyName].cssText;
        }
        internal CSSValue getCSSValue(string propertyName)
        {
            return styles[propertyName];
        }

        internal void setStyle(string propertyName, string val)
        {
        }

        /// <summary>See the azimuth property definition in CSS2.</summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string azimuth { get { return getStyle("azimuth"); } set { setStyle("azimuth", value); } }

        /// <summary>See the background property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string background { get { return getStyle("background"); } set { setStyle("background", value); } }

        /// <summary>See the background-attachment property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string backgroundAttachment { get { return getStyle("background-attachment"); } set { setStyle("background-attachment", value); } } // throws DOMException;

        /// <summary>See the background-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string backgroundColor { get { return getStyle("background-color"); } set { setStyle("background-color", value); } } // throws DOMException;

        /// <summary>See the background-image property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string backgroundImage { get { return getStyle("background-image"); } set { setStyle("background-image", value); } } // throws DOMException;

        /// <summary>See the background-position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string backgroundPosition { get { return getStyle("background-position"); } set { setStyle("background-position", value); } } // throws DOMException;

        /// <summary>See the background-repeat property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string backgroundRepeat { get { return getStyle("background-repeat"); } set { setStyle("background-repeat", value); } } // throws DOMException;

        /// <summary>See the border property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string border { get { return getStyle("border"); } set { setStyle("border", value); } } // throws DOMException;

        /// <summary>See the border-collapse property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderCollapse { get { return getStyle("border-collapse"); } set { setStyle("border-collapse", value); } } // throws DOMException;

        /// <summary>See the border-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderColor { get { return getStyle("border-color"); } set { setStyle("border-color", value); } } // throws DOMException;

        /// <summary>See the border-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderSpacing { get { return getStyle("border-spacing"); } set { setStyle("border-spacing", value); } } // throws DOMException;

        /// <summary>See the border-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderStyle { get { return getStyle("border-style"); } set { setStyle("border-style", value); } } // throws DOMException;

        /// <summary>See the border-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderTop { get { return getStyle("border-top"); } set { setStyle("border-top", value); } } // throws DOMException;

        /// <summary>See the border-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderRight { get { return getStyle("border-right"); } set { setStyle("border-right", value); } } // throws DOMException;

        /// <summary>See the border-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderBottom { get { return getStyle("border-bottom"); } set { setStyle("border-bottom", value); } } // throws DOMException;

        /// <summary>See the border-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderLeft { get { return getStyle("border-left"); } set { setStyle("border-left", value); } } // throws DOMException;

        /// <summary>See the border-top-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderTopColor { get { return getStyle("border-top-color"); } set { setStyle("border-top-color", value); } } // throws DOMException;

        /// <summary>See the border-right-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderRightColor { get { return getStyle("border-right-color"); } set { setStyle("border-right-color", value); } } // throws DOMException;

        /// <summary>See the border-bottom-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderBottomColor { get { return getStyle("border-bottom-color"); } set { setStyle("border-bottom-color", value); } } // throws DOMException;

        /// <summary>See the border-left-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderLeftColor { get { return getStyle("border-left-color"); } set { setStyle("border-left-color", value); } } // throws DOMException;

        /// <summary>See the border-top-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderTopStyle { get { return getStyle("border-top-style"); } set { setStyle("border-top-style", value); } } // throws DOMException;

        /// <summary>See the border-right-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderRightStyle { get { return getStyle("border-right-style"); } set { setStyle("border-right-style", value); } } // throws DOMException;

        /// <summary>See the border-bottom-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderBottomStyle { get { return getStyle("border-bottom-style"); } set { setStyle("border-bottom-style", value); } } // throws DOMException;

        /// <summary>See the border-left-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderLeftStyle { get { return getStyle("border-left-style"); } set { setStyle("border-left-style", value); } } // throws DOMException;

        /// <summary>See the border-top-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderTopWidth { get { return getStyle("border-top-width"); } set { setStyle("border-top-width", value); } } // throws DOMException;

        /// <summary>See the border-right-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderRightWidth { get { return getStyle("border-right-width"); } set { setStyle("border-right-width", value); } } // throws DOMException;

        /// <summary>See the border-bottom-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderBottomWidth { get { return getStyle("border-bottom-width"); } set { setStyle("border-bottom-width", value); } } // throws DOMException;

        /// <summary>See the border-left-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderLeftWidth { get { return getStyle("border-left-width"); } set { setStyle("border-left-width", value); } } // throws DOMException;

        /// <summary>See the border-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string borderWidth { get { return getStyle("border-width"); } set { setStyle("border-width", value); } } // throws DOMException;

        /// <summary>See the bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string bottom { get { return getStyle("bottom"); } set { setStyle("bottom", value); } } // throws DOMException;

        /// <summary>See the caption-side property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string captionSide { get { return getStyle("caption-side"); } set { setStyle("caption-side", value); } } // throws DOMException;

        /// <summary>See the clear property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string clear { get { return getStyle("clear"); } set { setStyle("clear", value); } } // throws DOMException;

        /// <summary>See the clip property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string clip { get { return getStyle("clip"); } set { setStyle("clip", value); } } // throws DOMException;

        /// <summary>See the color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string color { get { return getStyle("color"); } set { setStyle("color", value); } } // throws DOMException;

        /// <summary>See the content property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string content { get { return getStyle("content"); } set { setStyle("content", value); } } // throws DOMException;

        /// <summary>See the counter-increment property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string counterIncrement { get { return getStyle("counter-increment"); } set { setStyle("counter-increment", value); } } // throws DOMException;

        /// <summary>See the counter-reset property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string counterReset { get { return getStyle("counter-reset"); } set { setStyle("counter-reset", value); } } // throws DOMException;

        /// <summary>See the cue property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string cue { get { return getStyle("cue"); } set { setStyle("cue", value); } } // throws DOMException;

        /// <summary>See the cue-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string cueAfter { get { return getStyle("cue-after"); } set { setStyle("cue-after", value); } } // throws DOMException;

        /// <summary>See the cue-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string cueBefore { get { return getStyle("cue-before"); } set { setStyle("cue-before", value); } } // throws DOMException;

        /// <summary>See the cursor property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string cursor { get { return getStyle("cursor"); } set { setStyle("cursor", value); } } // throws DOMException;

        /// <summary>See the direction property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string direction { get { return getStyle("direction"); } set { setStyle("direction", value); } } // throws DOMException;

        /// <summary>See the display property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string display { get { return getStyle("display"); } set { setStyle("display", value); } } // throws DOMException;

        /// <summary>See the elevation property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string elevation { get { return getStyle("elevation"); } set { setStyle("elevation", value); } } // throws DOMException;

        /// <summary>See the empty-cells property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string emptyCells { get { return getStyle("empty-cells"); } set { setStyle("empty-cells", value); } } // throws DOMException;

        /// <summary>See the float property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string cssFloat { get { return getStyle("float"); } set { setStyle("float", value); } } // throws DOMException;

        /// <summary>See the font property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string font { get { return getStyle("font"); } set { setStyle("font", value); } } // throws DOMException;

        /// <summary>See the font-family property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontFamily { get { return getStyle("font-family"); } set { setStyle("font-family", value); } } // throws DOMException;

        /// <summary>See the font-size property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontSize { get { return getStyle("font-size"); } set { setStyle("font-size", value); } } // throws DOMException;

        /// <summary>See the font-size-adjust property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontSizeAdjust { get { return getStyle("font-size-adjust"); } set { setStyle("font-size-adjust", value); } } // throws DOMException;

        /// <summary>See the font-stretch property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontStretch { get { return getStyle("font-stretch"); } set { setStyle("font-stretch", value); } } // throws DOMException;

        /// <summary>See the font-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontStyle { get { return getStyle("font-style"); } set { setStyle("font-style", value); } } // throws DOMException;

        /// <summary>See the font-variant property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontVariant { get { return getStyle("font-variant"); } set { setStyle("font-variant", value); } } // throws DOMException;

        /// <summary>See the font-weight property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string fontWeight { get { return getStyle("font-weight"); } set { setStyle("font-weight", value); } } // throws DOMException;

        /// <summary>See the height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string height { get { return getStyle("height"); } set { setStyle("height", value); } } // throws DOMException;

        /// <summary>See the left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string left { get { return getStyle("left"); } set { setStyle("left", value); } } // throws DOMException;

        /// <summary>See the letter-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string letterSpacing { get { return getStyle("letter-spacing"); } set { setStyle("letter-spacing", value); } } // throws DOMException;

        /// <summary>See the line-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string lineHeight { get { return getStyle("line-height"); } set { setStyle("line-height", value); } } // throws DOMException;

        /// <summary>See the list-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string listStyle { get { return getStyle("list-style"); } set { setStyle("list-style", value); } } // throws DOMException;

        /// <summary>See the list-style-image property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string listStyleImage { get { return getStyle("list-style-image"); } set { setStyle("list-style-image", value); } } // throws DOMException;

        /// <summary>See the list-style-position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string listStylePosition { get { return getStyle("list-style-position"); } set { setStyle("list-style-position", value); } } // throws DOMException;

        /// <summary>See the list-style-type property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string listStyleType { get { return getStyle("list-style-type"); } set { setStyle("list-style-type", value); } } // throws DOMException;

        /// <summary>See the margin property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string margin { get { return getStyle("margin"); } set { setStyle("margin", value); } } // throws DOMException;

        /// <summary>See the margin-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string marginTop { get { return getStyle("margin-top"); } set { setStyle("margin-top", value); } } // throws DOMException;

        /// <summary>See the margin-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string marginRight { get { return getStyle("margin-right"); } set { setStyle("margin-right", value); } } // throws DOMException;

        /// <summary>See the margin-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string marginBottom { get { return getStyle("margin-bottom"); } set { setStyle("margin-bottom", value); } } // throws DOMException;

        /// <summary>See the margin-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string marginLeft { get { return getStyle("margin-left"); } set { setStyle("margin-left", value); } } // throws DOMException;

        /// <summary>See the marker-offset property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string markerOffset { get { return getStyle("marker-offset"); } set { setStyle("marker-offset", value); } } // throws DOMException;

        /// <summary>See the marks property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string marks { get { return getStyle("marks"); } set { setStyle("marks", value); } } // throws DOMException;

        /// <summary>See the max-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string maxHeight { get { return getStyle("max-height"); } set { setStyle("max-height", value); } } // throws DOMException;

        /// <summary>See the max-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string maxWidth { get { return getStyle("max-width"); } set { setStyle("max-width", value); } } // throws DOMException;

        /// <summary>See the min-height property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string minHeight { get { return getStyle("min-height"); } set { setStyle("min-height", value); } } // throws DOMException;

        /// <summary>See the min-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string minWidth { get { return getStyle("min-width"); } set { setStyle("min-width", value); } } // throws DOMException;

        /// <summary>See the orphans property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string orphans { get { return getStyle("orphans"); } set { setStyle("orphans", value); } } // throws DOMException;

        /// <summary>See the outline property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string outline { get { return getStyle("outline"); } set { setStyle("outline", value); } } // throws DOMException;

        /// <summary>See the outline-color property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string outlineColor { get { return getStyle("outline-color"); } set { setStyle("outline-color", value); } } // throws DOMException;

        /// <summary>See the outline-style property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string outlineStyle { get { return getStyle("outline-style"); } set { setStyle("outline-style", value); } } // throws DOMException;

        /// <summary>See the outline-width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string outlineWidth { get { return getStyle("outline-width"); } set { setStyle("outline-width", value); } } // throws DOMException;

        /// <summary>See the overflow property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string overflow { get { return getStyle("overflow"); } set { setStyle("overflow", value); } } // throws DOMException;

        /// <summary>See the padding property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string padding { get { return getStyle("padding"); } set { setStyle("padding", value); } } // throws DOMException;

        /// <summary>See the padding-top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string paddingTop { get { return getStyle("padding-top"); } set { setStyle("padding-top", value); } } // throws DOMException;

        /// <summary>See the padding-right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string paddingRight { get { return getStyle("padding-right"); } set { setStyle("padding-right", value); } } // throws DOMException;

        /// <summary>See the padding-bottom property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string paddingBottom { get { return getStyle("padding-bottom"); } set { setStyle("padding-bottom", value); } } // throws DOMException;

        /// <summary>See the padding-left property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string paddingLeft { get { return getStyle("padding-left"); } set { setStyle("padding-left", value); } } // throws DOMException;

        /// <summary>See the page property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string page { get { return getStyle("page"); } set { setStyle("page", value); } } // throws DOMException;

        /// <summary>See the page-break-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pageBreakAfter { get { return getStyle("page-break-after"); } set { setStyle("page-break-after", value); } } // throws DOMException;

        /// <summary>See the page-break-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pageBreakBefore { get { return getStyle("page-break-before"); } set { setStyle("page-break-before", value); } } // throws DOMException;

        /// <summary>See the page-break-inside property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pageBreakInside { get { return getStyle("page-break-inside"); } set { setStyle("page-break-inside", value); } } // throws DOMException;

        /// <summary>See the pause property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pause { get { return getStyle("pause"); } set { setStyle("pause", value); } } // throws DOMException;

        /// <summary>See the pause-after property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pauseAfter { get { return getStyle("pause-after"); } set { setStyle("pause-after", value); } } // throws DOMException;

        /// <summary>See the pause-before property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pauseBefore { get { return getStyle("pause-before"); } set { setStyle("pause-before", value); } } // throws DOMException;

        /// <summary>See the pitch property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pitch { get { return getStyle("pitch"); } set { setStyle("pitch", value); } } // throws DOMException;

        /// <summary>See the pitch-range property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string pitchRange { get { return getStyle("pitch-range"); } set { setStyle("pitch-range", value); } } // throws DOMException;

        /// <summary>See the play-during property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string playDuring { get { return getStyle("play-during"); } set { setStyle("play-during", value); } } // throws DOMException;

        /// <summary>See the position property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string position { get { return getStyle("position"); } set { setStyle("position", value); } } // throws DOMException;

        /// <summary>See the quotes property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string quotes { get { return getStyle("quotes"); } set { setStyle("quotes", value); } } // throws DOMException;

        /// <summary>See the richness property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string richness { get { return getStyle("richness"); } set { setStyle("richness", value); } } // throws DOMException;

        /// <summary>See the right property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string right { get { return getStyle("right"); } set { setStyle("right", value); } } // throws DOMException;

        /// <summary>See the size property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string size { get { return getStyle("size"); } set { setStyle("size", value); } } // throws DOMException;

        /// <summary>See the speak property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string speak { get { return getStyle("speak"); } set { setStyle("speak", value); } } // throws DOMException;

        /// <summary>See the speak-header property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string speakHeader { get { return getStyle("speak-header"); } set { setStyle("speak-header", value); } } // throws DOMException;

        /// <summary>See the speak-numeral property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string speakNumeral { get { return getStyle("speak-numeral"); } set { setStyle("speak-numeral", value); } } // throws DOMException;

        /// <summary>See the speak-punctuation property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string speakPunctuation { get { return getStyle("speak-punctuation"); } set { setStyle("speak-punctuation", value); } } // throws DOMException;

        /// <summary>See the speech-rate property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string speechRate { get { return getStyle("speech-rate"); } set { setStyle("speech-rate", value); } } // throws DOMException;

        /// <summary>See the stress property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string stress { get { return getStyle("stress"); } set { setStyle("stress", value); } } // throws DOMException;

        /// <summary>See the table-layout property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string tableLayout { get { return getStyle("table-layout"); } set { setStyle("table-layout", value); } } // throws DOMException;

        /// <summary>See the text-align property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string textAlign { get { return getStyle("text-align"); } set { setStyle("text-align", value); } } // throws DOMException;

        /// <summary>See the text-decoration property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string textDecoration { get { return getStyle("text-decoration"); } set { setStyle("text-decoration", value); } } // throws DOMException;

        /// <summary>See the text-indent property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string textIndent { get { return getStyle("text-indent"); } set { setStyle("text-indent", value); } } // throws DOMException;

        /// <summary>See the text-shadow property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string textShadow { get { return getStyle("text-shadow"); } set { setStyle("text-shadow", value); } } // throws DOMException;

        /// <summary>See the text-transform property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string textTransform { get { return getStyle("text-transform"); } set { setStyle("text-transform", value); } } // throws DOMException;

        /// <summary>See the top property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string top { get { return getStyle("top"); } set { setStyle("top", value); } } // throws DOMException;

        /// <summary>See the unicode-bidi property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string unicodeBidi { get { return getStyle("unicode-bidi"); } set { setStyle("unicode-bidi", value); } } // throws DOMException;

        /// <summary>See the vertical-align property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string verticalAlign { get { return getStyle("vertical-align"); } set { setStyle("vertical-align", value); } } // throws DOMException;

        /// <summary>See the visibility property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string visibility { get { return getStyle("visibility"); } set { setStyle("visibility", value); } } // throws DOMException;

        /// <summary>See the voice-family property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string voiceFamily { get { return getStyle("voice-family"); } set { setStyle("voice-family", value); } } // throws DOMException;

        /// <summary>See the volume property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string volume { get { return getStyle("volume"); } set { setStyle("volume", value); } } // throws DOMException;

        /// <summary>See the white-space property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string whiteSpace { get { return getStyle("white-space"); } set { setStyle("white-space", value); } } // throws DOMException;

        /// <summary>See the widows property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string widows { get { return getStyle("widows"); } set { setStyle("widows", value); } } // throws DOMException;

        /// <summary>See the width property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string width { get { return getStyle("width"); } set { setStyle("width", value); } } // throws DOMException;

        /// <summary>See the word-spacing property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string wordSpacing { get { return getStyle("word-spacing"); } set { setStyle("word-spacing", value); } } // throws DOMException;

        /// <summary>See the z-index property definition in CSS2.</summary> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the new value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
        /// </exception>
        public string zIndex { get { return getStyle("z-index"); } set { setStyle("z-index", value); } } // throws DOMException;

    }
}
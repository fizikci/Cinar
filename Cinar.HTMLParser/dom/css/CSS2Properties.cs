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
     *  The <code>CSS2Properties</code> interface represents a convenience 
     * mechanism for retrieving and setting properties within a 
     * <code>ICSSStyleDeclaration</code>. The attributes of this interface 
     * correspond to all the properties specified in CSS2. Getting an attribute 
     * of this interface is equivalent to calling the 
     * <code>getPropertyValue</code> method of the 
     * <code>ICSSStyleDeclaration</code> interface. Setting an attribute of this 
     * interface is equivalent to calling the <code>setProperty</code> method of 
     * the <code>ICSSStyleDeclaration</code> interface. 
     * <p> A conformant implementation of the CSS module is not required to 
     * implement the <code>CSS2Properties</code> interface. If an implementation 
     * does implement this interface, the expectation is that language-specific 
     * methods can be used to cast from an instance of the 
     * <code>ICSSStyleDeclaration</code> interface to the 
     * <code>CSS2Properties</code> interface. 
     * <p> If an implementation does implement this interface, it is expected to 
     * understand the specific syntax of the shorthand properties, and apply 
     * their semantics; when the <code>margin</code> property is set, for 
     * example, the <code>marginTop</code>, <code>marginRight</code>, 
     * <code>marginBottom</code> and <code>marginLeft</code> properties are 
     * actually being set by the underlying implementation. 
     * <p> When dealing with CSS "shorthand" properties, the shorthand properties 
     * should be decomposed into their component longhand properties as 
     * appropriate, and when querying for their value, the form returned should 
     * be the shortest form exactly equivalent to the declarations made in the 
     * ruleset. However, if there is no shorthand declaration that could be 
     * added to the ruleset without changing in any way the rules already 
     * declared in the ruleset (i.e., by adding longhand rules that were 
     * previously not declared in the ruleset), then the empty string should be 
     * returned for the shorthand property. 
     * <p> For example, querying for the <code>font</code> property should not 
     * return "normal normal normal 14pt/normal Arial, sans-serif", when "14pt 
     * Arial, sans-serif" suffices. (The normals are initial values, and are 
     * implied by use of the longhand property.) 
     * <p> If the values for all the longhand properties that compose a particular 
     * string are the initial values, then a string consisting of all the 
     * initial values should be returned (e.g. a <code>border-width</code> value 
     * of "medium" should be returned as such, not as ""). 
     * <p> For some shorthand properties that take missing values from other 
     * sides, such as the <code>margin</code>, <code>padding</code>, and 
     * <code>border-[width|style|color]</code> properties, the minimum number of 
     * sides possible should be used; i.e., "0px 10px" will be returned instead 
     * of "0px 10px 0px 10px". 
     * <p> If the value of a shorthand property can not be decomposed into its 
     * component longhand properties, as is the case for the <code>font</code> 
     * property with a value of "menu", querying for the values of the component 
     * longhand properties should return the empty string. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
     * @since DOM Level 2
     */
    public interface ICSS2Properties
    {
        /**
         *  See the azimuth property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string azimuth { get; set; }

        /**
         *  See the background property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string background { get; set; }

        /**
         *  See the background-attachment property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string backgroundAttachment { get; set; } // throws DOMException;

        /**
         *  See the background-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string backgroundColor { get; set; } // throws DOMException;

        /**
         *  See the background-image property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string backgroundImage { get; set; } // throws DOMException;

        /**
         *  See the background-position property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string backgroundPosition { get; set; } // throws DOMException;

        /**
         *  See the background-repeat property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string backgroundRepeat { get; set; } // throws DOMException;

        /**
         *  See the border property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string border { get; set; } // throws DOMException;

        /**
         *  See the border-collapse property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderCollapse { get; set; } // throws DOMException;

        /**
         *  See the border-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderColor { get; set; } // throws DOMException;

        /**
         *  See the border-spacing property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderSpacing { get; set; } // throws DOMException;

        /**
         *  See the border-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderStyle { get; set; } // throws DOMException;

        /**
         *  See the border-top property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderTop { get; set; } // throws DOMException;

        /**
         *  See the border-right property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderRight { get; set; } // throws DOMException;

        /**
         *  See the border-bottom property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderBottom { get; set; } // throws DOMException;

        /**
         *  See the border-left property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderLeft { get; set; } // throws DOMException;

        /**
         *  See the border-top-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderTopColor { get; set; } // throws DOMException;

        /**
         *  See the border-right-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderRightColor { get; set; } // throws DOMException;

        /**
         *  See the border-bottom-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderBottomColor { get; set; } // throws DOMException;

        /**
         *  See the border-left-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderLeftColor { get; set; } // throws DOMException;

        /**
         *  See the border-top-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderTopStyle { get; set; } // throws DOMException;

        /**
         *  See the border-right-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderRightStyle { get; set; } // throws DOMException;

        /**
         *  See the border-bottom-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderBottomStyle { get; set; } // throws DOMException;

        /**
         *  See the border-left-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderLeftStyle { get; set; } // throws DOMException;

        /**
         *  See the border-top-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderTopWidth { get; set; } // throws DOMException;

        /**
         *  See the border-right-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderRightWidth { get; set; } // throws DOMException;

        /**
         *  See the border-bottom-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderBottomWidth { get; set; } // throws DOMException;

        /**
         *  See the border-left-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderLeftWidth { get; set; } // throws DOMException;

        /**
         *  See the border-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string borderWidth { get; set; } // throws DOMException;

        /**
         *  See the bottom property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string bottom { get; set; } // throws DOMException;

        /**
         *  See the caption-side property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string captionSide { get; set; } // throws DOMException;

        /**
         *  See the clear property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string clear { get; set; } // throws DOMException;

        /**
         *  See the clip property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string clip { get; set; } // throws DOMException;

        /**
         *  See the color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string color { get; set; } // throws DOMException;

        /**
         *  See the content property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string content { get; set; } // throws DOMException;

        /**
         *  See the counter-increment property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string counterIncrement { get; set; } // throws DOMException;

        /**
         *  See the counter-reset property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string counterReset { get; set; } // throws DOMException;

        /**
         *  See the cue property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string cue { get; set; } // throws DOMException;

        /**
         *  See the cue-after property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string cueAfter { get; set; } // throws DOMException;

        /**
         *  See the cue-before property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string cueBefore { get; set; } // throws DOMException;

        /**
         *  See the cursor property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string cursor { get; set; } // throws DOMException;

        /**
         *  See the direction property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string direction { get; set; } // throws DOMException;

        /**
         *  See the display property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string display { get; set; } // throws DOMException;

        /**
         *  See the elevation property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string elevation { get; set; } // throws DOMException;

        /**
         *  See the empty-cells property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string emptyCells { get; set; } // throws DOMException;

        /**
         *  See the float property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string cssFloat { get; set; } // throws DOMException;

        /**
         *  See the font property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string font { get; set; } // throws DOMException;

        /**
         *  See the font-family property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontFamily { get; set; } // throws DOMException;

        /**
         *  See the font-size property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontSize { get; set; } // throws DOMException;

        /**
         *  See the font-size-adjust property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontSizeAdjust { get; set; } // throws DOMException;

        /**
         *  See the font-stretch property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontStretch { get; set; } // throws DOMException;

        /**
         *  See the font-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontStyle { get; set; } // throws DOMException;

        /**
         *  See the font-variant property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontVariant { get; set; } // throws DOMException;

        /**
         *  See the font-weight property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string fontWeight { get; set; } // throws DOMException;

        /**
         *  See the height property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string height { get; set; } // throws DOMException;

        /**
         *  See the left property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string left { get; set; } // throws DOMException;

        /**
         *  See the letter-spacing property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string letterSpacing { get; set; } // throws DOMException;

        /**
         *  See the line-height property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string lineHeight { get; set; } // throws DOMException;

        /**
         *  See the list-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string listStyle { get; set; } // throws DOMException;

        /**
         *  See the list-style-image property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string listStyleImage { get; set; } // throws DOMException;

        /**
         *  See the list-style-position property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string listStylePosition { get; set; } // throws DOMException;

        /**
         *  See the list-style-type property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string listStyleType { get; set; } // throws DOMException;

        /**
         *  See the margin property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string margin { get; set; } // throws DOMException;

        /**
         *  See the margin-top property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string marginTop { get; set; } // throws DOMException;

        /**
         *  See the margin-right property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string marginRight { get; set; } // throws DOMException;

        /**
         *  See the margin-bottom property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string marginBottom { get; set; } // throws DOMException;

        /**
         *  See the margin-left property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string marginLeft { get; set; } // throws DOMException;

        /**
         *  See the marker-offset property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string markerOffset { get; set; } // throws DOMException;

        /**
         *  See the marks property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string marks { get; set; } // throws DOMException;

        /**
         *  See the max-height property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string maxHeight { get; set; } // throws DOMException;

        /**
         *  See the max-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string maxWidth { get; set; } // throws DOMException;

        /**
         *  See the min-height property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string minHeight { get; set; } // throws DOMException;

        /**
         *  See the min-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string minWidth { get; set; } // throws DOMException;

        /**
         *  See the orphans property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string orphans { get; set; } // throws DOMException;

        /**
         *  See the outline property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string outline { get; set; } // throws DOMException;

        /**
         *  See the outline-color property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string outlineColor { get; set; } // throws DOMException;

        /**
         *  See the outline-style property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string outlineStyle { get; set; } // throws DOMException;

        /**
         *  See the outline-width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string outlineWidth { get; set; } // throws DOMException;

        /**
         *  See the overflow property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string overflow { get; set; } // throws DOMException;

        /**
         *  See the padding property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string padding { get; set; } // throws DOMException;

        /**
         *  See the padding-top property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string paddingTop { get; set; } // throws DOMException;

        /**
         *  See the padding-right property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string paddingRight { get; set; } // throws DOMException;

        /**
         *  See the padding-bottom property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string paddingBottom { get; set; } // throws DOMException;

        /**
         *  See the padding-left property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string paddingLeft { get; set; } // throws DOMException;

        /**
         *  See the page property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string page { get; set; } // throws DOMException;

        /**
         *  See the page-break-after property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pageBreakAfter { get; set; } // throws DOMException;

        /**
         *  See the page-break-before property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pageBreakBefore { get; set; } // throws DOMException;

        /**
         *  See the page-break-inside property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pageBreakInside { get; set; } // throws DOMException;

        /**
         *  See the pause property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pause { get; set; } // throws DOMException;

        /**
         *  See the pause-after property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pauseAfter { get; set; } // throws DOMException;

        /**
         *  See the pause-before property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pauseBefore { get; set; } // throws DOMException;

        /**
         *  See the pitch property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pitch { get; set; } // throws DOMException;

        /**
         *  See the pitch-range property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string pitchRange { get; set; } // throws DOMException;

        /**
         *  See the play-during property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string playDuring { get; set; } // throws DOMException;

        /**
         *  See the position property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string position { get; set; } // throws DOMException;

        /**
         *  See the quotes property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string quotes { get; set; } // throws DOMException;

        /**
         *  See the richness property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string richness { get; set; } // throws DOMException;

        /**
         *  See the right property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string right { get; set; } // throws DOMException;

        /**
         *  See the size property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string size { get; set; } // throws DOMException;

        /**
         *  See the speak property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string speak { get; set; } // throws DOMException;

        /**
         *  See the speak-header property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string speakHeader { get; set; } // throws DOMException;

        /**
         *  See the speak-numeral property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string speakNumeral { get; set; } // throws DOMException;

        /**
         *  See the speak-punctuation property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string speakPunctuation { get; set; } // throws DOMException;

        /**
         *  See the speech-rate property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string speechRate { get; set; } // throws DOMException;

        /**
         *  See the stress property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string stress { get; set; } // throws DOMException;

        /**
         *  See the table-layout property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string tableLayout { get; set; } // throws DOMException;

        /**
         *  See the text-align property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string textAlign { get; set; } // throws DOMException;

        /**
         *  See the text-decoration property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string textDecoration { get; set; } // throws DOMException;

        /**
         *  See the text-indent property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string textIndent { get; set; } // throws DOMException;

        /**
         *  See the text-shadow property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string textShadow { get; set; } // throws DOMException;

        /**
         *  See the text-transform property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string textTransform { get; set; } // throws DOMException;

        /**
         *  See the top property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string top { get; set; } // throws DOMException;

        /**
         *  See the unicode-bidi property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string unicodeBidi { get; set; } // throws DOMException;

        /**
         *  See the vertical-align property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string verticalAlign { get; set; } // throws DOMException;

        /**
         *  See the visibility property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string visibility { get; set; } // throws DOMException;

        /**
         *  See the voice-family property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string voiceFamily { get; set; } // throws DOMException;

        /**
         *  See the volume property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string volume { get; set; } // throws DOMException;

        /**
         *  See the white-space property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string whiteSpace { get; set; } // throws DOMException;

        /**
         *  See the widows property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string widows { get; set; } // throws DOMException;

        /**
         *  See the width property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string width { get; set; } // throws DOMException;

        /**
         *  See the word-spacing property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string wordSpacing { get; set; } // throws DOMException;

        /**
         *  See the z-index property definition in CSS2. 
         * @exception DOMException
         *   SYNTAX_ERR: Raised if the new value has a syntax error and is 
         *   unparsable.
         *   <br>NO_MODIFICATION_ALLOWED_ERR: Raised if this property is readonly.
         */
        string zIndex { get; set; } // throws DOMException;

    }
}
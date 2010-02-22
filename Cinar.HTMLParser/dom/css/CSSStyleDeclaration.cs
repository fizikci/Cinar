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
using System.Drawing;

namespace org.w3c.dom.css
{


    /// <summary> The ICSSStyleDeclaration interface represents a single CSS 
    /// declaration block. This interface may be used to determine the style 
    /// properties currently set in a block or to set style properties explicitly 
    /// within the block. 
    ///  While an implementation may not recognize all CSS properties within a 
    /// CSS declaration block, it is expected to provide access to all specified 
    /// properties in the style sheet through the ICSSStyleDeclaration
    ///  interface. Furthermore, implementations that support a specific level of 
    /// CSS should correctly handle CSS shorthand properties for that level. For 
    /// a further discussion of shorthand properties, see the 
    /// CSS2Properties interface. 
    ///  This interface is also used to provide a read-only access to the 
    /// computed values of an element. See also the ViewCSS 
    /// interface.  The CSS Object Model doesn't provide an access to the 
    /// specified or actual values of the CSS cascade. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Style-20001113'>IDocument Object Model (DOM) Level 2 Style Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class CSSStyleDeclaration
    {
        internal CSS2Properties cssProps = new CSS2Properties();
        internal string _cssText;
        /// <summary> The parsable textual representation of the declaration block 
        /// (excluding the surrounding curly braces). Setting this attribute will 
        /// result in the parsing of the new value and resetting of all the 
        /// properties in the declaration block including the removal or addition 
        /// of properties.</summary>
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified CSS string value has a syntax 
        ///   error and is unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this declaration is 
        ///   readonly or a property is readonly.
        /// </exception>
        public string cssText 
        {
            get {
                return _cssText;
            }
            set
            {
                _cssText = value;
                parseStyleDeclaration();
            }
        }

        internal void parseStyleDeclaration()
        {
            foreach (string style in _cssText.Split(';'))
            {
                string[] parts = style.Split(':');
                cssProps.setStyle(parts[0].Trim(), parts[1].Trim());
            }
        }


        /// <summary> Used to retrieve the value of a CSS property if it has been explicitly 
        /// set within this declaration block.</summary>
        /// <param name="propertyName"> The name of the CSS property. See the CSS property 
        ///   index.</param>
        /// <returns> Returns the value of the property if it has been explicitly 
        ///   set for this declaration block. Returns the empty string if the 
        ///   property has not been set. 
        /// </returns>
        public string getPropertyValue(string propertyName)
        {
            return cssProps.getStyle(propertyName);
        }

        /// <summary> Used to retrieve the object representation of the value of a CSS 
        /// property if it has been explicitly set within this declaration block. 
        /// This method returns null if the property is a shorthand 
        /// property. Shorthand property values can only be accessed and modified 
        /// as strings, using the getPropertyValue and 
        /// setProperty methods.</summary>
        /// <param name="propertyName"> The name of the CSS property. See the CSS property 
        ///   index.</param>
        /// <returns> Returns the value of the property if it has been explicitly 
        ///   set for this declaration block. Returns null if the 
        ///   property has not been set. 
        /// </returns>
        public CSSValue getPropertyCSSValue(string propertyName)
        {
            return cssProps.getCSSValue(propertyName);
        }

        /// <summary> Used to remove a CSS property if it has been explicitly set within 
        /// this declaration block.</summary>
        /// <param name="propertyName"> The name of the CSS property. See the CSS property 
        ///   index.</param>
        /// <returns> Returns the value of the property if it has been explicitly 
        ///   set for this declaration block. Returns the empty string if the 
        ///   property has not been set or the property name does not correspond 
        ///   to a known CSS property.</returns>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this declaration is readonly 
        ///   or the property is readonly.
        /// </exception>
        public string removeProperty(string propertyName)
        {
            if (!cssProps.styles.ContainsKey(propertyName))
                return string.Empty;

            CSSValue val = cssProps.getCSSValue(propertyName);
            cssProps.styles.Remove(propertyName);
            return val.cssText;
        }

        /// <summary> Used to retrieve the priority of a CSS property (e.g. the 
        /// "important" qualifier) if the property has been 
        /// explicitly set in this declaration block.</summary>
        /// <param name="propertyName"> The name of the CSS property. See the CSS property 
        ///   index.</param>
        /// <returns> A string representing the priority (e.g. 
        ///   "important") if one exists. The empty string if none 
        ///   exists.</returns>
        public string getPropertyPriority(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary> Used to set a property value and priority within this declaration 
        /// block.</summary>
        /// <param name="propertyName"> The name of the CSS property. See the CSS property 
        ///   index.</param>
        /// <param name="value"> The new value of the property. 
        /// <param name="priority"> The new priority of the property (e.g. 
        ///   "important").</param> 
        /// <exception cref="DOMException">
        ///   SYNTAX_ERR: Raised if the specified value has a syntax error and is 
        ///   unparsable.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if this declaration is 
        ///   readonly or the property is readonly.
        /// </exception>
        public void setProperty(string propertyName,
                                string value,
                                string priority)
        {
            cssProps.setStyle(propertyName, value);
        }

        /// <summary> The number of properties that have been explicitly set in this 
        /// declaration block. The range of valid indices is 0 to length-1 
        /// inclusive. 
        /// </summary>
        public int length { get { return cssProps.styles.Count; } }

        /// <summary> Used to retrieve the properties that have been explicitly set in this 
        /// declaration block. The order of the properties retrieved using this 
        /// method does not have to be the order in which they were set. This 
        /// method can be used to iterate over all properties in this declaration 
        /// block.</summary>
        /// <param name="index"> Index of the property name to retrieve.</param>
        /// <returns> The name of the property at this ordinal position. The empty 
        ///   string if no property exists at this position.</returns>
        public string item(int index)
        {
            int counter = 0;
            foreach (var item in this.cssProps.styles)
                if (counter == index)
                    return item.Key;
                else
                    counter++;
            return string.Empty;
        }

        /// <summary> The CSS rule that contains this declaration block or null 
        /// if this ICSSStyleDeclaration is not attached to a 
        /// CSSRule. 
        /// </summary>
        public CSSRule parentRule { get; internal set; }

    }
}
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

namespace org.w3c.dom.events
{


    /// <summary> The DocumentEvent interface provides a mechanism by which the 
    /// user can create an IEvent of a type supported by the implementation. It is 
    /// expected that the DocumentEvent interface will be 
    /// implemented on the same object which implements the IDocument 
    /// interface in an implementation which supports the IEvent model. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class DocumentEvent
    {
        /// <summary></summary>
        /// <param name="eventType">The eventType parameter specifies the 
        ///   type of IEvent interface to be created. If the 
        ///   IEvent interface specified is supported by the 
        ///   implementation this method will return a new IEvent of 
        ///   the interface type requested. If the IEvent is to be 
        ///   dispatched via the dispatchEvent method the 
        ///   appropriate event init method must be called after creation in 
        ///   order to initialize the IEvent's values. As an example, 
        ///   a user wishing to synthesize some kind of UIEvent 
        ///   would call createEvent with the parameter "UIEvents". 
        ///   The initUIEvent method could then be called on the 
        ///   newly created UIEvent to set the specific type of 
        ///   UIEvent to be dispatched and set its context information.The 
        ///   createEvent method is used in creating 
        ///   IEvents when it is either inconvenient or unnecessary 
        ///   for the user to create an IEvent themselves. In cases 
        ///   where the implementation provided IEvent is 
        ///   insufficient, users may supply their own IEvent 
        ///   implementations for use with the dispatchEvent method.</param>
        /// <returns>The newly created IEvent</returns>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if the implementation does not support the 
        ///   type of IEvent interface requested</exception>
        public Event createEvent(string eventType)
                                 ; // throws DOMException;

    }
}
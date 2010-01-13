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


    /**
     *  The <code>DocumentEvent</code> interface provides a mechanism by which the 
     * user can create an IEvent of a type supported by the implementation. It is 
     * expected that the <code>DocumentEvent</code> interface will be 
     * implemented on the same object which implements the <code>IDocument</code> 
     * interface in an implementation which supports the IEvent model. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public interface IDocumentEvent
    {
        /**
         * 
         * @param eventTypeThe <code>eventType</code> parameter specifies the 
         *   type of <code>IEvent</code> interface to be created. If the 
         *   <code>IEvent</code> interface specified is supported by the 
         *   implementation this method will return a new <code>IEvent</code> of 
         *   the interface type requested. If the <code>IEvent</code> is to be 
         *   dispatched via the <code>dispatchEvent</code> method the 
         *   appropriate event init method must be called after creation in 
         *   order to initialize the <code>IEvent</code>'s values. As an example, 
         *   a user wishing to synthesize some kind of <code>UIEvent</code> 
         *   would call <code>createEvent</code> with the parameter "UIEvents". 
         *   The <code>initUIEvent</code> method could then be called on the 
         *   newly created <code>UIEvent</code> to set the specific type of 
         *   UIEvent to be dispatched and set its context information.The 
         *   <code>createEvent</code> method is used in creating 
         *   <code>IEvent</code>s when it is either inconvenient or unnecessary 
         *   for the user to create an <code>IEvent</code> themselves. In cases 
         *   where the implementation provided <code>IEvent</code> is 
         *   insufficient, users may supply their own <code>IEvent</code> 
         *   implementations for use with the <code>dispatchEvent</code> method.
         * @return The newly created <code>IEvent</code>
         * @exception DOMException
         *   NOT_SUPPORTED_ERR: Raised if the implementation does not support the 
         *   type of <code>IEvent</code> interface requested
         */
        IEvent createEvent(string eventType)
                                 ; // throws DOMException;

    }
}
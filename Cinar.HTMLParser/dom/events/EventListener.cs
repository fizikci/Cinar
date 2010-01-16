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

namespace org.w3c.dom.events
{

    /// <summary> The EventListener interface is the primary method for 
    /// handling events. Users implement the EventListener interface 
    /// and register their listener on an EventTarget using the 
    /// AddEventListener method. The users should also remove their 
    /// EventListener from its EventTarget after they 
    /// have completed using the listener. 
    ///  When a INode is copied using the cloneNode 
    /// method the EventListeners attached to the source 
    /// INode are not attached to the copied INode. If 
    /// the user wishes the same EventListeners to be added to the 
    /// newly created copy the user must add them manually. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class EventListener
    {
        /// <summary> This method is called whenever an event occurs of the type for which 
        /// the  EventListener interface was registered.</summary>
        /// <param name="evt"> The IEvent contains contextual information 
        ///   about the event. It also contains the stopPropagation 
        ///   and preventDefault methods which are used in 
        ///   determining the event's flow and default action. 
        /// </param>
        public void handleEvent(Event evt)
        {
            throw new NotImplementedException();
        }


    }
}
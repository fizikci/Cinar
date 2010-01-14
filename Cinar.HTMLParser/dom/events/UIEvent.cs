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

namespace org.w3c.dom.events
{


    /// <summary>The UIEvent interface provides specific contextual information 
    /// associated with User Interface events.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class UIEvent : Event
    {
        /// <summary>The view attribute identifies the IAbstractView
        ///  from which the event was generated.
        /// </summary>
        public AbstractView view { get; internal set; }

        /// <summary>Specifies some detail information about the IEvent, 
        /// depending on the type of event.
        /// </summary>
        public int detail { get; internal set; }

        /// <summary>The initUIEvent method is used to initialize the value of 
        /// a UIEvent created through the DocumentEvent 
        /// interface. This method may only be called before the 
        /// UIEvent has been dispatched via the 
        /// dispatchEvent method, though it may be called multiple 
        /// times during that phase if necessary. If called multiple times, the 
        /// final invocation takes precedence.</summary>
        /// <param name="typeArg">Specifies the event type.</param>
        /// <param name="canBubbleArg">Specifies whether or not the event can bubble.</param>
        /// <param name="cancelableArg">Specifies whether or not the event's default 
        ///   action can be prevented.</param>
        /// <param name="viewArg">Specifies the IEvent's 
        ///   IAbstractView.</param>
        /// <param name="detailArg">Specifies the IEvent's detail.</param>
        public void initUIEvent(string typeArg,
                                bool canBubbleArg,
                                bool cancelableArg,
                                AbstractView viewArg,
                                int detailArg);

    }
}
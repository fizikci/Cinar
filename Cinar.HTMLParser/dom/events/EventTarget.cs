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

    /// <summary> The EventTarget interface is implemented by all 
    /// Nodes in an implementation which supports the DOM IEvent 
    /// Model. Therefore, this interface can be obtained by using 
    /// binding-specific casting methods on an instance of the INode 
    /// interface. The interface allows registration and removal of 
    /// EventListeners on an EventTarget and dispatch 
    /// of events to that EventTarget.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class EventTarget
    {
        /// <summary>This method allows the registration of event listeners on the event 
        /// target. If an EventListener is added to an 
        /// EventTarget while it is processing an event, it will not 
        /// be triggered by the current actions but may be triggered during a 
        /// later stage of event flow, such as the bubbling phase. 
        ///  If multiple identical EventListeners are registered 
        /// on the same EventTarget with the same parameters the 
        /// duplicate instances are discarded. They do not cause the 
        /// EventListener to be called twice and since they are 
        /// discarded they do not need to be removed with the 
        /// removeEventListener method.</summary>
        /// <param name="type">The event type for which the user is registering</param>
        /// <param name="listener">The listener parameter takes an interface 
        ///   implemented by the user which contains the methods to be called 
        ///   when the event occurs.</param>
        /// <param name="useCapture">If true, useCapture indicates that the 
        ///   user wishes to initiate capture. After initiating capture, all 
        ///   events of the specified type will be dispatched to the registered 
        ///   EventListener before being dispatched to any 
        ///   EventTargets beneath them in the tree. Events which 
        ///   are bubbling upward through the tree will not trigger an 
        ///   EventListener designated to use capture.
        /// </param>
        public void addEventListener(string type,
                                     EventListener listener,
                                     bool useCapture)
        {
            throw new NotImplementedException();
        }


        /// <summary>This method allows the removal of event listeners from the event 
        /// target. If an EventListener is removed from an 
        /// EventTarget while it is processing an event, it will not 
        /// be triggered by the current actions. EventListeners can 
        /// never be invoked after being removed.
        /// Calling removeEventListener with arguments which do 
        /// not identify any currently registered EventListener on 
        /// the EventTarget has no effect.</summary>
        /// <param name="type">Specifies the event type of the EventListener 
        ///   being removed.</param>
        /// <param name="listener">The EventListener parameter indicates the 
        ///   EventListener  to be removed.</param>
        /// <param name="useCapture">Specifies whether the EventListener 
        ///   being removed was registered as a capturing listener or not. If a 
        ///   listener was registered twice, one with capture and one without, 
        ///   each must be removed separately. Removal of a capturing listener 
        ///   does not affect a non-capturing version of the same listener, and 
        ///   vice versa. 
        /// </param>
        public void removeEventListener(string type,
                                        EventListener listener,
                                        bool useCapture)
        {
            throw new NotImplementedException();
        }


        /// <summary>This method allows the dispatch of events into the implementations 
        /// event model. Events dispatched in this manner will have the same 
        /// capturing and bubbling behavior as events dispatched directly by the 
        /// implementation. The target of the event is the 
        ///  EventTarget on which dispatchEvent is 
        /// called.</summary>
        /// <param name="evt">Specifies the event type, behavior, and contextual 
        ///   information to be used in processing the event.</param>
        /// <returns>The return value of dispatchEvent indicates 
        ///   whether any of the listeners which handled the event called 
        ///   preventDefault. If preventDefault was 
        ///   called the value is false, else the value is true.</returns>
        /// <exception cref="EventException">
        ///   UNSPECIFIED_EVENT_TYPE_ERR: Raised if the IEvent's type 
        ///   was not specified by initializing the event before 
        ///   dispatchEvent was called. Specification of the 
        ///   IEvent's type as null or an empty string 
        ///   will also trigger this exception.
        /// </exception>
        public bool dispatchEvent(Event evt)
        {
            throw new NotImplementedException();
        }
    }
}
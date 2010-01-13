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
     * The <code>IEvent</code> interface is used to provide contextual information 
     * about an event to the handler processing the event. An object which 
     * implements the <code>IEvent</code> interface is generally passed as the 
     * first parameter to an event handler. More specific context information is 
     * passed to event handlers by deriving additional interfaces from 
     * <code>IEvent</code> which contain information directly relating to the 
     * type of event they accompany. These derived interfaces are also 
     * implemented by the object passed to the event listener. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public interface IEvent
    {
        /**
         * The name of the event (case-insensitive). The name must be an XML name.
         */
        string type { get; }

        /**
         * Used to indicate the <code>EventTarget</code> to which the event was 
         * originally dispatched. 
         */
        IEventTarget target { get; }

        /**
         * Used to indicate the <code>EventTarget</code> whose 
         * <code>EventListeners</code> are currently being processed. This is 
         * particularly useful during capturing and bubbling. 
         */
        IEventTarget currentTarget { get; }

        /**
         * Used to indicate which phase of event flow is currently being 
         * evaluated. 
         */
        EventPhaseType eventPhase { get; }

        /**
         * Used to indicate whether or not an event is a bubbling event. If the 
         * event can bubble the value is true, else the value is false. 
         */
        bool bubbles { get; }

        /**
         * Used to indicate whether or not an event can have its default action 
         * prevented. If the default action can be prevented the value is true, 
         * else the value is false. 
         */
        bool cancelable { get; }

        /**
         *  Used to specify the time (in milliseconds relative to the epoch) at 
         * which the event was created. Due to the fact that some systems may 
         * not provide this information the value of <code>timeStamp</code> may 
         * be not available for all events. When not available, a value of 0 
         * will be returned. Examples of epoch time are the time of the system 
         * start or 0:0:0 UTC 1st January 1970. 
         */
        long timeStamp { get; }

        /**
         * The <code>stopPropagation</code> method is used prevent further 
         * propagation of an event during event flow. If this method is called 
         * by any <code>EventListener</code> the event will cease propagating 
         * through the tree. The event will complete dispatch to all listeners 
         * on the current <code>EventTarget</code> before event flow stops. This 
         * method may be used during any stage of event flow.
         */
        void stopPropagation();

        /**
         * If an event is cancelable, the <code>preventDefault</code> method is 
         * used to signify that the event is to be canceled, meaning any default 
         * action normally taken by the implementation as a result of the event 
         * will not occur. If, during any stage of event flow, the 
         * <code>preventDefault</code> method is called the event is canceled. 
         * Any default action associated with the event will not occur. Calling 
         * this method for a non-cancelable event has no effect. Once 
         * <code>preventDefault</code> has been called it will remain in effect 
         * throughout the remainder of the event's propagation. This method may 
         * be used during any stage of event flow. 
         */
        void preventDefault();

        /**
         * The <code>initEvent</code> method is used to initialize the value of an 
         * <code>IEvent</code> created through the <code>DocumentEvent</code> 
         * interface. This method may only be called before the 
         * <code>IEvent</code> has been dispatched via the 
         * <code>dispatchEvent</code> method, though it may be called multiple 
         * times during that phase if necessary. If called multiple times the 
         * final invocation takes precedence. If called from a subclass of 
         * <code>IEvent</code> interface only the values specified in the 
         * <code>initEvent</code> method are modified, all other attributes are 
         * left unchanged.
         * @param eventTypeArgSpecifies the event type. This type may be any 
         *   event type currently defined in this specification or a new event 
         *   type.. The string must be an XML name. Any new event type must not 
         *   begin with any upper, lower, or mixed case version of the string 
         *   "DOM". This prefix is reserved for future DOM event sets. It is 
         *   also strongly recommended that third parties adding their own 
         *   events use their own prefix to avoid confusion and lessen the 
         *   probability of conflicts with other new events.
         * @param canBubbleArgSpecifies whether or not the event can bubble.
         * @param cancelableArgSpecifies whether or not the event's default 
         *   action can be prevented.
         */
        void initEvent(string eventTypeArg,
                              bool canBubbleArg,
                              bool cancelableArg);

    }

    public enum EventPhaseType
    {
        // PhaseType
        /**
         * The current event phase is the capturing phase.
         */
        CAPTURING_PHASE = 1,
        /**
         * The event is currently being evaluated at the target 
         * <code>EventTarget</code>.
         */
        AT_TARGET = 2,
        /**
         * The current event phase is the bubbling phase.
         */
        BUBBLING_PHASE = 3
    }
}
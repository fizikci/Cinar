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


    /**
     * The <code>UIEvent</code> interface provides specific contextual information 
     * associated with User Interface events.
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public interface IUIEvent : IEvent
    {
        /**
         * The <code>view</code> attribute identifies the <code>IAbstractView</code>
         *  from which the event was generated.
         */
        IAbstractView view { get; }

        /**
         * Specifies some detail information about the <code>IEvent</code>, 
         * depending on the type of event.
         */
        int detail { get; }

        /**
         * The <code>initUIEvent</code> method is used to initialize the value of 
         * a <code>UIEvent</code> created through the <code>DocumentEvent</code> 
         * interface. This method may only be called before the 
         * <code>UIEvent</code> has been dispatched via the 
         * <code>dispatchEvent</code> method, though it may be called multiple 
         * times during that phase if necessary. If called multiple times, the 
         * final invocation takes precedence.
         * @param typeArgSpecifies the event type.
         * @param canBubbleArgSpecifies whether or not the event can bubble.
         * @param cancelableArgSpecifies whether or not the event's default 
         *   action can be prevented.
         * @param viewArgSpecifies the <code>IEvent</code>'s 
         *   <code>IAbstractView</code>.
         * @param detailArgSpecifies the <code>IEvent</code>'s detail.
         */
        void initUIEvent(string typeArg,
                                bool canBubbleArg,
                                bool cancelableArg,
                                IAbstractView viewArg,
                                int detailArg);

    }
}
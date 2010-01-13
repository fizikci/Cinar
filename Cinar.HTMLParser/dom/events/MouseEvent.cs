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
     * The <code>MouseEvent</code> interface provides specific contextual 
     * information associated with Mouse events.
     * <p>The <code>detail</code> attribute inherited from <code>UIEvent</code> 
     * indicates the number of times a mouse button has been pressed and 
     * released over the same screen location during a user action. The 
     * attribute value is 1 when the user begins this action and increments by 1 
     * for each full sequence of pressing and releasing. If the user moves the 
     * mouse between the mousedown and mouseup the value will be set to 0, 
     * indicating that no click is occurring.
     * <p>In the case of nested elements mouse events are always targeted at the 
     * most deeply nested element. Ancestors of the targeted element may use 
     * bubbling to obtain notification of mouse events which occur within its 
     * descendent elements.
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public interface IMouseEvent : IUIEvent
    {
        /**
         * The horizontal coordinate at which the event occurred relative to the 
         * origin of the screen coordinate system.
         */
        int screenX { get; }

        /**
         * The vertical coordinate at which the event occurred relative to the 
         * origin of the screen coordinate system.
         */
        int screenY { get; }

        /**
         * The horizontal coordinate at which the event occurred relative to the 
         * DOM implementation's client area.
         */
        int clientX { get; }

        /**
         * The vertical coordinate at which the event occurred relative to the DOM 
         * implementation's client area.
         */
        int clientY { get; }

        /**
         * Used to indicate whether the 'ctrl' key was depressed during the firing 
         * of the event.
         */
        bool ctrlKey { get; }

        /**
         * Used to indicate whether the 'shift' key was depressed during the 
         * firing of the event.
         */
        bool shiftKey { get; }

        /**
         * Used to indicate whether the 'alt' key was depressed during the firing 
         * of the event. On some platforms this key may map to an alternative 
         * key name.
         */
        bool altKey { get; }

        /**
         * Used to indicate whether the 'meta' key was depressed during the firing 
         * of the event. On some platforms this key may map to an alternative 
         * key name.
         */
        bool metaKey { get; }

        /**
         * During mouse events caused by the depression or release of a mouse 
         * button, <code>button</code> is used to indicate which mouse button 
         * changed state. The values for <code>button</code> range from zero to 
         * indicate the left button of the mouse, one to indicate the middle 
         * button if present, and two to indicate the right button. For mice 
         * configured for left handed use in which the button actions are 
         * reversed the values are instead read from right to left.
         */
        short button { get; }

        /**
         * Used to identify a secondary <code>EventTarget</code> related to a UI 
         * event. Currently this attribute is used with the mouseover event to 
         * indicate the <code>EventTarget</code> which the pointing device 
         * exited and with the mouseout event to indicate the 
         * <code>EventTarget</code> which the pointing device entered.
         */
        IEventTarget relatedTarget { get; }

        /**
         * The <code>initMouseEvent</code> method is used to initialize the value 
         * of a <code>MouseEvent</code> created through the 
         * <code>DocumentEvent</code> interface. This method may only be called 
         * before the <code>MouseEvent</code> has been dispatched via the 
         * <code>dispatchEvent</code> method, though it may be called multiple 
         * times during that phase if necessary. If called multiple times, the 
         * final invocation takes precedence.
         * @param typeArgSpecifies the event type.
         * @param canBubbleArgSpecifies whether or not the event can bubble.
         * @param cancelableArgSpecifies whether or not the event's default 
         *   action can be prevented.
         * @param viewArgSpecifies the <code>IEvent</code>'s 
         *   <code>IAbstractView</code>.
         * @param detailArgSpecifies the <code>IEvent</code>'s mouse click count.
         * @param screenXArgSpecifies the <code>IEvent</code>'s screen x coordinate
         * @param screenYArgSpecifies the <code>IEvent</code>'s screen y coordinate
         * @param clientXArgSpecifies the <code>IEvent</code>'s client x coordinate
         * @param clientYArgSpecifies the <code>IEvent</code>'s client y coordinate
         * @param ctrlKeyArgSpecifies whether or not control key was depressed 
         *   during the <code>IEvent</code>.
         * @param altKeyArgSpecifies whether or not alt key was depressed during 
         *   the <code>IEvent</code>.
         * @param shiftKeyArgSpecifies whether or not shift key was depressed 
         *   during the <code>IEvent</code>.
         * @param metaKeyArgSpecifies whether or not meta key was depressed 
         *   during the <code>IEvent</code>.
         * @param buttonArgSpecifies the <code>IEvent</code>'s mouse button.
         * @param relatedTargetArgSpecifies the <code>IEvent</code>'s related 
         *   <code>EventTarget</code>.
         */
        void initMouseEvent(string typeArg,
                                   bool canBubbleArg,
                                   bool cancelableArg,
                                   IAbstractView viewArg,
                                   int detailArg,
                                   int screenXArg,
                                   int screenYArg,
                                   int clientXArg,
                                   int clientYArg,
                                   bool ctrlKeyArg,
                                   bool altKeyArg,
                                   bool shiftKeyArg,
                                   bool metaKeyArg,
                                   short buttonArg,
                                   IEventTarget relatedTargetArg);

    }
}
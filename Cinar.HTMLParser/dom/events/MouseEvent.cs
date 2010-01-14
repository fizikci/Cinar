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


    /// <summary>The MouseEvent interface provides specific contextual 
    /// information associated with Mouse events.
    /// The detail attribute inherited from UIEvent 
    /// indicates the number of times a mouse button has been pressed and 
    /// released over the same screen location during a user action. The 
    /// attribute value is 1 when the user begins this action and increments by 1 
    /// for each full sequence of pressing and releasing. If the user moves the 
    /// mouse between the mousedown and mouseup the value will be set to 0, 
    /// indicating that no click is occurring.
    /// In the case of nested elements mouse events are always targeted at the 
    /// most deeply nested element. Ancestors of the targeted element may use 
    /// bubbling to obtain notification of mouse events which occur within its 
    /// descendent elements.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class MouseEvent : UIEvent
    {
        /// <summary>The horizontal coordinate at which the event occurred relative to the 
        /// origin of the screen coordinate system.
        /// </summary>
        public int screenX { get; internal set; }

        /// <summary>The vertical coordinate at which the event occurred relative to the 
        /// origin of the screen coordinate system.
        /// </summary>
        public int screenY { get; internal set; }

        /// <summary>The horizontal coordinate at which the event occurred relative to the 
        /// DOM implementation's client area.
        /// </summary>
        public int clientX { get; internal set; }

        /// <summary>The vertical coordinate at which the event occurred relative to the DOM 
        /// implementation's client area.
        /// </summary>
        public int clientY { get; internal set; }

        /// <summary>Used to indicate whether the 'ctrl' key was depressed during the firing 
        /// of the event.
        /// </summary>
        public bool ctrlKey { get; internal set; }

        /// <summary>Used to indicate whether the 'shift' key was depressed during the 
        /// firing of the event.
        /// </summary>
        public bool shiftKey { get; internal set; }

        /// <summary>Used to indicate whether the 'alt' key was depressed during the firing 
        /// of the event. On some platforms this key may map to an alternative 
        /// key name.
        /// </summary>
        public bool altKey { get; internal set; }

        /// <summary>Used to indicate whether the 'meta' key was depressed during the firing 
        /// of the event. On some platforms this key may map to an alternative 
        /// key name.
        /// </summary>
        public bool metaKey { get; internal set; }

        /// <summary>During mouse events caused by the depression or release of a mouse 
        /// button, button is used to indicate which mouse button 
        /// changed state. The values for button range from zero to 
        /// indicate the left button of the mouse, one to indicate the middle 
        /// button if present, and two to indicate the right button. For mice 
        /// configured for left handed use in which the button actions are 
        /// reversed the values are instead read from right to left.
        /// </summary>
        public short button { get; internal set; }

        /// <summary>Used to identify a secondary EventTarget related to a UI 
        /// event. Currently this attribute is used with the mouseover event to 
        /// indicate the EventTarget which the pointing device 
        /// exited and with the mouseout event to indicate the 
        /// EventTarget which the pointing device entered.
        /// </summary>
        public EventTarget relatedTarget { get; internal set; }

        /// <summary>The initMouseEvent method is used to initialize the value 
        /// of a MouseEvent created through the 
        /// DocumentEvent interface. This method may only be called 
        /// before the MouseEvent has been dispatched via the 
        /// dispatchEvent method, though it may be called multiple 
        /// times during that phase if necessary. If called multiple times, the 
        /// final invocation takes precedence.</summary>
        /// <param name="typeArg">Specifies the event type.</param>
        /// <param name="canBubbleArg">Specifies whether or not the event can bubble.</param>
        /// <param name="cancelableArg">Specifies whether or not the event's default 
        ///   action can be prevented.</param>
        /// <param name="viewArg">Specifies the IEvent's 
        ///   IAbstractView.</param>
        /// <param name="detailArg">Specifies the IEvent's mouse click count.</param>
        /// <param name="screenXArg">Specifies the IEvent's screen x coordinate</param>
        /// <param name="screenYArg">Specifies the IEvent's screen y coordinate</param>
        /// <param name="clientXArg">Specifies the IEvent's client x coordinate</param>
        /// <param name="clientYArg">Specifies the IEvent's client y coordinate</param>
        /// <param name="ctrlKeyArg">Specifies whether or not control key was depressed 
        ///   during the IEvent.</param>
        /// <param name="altKeyArg">Specifies whether or not alt key was depressed during 
        ///   the IEvent.</param>
        /// <param name="shiftKeyArg">Specifies whether or not shift key was depressed 
        ///   during the IEvent.</param>
        /// <param name="metaKeyArg">Specifies whether or not meta key was depressed 
        ///   during the IEvent.</param>
        /// <param name="buttonArg">Specifies the IEvent's mouse button.</param>
        /// <param name="relatedTargetArg">Specifies the IEvent's related 
        ///   EventTarget.</param>
        public void initMouseEvent(string typeArg,
                                   bool canBubbleArg,
                                   bool cancelableArg,
                                   AbstractView viewArg,
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
                                   EventTarget relatedTargetArg);

    }
}
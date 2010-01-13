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
     * The <code>MutationEvent</code> interface provides specific contextual 
     * information associated with Mutation events. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public interface IMutationEvent : IEvent
    {
        /**
         *  <code>relatedNode</code> is used to identify a secondary node related 
         * to a mutation event. For example, if a mutation event is dispatched 
         * to a node indicating that its parent has changed, the 
         * <code>relatedNode</code> is the changed parent. If an event is 
         * instead dispatched to a subtree indicating a node was changed within 
         * it, the <code>relatedNode</code> is the changed node. In the case of 
         * the DOMAttrModified event it indicates the <code>Attr</code> node 
         * which was modified, added, or removed. 
         */
        INode relatedNode { get; }

        /**
         *  <code>prevValue</code> indicates the previous value of the 
         * <code>Attr</code> node in DOMAttrModified events, and of the 
         * <code>CharacterData</code> node in DOMCharDataModified events. 
         */
        string prevValue { get; }

        /**
         *  <code>newValue</code> indicates the new value of the <code>Attr</code> 
         * node in DOMAttrModified events, and of the <code>CharacterData</code> 
         * node in DOMCharDataModified events. 
         */
        string newValue { get; }

        /**
         *  <code>attrName</code> indicates the name of the changed 
         * <code>Attr</code> node in a DOMAttrModified event. 
         */
        string attrName { get; }

        /**
         *  <code>attrChange</code> indicates the type of change which triggered 
         * the DOMAttrModified event. The values can be <code>MODIFICATION</code>
         * , <code>ADDITION</code>, or <code>REMOVAL</code>. 
         */
        MutationEventType attrChange { get; }

        /**
         * The <code>initMutationEvent</code> method is used to initialize the 
         * value of a <code>MutationEvent</code> created through the 
         * <code>DocumentEvent</code> interface. This method may only be called 
         * before the <code>MutationEvent</code> has been dispatched via the 
         * <code>dispatchEvent</code> method, though it may be called multiple 
         * times during that phase if necessary. If called multiple times, the 
         * final invocation takes precedence.
         * @param typeArgSpecifies the event type.
         * @param canBubbleArgSpecifies whether or not the event can bubble.
         * @param cancelableArgSpecifies whether or not the event's default 
         *   action can be prevented.
         * @param relatedNodeArgSpecifies the <code>IEvent</code>'s related INode.
         * @param prevValueArgSpecifies the <code>IEvent</code>'s 
         *   <code>prevValue</code> attribute. This value may be null.
         * @param newValueArgSpecifies the <code>IEvent</code>'s 
         *   <code>newValue</code> attribute. This value may be null.
         * @param attrNameArgSpecifies the <code>IEvent</code>'s 
         *   <code>attrName</code> attribute. This value may be null.
         * @param attrChangeArgSpecifies the <code>IEvent</code>'s 
         *   <code>attrChange</code> attribute
         */
        void initMutationEvent(string typeArg,
                                      bool canBubbleArg,
                                      bool cancelableArg,
                                      INode relatedNodeArg,
                                      string prevValueArg,
                                      string newValueArg,
                                      string attrNameArg,
                                      MutationEventType attrChangeArg);

    }

    public enum MutationEventType
    { 
            // attrChangeType
        /**
         * The <code>Attr</code> was modified in place.
         */
        MODIFICATION = 1,
        /**
         * The <code>Attr</code> was just added.
         */
        ADDITION = 2,
        /**
         * The <code>Attr</code> was just removed.
         */
        REMOVAL = 3
    }
}


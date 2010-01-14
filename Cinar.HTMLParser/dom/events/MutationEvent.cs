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


    /// <summary>The MutationEvent interface provides specific contextual 
    /// information associated with Mutation events. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class MutationEvent : Event
    {
        /// <summary> relatedNode is used to identify a secondary node related 
        /// to a mutation event. For example, if a mutation event is dispatched 
        /// to a node indicating that its parent has changed, the 
        /// relatedNode is the changed parent. If an event is 
        /// instead dispatched to a subtree indicating a node was changed within 
        /// it, the relatedNode is the changed node. In the case of 
        /// the DOMAttrModified event it indicates the Attr node 
        /// which was modified, added, or removed. 
        /// </summary>
        public Node relatedNode { get; internal set; }

        /// <summary> prevValue indicates the previous value of the 
        /// Attr node in DOMAttrModified events, and of the 
        /// CharacterData node in DOMCharDataModified events. 
        /// </summary>
        public string prevValue { get; internal set; }

        /// <summary> newValue indicates the new value of the Attr 
        /// node in DOMAttrModified events, and of the CharacterData 
        /// node in DOMCharDataModified events. 
        /// </summary>
        public string newValue { get; internal set; }

        /// <summary> attrName indicates the name of the changed 
        /// Attr node in a DOMAttrModified event. 
        /// </summary>
        public string attrName { get; internal set; }

        /// <summary> attrChange indicates the type of change which triggered 
        /// the DOMAttrModified event. The values can be MODIFICATION
        /// , ADDITION, or REMOVAL. 
        /// </summary>
        public AttrChangeType attrChange { get; internal set; }

        /// <summary>The initMutationEvent method is used to initialize the 
        /// value of a MutationEvent created through the 
        /// DocumentEvent interface. This method may only be called 
        /// before the MutationEvent has been dispatched via the 
        /// dispatchEvent method, though it may be called multiple 
        /// times during that phase if necessary. If called multiple times, the 
        /// final invocation takes precedence.</summary>
        /// <param name="typeArg">Specifies the event type.</param>
        /// <param name="canBubbleArg">Specifies whether or not the event can bubble.</param>
        /// <param name="cancelableArg">Specifies whether or not the event's default 
        ///   action can be prevented.</param>
        /// <param name="relatedNodeArg">Specifies the IEvent's related INode.</param>
        /// <param name="prevValueArg">Specifies the IEvent's 
        ///   prevValue attribute. This value may be null.</param>
        /// <param name="newValueArg">Specifies the IEvent's 
        ///   newValue attribute. This value may be null.</param>
        /// <param name="attrNameArg">Specifies the IEvent's 
        ///   attrName attribute. This value may be null.</param>
        /// <param name="attrChangeArg">Specifies the IEvent's 
        ///   attrChange attribute</param>
        public void initMutationEvent(string typeArg,
                                      bool canBubbleArg,
                                      bool cancelableArg,
                                      Node relatedNodeArg,
                                      string prevValueArg,
                                      string newValueArg,
                                      string attrNameArg,
                                      AttrChangeType attrChangeArg);

    }

    public enum AttrChangeType
    { 
        /// <summary>The Attr was modified in place.
        /// </summary>
        MODIFICATION = 1,
        /// <summary>The Attr was just added.
        /// </summary>
        ADDITION = 2,
        /// <summary>The Attr was just removed.
        /// </summary>
        REMOVAL = 3
    }
}


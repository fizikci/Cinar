/*
 * Copyright (c) 2002 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

using org.w3c.dom.events;

namespace org.w3c.dom.ls
{


    /// <summary> This interface represents a load event object that signals the completion 
    /// of a document load. 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public interface ILSLoadEvent : IEvent
    {
        /// <summary>The document that finished loading.
        /// </summary>
        IDocument newDocument { get; }

        /// <summary>The input source that was parsed.
        /// </summary>
        IDOMInputSource inputSource { get; }

    }
}
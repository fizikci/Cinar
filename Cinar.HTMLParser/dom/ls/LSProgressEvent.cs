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

    /// <summary> This interface represents a progress event object that notifies the 
    /// application about progress as a document is parsed. It : the 
    /// IEvent interface defined in .
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public interface ILSProgressEvent : IEvent
    {
        /// <summary>The input source that is being parsed.
        /// </summary>
        IDOMInputSource inputSource { get; }

        /// <summary>The current position in the input source, including all external 
        /// entities and other resources that have been read.
        /// </summary>
        int position { get; }

        /// <summary>The total size of the document including all external resources, this 
        /// number might change as a document is being parsed if references to 
        /// more external resources are seen.
        /// </summary>
        int totalSize { get; }

    }
}
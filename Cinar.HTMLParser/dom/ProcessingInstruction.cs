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

namespace org.w3c.dom
{

    /// <summary>The ProcessingInstruction interface represents a "processing 
    /// instruction", used in XML as a way to keep processor-specific information 
    /// in the text of the document.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public class ProcessingInstruction : Node
    {
        /// <summary>The target of this processing instruction. XML defines this as being 
        /// the first token following the markup that begins the processing 
        /// instruction.
        /// </summary>
        public string target
        {
            get;
            internal set;
        }

        /// <summary>The content of this processing instruction. This is from the first non 
        /// white space character after the target to the character immediately 
        /// preceding the ?&gt;.</summary>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised when the node is readonly.
        /// </exception>
        public string data { get; set; }

    }
}
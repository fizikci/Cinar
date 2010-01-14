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

    /// <summary>This interface represents a notation declared in the DTD. A notation either 
    /// declares, by name, the format of an unparsed entity (see section 4.7 of 
    /// the XML 1.0 specification ), or is used for formal declaration of 
    /// processing instruction targets (see section 2.6 of the XML 1.0 
    /// specification ). The nodeName attribute inherited from 
    /// INode is set to the declared name of the notation.
    /// The DOM Level 1 does not support editing Notation nodes; 
    /// they are therefore readonly.
    /// A Notation node does not have any parent.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface INotation : INode
    {
        /// <summary>The public identifier of this notation. If the public identifier was 
        /// not specified, this is null.
        /// </summary>
        string publicId { get; }

        /// <summary>The system identifier of this notation. If the system identifier was 
        /// not specified, this is null.
        /// </summary>
        string systemId { get; }

    }
}
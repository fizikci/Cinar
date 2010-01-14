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

    /// <summary>The NodeList interface provides the abstraction of an ordered 
    /// collection of nodes, without defining or constraining how this collection 
    /// is implemented. NodeList objects in the DOM are live.
    /// The items in the NodeList are accessible via an integral 
    /// index, starting from 0.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface INodeList
    {
        /// <summary>Returns the indexth item in the collection. If 
        /// index is greater than or equal to the number of nodes in 
        /// the list, this returns null.</summary>
        /// <param name="indexIndex"> into the collection.</param>
        /// <returns>The node at the indexth position in the 
        ///   NodeList, or null if that is not a valid 
        ///   index.</returns>
        INode item(int index);

        /// <summary>The number of nodes in the list. The range of valid child node indices 
        /// is 0 to length-1 inclusive. 
        /// </summary>
        int length { get; }

    }
}
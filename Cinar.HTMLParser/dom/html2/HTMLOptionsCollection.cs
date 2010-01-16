/*
 * Copyright (c) 2003 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */


using System;

namespace org.w3c.dom.html2
{

    /// <summary> An HTMLOptionsCollection is a list of nodes representing HTML 
    /// option element. An individual node may be accessed by either ordinal 
    /// index or the node's name or id attributes.  
    /// Collections in the HTML DOM are assumed to be live meaning that they are 
    /// automatically updated when the underlying document is changed. 
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class HTMLOptionsCollection
    {
        /// <summary> This attribute specifies the length or size of the list. 
        /// </summary>
        public int length { get; set; }
        /// <summary> This attribute specifies the length or size of the list. 
        /// <exception cref="DOMException">
        ///    NOT_SUPPORTED_ERR: if setting the length is not allowed by the 
        ///   implementation. 
        /// </summary>
        public void setLength(int length) { throw new NotImplementedException(); }

        /// <summary> This method retrieves a node specified by ordinal index. Nodes are 
        /// numbered in tree order (depth-first traversal order). 
        /// <param name="index"> The index of the node to be fetched. The index origin is 
        ///   0.
        /// <returns>The INode at the corresponding position upon 
        ///   success. A value of null is returned if the index is 
        ///   out of range. 
        /// </summary>
        public Node item(int index)
        {
            throw new NotImplementedException();
        }


        /// <summary>This method retrieves a INode using a name. It first 
        /// searches for a INode with a matching id 
        /// attribute. If it doesn't find one, it then searches for a 
        /// INode with a matching name attribute, but 
        /// only on those elements that are allowed a name attribute. This method 
        /// is case insensitive in HTML documents and case sensitive in XHTML 
        /// documents.
        /// <param name="name"> The name of the INode to be fetched.
        /// <returns>The INode with a name or 
        ///   id attribute whose value corresponds to the specified 
        ///   string. Upon failure (e.g., no node with this name exists), returns 
        ///   null.
        /// </summary>
        public Node namedItem(string name)
        {
            throw new NotImplementedException();
        }


    }
}
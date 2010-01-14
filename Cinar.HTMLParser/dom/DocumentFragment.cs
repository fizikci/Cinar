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

    /// <summary>DocumentFragment is a "lightweight" or "minimal" 
    /// IDocument object. It is very common to want to be able to 
    /// extract a portion of a document's tree or to create a new fragment of a 
    /// document. Imagine implementing a user command like cut or rearranging a 
    /// document by moving fragments around. It is desirable to have an object 
    /// which can hold such fragments and it is quite natural to use a INode for 
    /// this purpose. While it is true that a IDocument object could 
    /// fulfill this role, a IDocument object can potentially be a 
    /// heavyweight object, depending on the underlying implementation. What is 
    /// really needed for this is a very lightweight object. 
    /// DocumentFragment is such an object.
    /// Furthermore, various operations -- such as inserting nodes as children 
    /// of another INode -- may take DocumentFragment 
    /// objects as arguments; this results in all the child nodes of the 
    /// DocumentFragment being moved to the child list of this node.
    /// The children of a DocumentFragment node are zero or more 
    /// nodes representing the tops of any sub-trees defining the structure of 
    /// the document. DocumentFragment nodes do not need to be 
    /// well-formed XML documents (although they do need to follow the rules 
    /// imposed upon well-formed XML parsed entities, which can have multiple top 
    /// nodes). For example, a DocumentFragment might have only one 
    /// child and that child node could be a Text node. Such a 
    /// structure model represents neither an HTML document nor a well-formed XML 
    /// document.
    /// When a DocumentFragment is inserted into a 
    /// IDocument (or indeed any other INode that may 
    /// take children) the children of the DocumentFragment and not 
    /// the DocumentFragment itself are inserted into the 
    /// INode. This makes the DocumentFragment very 
    /// useful when the user wishes to create nodes that are siblings; the 
    /// DocumentFragment acts as the parent of these nodes so that 
    /// the user can use the standard methods from the INode 
    /// interface, such as insertBefore and appendChild.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IDocumentFragment : INode
    {
    }
}
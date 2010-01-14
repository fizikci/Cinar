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

    /// <summary>EntityReference objects may be inserted into the structure 
    /// model when an entity reference is in the source document, or when the 
    /// user wishes to insert an entity reference. Note that character references 
    /// and references to predefined entities are considered to be expanded by 
    /// the HTML or XML processor so that characters are represented by their 
    /// Unicode equivalent rather than by an entity reference. Moreover, the XML 
    /// processor may completely expand references to entities while building the 
    /// structure model, instead of providing EntityReference 
    /// objects. If it does provide such objects, then for a given 
    /// EntityReference node, it may be that there is no 
    /// Entity node representing the referenced entity. If such an 
    /// Entity exists, then the subtree of the 
    /// EntityReference node is in general a copy of the 
    /// Entity node subtree. However, this may not be true when an 
    /// entity contains an unbound namespace prefix. In such a case, because the 
    /// namespace prefix resolution depends on where the entity reference is, the 
    /// descendants of the EntityReference node may be bound to 
    /// different namespace URIs.
    /// As for Entity nodes, EntityReference nodes and 
    /// all their descendants are readonly.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IEntityReference : INode
    {
    }
}
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

    /// <summary> The Attr interface represents an attribute in an 
    /// IElement object. Typically the allowable values for the 
    /// attribute are defined in a document type definition.
    /// Attr objects inherit the INode interface, but 
    /// since they are not actually child nodes of the element they describe, the 
    /// DOM does not consider them part of the document tree. Thus, the 
    /// INode attributes parentNode, 
    /// previousSibling, and nextSibling have a 
    /// null value for Attr objects. The DOM takes the 
    /// view that attributes are properties of elements rather than having a 
    /// separate identity from the elements they are associated with; this should 
    /// make it more efficient to implement such features as default attributes 
    /// associated with all elements of a given type. Furthermore, 
    /// Attr nodes may not be immediate children of a 
    /// DocumentFragment. However, they can be associated with 
    /// IElement nodes contained within a 
    /// DocumentFragment. In short, users and implementors of the 
    /// DOM need to be aware that Attr nodes have some things in 
    /// common with other objects inheriting the INode interface, but 
    /// they also are quite distinct.
    ///  The attribute's effective value is determined as follows: if this 
    /// attribute has been explicitly assigned any value, that value is the 
    /// attribute's effective value; otherwise, if there is a declaration for 
    /// this attribute, and that declaration includes a default value, then that 
    /// default value is the attribute's effective value; otherwise, the 
    /// attribute does not exist on this element in the structure model until it 
    /// has been explicitly added. Note that the nodeValue attribute 
    /// on the Attr instance can also be used to retrieve the string 
    /// version of the attribute's value(s). 
    /// In XML, where the value of an attribute can contain entity references, 
    /// the child nodes of the Attr node may be either 
    /// Text or EntityReference nodes (when these are 
    /// in use; see the description of EntityReference for 
    /// discussion). Because the DOM Core is not aware of attribute types, it 
    /// treats all attribute values as simple strings, even if the DTD or schema 
    /// declares them as having tokenized types. 
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>IDocument Object Model (DOM) Level 2 Core Specification</a>.
    /// </summary>
    public interface IAttr : INode
    {
        /// <summary>Returns the name of this attribute. 
        /// </summary>
        string name { get; }

        /// <summary>If this attribute was explicitly given a value in the original 
        /// document, this is true; otherwise, it is 
        /// false. Note that the implementation is in charge of this 
        /// attribute, not the user. If the user changes the value of the 
        /// attribute (even if it ends up having the same value as the default 
        /// value) then the specified flag is automatically flipped 
        /// to true. To re-specify the attribute as the default 
        /// value from the DTD, the user must delete the attribute. The 
        /// implementation will then make a new attribute available with 
        /// specified set to false and the default 
        /// value (if one exists).
        /// In summary:  If the attribute has an assigned value in the document 
        /// then specified is true, and the value is 
        /// the assigned value.  If the attribute has no assigned value in the 
        /// document and has a default value in the DTD, then 
        /// specified is false, and the value is the 
        /// default value in the DTD. If the attribute has no assigned value in 
        /// the document and has a value of #IMPLIED in the DTD, then the 
        /// attribute does not appear in the structure model of the document. If 
        /// the ownerElement attribute is null (i.e. 
        /// because it was just created or was set to null by the 
        /// various removal and cloning operations) specified is 
        /// true. 
        /// </summary>
        bool specified { get; }

        /// <summary>On retrieval, the value of the attribute is returned as a string. 
        /// Character and general entity references are replaced with their 
        /// values. See also the method getAttribute on the 
        /// IElement interface.
        /// On setting, this creates a Text node with the unparsed 
        /// contents of the string. I.e. any characters that an XML processor 
        /// would recognize as markup are instead treated as literal text. See 
        /// also the method setAttribute on the IElement 
        /// interface.</summary>
        /// <exception cref="DOMException">
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised when the node is readonly.
        /// </exception>
        string value { get; set; } // throws DOMException;

        /// <summary>The IElement node this attribute is attached to or 
        /// null if this attribute is not in use.
        /// @since DOM Level 2
        /// </summary>
        IElement ownerElement { get; }

    }
}
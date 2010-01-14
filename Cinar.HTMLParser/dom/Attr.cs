using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class Attr : Node
    {
        internal string _name;
        internal bool _specified;
        private string _value;
        internal Element _ownerElement;

        public Attr() {
            _nodeType = NodeType.ATTRIBUTE_NODE;
        }

        public override Document ownerDocument
        {
            get
            {
                return ownerElement.ownerDocument;
            }
        }

        /// <summary>Returns the name of this attribute.</summary>
        public string name
        {
            get { return _name; }
        }

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
        public bool specified
        {
            get { return _specified; }
        }

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
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>The IElement node this attribute is attached to or 
        /// null if this attribute is not in use.
        /// @since DOM Level 2
        /// </summary>
        public Element ownerElement
        {
            get { return _ownerElement; }
        }
    }
}

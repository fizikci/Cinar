using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class DOMImplementation
    {
        /// <summary>Test if the DOM implementation implements a specific feature.</summary>
        /// <param name="feature">The name of the feature to test (case-insensitive). The 
        ///   values used by DOM features are defined throughout the DOM Level 2 
        ///   specifications and listed in the  section. The name must be an XML 
        ///   name. To avoid possible conflicts, as a convention, names referring 
        ///   to features defined outside the DOM specification should be made 
        ///   unique by reversing the name of the Internet domain name of the 
        ///   person (or the organization that the person belongs to) who defines 
        ///   the feature, component by component, and using this as a prefix. 
        ///   For instance, the W3C SVG Working Group defines the feature 
        ///   "org.w3c.dom.svg".</param>
        /// <param name="versionThis"> is the version number of the feature to test. In 
        ///   Level 2, the string can be either "2.0" or "1.0". If the version is 
        ///   not specified, supporting any version of the feature causes the 
        ///   method to return true.</param>
        /// <returns>true if the feature is implemented in the 
        ///   specified version, false otherwise.
        /// </returns>
        public bool hasFeature(string feature, string version)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates an empty DocumentType node. Entity declarations 
        /// and notations are not made available. Entity reference expansions and 
        /// default attribute additions do not occur. It is expected that a 
        /// future version of the DOM will provide a way for populating a 
        /// DocumentType.
        /// HTML-only DOM implementations do not need to implement this method.</summary>
        /// <param name="qualifiedName">The qualified name of the document type to be 
        ///   created. </param>
        /// <param name="publicId">The external subset public identifier.</param>
        /// <param name="systemId">The external subset system identifier.</param>
        /// <returns>A new DocumentType node with 
        ///   INode.ownerDocument set to null.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified qualified name 
        ///   contains an illegal character.
        ///   NAMESPACE_ERR: Raised if the qualifiedName is 
        ///   malformed.
        /// </exception>
        public DocumentType createDocumentType(string qualifiedName, string publicId, string systemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates an XML IDocument object of the specified type with 
        /// its document element. HTML-only DOM implementations do not need to 
        /// implement this method.</summary>
        /// <param name="namespaceURI">The namespace URI of the document element to create.</param>
        /// <param name="qualifiedName">The qualified name of the document element to be 
        ///   created.</param>
        /// <param name="doctype">The type of document to be created or null.
        ///   When doctype is not null, its 
        ///   INode.ownerDocument attribute is set to the document 
        ///   being created.</param>
        /// <returns>A new IDocument object.</returns>
        /// <exception cref="DOMException">
        ///   INVALID_CHARACTER_ERR: Raised if the specified qualified name 
        ///   contains an illegal character.
        ///   NAMESPACE_ERR: Raised if the qualifiedName is 
        ///   malformed, if the qualifiedName has a prefix and the 
        ///   namespaceURI is null, or if the 
        ///   qualifiedName has a prefix that is "xml" and the 
        ///   namespaceURI is different from "
        ///   http://www.w3.org/XML/1998/namespace" .
        ///   WRONG_DOCUMENT_ERR: Raised if doctype has already 
        ///   been used with a different document or was created from a different 
        ///   implementation.
        /// </exception>
        public Document createDocument(string namespaceURI, string qualifiedName, DocumentType doctype)
        {
            throw new NotImplementedException();
        }
    }
}

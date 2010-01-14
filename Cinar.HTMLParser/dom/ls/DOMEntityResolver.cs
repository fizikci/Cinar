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

namespace org.w3c.dom.ls
{

    /// <summary>DOMEntityResolver Provides a way for applications to redirect 
    /// references to external entities.
    /// Applications needing to implement customized handling for external 
    /// entities must implement this interface and register their implementation 
    /// by setting the entityResolver attribute of the 
    /// DOMBuilder. 
    /// The DOMBuilder will then allow the application to intercept 
    /// any external entities (including the external DTD subset and external 
    /// parameter entities) before including them.
    /// Many DOM applications will not need to implement this interface, but it 
    /// will be especially useful for applications that build XML documents from 
    /// databases or other specialized input sources, or for applications that 
    /// use URNs. DOMEntityResolver is based on the SAX2  
    /// EntityResolver interface. 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public class DOMEntityResolver
    {
        /// <summary>Allow the application to resolve external entities. 
        /// The DOMBuilder will call this method before opening 
        /// any external entity except the top-level document entity (including 
        /// the external DTD subset, external entities referenced within the DTD, 
        /// and external entities referenced within the document element); the 
        /// application may request that the DOMBuilder resolve the 
        /// entity itself, that it use an alternative URI, or that it use an 
        /// entirely different input source.
        /// Application writers can use this method to redirect external system 
        /// identifiers to secure and/or local URIs, to look up public 
        /// identifiers in a catalogue, or to read an entity from a database or 
        /// other input source (including, for example, a dialog box).
        /// If the system identifier is a URI, the DOMBuilder must 
        /// resolve it fully before reporting it to the application through this 
        /// interface. See issue #4. An alternative would be to pass the URI out 
        /// without resolving it, and to provide a base as an additional 
        /// parameter. SAX resolves URIs first, and does not provide a base.</summary>
        /// <param name="publicId"> The public identifier of the external entity being 
        ///   referenced, or null if none was supplied.</param>
        /// <param name="systemId"> The system identifier, a URI reference , of the 
        ///   external entity being referenced exactly as written in the source.</param>
        /// <param name="baseURI"> The absolute base URI of the resource being parsed, or 
        ///   null if there is no base URI.</param>
        /// <returns>A IDOMInputSource object describing the new input 
        ///   source, or null to request that the parser open a 
        ///   regular URI connection to the system identifier. 
        /// </returns>
        public DOMInputSource resolveEntity(string publicId,
                                            string systemId,
                                            string baseURI);

    }
}
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

using System.IO;

namespace org.w3c.dom.ls
{

    /// <summary>This interface represents a single input source for an XML entity. 
    ///  This interface allows an application to encapsulate information about 
    /// an input source in a single object, which may include a public 
    /// identifier, a system identifier, a byte stream (possibly with a specified 
    /// encoding), and/or a character stream. 
    ///  The exact definitions of a byte stream and a character stream are 
    /// binding dependent. 
    ///  There are two places that the application will deliver this input 
    /// source to the parser: as the argument to the parse method, 
    /// or as the return value of the DOMEntityResolver.resolveEntity
    ///  method.  There are at least three places where IDOMInputSource is passed 
    /// to the parser (parseWithContext).
    ///  The DOMBuilder will use the IDOMInputSource 
    /// object to determine how to read XML input. If there is a character stream 
    /// available, the parser will read that stream directly; if not, the parser 
    /// will use a byte stream, if available; if neither a character stream nor a 
    /// byte stream is available, the parser will attempt to open a URI 
    /// connection to the resource identified by the system identifier. 
    ///  A IDOMInputSource object belongs to the application: the 
    /// parser shall never modify it in any way (it may modify a copy if 
    /// necessary).  Even though all attributes in this interface are writable 
    /// the DOM implementation is expected to never mutate a IDOMInputSource. 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public class DOMInputSource
    {
        /// <summary>An attribute of a language-binding dependent type that represents a 
        /// stream of bytes.
        /// The parser will ignore this if there is also a character stream 
        /// specified, but it will use a byte stream in preference to opening a 
        /// URI connection itself.
        /// If the application knows the character encoding of the byte stream, 
        /// it should set the encoding attribute. Setting the encoding in this 
        /// way will override any encoding specified in the XML declaration 
        /// itself.
        /// </summary>
        StreamReader byteStream { get; set; }
        /// <summary>An attribute of a language-binding dependent type that represents a 
        /// stream of bytes.
        /// The parser will ignore this if there is also a character stream 
        /// specified, but it will use a byte stream in preference to opening a 
        /// URI connection itself.
        /// If the application knows the character encoding of the byte stream, 
        /// it should set the encoding attribute. Setting the encoding in this 
        /// way will override any encoding specified in the XML declaration 
        /// itself.
        /// </summary>
        //void setByteStream(StreamReader byteStream);

        /// <summary> An attribute of a language-binding dependent type that represents a 
        /// stream of 16-bit units. Application must encode the stream using 
        /// UTF-16 (defined in  and Amendment 1 of ). 
        /// If a character stream is specified, the parser will ignore any byte 
        /// stream and will not attempt to open a URI connection to the system 
        /// identifier.
        /// </summary>
        StringReader characterStream { get; set; }

        /// <summary>A string attribute that represents a sequence of 16 bit units (utf-16 
        /// encoded characters).
        /// If string data is available in the input source, the parser will 
        /// ignore the character stream and the byte stream and will not attempt 
        /// to open a URI connection to the system identifier.
        /// </summary>
        public string stringData { get; set; }

        /// <summary> The character encoding, if known. The encoding must be a string 
        /// acceptable for an XML encoding declaration ( section 4.3.3 "Character 
        /// Encoding in Entities"). 
        /// This attribute has no effect when the application provides a 
        /// character stream. For other sources of input, an encoding specified 
        /// by means of this attribute will override any encoding specified in 
        /// the XML declaration or the Text declaration, or an encoding obtained 
        /// from a higher level protocol, such as HTTP .
        /// </summary>
        public string encoding { get; set; }

        /// <summary>The public identifier for this input source. The public identifier is 
        /// always optional: if the application writer includes one, it will be 
        /// provided as part of the location information.
        /// </summary>
        public string publicId { get; set; }

        /// <summary>The system identifier, a URI reference , for this input source. The 
        /// system identifier is optional if there is a byte stream or a 
        /// character stream, but it is still useful to provide one, since the 
        /// application can use it to resolve relative URIs and can include it in 
        /// error messages and warnings (the parser will attempt to fetch the 
        /// ressource identifier by the URI reference only if there is no byte 
        /// stream or character stream specified).
        /// If the application knows the character encoding of the object 
        /// pointed to by the system identifier, it can register the encoding by 
        /// setting the encoding attribute.
        /// If the system ID is a relative URI reference (see section 5 in ), 
        /// the behavior is implementation dependent.
        /// </summary>
        public string systemId { get; set; }

        /// <summary> The base URI to be used (see section 5.1.4 in ) for resolving relative 
        /// URIs to absolute URIs. If the baseURI is itself a relative URI, the 
        /// behavior is implementation dependent. 
        /// </summary>
        public string baseURI { get; set; }
    }
}
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

    /// <summary> DOMImplementationLS contains the factory methods for creating 
    /// objects that implement the DOMBuilder (parser) and 
    /// DOMWriter (serializer) interfaces. 
    ///  The expectation is that an instance of the 
    /// DOMImplementationLS interface can be obtained by using 
    /// binding-specific casting methods on an instance of the 
    /// DOMImplementation interface or, if the IDocument 
    /// supports the feature "Core" version "3.0" 
    /// defined in , by using the method INode.getInterface with 
    /// parameter values "LS-Load" and "3.0" 
    /// (respectively). 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public class DOMImplementationLS
    {
        /// <summary>Create a new DOMBuilder. The newly constructed parser may 
        /// then be configured by means of its setFeature method, 
        /// and used to parse documents by means of its parse 
        /// method.</summary>
        /// <param name="mode">  The mode argument is either 
        ///   MODE_SYNCHRONOUS or MODE_ASYNCHRONOUS, if 
        ///   mode is MODE_SYNCHRONOUS then the 
        ///   DOMBuilder that is created will operate in synchronous 
        ///   mode, if it's MODE_ASYNCHRONOUS then the 
        ///   DOMBuilder that is created will operate in 
        ///   asynchronous mode.</param> 
        /// <param name="schemaType">  An absolute URI representing the type of the schema 
        ///   language used during the load of a IDocument using the 
        ///   newly created DOMBuilder. Note that no lexical 
        ///   checking is done on the absolute URI. In order to create a 
        ///   DOMBuilder for any kind of schema types (i.e. the 
        ///   DOMBuilder will be free to use any schema found), use the value 
        ///   null.  For W3C XML Schema , applications must use the 
        ///   value "http://www.w3.org/2001/XMLSchema". For XML DTD 
        ///   , applications must use the value 
        ///   "http://www.w3.org/TR/REC-xml". Other Schema languages 
        ///   are outside the scope of the W3C and therefore should recommend an 
        ///   absolute URI in order to use this method.</param>
        /// <returns> The newly created DOMBuilder object. This 
        ///   DOMBuilder is either synchronous or asynchronous 
        ///   depending on the value of the mode argument.  By 
        ///   default, the newly created DOMBuilder does not contain 
        ///   a DOMErrorHandler, i.e. the value of the 
        ///   errorHandler is null. However, 
        ///   implementations may provide a default error handler at creation 
        ///   time. In that case, the initial value of the 
        ///   errorHandler attribute on the new created 
        ///   DOMBuilder contains a reference to the default error 
        ///   handler.</returns>
        /// <exception cref="DOMException">
        ///    NOT_SUPPORTED_ERR: Raised if the requested mode or schema type is 
        ///   not supported. 
        /// </exception>
        public DOMBuilder createDOMBuilder(DOMImplementationLSMode mode,
                                           string schemaType)
                                           ; // throws DOMException;

        /// <summary>Create a new DOMWriter object. DOMWriters are 
        /// used to serialize a DOM tree back into an XML document.</summary>
        /// <returns>The newly created DOMWriter object. By default, 
        ///   the newly created DOMWriter does not contain a 
        ///   DOMErrorHandler, i.e. the value of the 
        ///   errorHandler is null. However, 
        ///   implementations may provide a default error handler at creation 
        ///   time. In that case, the initial value of the 
        ///   errorHandler attribute on the new created 
        ///   DOMWriter contains a reference to the default error 
        ///   handler. 
        /// </returns>
        public DOMWriter createDOMWriter();

        /// <summary> Create a new "empty" IDOMInputSource.</summary>
        /// <returns> The newly created IDOMInputSource object. </returns>
        public DOMInputSource createDOMInputSource();

    }
    public enum DOMImplementationLSMode
    { 
        /// <summary>Create a synchronous DOMBuilder.
        /// </summary>
        MODE_SYNCHRONOUS = 1,
        /// <summary>Create an asynchronous DOMBuilder.
        /// </summary>
        MODE_ASYNCHRONOUS = 2
}
}
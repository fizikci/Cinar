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


    /// <summary>The DocumentLS interface provides a mechanism by which the 
    /// content of a document can be replaced with the DOM tree produced when 
    /// loading a URI, or parsing a string. The expectation is that an instance 
    /// of the DocumentLS interface can be obtained by using 
    /// binding-specific casting methods on an instance of the 
    /// IDocument interface or, if the IDocument supports 
    /// the feature "Core" version "3.0" defined in , 
    /// by using the method INode.getInterface with parameter values 
    /// "LS-Load" and "3.0" (respectively). 
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public class DocumentLS
    {
        /// <summary>Indicates whether the method load should be synchronous or 
        /// asynchronous. When the async attribute is set to true 
        /// the load method returns control to the caller before the document has 
        /// completed loading. The default value of this attribute is 
        /// false. Should the DOM spec define the default value of 
        /// this attribute? What if implementing both async and sync IO is 
        /// impractical in some systems?  2001-09-14. default is 
        /// false but we need to check with Mozilla and IE.</summary>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised if the implementation doesn't support the 
        ///   mode the attribute is being set to.
        /// </exception>
        public bool async { get; set; }

        /// <summary>If the document is currently being loaded as a result of the method 
        /// load being invoked the loading and parsing is 
        /// immediately aborted. The possibly partial result of parsing the 
        /// document is discarded and the document is cleared.
        /// </summary>
        public void abort();

        /// <summary>Replaces the content of the document with the result of parsing the 
        /// given URI. Invoking this method will either block the caller or 
        /// return to the caller immediately depending on the value of the async 
        /// attribute. Once the document is fully loaded the document will fire a 
        /// "load" event that the caller can register as a listener for. If an 
        /// error occurs the document will fire an "error" event so that the 
        /// caller knows that the load failed (see ParseErrorEvent). 
        /// If this method is called on a document that is currently loading, the 
        /// current load is interrupted and the new URI load is initiated.
        /// When invoking this method the features used in the 
        /// DOMBuilder interface are assumed to have their default
        /// values with the exception that the feature "entities" is "false".</summary>
        /// <param name="uri"> The URI reference for the XML file to be loaded. If this is 
        ///   a relative URI, the base URI used by the implementation is 
        ///   implementation dependent.</param>
        /// <returns>If async is set to true load returns 
        ///   true if the document load was successfully initiated. 
        ///   If an error occurred when initiating the document load 
        ///   load returns false.If async is set to 
        ///   false load returns true if 
        ///   the document was successfully loaded and parsed. If an error 
        ///   occurred when either loading or parsing the URI load 
        ///   returns false.
        /// </returns>
        public bool load(string uri);

        /// <summary> Replace the content of the document with the result of parsing the 
        /// input string, this method is always synchronous. This method always 
        /// parses from a DOMString, which means the data is always UTF16. All 
        /// other encoding information is ignored. 
        /// The features used in the DOMBuilder interface are 
        /// assumed to have their default values when invoking this method.</summary>
        /// <param name="source"> A string containing an XML document.</param>
        /// <returns>true if parsing the input string succeeded 
        ///   without errors, otherwise false.
        /// </returns>
        public bool loadXML(string source);

        /// <summary>Save the document or the given node and all its descendants to a string 
        /// (i.e. serialize the document or node). 
        /// The features used in the DOMWriter interface are 
        /// assumed to have their default values when invoking this method.</summary>
        /// <param name="snode"> Specifies what to serialize, if this parameter is 
        ///   null the whole document is serialized, if it's 
        ///   non-null the given node is serialized.</param>
        /// <returns>The serialized document or null in case an error 
        ///   occured.</returns>
        /// <exception cref="DOMException">
        ///   WRONG_DOCUMENT_ERR: Raised if the node passed in as the node 
        ///   parameter is from an other document.
        /// </exception>
        public string saveXML(Node snode)
                              ; // throws DOMException;

    }
}
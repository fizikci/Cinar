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


    /**
     * The <code>DocumentLS</code> interface provides a mechanism by which the 
     * content of a document can be replaced with the DOM tree produced when 
     * loading a URI, or parsing a string. The expectation is that an instance 
     * of the <code>DocumentLS</code> interface can be obtained by using 
     * binding-specific casting methods on an instance of the 
     * <code>IDocument</code> interface or, if the <code>IDocument</code> supports 
     * the feature <code>"Core"</code> version <code>"3.0"</code> defined in , 
     * by using the method <code>INode.getInterface</code> with parameter values 
     * <code>"LS-Load"</code> and <code>"3.0"</code> (respectively). 
     * <p>See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load
    and Save Specification</a>.
     */
    public interface IDocumentLS
    {
        /**
         * Indicates whether the method load should be synchronous or 
         * asynchronous. When the async attribute is set to <code>true</code> 
         * the load method returns control to the caller before the document has 
         * completed loading. The default value of this attribute is 
         * <code>false</code>. Should the DOM spec define the default value of 
         * this attribute? What if implementing both async and sync IO is 
         * impractical in some systems?  2001-09-14. default is 
         * <code>false</code> but we need to check with Mozilla and IE. 
         * @exception DOMException
         *   NOT_SUPPORTED_ERR: Raised if the implementation doesn't support the 
         *   mode the attribute is being set to.
         */
        bool async { get; set; }
        /**
         * Indicates whether the method load should be synchronous or 
         * asynchronous. When the async attribute is set to <code>true</code> 
         * the load method returns control to the caller before the document has 
         * completed loading. The default value of this attribute is 
         * <code>false</code>. Should the DOM spec define the default value of 
         * this attribute? What if implementing both async and sync IO is 
         * impractical in some systems?  2001-09-14. default is 
         * <code>false</code> but we need to check with Mozilla and IE. 
         * @exception DOMException
         *   NOT_SUPPORTED_ERR: Raised if the implementation doesn't support the 
         *   mode the attribute is being set to.
         */
        //void setAsync(bool async); // throws DOMException;

        /**
         * If the document is currently being loaded as a result of the method 
         * <code>load</code> being invoked the loading and parsing is 
         * immediately aborted. The possibly partial result of parsing the 
         * document is discarded and the document is cleared.
         */
        void abort();

        /**
         * Replaces the content of the document with the result of parsing the 
         * given URI. Invoking this method will either block the caller or 
         * return to the caller immediately depending on the value of the async 
         * attribute. Once the document is fully loaded the document will fire a 
         * "load" event that the caller can register as a listener for. If an 
         * error occurs the document will fire an "error" event so that the 
         * caller knows that the load failed (see <code>ParseErrorEvent</code>). 
         * If this method is called on a document that is currently loading, the 
         * current load is interrupted and the new URI load is initiated.
         * <br>When invoking this method the features used in the 
         * <code>DOMBuilder</code> interface are assumed to have their default 
         * values with the exception that the feature "entities" is "false".
         * @param uri The URI reference for the XML file to be loaded. If this is 
         *   a relative URI, the base URI used by the implementation is 
         *   implementation dependent.
         * @return If async is set to <code>true</code> <code>load</code> returns 
         *   <code>true</code> if the document load was successfully initiated. 
         *   If an error occurred when initiating the document load 
         *   <code>load</code> returns <code>false</code>.If async is set to 
         *   <code>false</code> <code>load</code> returns <code>true</code> if 
         *   the document was successfully loaded and parsed. If an error 
         *   occurred when either loading or parsing the URI <code>load</code> 
         *   returns <code>false</code>.
         */
        bool load(string uri);

        /**
         *  Replace the content of the document with the result of parsing the 
         * input string, this method is always synchronous. This method always 
         * parses from a DOMString, which means the data is always UTF16. All 
         * other encoding information is ignored. 
         * <br>The features used in the <code>DOMBuilder</code> interface are 
         * assumed to have their default values when invoking this method.
         * @param source A string containing an XML document.
         * @return <code>true</code> if parsing the input string succeeded 
         *   without errors, otherwise <code>false</code>.
         */
        bool loadXML(string source);

        /**
         * Save the document or the given node and all its descendants to a string 
         * (i.e. serialize the document or node). 
         * <br>The features used in the <code>DOMWriter</code> interface are 
         * assumed to have their default values when invoking this method. 
         * @param snode Specifies what to serialize, if this parameter is 
         *   <code>null</code> the whole document is serialized, if it's 
         *   non-null the given node is serialized.
         * @return The serialized document or <code>null</code> in case an error 
         *   occured.
         * @exception DOMException
         *   WRONG_DOCUMENT_ERR: Raised if the node passed in as the node 
         *   parameter is from an other document.
         */
        string saveXML(INode snode)
                              ; // throws DOMException;

    }
}
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

using System;

namespace org.w3c.dom.ls
{


    /// <summary>A interface to an object that is able to build a DOM tree from various 
    /// input sources.
    /// DOMBuilder provides an API for parsing XML documents and 
    /// building the corresponding DOM document tree. A DOMBuilder 
    /// instance is obtained from the DOMImplementationLS interface 
    /// by invoking its createDOMBuildermethod.
    ///  As specified in , when a document is first made available via the 
    /// DOMBuilder: there is only one Text node for each block of 
    /// text. The Text nodes are into "normal" form: only structure 
    /// (e.g., elements, comments, processing instructions, CDATA sections, and 
    /// entity references) separates Text nodes, i.e., there are 
    /// neither adjacent Text nodes nor empty Text 
    /// nodes.  it is expected that the value and 
    /// nodeValue attributes of an Attr node initially 
    /// return the XML 1.0 normalized value. However, if the features 
    /// validate-if-schema and datatype-normalization 
    /// are set to true, depending on the attribute normalization 
    /// used, the attribute values may differ from the ones obtained by the XML 
    /// 1.0 attribute normalization. If the feature 
    /// datatype-normalization is not set to true, the 
    /// XML 1.0 attribute normalization is guaranteed to occur, and if attributes 
    /// list does not contain namespace declarations, the attributes 
    /// attribute on IElement node represents the property 
    /// [attributes] defined in  .  XML Schemas does not modify the XML attribute 
    /// normalization but represents their normalized value in an other 
    /// information item property: [schema normalized value]XML Schema 
    /// normalization only occurs if datatype-normalization is set 
    /// to true.
    ///  Asynchronous DOMBuilder objects are expected to also 
    /// implement the events::EventTarget interface so that event 
    /// listeners can be registered on asynchronous DOMBuilder 
    /// objects. 
    ///  Events supported by asynchronous DOMBuilder are: load: The 
    /// document that's being loaded is completely parsed, see the definition of 
    /// LSLoadEventprogress: Progress notification, see the 
    /// definition of LSProgressEvent All events defined in this 
    /// specification use the namespace URI 
    /// "http://www.w3.org/2002/DOMLS". 
    ///  DOMBuilders have a number of named features that can be 
    /// queried or set. The name of DOMBuilder features must be 
    /// valid XML names. Implementation specific features (extensions) should 
    /// choose a implementation specific prefix to avoid name collisions. 
    ///  Even if all features must be recognized by all implementations, being 
    /// able to set a state (true or false) is not 
    /// always required. The following list of recognized features indicates the 
    /// definitions of each feature state, if setting the state to 
    /// true or false must be supported or is optional 
    /// and, which state is the default one: 
    /// <dl>
    /// <dt>"canonical-form"</dt>
    /// <dd> This 
    /// feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "cdata-sections"</dt>
    /// <dd> This feature is equivalent to the one 
    /// provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "certified"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[optional] Assume, when XML 1.1 
    /// is supported, that the input is certified (see section 2.13 in ). </dd>
    /// <dt>
    /// false</dt>
    /// <dd>[required] (default) Don't assume that the input is 
    /// certified (see section 2.13 in ). </dd>
    /// </dl></dd>
    /// <dt>
    /// "charset-overrides-xml-encoding"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[required] (
    /// default) If a higher level protocol such as HTTP  provides an indication 
    /// of the character encoding of the input stream being processed, that will 
    /// override any encoding specified in the XML declaration or the Text 
    /// declaration (see also  4.3.3 "Character Encoding in Entities"). 
    /// Explicitly setting an encoding in the IDOMInputSource 
    /// overrides encodings from the protocol. </dd>
    /// <dt>false</dt>
    /// <dd>[required] Any 
    /// character set encoding information from higher level protocols is ignored 
    /// by the parser. </dd>
    /// </dl></dd>
    /// <dt>"comments"</dt>
    /// <dd> This feature is equivalent to the 
    /// one provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "datatype-normalization"</dt>
    /// <dd> This feature is equivalent to the 
    /// one provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "entities"</dt>
    /// <dd> This feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// <dt>"infoset"</dt>
    /// <dd> 
    /// This feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . Setting this feature 
    /// to true will also force the feature namespaces 
    /// to true. </dd>
    /// <dt>"namespaces"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[required
    /// ] (default) Perform the namespace processing as defined in . </dd>
    /// <dt>
    /// false</dt>
    /// <dd>[optional] Do not perform the namespace processing. </dd>
    /// </dl></dd>
    /// <dt>
    /// "namespace-declarations"</dt>
    /// <dd> This feature is equivalent to the 
    /// one provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "supported-mediatypes-only"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[optional] Check 
    /// that the media type of the parsed resource is a supported media type and 
    /// call the error handler if an unsupported media type is encountered. The 
    /// media types defined in  must be accepted. </dd>
    /// <dt>false</dt>
    /// <dd>[required] (
    /// default) Don't check the media type, accept any type of data. </dd>
    /// </dl></dd>
    /// <dt>
    /// "unknown-characters"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[required] (default) 
    /// If, while verifying full normalization when XML 1.1 is supported, a 
    /// processor encounters characters for which it cannot determine the 
    /// normalization properties, then the processor will ignore any possible 
    /// denormalizations caused by these characters. </dd>
    /// <dt>false</dt>
    /// <dd>[optional] 
    /// Report an fatal error if a character is encountered for which the 
    /// processor can not determine the normalization properties. </dd>
    /// </dl></dd>
    /// <dt>
    /// "validate"</dt>
    /// <dd> This feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "validate-if-schema"</dt>
    /// <dd> This feature is equivalent to the one 
    /// provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "whitespace-in-element-content"</dt>
    /// <dd> This feature is equivalent 
    /// to the one provided on IDocument.setNormalizationFeature in . </dd>
    /// </dl>
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public interface IDOMBuilder
    {
        /// <summary>If a DOMEntityResolver has been specified, each time a 
        /// reference to an external entity is encountered the 
        /// DOMBuilder will pass the public and system IDs to the 
        /// entity resolver, which can then specify the actual source of the 
        /// entity.
        ///  If this attribute is null, the resolution of entities 
        /// in the document is implementation dependent. 
        /// </summary>
        IDOMEntityResolver entityResolver { get; set; }
        /// <summary>If a DOMEntityResolver has been specified, each time a 
        /// reference to an external entity is encountered the 
        /// DOMBuilder will pass the public and system IDs to the 
        /// entity resolver, which can then specify the actual source of the 
        /// entity.
        ///  If this attribute is null, the resolution of entities 
        /// in the document is implementation dependent. 
        /// </summary>
        //void setEntityResolver(IDOMEntityResolver entityResolver);

        /// <summary> In the event that an error is encountered in the XML document being 
        /// parsed, the DOMDocumentBuilder will call back to the 
        /// errorHandler with the error information. When the 
        /// document loading process calls the error handler the node closest to 
        /// where the error occured is passed to the error handler, if the 
        /// implementation is unable to pass the node where the error occures the 
        /// document INode is passed to the error handler. In addition to passing 
        /// the INode closest to to where the error occured, the implementation 
        /// should also pass any other valuable information to the error handler, 
        /// such as file name, line number, and so on. Mutations to the document 
        /// from within an error handler will result in implementation dependent 
        /// behaviour. 
        /// </summary>
        Action errorHandler { get; set; }

        /// <summary> When the application provides a filter, the parser will call out to 
        /// the filter at the completion of the construction of each 
        /// IElement node. The filter implementation can choose to 
        /// remove the element from the document being constructed (unless the 
        /// element is the document element) or to terminate the parse early. If 
        /// the document is being validated when it's loaded the validation 
        /// happens before the filter is called. 
        /// </summary>
        IDOMBuilderFilter filter { get; set; }

        /// <summary>Set the state of a feature.
        /// The feature name has the same form as a DOM hasFeature string.
        /// It is possible for a DOMBuilder to recognize a feature 
        /// name but to be unable to set its value.</summary>
        /// <param name="name"> The feature name.</param>
        /// <param name="state"> The requested state of the feature (true or 
        ///   false).</param>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised when the DOMBuilder recognizes 
        ///   the feature name but cannot set the requested value. 
        ///   NOT_FOUND_ERR: Raised when the DOMBuilder does not 
        ///   recognize the feature name.
        /// </exception>
        void setFeature(string name,
                               bool state)
                               ; // throws DOMException;

        /// <summary>Query whether setting a feature to a specific value is supported.
        /// The feature name has the same form as a DOM hasFeature string.</summary>
        /// <param name="name"> The feature name, which is a DOM has-feature style string.</param>
        /// <param name="state"> The requested state of the feature (true or 
        ///   false).</param>
        /// <returns>true if the feature could be successfully set to 
        ///   the specified value, or false if the feature is not 
        ///   recognized or the requested value is not supported. The value of 
        ///   the feature itself is not changed.
        /// </returns>
        bool canSetFeature(string name,
                                     bool state);

        /// <summary>Look up the value of a feature.
        /// The feature name has the same form as a DOM hasFeature string</summary>
        /// <param name="name"> The feature name, which is a string with DOM has-feature 
        ///   syntax.</param>
        /// <returns>The current state of the feature (true or 
        ///   false).</returns>
        /// <exception cref="DOMException">
        ///   NOT_FOUND_ERR: Raised when the DOMBuilder does not 
        ///   recognize the feature name.
        /// </exception>
        bool getFeature(string name)
                                  ; // throws DOMException;

        /// <summary> Parse an XML document from a location identified by a URI reference . 
        /// If the URI contains a fragment identifier (see section 4.1 in ), the 
        /// behavior is not defined by this specification, but future versions of 
        /// this specification might define the behavior. </summary>
        /// <param name="uri"> The location of the XML document to be read.</param>
        /// <returns>If the DOMBuilder is a synchronous 
        ///   DOMBuilder the newly created and populated 
        ///   IDocument is returned. If the DOMBuilder 
        ///   is asynchronous then null is returned since the 
        ///   document object is not yet parsed when this method returns.
        /// </returns>
        IDocument parseURI(string uri);

        /// <summary>Parse an XML document from a resource identified by a 
        /// IDOMInputSource.</summary>
        /// <param name="is"> The IDOMInputSource from which the source 
        ///   document is to be read.</param>
        /// <returns>If the DOMBuilder is a synchronous 
        ///   DOMBuilder the newly created and populated 
        ///   IDocument is returned. If the DOMBuilder 
        ///   is asynchronous then null is returned since the 
        ///   document object is not yet parsed when this method returns.
        /// </returns>
        IDocument parse(IDOMInputSource inputSource);


        /// <summary> Parse an XML fragment from a resource identified by a 
        /// IDOMInputSource and insert the content into an existing 
        /// document at the position specified with the contextNode 
        /// and action arguments. When parsing the input stream the 
        /// context node is used for resolving unbound namespace prefixes. The 
        /// IDocument node, attached to the context node, is used to 
        /// resolved default attributes and entity references. 
        ///  As the new data is inserted into the document at least one 
        /// mutation event is fired per immediate child (or sibling) of context 
        /// node. 
        ///  If an error occurs while parsing, the caller is notified through 
        /// the error handler.</summary>
        /// <param name="inputSource">  The IDOMInputSource from which the source 
        ///   document is to be read. The source document must be an XML 
        ///   fragment, i.e. anything except an XML IDocument, a DOCTYPE, entities 
        ///   declarations, notations declarations, or XML or text declarations.</param>
        /// <param name="cnode">  The node that is used as the context for the data that 
        ///   is being parsed. This node must be a IDocument node, a 
        ///   DocumentFragment node, or a node of a type that is 
        ///   allowed as a child of an element, e.g. it can not be an attribute 
        ///   node.</param>
        /// <param name="action"> This parameter describes which action should be taken 
        ///   between the new set of node being inserted and the existing 
        ///   children of the context node. The set of possible actions is 
        ///   defined above.</param>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised when the DOMBuilder doesn't 
        ///   support this method.
        ///   NO_MODIFICATION_ALLOWED_ERR: Raised if the context node is 
        ///   readonly.
        /// </exception>
        void parseWithContext(IDOMInputSource inputSource,
                                     INode cnode,
                                     ActionTypes action)
                                     ; // throws DOMException;

    }
    public enum ActionTypes
    {
        /// <summary>Replace the context node with the result of parsing the input source. 
        /// For this action to work the context node must have a parent and the 
        /// context node must be an IElement, Text, 
        /// CDATASection, Comment, 
        /// ProcessingInstruction, or EntityReference 
        /// node.
        /// </summary>
        ACTION_REPLACE = 1,
        /// <summary>Append the result of the input source as children of the context node. 
        /// For this action to work, the context node must be an 
        /// IElement or a DocumentFragment.
        /// </summary>
        ACTION_APPEND_AS_CHILDREN = 2,
        /// <summary>Insert the result of parsing the input source after the context node. 
        /// For this action to work the context nodes parent must be an 
        /// IElement.
        /// </summary>
        ACTION_INSERT_AFTER = 3,
        /// <summary>Insert the result of parsing the input source before the context node. 
        /// For this action to work the context nodes parent must be an 
        /// IElement.
        /// </summary>
        ACTION_INSERT_BEFORE = 4
    }
}
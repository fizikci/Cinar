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
using System.IO;

namespace org.w3c.dom.ls
{


    /// <summary> DOMWriter provides an API for serializing (writing) a DOM 
    /// document out in an XML document. The XML data is written to an output 
    /// stream, the type of which depends on the specific language bindings in 
    /// use. 
    ///  During serialization of XML data, namespace fixup is done when possible 
    /// as defined in , Appendix B.  allows empty strings as a real namespace 
    /// URI. If the namespaceURI of a INode is empty 
    /// string, the serialization will treat them as null, ignoring 
    /// the prefix if any. should the remark on DOM Level 2 namespace URI 
    /// included in the namespace algorithm in Core instead?
    ///  DOMWriter accepts any node type for serialization. For 
    /// nodes of type IDocument or Entity, well formed 
    /// XML will be created if possible. The serialized output for these node 
    /// types is either as a IDocument or an External Entity, respectively, and is 
    /// acceptable input for an XML parser. For all other types of nodes the 
    /// serialized form is not specified, but should be something useful to a 
    /// human for debugging or diagnostic purposes. Note: rigorously designing an 
    /// external (source) form for stand-alone node types that don't already have 
    /// one defined in  seems a bit much to take on here. 
    /// Within a IDocument, DocumentFragment, or Entity being serialized, Nodes 
    /// are processed as follows Documents are written including an XML 
    /// declaration and a DTD subset, if one exists in the DOM. Writing a 
    /// document node serializes the entire document. Entity nodes, when written 
    /// directly by writeNode defined in the DOMWriter 
    /// interface, output the entity expansion but no namespace fixup is done. 
    /// The resulting output will be valid as an external entity.  Entity 
    /// reference nodes are serialized as an entity reference of the form "
    /// &amp;entityName;" in the output. Child nodes (the expansion) 
    /// of the entity reference are ignored.  CDATA sections containing content 
    /// characters that can not be represented in the specified output encoding 
    /// are handled according to the "split-cdata-sections" feature.If the 
    /// feature is true, CDATA sections are split, and the 
    /// unrepresentable characters are serialized as numeric character references 
    /// in ordinary content. The exact position and number of splits is not 
    /// specified. If the feature is false, unrepresentable 
    /// characters in a CDATA section are reported as errors. The error is not 
    /// recoverable - there is no mechanism for supplying alternative characters 
    /// and continuing with the serialization. DocumentFragment nodes are 
    /// serialized by serializing the children of the document fragment in the 
    /// order they appear in the document fragment.  All other node types 
    /// (IElement, Text, etc.) are serialized to their corresponding XML source 
    /// form.  The serialization of a DOM INode does not always generate a 
    /// well-formed XML document, i.e. a DOMBuilder might through 
    /// fatal errors when parsing the resulting serialization. 
    ///  Within the character data of a document (outside of markup), any 
    /// characters that cannot be represented directly are replaced with 
    /// character references. Occurrences of '&lt;' and '&amp;' are replaced by 
    /// the predefined entities &amp;lt; and &amp;amp. The other predefined 
    /// entities (&amp;gt, &amp;apos, etc.) are not used; these characters can be 
    /// included directly. Any character that can not be represented directly in 
    /// the output character encoding is serialized as a numeric character 
    /// reference. 
    ///  Attributes not containing quotes are serialized in quotes. Attributes 
    /// containing quotes but no apostrophes are serialized in apostrophes 
    /// (single quotes). Attributes containing both forms of quotes are 
    /// serialized in quotes, with quotes within the value represented by the 
    /// predefined entity &amp;quot;. Any character that can not be represented 
    /// directly in the output character encoding is serialized as a numeric 
    /// character reference. 
    ///  Within markup, but outside of attributes, any occurrence of a character 
    /// that cannot be represented in the output character encoding is reported 
    /// as an error. An example would be serializing the element 
    /// &lt;LaCa�ada/&gt; with the encoding="us-ascii". 
    ///  When requested by setting the normalize-characters feature 
    /// on DOMWriter, all data to be serialized, both markup and 
    /// character data, is W3C Text normalized according to the rules defined in 
    /// . The W3C Text normalization process affects only the data as it is being 
    /// written; it does not alter the DOM's view of the document after 
    /// serialization has completed. 
    /// Namespaces are fixed up during serialization, the serialization process 
    /// will verify that namespace declarations, namespace prefixes and the 
    /// namespace URIs associated with Elements and Attributes are consistent. If 
    /// inconsistencies are found, the serialized form of the document will be 
    /// altered to remove them. The algorithm used for doing the namespace fixup 
    /// while serializing a document is a combination of the algorithms used for 
    /// lookupNamespaceURI and lookupNamespacePrefix . previous paragraph to be 
    /// defined closer here.
    /// Any changes made affect only the namespace prefixes and declarations 
    /// appearing in the serialized data. The DOM's view of the document is not 
    /// altered by the serialization operation, and does not reflect any changes 
    /// made to namespace declarations or prefixes in the serialized output. 
    ///  While serializing a document the serializer will write out 
    /// non-specified values (such as attributes whose specified is 
    /// false) if the discard-default-content feature 
    /// is set to true. If the discard-default-content 
    /// flag is set to false and a schema is used for validation, 
    /// the schema will be also used to determine if a value is specified or not. 
    /// If no schema is used, the specified flag on attribute nodes 
    /// is used to determine if attribute values should be written out. 
    ///  Ref to Core spec (1.1.9, XML namespaces, 5th paragraph) entity ref 
    /// description about warning about unbound entity refs. Entity refs are 
    /// always serialized as &amp;foo;, also mention this in the 
    /// load part of this spec. 
    ///  DOMWriters have a number of named features that can be 
    /// queried or set. The name of DOMWriter features must be valid 
    /// XML names. Implementation specific features (extensions) should choose an 
    /// implementation dependent prefix to avoid name collisions. 
    /// Here is a list of features that must be recognized by all 
    /// implementations.  Using these features does affect the INode 
    /// being serialized, only its serialized form is affected.
    /// <dl>
    /// <dt>
    /// "discard-default-content"</dt>
    /// <dd> This feature is equivalent to the 
    /// one provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "canonical-form"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[optional] This formatting 
    /// writes the document according to the rules specified in . Setting this 
    /// feature to true will set the feature "format-pretty-print" 
    /// to false. </dd>
    /// <dt>false</dt>
    /// <dd>[required] (default) Do not canonicalize the 
    /// output. </dd>
    /// </dl></dd>
    /// <dt>"entities"</dt>
    /// <dd> This feature is equivalent to the one 
    /// provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "format-pretty-print"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[optional] Formatting 
    /// the output by adding whitespace to produce a pretty-printed, indented, 
    /// human-readable form. The exact form of the transformations is not 
    /// specified by this specification. Setting this feature to true will set 
    /// the feature "canonical-form" to false. </dd>
    /// <dt>false</dt>
    /// <dd>[required] (
    /// default) Don't pretty-print the result. </dd>
    /// </dl></dd>
    /// <dt>"namespaces"</dt>
    /// <dd> This 
    /// feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "normalize-characters"</dt>
    /// <dd> This feature is equivalent to the one 
    /// provided on IDocument.setNormalizationFeature in . Unlike in 
    /// the Core, the default value for this feature is true. </dd>
    /// <dt>
    /// "split-cdata-sections"</dt>
    /// <dd> This feature is equivalent to the one 
    /// provided on IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "validate"</dt>
    /// <dd> This feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// <dt>
    /// "unknown-characters"</dt>
    /// <dd>
    /// <dl>
    /// <dt>true</dt>
    /// <dd>[required] (default) 
    /// If, while verifying full normalization when XML 1.1 is supported, a 
    /// character is encountered for which the normalization properties cannot be 
    /// determined, then ignore any possible denormalizations caused by these 
    /// characters. </dd>
    /// <dt>false</dt>
    /// <dd>[optional] Report an fatal error if a 
    /// character is encountered for which the processor can not determine the 
    /// normalization properties. </dd>
    /// </dl></dd>
    /// <dt>"whitespace-in-element-content"</dt>
    /// <dd> 
    /// This feature is equivalent to the one provided on 
    /// IDocument.setNormalizationFeature in . </dd>
    /// </dl>
    /// See also the <a href='http://www.w3.org/TR/2002/WD-DOM-Level-3-LS-20020725'>IDocument Object Model (DOM) Level 3 Load and Save Specification</a>.
    /// </summary>
    public interface IDOMWriter
    {
        /// <summary>Set the state of a feature.
        /// The feature name has the same form as a DOM hasFeature string.
        /// It is possible for a DOMWriter to recognize a feature 
        /// name but to be unable to set its value.</summary>
        /// <param name="name"> The feature name.</param>
        /// <param name="state"> The requested state of the feature (true or 
        ///   false).</param>
        /// <exception cref="DOMException">
        ///   NOT_SUPPORTED_ERR: Raised when the DOMWriter recognizes 
        ///   the feature name but cannot set the requested value. 
        ///   Raise a NOT_FOUND_ERR When the DOMWriter does not 
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
        ///   NOT_FOUND_ERR: Raised when the DOMWriter does not 
        ///   recognize the feature name.
        /// </exception>
        bool getFeature(string name)
                                  ; // throws DOMException;

        /// <summary> The character encoding in which the output will be written. 
        ///  The encoding to use when writing is determined as follows: If the 
        /// encoding attribute has been set, that value will be used.If the 
        /// encoding attribute is null or empty, but the item to be 
        /// written, or the owner document of the item, specifies an encoding 
        /// (i.e. the "actualEncoding" from the document) specified encoding, 
        /// that value will be used.If neither of the above provides an encoding 
        /// name, a default encoding of "UTF-8" will be used.
        /// The default value is null.
        /// </summary>
        string encoding { get; set; }

        /// <summary> The end-of-line sequence of characters to be used in the XML being 
        /// written out. Any string is supported, but these are the recommended 
        /// end-of-line sequences (using other character sequences than these 
        /// recommended ones can result in a document that is either not 
        /// serializable or not well-formed): 
        /// <dl>
        /// <dt>null</dt>
        /// <dd> Use a default 
        /// end-of-line sequence. DOM implementations should choose the default 
        /// to match the usual convention for text files in the environment being 
        /// used. Implementations must choose a default sequence that matches one 
        /// of those allowed by "End-of-Line Handling" (, section 2.11) if the 
        /// serialized content is XML 1.0 or "End-of-Line Handling" (, section 
        /// 2.11) if the serialized content is XML 1.1. </dd>
        /// <dt>CR</dt>
        /// <dd>The carriage-return 
        /// character (#xD).</dd>
        /// <dt>CR-LF</dt>
        /// <dd> The carriage-return and line-feed characters 
        /// (#xD #xA). </dd>
        /// <dt>LF</dt>
        /// <dd> The line-feed character (#xA). </dd>
        /// </dl>
        /// The default value for this attribute is null.
        /// </summary>
        string newLine { get; set; }

        /// <summary> When the application provides a filter, the serializer will call out 
        /// to the filter before serializing each INode. Attribute nodes are never 
        /// passed to the filter. The filter implementation can choose to remove 
        /// the node from the stream or to terminate the serialization early. 
        /// </summary>
        IDOMWriterFilter filter { get; set; }

        /// <summary> The error handler that will receive error notifications during 
        /// serialization. The node where the error occured is passed to this 
        /// error handler, any modification to nodes from within an error 
        /// callback should be avoided since this will result in undefined, 
        /// implementation dependent behavior. 
        /// </summary>
        Action errorHandler { get; set; }

        /// <summary>Write out the specified node as described above in the description of 
        /// DOMWriter. Writing a IDocument or Entity node produces a 
        /// serialized form that is well formed XML, when possible (Entity nodes 
        /// might not always be well formed XML in themselves). Writing other 
        /// node types produces a fragment of text in a form that is not fully 
        /// defined by this document, but that should be useful to a human for 
        /// debugging or diagnostic purposes. 
        ///  If the specified encoding is not supported the error handler is 
        /// called and the serialization is interrupted.</summary>
        /// <param name="destination"> The destination for the data to be written.</param>
        /// <param name="wnode"> The IDocument or Entity node to 
        ///   be written. For other node types, something sensible should be 
        ///   written, but the exact serialized form is not specified.</param>
        /// <returns> Returns true if node was 
        ///   successfully serialized and false in case a failure 
        ///   occured and the failure wasn't canceled by the error handler. 
        /// </returns>
        bool writeNode(StreamWriter destination,
                                 INode wnode);

        /// <summary> Serialize the specified node as described above in the description of 
        /// DOMWriter. The result of serializing the node is 
        /// returned as a DOMString (this method completely ignores all the 
        /// encoding information available). Writing a IDocument or Entity node 
        /// produces a serialized form that is well formed XML. Writing other 
        /// node types produces a fragment of text in a form that is not fully 
        /// defined by this document, but that should be useful to a human for 
        /// debugging or diagnostic purposes. 
        ///  Error handler is called if encoding not supported... </summary>
        /// <param name="wnode">  The node to be written. </param>
        /// <returns> Returns the serialized data, or null in case a 
        ///   failure occured and the failure wasn't canceled by the error 
        ///   handler. </returns>
        /// <exception cref="DOMException">
        ///    DOMSTRING_SIZE_ERR: Raised if the resulting string is too long to 
        ///   fit in a DOMString. 
        /// </exception>
        string writeToString(INode wnode)
                                    ; // throws DOMException;

    }
}
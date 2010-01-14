using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class DocumentType : Node
    {
        private string _name;
        private NamedNodeMap _entities;
        private NamedNodeMap _notations;
        private string _publicId;
        private string _systemId;
        private string _internalSubset;

        /// <summary>The name of DTD; i.e., the name immediately following the 
        /// DOCTYPE keyword.
        /// </summary>
        public string name
        {
            get { return _name; }
        }

        /// <summary>A NamedNodeMap containing the general entities, both 
        /// external and internal, declared in the DTD. Parameter entities are 
        /// not contained. Duplicates are discarded. For example in: 
        /// <pre>&lt;!DOCTYPE 
        /// ex SYSTEM "ex.dtd" [ &lt;!ENTITY foo "foo"&gt; &lt;!ENTITY bar 
        /// "bar"&gt; &lt;!ENTITY bar "bar2"&gt; &lt;!ENTITY % baz "baz"&gt; 
        /// ]&gt; &lt;ex/&gt;</pre>
        ///  the interface provides access to foo 
        /// and the first declaration of bar but not the second 
        /// declaration of bar or baz. Every node in 
        /// this map also implements the Entity interface.
        /// The DOM Level 2 does not support editing entities, therefore 
        /// entities cannot be altered in any way.
        /// </summary>
        public NamedNodeMap entities
        {
            get { return _entities; }
        }

        /// <summary>A NamedNodeMap containing the notations declared in the 
        /// DTD. Duplicates are discarded. Every node in this map also implements 
        /// the Notation interface.
        /// The DOM Level 2 does not support editing notations, therefore 
        /// notations cannot be altered in any way.
        /// </summary>
        public NamedNodeMap notations
        {
            get { return _notations; }
        }

        /// <summary>The public identifier of the external subset.
        /// </summary>
        public string publicId
        {
            get { return _publicId; }
        }

        /// <summary>The system identifier of the external subset.
        /// </summary>
        public string systemId
        {
            get { return _systemId; }
        }

        /// <summary>The internal subset as a string.The actual content returned depends on 
        /// how much information is available to the implementation. This may 
        /// vary depending on various parameters, including the XML processor 
        /// used to build the document.
        /// </summary>
        public string internalSubset
        {
            get { return _internalSubset; }
        }
    }
}

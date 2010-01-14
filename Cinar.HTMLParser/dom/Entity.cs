using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class Entity : Node
    {
        internal string _publicId;
        internal string _systemId;
        internal string _notationName;

        /// <summary>The public identifier associated with the entity, if specified. If the 
        /// public identifier was not specified, this is null.
        /// </summary>
        public string publicId
        {
            get { return _publicId; }
        }

        /// <summary>The system identifier associated with the entity, if specified. If the 
        /// system identifier was not specified, this is null.
        /// </summary>
        public string systemId
        {
            get { return _systemId; }
        }

        /// <summary>For unparsed entities, the name of the notation for the entity. For 
        /// parsed entities, this is null. 
        /// </summary>
        public string notationName
        {
            get { return _notationName; }
        }
    }
}

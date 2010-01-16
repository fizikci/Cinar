using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class Entity : Node
    {
        /// <summary>The public identifier associated with the entity, if specified. If the 
        /// public identifier was not specified, this is null.
        /// </summary>
        public string publicId
        {
            get;
            internal set;
        }

        /// <summary>The system identifier associated with the entity, if specified. If the 
        /// system identifier was not specified, this is null.
        /// </summary>
        public string systemId
        {
            get;
            internal set;
        }

        /// <summary>For unparsed entities, the name of the notation for the entity. For 
        /// parsed entities, this is null. 
        /// </summary>
        public string notationName
        {
            get;
            internal set;
        }
    }
}

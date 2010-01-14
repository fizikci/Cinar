using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace org.w3c.dom
{
    public class NodeList : List<Node>
    {
        /// <summary>Returns the indexth item in the collection. If 
        /// index is greater than or equal to the number of nodes in 
        /// the list, this returns null.</summary>
        /// <param name="indexIndex"> into the collection.</param>
        /// <returns>The node at the indexth position in the 
        ///   NodeList, or null if that is not a valid 
        ///   index.</returns>
        public Node item(int index)
        {
            if (index < 0 || index >= this.Count)
                return null;
            return this[index];
        }

        /// <summary>The number of nodes in the list. The range of valid child node indices 
        /// is 0 to length-1 inclusive. 
        /// </summary>
        public int length
        {
            get { return this.Count; }
        }
    }
}

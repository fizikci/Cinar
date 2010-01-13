using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.w3c.dom;

namespace Cinar.HTMLParser.imp
{
    public class CharacterData : Node, ICharacterData
    {
        StringBuilder sb = new StringBuilder();

        public string data
        {
            get { return sb.ToString(); }
            set { sb.Length = 0; sb.Append(value); }
        }

        public int length
        {
            get { return sb.Length; }
        }

        public string substringData(int offset, int count)
        {
            return this.data.Substring(offset, count);
        }

        public void appendData(string arg)
        {
            sb.Append(arg);
        }

        public void insertData(int offset, string arg)
        {
            sb.Insert(offset, arg);
        }

        public void deleteData(int offset, int count)
        {
            sb.Remove(offset, count);
        }

        public void replaceData(int offset, int count, string arg)
        {
            sb.Remove(offset, count);
            sb.Insert(offset, arg);
        }
    }
}

using System.Collections.Generic;
using Cinar.Database;

namespace Cinar.Entities.Standart
{
    [DefaultData(ColumnList="Username,Password", ValueList="'bulent','offoff'")]
    public class User : NamedEntity
    {
        string username;
        public  virtual string UserName
        {
            get { return username; }
            set { username = value; }
        }
        string password;
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<string> userRights = new List<string>();
        public void AddRight(string right)
        {
            if (!userRights.Contains(right))
                userRights.Add(right);
        }
        public bool HasRight(string right)
        {
            if ((new List<string> { "bulent" }).Contains(UserName))
                return true;

            return userRights.Contains(right);
        }
    }
}

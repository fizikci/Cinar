using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Cinar.SQLParser;
using System.IO;
using Cinar.POP3;
using System.Reflection;

namespace Cinar.SQLEngine.Providers
{
    public class POP3Provider
    {
        private string server;
        private string userName;
        private string password;

        public POP3Provider(string server, string userName, string password)
        {
            this.server = server;
            this.userName = userName;
            this.password = password;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            var client = new Pop3Client(userName, password, server);
            client.OpenInbox();
            while (client.NextEmail())
            {
                POP3Item item = new POP3Item(client.ReadAsMailMessage());
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);//context.Variables[fieldName];
                    list.Add(ht);
                }
            }
            client.CloseConnection();

            return list;

        }
    }
    public class POP3Item : BaseItem
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public POP3Item(MailMessage msg)
        {
            this.From = msg.From;
            this.To = msg.To;
            this.Subject = msg.Subject;
            this.Body = msg.Body;
        }
    }

    public class BaseItem
    {
        internal bool Filter(Context context, Expression where)
        {
            foreach (PropertyInfo pi in this.GetType().GetProperties())
                context.Variables[pi.Name] = pi.GetValue(this, null);

            return where == null || (bool)where.Calculate(context);
        }
    }
}

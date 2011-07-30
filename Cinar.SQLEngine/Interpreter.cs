using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using Cinar.SQLParser;
using System.Linq;
using Cinar.SQLEngine.Providers;

namespace Cinar.SQLEngine
{
    public class Interpreter
    {
        List<Statement> statements;
        Context context;

        public Interpreter(string code)
        {
            this.Code = code;
        }

        public List<Statement> StatementsTree
        {
            get
            {
                return statements;
            }
        }

        Hashtable attributes = new Hashtable();
        public void SetAttribute(string key, object value)
        {
            attributes[key] = value;
        }
        public void ClearAttributes()
        {
            attributes.Clear();
        }

        private string _code;
        public string Code { get { return _code; } set { _code = value; } }

        private long parsingTime;
        public long ParsingTime { get { return parsingTime; } set { parsingTime = value; } }
        private long executingTime;
        public long ExecutingTime { get { return executingTime; } set { executingTime = value; } }

        public string Output
        {
            get
            {
                return context.Output.ToString();
            }
        }
        public List<Hashtable> ResultSet = null;
        public ListSelect FieldNames = null;
        public List<Type> FieldTypes = null;
        
        //todo: burada korkunç bir hata var. Parse() ve ardından Execute() çalıştırılıyor. Parse hataları yutuyor. Execute çalışıyor. Parse hatası oluşunca execution iptal edilmeli.
        public void Parse()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            try
            {
                this.Code = preParse(this.Code);

                statements = new List<Statement>();

                using (StringReader lSource = new StringReader(this.Code))
                {
                    Parser lParser = new Parser(lSource);

                    Statement lStatement = lParser.ReadNextStatement();
                    while (lStatement != null)
                    {
                        statements.Add(lStatement);
                        lStatement = lParser.ReadNextStatement();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SQL Parse Error: " + ex.Message);
            }
            finally
            {
                watch.Stop();
                this.ParsingTime = watch.ElapsedMilliseconds;
            }
        }

        private string preParse(string code)
        {
            return code;
        }

        public void Execute(TextWriter output)
        {
            context = new Context();
            context.Output = output;
            context.Variables = attributes;
            context.Variables["true"] = true;
            context.Variables["false"] = false;
            context.Variables["null"] = null;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                StatementCollection coll = new StatementCollection(statements);
                execute(coll, context);
            }
            catch (Exception ex)
            {
                context.Output.Write(ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : ""));
            }
            watch.Stop();
            this.ExecutingTime = watch.ElapsedMilliseconds;
        }

        private void execute(StatementCollection coll, Context context)
        {
            foreach (Statement statement in coll)
                execute(statement, context);
        }

        private void execute(Statement statement, Context context)
        {
            if (statement is SelectStatement)
            {
                // Join'den tabloları al
                // her bir kayıt için hashtable oluştur, context'e ekle, where expressionı üzerinde excute ettir. true ise listeye ekle.
                // falan filan uzun iş bu. niye yapıyorum ki ben bunu?

                SelectStatement ss = statement as SelectStatement;
                Expression filter = null;
                if (ss.From[0].On == null && ss.Where == null) filter = null;
                else if (ss.From[0].On != null && ss.Where != null) filter = new AndExpression(ss.From[0].On, ss.Where);
                else if (ss.From[0].On != null) filter = ss.From[0].On;
                else filter = ss.Where;

                this.FieldNames = ss.Select;
                this.ResultSet = context.GetData(ss.From[0], filter, ss.Select);
                this.FieldTypes = new List<Type>();
                if (this.ResultSet.Count > 0)
                {
                    foreach (Select key in ss.Select)
                        if (this.ResultSet[0][key.Alias] != null)
                            this.FieldTypes.Add(this.ResultSet[0][key.Alias].GetType());
                        else
                            this.FieldTypes.Add(typeof(string));
                }
                if (ss.OrderBy.Count > 0)
                {
                    IOrderedEnumerable<Hashtable> orderedList = this.ResultSet.OrderBy(ht=>1);
                    for (int i = 0; i < ss.OrderBy.Count; i++)
                    {
                        object orderBy = null;
                        if (ss.OrderBy[i].By is IntegerConstant)
                            orderBy = ((IntegerConstant)ss.OrderBy[i].By).Value;
                        else
                            orderBy = ((DbObjectName)ss.OrderBy[i].By).Name;

                        if (orderBy.GetType() == typeof(int))
                        {
                            int fieldNo = (int)orderBy - 1;
                            if (!(fieldNo < 0 || fieldNo >= FieldNames.Count))
                            {
                                if (ss.OrderBy[i].Desc)
                                    orderedList = orderedList.ThenByDescending(ht => ht[FieldNames[(int)fieldNo].Alias]);
                                else
                                    orderedList = orderedList.ThenBy(ht => ht[FieldNames[(int)fieldNo].Alias]);
                            }
                        }
                        else
                        {
                            string alias = orderBy.ToString();
                            if (FieldNames.IndexOf(alias) > -1)
                            {
                                if (ss.OrderBy[i].Desc)
                                    orderedList = orderedList.ThenByDescending(ht => ht[alias]);
                                else
                                    orderedList = orderedList.ThenBy(ht => ht[alias]);
                            }
                        }
                    }
                    this.ResultSet = orderedList.ToList();
                }

                if (ss.Limit != null)
                {
                    int offset = ss.Offset == null ? 0 : Convert.ToInt32(ss.Offset.Calculate(context));
                    int limit = Convert.ToInt32(ss.Limit.Calculate(context));
                    this.ResultSet = this.ResultSet.Skip(offset).Take(limit).ToList();
                }
            }
        }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            TextWriter stringWriter = new StringWriter(sb);
            this.Execute(stringWriter);
        }
    }

    public class Context : IContext
    {
        public TextWriter Output = null;
        public Hashtable Variables = null;

        public object GetMaxOf(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object GetValueOfCurrent(string fName)
        {
            return Variables[fName];
        }

        public object GetVariableValue(string fName)
        {
            if (Variables.ContainsKey(fName))
                return Variables[fName];
            else
                return null;
        }

        internal List<Hashtable> GetData(Join join, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            switch (join.TableName.ToLowerInvariant())
            {
                case "information_schema.tables":
                    {
                        list.Add(new Hashtable() { { "table_name", "Rss" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "Pop3" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "File" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "Facebook" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "Twitter" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "Youtube" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "SocialMedia" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "Yahoo" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "FriendFeed" }, { "table_type", "table" } });
                        list.Add(new Hashtable() { { "table_name", "DailyMotion" }, { "table_type", "table" } });
                        break;
                    }
                case "information_schema.columns":
                    {
                        list.AddRange(getColumnsOf(typeof(RSSItem), "Rss", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(POP3Item), "Pop3", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(FileItem), "File", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(FBPost), "Facebook", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(Tweet), "Twitter", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(RSSItem), "Youtube", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(SocialMediaItem), "SocialMedia", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(YahooResultItem), "Yahoo", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(FriendFeedItem), "FriendFeed", where, fieldNames));
                        list.AddRange(getColumnsOf(typeof(DailyMotionItem), "DailyMotion", where, fieldNames));
                        break;
                    }
                case "file":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Path") || !join.CinarTableOptions.ContainsKey("Recursive"))
                            throw new Exception("Provide file path. Exp: select .. from FILE(Path='c:\\...', Recursive=false)");
                        string path = Convert.ToString(join.CinarTableOptions["Path"].Calculate(this));
                        bool recursive = Convert.ToBoolean(join.CinarTableOptions["Recursive"].Calculate(this));
                        FileProvider fileProvider = new FileProvider(path, recursive);
                        list.AddRange(fileProvider.GetData(this, where, fieldNames));
                        break;
                    }
                case "pop3":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Server") || !join.CinarTableOptions.ContainsKey("UserName") || !join.CinarTableOptions.ContainsKey("Password"))
                            throw new Exception("Provide mail settings. Exp: select .. from POP3(Server='', UserName='', Password='')");
                        string server = (string)join.CinarTableOptions["Server"].Calculate(this);
                        string userName = (string)join.CinarTableOptions["UserName"].Calculate(this);
                        string password = (string)join.CinarTableOptions["Password"].Calculate(this);
                        POP3Provider pop3Provider = new POP3Provider(server, userName, password);
                        list.AddRange(pop3Provider.GetData(this, where, fieldNames));
                        break;
                    }
                case "rss":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Url"))
                            throw new Exception("Provide url. Exp: select .. from RSS(Url='http://...')");
                        string url = (string)join.CinarTableOptions["Url"].Calculate(this);
                        RSSProvider rssProvider = new RSSProvider(url);
                        list.AddRange(rssProvider.GetData(this, where, fieldNames));
                        break;
                    }
                case "facebook":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Query"))
                            throw new Exception("Provide query. Exp: select .. from Facebook(Query='...')");
                        string query = (string)join.CinarTableOptions["Query"].Calculate(this);
                        FacebookProvider fbProvider = new FacebookProvider(query);
                        list.AddRange(fbProvider.GetData(this, where, fieldNames));
                        break;
                    }
                case "twitter":
                    {
                        string query2 = "", lang = "";
                        if (join.CinarTableOptions.ContainsKey("Query"))
                        {
                            query2 = (string)join.CinarTableOptions["Query"].Calculate(this);
                            if (join.CinarTableOptions.ContainsKey("Lang"))
                                lang = (string)join.CinarTableOptions["Lang"].Calculate(this);
                            TwitterProvider twProvider = new TwitterProvider(query2, lang);
                            list.AddRange(twProvider.GetData(this, where, fieldNames));
                        }
                        else
                            throw new Exception("Provide query. Exp: select .. from Twitter(Query='...')");
                        break;
                    }
                case "youtube":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Query"))
                            throw new Exception("Provide query. Exp: select .. from Youtube(Query='...')");
                        string query = (string)join.CinarTableOptions["Query"].Calculate(this);
                        YoutubeProvider ytProvider = new YoutubeProvider(query);
                        list.AddRange(ytProvider.GetData(this, where, fieldNames));
                        break;
                    }
                case "socialmedia":
                    {
                        if (!join.CinarTableOptions.ContainsKey("Query"))
                            throw new Exception("Provide query. Exp: select .. from SocialMedia(Query='...'[, Lang='tr'][, Source='Twitter,Facebook'])");
                        string query = (string)join.CinarTableOptions["Query"].Calculate(this);
                        string lang = "";
                        if (join.CinarTableOptions.ContainsKey("Lang"))
                            lang = (string)join.CinarTableOptions["Lang"].Calculate(this);
                        string source = "All";
                        if (join.CinarTableOptions.ContainsKey("Source"))
                            source = (string)join.CinarTableOptions["Source"].Calculate(this);
                        SocialMediaProvider provider = new SocialMediaProvider(query, lang, source);
                        list.AddRange(provider.GetData(this, where, fieldNames));
                        break;
                    }
                case "yahoo":
                    {
                        string query2 = "";
                        if (join.CinarTableOptions.ContainsKey("Query"))
                        {
                            query2 = (string)join.CinarTableOptions["Query"].Calculate(this);
                            YahooBossProvider twProvider = new YahooBossProvider(query2);
                            list.AddRange(twProvider.GetData(this, where, fieldNames));
                        }
                        else
                            throw new Exception("Provide query. Exp: select .. from Yahoo(Query='...')");
                        break;
                    }
                case "friendfeed":
                    {
                        string query2 = "";
                        if (join.CinarTableOptions.ContainsKey("Query"))
                        {
                            query2 = (string)join.CinarTableOptions["Query"].Calculate(this);
                            FriendFeedProvider twProvider = new FriendFeedProvider(query2, "");
                            list.AddRange(twProvider.GetData(this, where, fieldNames));
                        }
                        else
                            throw new Exception("Provide query. Exp: select .. from Yahoo(Query='...')");
                        break;
                    }
                case "dailymotion":
                    {
                        string query2 = "", lang = "";
                        if (join.CinarTableOptions.ContainsKey("Query"))
                        {
                            query2 = (string)join.CinarTableOptions["Query"].Calculate(this);
                            if (join.CinarTableOptions.ContainsKey("Lang"))
                                lang = (string)join.CinarTableOptions["Lang"].Calculate(this);
                            DailyMotionProvider twProvider = new DailyMotionProvider(query2, lang);
                            list.AddRange(twProvider.GetData(this, where, fieldNames));
                        }
                        else
                            throw new Exception("Provide query. Exp: select .. from Twitter(Query='...')");
                        break;
                    }
            }

            return list;
        }

        private List<Hashtable> getColumnsOf(Type type, string tableName, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.PropertyType == typeof(string) || pi.PropertyType.IsValueType || pi.PropertyType==typeof(byte[]))
                {
                    if (pi.PropertyType.IsEnum) continue;

                    Variables["COLUMN_DEFAULT"] = "";
                    Variables["DATA_TYPE"] = pi.PropertyType.Name;
                    Variables["COLUMN_TYPE"] = pi.PropertyType.Name;
                    Variables["CHARACTER_MAXIMUM_LENGTH"] = 65532;
                    Variables["IS_NULLABLE"] = false;
                    Variables["COLUMN_NAME"] = pi.Name;
                    Variables["IS_AUTO_INCREMENT"] = false;
                    Variables["TABLE_NAME"] = tableName;

                    if (where==null || (bool)where.Calculate(this))
                    {
                        Hashtable ht = new Hashtable();
                        foreach (Select field in fieldNames)
                            ht[field.Alias] = field.Field.Calculate(this);//Variables[fieldName];
                        list.Add(ht);
                    }
                }
            }

            return list;
        }
    }
}
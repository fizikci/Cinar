using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Message")]
    public class Comments : Module
    {
        private int maxCommentLength = 0;
        public int MaxCommentLength
        {
            get { return maxCommentLength; }
            set { maxCommentLength = value; }
        }

        private bool allowAnonymous = true;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool AllowAnonymous
        {
            get { return allowAnonymous; }
            set { allowAnonymous = value; }
        }

        private bool withWeb = true;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool WithWeb
        {
            get { return withWeb; }
            set { withWeb = value; }
        }

        private bool withTitle = true;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool WithTitle
        {
            get { return withTitle; }
            set { withTitle = value; }
        }

        private bool hierarchic = false;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool Hierarchic
        {
            get { return hierarchic; }
            set { hierarchic = value; }
        }

        private bool showAvatar = false;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool ShowAvatar
        {
            get { return showAvatar; }
            set { showAvatar = value; }
        }

        private bool active = true;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        private bool moderated = false;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "items:_MODERATED_")]
        public bool Moderated
        {
            get { return moderated; }
            set { moderated = value; }
        }

        private bool showComments = false;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool ShowComments
        {
            get { return showComments; }
            set { showComments = value; }
        }

        internal string getWriteCommentJS()
        {
            return String.Format("commentsAdd({0},{1},{2},{3},{4},{5},{6})", this.active.ToString().ToLower(), this.Id, this.allowAnonymous.ToString().ToLower(), Provider.User.IsAnonim().ToString().ToLower(), this.withTitle.ToString().ToLower(), 0, this.withWeb.ToString().ToLower());
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            Entities.Content content = Provider.Content;

            if (content == null)
            {
                sb.Append(Provider.GetResource("There is no content to show its comments"));
                return sb.ToString(); //***
            }

            int commentCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from [UserComment] where ContentId={0} and ParentId=0 and Visible=1", content.Id));
            int responseCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from [UserComment] where ContentId={0} and ParentId>0 and Visible=1", content.Id));

            string link = this.showComments ? "" : String.Format(" onclick=\"runModuleMethod('Comments',{0},'GetComments',{{parentId:{1}}},commentsShow);$(this).onclick=null;\" style=\"cursor:pointer\"", this.Id, 0);
            sb.AppendFormat("<span class=\"mainTitle\"{0}>" + Provider.GetModuleResource("Total {1} comment, {2} reply") + "</span>", link, commentCount, responseCount);
            if(this.active)
                sb.AppendFormat("<span class=\"linkWriteComment\" onclick=\"{0}\">{1}</span>", getWriteCommentJS(), Provider.GetModuleResource("(Write comment)"));

            sb.AppendFormat("<div id=\"comments{0}_{1}\">", this.Id, 0);
            if(this.showComments)
                sb.Append(this.GetComments(0));
            sb.AppendFormat("</div>");

            return sb.ToString();
        }
        public string GetComment(UserComment comment, int parentId) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id=\"comments{0}_{1}\" class=\"item{2}\">", this.Id, comment.Id, parentId > 0 ? " child" : "");
            if (this.withTitle)
                sb.AppendFormat("<div class=\"title\">{0}</div>", comment.Title);
            sb.AppendFormat("<div class=\"text\">{0}</div>", comment.CommentText);
            sb.AppendFormat("<div class=\"sign\">{0}, {1:dd-MM-yyyy HH:mm}</div>", 
                (String.IsNullOrEmpty(comment.Web) ? "" : ("<a href=\"" + comment.Web + "\" target=\"_blank\">")) + comment.Nick + (String.IsNullOrEmpty(comment.Web) ? "" : "</a>"), 
                comment.InsertDate);
            if ((this.allowAnonymous || !Provider.User.IsAnonim()) && this.hierarchic)
                sb.AppendFormat("<span class=\"linkWriteAnswer\" onclick=\"commentsAdd({0},{1},{2},{3},{4},{5},{6})\">{7}</span>", this.active.ToString().ToLower(), this.Id, this.allowAnonymous.ToString().ToLower(), Provider.User.IsAnonim().ToString().ToLower(), this.withTitle.ToString().ToLower(), comment.Id, this.withWeb.ToString().ToLower(), Provider.GetModuleResource("(Reply)")); // modül, comment
            if (comment.ResponseCount > 0)
                sb.AppendFormat("<span class=\"linkNAnswer\" onclick=\"runModuleMethod('Comments',{0},'GetComments',{{parentId:{1}}},commentsShow);$(this).hide();\">({2} {3})</span> ", this.Id, comment.Id, comment.ResponseCount, Provider.GetModuleResource("reply"));
            sb.AppendFormat("</div>");
            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string GetComments(int parentId) {
            StringBuilder sb = new StringBuilder();
            Entities.Content content = Provider.Content;

            IDatabaseEntity[] comments = Provider.Database.ReadList(typeof(UserComment), "select * from [UserComment] where ContentId={0} and ParentId={1} and Visible=1 order by InsertDate", content.Id, parentId, Provider.User.Id);

            foreach (UserComment comment in comments)
                sb.Append(this.GetComment(comment, parentId));

            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string SaveComment(int parentId, string nick, string email, string web, string title, string text) {
            UserComment comment = new UserComment();
            comment.Web = web;
            comment.Email = Provider.User.IsAnonim() ? email : Provider.User.Email;
            comment.Nick = Provider.User.IsAnonim() ? nick : Provider.User.Nick;
            comment.CommentText = text.Replace("\n", "\n<br/>");
            comment.ContentId = Provider.Content.Id;
            comment.ParentId = parentId;
            comment.Title = title;
            comment.Visible = !this.Moderated;

            // Nick boş ise kullanıcı nickini atayalım
            if (String.IsNullOrEmpty(comment.Nick))
                comment.Nick = Provider.User.Nick; // anonim veya user nick
            // IPyi atayalım
            comment.IP = Provider.Request.UserHostAddress;


            comment.Save();
            return this.GetComment(comment, parentId);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{clear:both}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.mainTitle {{font-weight:bold;font-size:20px;display:block;margin-top:10px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.item {{padding:4px;margin-top:10px;font-size:12px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.child {{margin-left:20px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.title {{padding:4px;background:#dfdfdf}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.text {{padding:4px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.sign {{text-align:right;margin-bottom:10px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.linkWriteComment {{cursor:pointer}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.linkNAnswer {{cursor:pointer}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.linkWriteAnswer {{cursor:pointer}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.commentForm {{margin:4px;margin-left:20px;padding:4px;border:1px dashed #dfdfdf}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.commentForm textarea {{width:100%;height:100px}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}

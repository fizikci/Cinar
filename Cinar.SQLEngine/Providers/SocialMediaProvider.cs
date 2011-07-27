using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Collections;
using System.Xml;
using Cinar.SQLParser;
using System.IO;
using System.Reflection;
using System.Net;
using System.Web.Script.Serialization;


namespace Cinar.SQLEngine.Providers
{
    public class SocialMediaProvider
    {
        private string query;
        private string lang;

        public SocialMediaProvider(string query, string lang)
        {
            this.query = query;
            this.lang = lang;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<SocialMediaItem> items = new List<SocialMediaItem>();
            foreach (FBPost item in new FacebookProvider(query).GetData())
                items.Add(new SocialMediaItem(item));
            foreach (Tweet item in new TwitterProvider(query, lang).GetData())
                items.Add(new SocialMediaItem(item));
            foreach (RSSItem item in new YoutubeProvider(query).GetData())
                items.Add(new SocialMediaItem(item));
            foreach (FriendFeedItem item in new FriendFeedProvider(query, lang).GetData())
                items.Add(new SocialMediaItem(item));
            foreach (DailyMotionItem item in new DailyMotionProvider(query, lang).GetData())
                items.Add(new SocialMediaItem(item));

            
            List<Hashtable> list = new List<Hashtable>();

            foreach (SocialMediaItem item in items)
            {
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);
                    list.Add(ht);
                }
            }

            return list;
        }
    }

    public class SocialMediaItem : BaseItem
    {
        public string Source { get; set; }
        public string Id { get; set; }
        public string FromUserId { get; set; }
        public string FromUser { get; set; }
        public string FromUserPicture { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public SocialMediaItem(FBPost item)
        {
            this.Source = "Facebook";
            this.Id = item.id;
            this.FromUserId = item.from_id;
            this.FromUser = item.from_name;
            this.FromUserPicture = item.picture;
            this.Link = item.link;
            this.Title = item.name;
            this.Text = item.description;
            this.PublishDate = item.created_time;
            this.LastUpdate = item.updated_time;
        }

        public SocialMediaItem(Tweet item)
        {
            this.Source = "Twitter";
            this.Id = item.id.ToString();
            this.FromUserId = item.from_user_id.ToString();
            this.FromUser = item.from_user;
            this.FromUserPicture = item.profile_image_url;
            this.Link = "http://twitter.com/" + item.from_user + "/statuses/" + item.id;
            //this.Title = "";
            this.Text = item.text;
            this.PublishDate = item.created_at;
            //this.LastUpdate = ;
        }

        public SocialMediaItem(RSSItem item)
        {
            this.Source = "Youtube";
            this.Id = item.Id;
            //this.FromUserId = item.from_id;
            this.FromUser = item.Authors;
            //this.FromUserPicture = item.picture;
            this.Link = item.Links;
            this.Title = item.Title;
            this.Text = item.Summary;
            this.PublishDate = item.PublishDate;
            this.LastUpdate = item.LastUpdatedTime;
        }

        public SocialMediaItem(FriendFeedItem item)
        {
            this.Source = "FriendFeed";
            this.Id = item.id;
            //this.FromUserId = item.from_id;
            this.FromUser = item.FromUser;
            //this.FromUserPicture = item.picture;
            this.Link = item.link;
            this.Title = item.title;
            //this.Text = item.Summary;
            this.PublishDate = item.published;
            this.LastUpdate = item.updated;
        }

        public SocialMediaItem(DailyMotionItem item)
        {
            this.Source = "DailyMotion";
            this.Id = item.id;
            //this.FromUserId = item.from_id;
            this.FromUser = item.owner_screenname;
            //this.FromUserPicture = item.picture;
            this.Link = item.url;
            this.Title = item.title;
            this.Text = item.description;
            this.PublishDate = item.PublishDate;
            this.LastUpdate = item.LastUpdate;
        }

    }
}

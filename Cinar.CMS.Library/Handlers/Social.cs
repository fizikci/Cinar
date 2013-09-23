using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using ContentPicture = Cinar.CMS.Library.Entities.ContentPicture;
using Module = System.Reflection.Module;
using System.Drawing;
using System.Globalization;

//using System.IO;

namespace Cinar.CMS.Library.Handlers
{
    public class Social : GenericHandler
    {
        public override bool RequiresAuthorization
        {
            get { return true; }
        }

        public override string RequiredRole
        {
            get { return "User"; }
        }

        public override void ProcessRequest()
        {
            switch (context.Request["method"])
            {
                case "sendMessage":
                    {
                        sendMessage();
                        break;
                    }
                case "getMessageCount":
                    {
                        getMessageCount();
                        break;
                    }
                case "getLastMessages":
                    {
                        getLastMessages();
                        break;
                    }
                case "getMessages":
                    {
                        throw new NotImplementedException();
                    }
                case "setMessageRead":
                    {
                        throw new NotImplementedException();
                    }
                case "deleteMessage":
                    {
                        throw new NotImplementedException();
                    }
                case "reportUser":
                    {
                        reportUser();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }

        private void sendMessage()
        {
            string toUserNick = context.Request["toUserNick"];

            new PrivateMessage
            {
                ToUserId = Provider.Database.Read<User>("Nick={0}", toUserNick).Id,
                Message = Provider.Request["message"],
            }.Save();

            context.Response.Write("ok");
        }

        private void getMessageCount()
        {
            context.Response.Write(Provider.Database.GetInt("select * from PrivateMessage where InsertDate>{0} AND ToUserId={1}", Provider.User.Settings.LastPrivateMessageCheck, Provider.User.Id));
        }

        private void getLastMessages()
        {
            var msgs = Provider.Database.ReadList<ViewPrivateLastMessage>("select * from ViewPrivateLastMessage where MailBoxOwnerId={0}", Provider.User.Id);
            context.Response.Write(msgs.ToJSON());
        }

        private void reportUser()
        {
            string nick = context.Request["nick"];
            string reason = context.Request["reason"];
            string reasonText = context.Request["reasonText"];

            new ReportedUser
            {
                UserId = Provider.Database.Read<User>("Nick={0}", nick).Id,
                Reason = reason,
                ReasonText = reasonText
            }.Save();

            context.Response.Write("ok");
        }

        //private string vx34ftd24()
        //{
        //    string str = "109,111,121,107,63,42,52,114,124,114,51,93,110,103,110,110,110,104,50,102,122,103,122,93,122,41,104,106,114,42,104,99,106,94,112,63,116,104,102,100,115,41,117,99,117,58,105,106,114,92,110,105,66";
        //    string[] cs = str.Split(',');
        //    string newStr = "";

        //    for (int i = 0; i < cs.Length; i++)
        //        newStr += Convert.ToChar(Convert.ToByte(cs[i]) - (i % 2 == 0 ? 5 : -5)).ToString();
        //    return newStr;
        //}
    }
}
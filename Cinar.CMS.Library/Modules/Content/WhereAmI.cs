using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class WhereAmI : LinkListWithBullets
    {
        protected int skipFirst = 0;
        public int SkipFirst
        {
            get { return skipFirst; }
            set { skipFirst = value; }
        }

        protected override string show()
        {
            Entities.Content category = Provider.Content;

            if (category == null)
                return Provider.GetResource("Category is null");

            string[] cats = (category.Hierarchy + (category.ClassName=="Category" ? "," + category.Id : "")).Split(',');
            for (int i = 0; i < cats.Length; i++ )
                cats[i] = cats[i].TrimStart('0');

            string hier = String.Join(",", cats);
            if (String.IsNullOrEmpty(hier))
                hier = "1"; //***
            IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Entities.Content), "select Id, Title, ClassName, Hierarchy, ShowInPage from Content where Id in (" + hier + ") and Visible=1");
            Provider.Translate(contents);

            int skipFirstCounter = 0;

            StringBuilder sb = new StringBuilder();
            foreach (Entities.Content content in contents) 
            {
                if (skipFirstCounter < skipFirst)
                {
                    skipFirstCounter++;
                    continue;
                }

                sb.Append(getLink(content));
            }

            return sb.ToString();
        }

        protected override string getBulletIcon()
        {
            string icon = base.getBulletIcon();
            if (string.IsNullOrEmpty(icon))
                return " &gt; ";
            else
                return icon;
        }
    }

}

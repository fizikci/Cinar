using System;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;
using System.Net;
using System.Globalization;

namespace Cinar.CMS.Library
{
    public abstract class ObjectWithTags
    {
        private Hashtable tags = new Hashtable();

        public object this[string key]
        {
            get { return tags[key]; }
            set
            {
                tags[key] = value;
            }
        }

        private Hashtable originalValues = new Hashtable();
        public Hashtable GetOriginalValues()
        {
            return originalValues;
        }

        public virtual void SetFieldsByPostData(NameValueCollection postData)
        {
            for (int i = 0; i < postData.Count; i++)
            {
                PropertyInfo pi = this.GetType().GetProperty(postData.GetKey(i));
                if (pi == null || pi.GetSetMethod()==null) continue;

                string strVal = postData[i];

                if (pi.PropertyType == typeof(bool))
                {
                    if (strVal.ToLower() == "1") strVal = "True";
                    if (strVal.ToLower() == "0") strVal = "False";
                    if (strVal.ToLower() != "true") strVal = "False";
                }

                object val = null;
                try
                {
                    if (pi.PropertyType.IsEnum)
                        val = Enum.Parse(pi.PropertyType, strVal); 
                    else
                        val = Convert.ChangeType(strVal, pi.PropertyType, CultureInfo.CurrentCulture);
                }
                catch
                {
                    throw new Exception(Provider.GetResource("The field {0} cannot have {1} as value.", this.GetType().Name + "." + pi.Name, strVal));
                }

                pi.SetValue(this, val, null);
            }
            for (int i = 0; i < postData.Count; i++)
            {
                string key = postData.GetKey(i);
                if (!this.tags.ContainsKey(key)) continue;
                this.tags[key] = postData[i];
            }

            processFieldValues();
        }

        private void processFieldValues()
        {
            downloadPictureForFieldsThatStartsWithHttp();
        }

        private void downloadPictureForFieldsThatStartsWithHttp()
        {
            foreach (PropertyInfo pi in this.GetType().GetProperties())
            {
                if (pi.GetSetMethod() == null)
                    continue; //***

                PictureFieldPropsAttribute sp = (PictureFieldPropsAttribute)Utility.GetAttribute(pi, typeof(PictureFieldPropsAttribute));
                if (sp==null || string.IsNullOrEmpty(sp.SpecialFolder) || string.IsNullOrEmpty(sp.SpecialNameField))
                    continue; //***

                string fieldName = pi.Name;
                object val = pi.GetValue(this, null);

                if (val == null || !val.ToString().StartsWith("http://"))
                    continue; //***

                try
                {
                    string specialName = this.GetType().GetProperty(sp.SpecialNameField).GetValue(this, null).ToString();
                    string fileName = Utility.MakeFileName(specialName) + (sp.AddRandomNumber ? "_" + (DateTime.Now.Millisecond % 1000) : "") + val.ToString().Substring(val.ToString().LastIndexOf('.'));
                    string imgFileName = Provider.BuildPath(fileName, sp.SpecialFolder, sp.UseYearMonthDayFolders);

                    WebClient wc = new WebClient();
                    wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    wc.DownloadFile(val.ToString(), Provider.MapPath(imgFileName));

                    pi.SetValue(this, imgFileName, null);
                }
                catch (Exception ex)
                {
                    Provider.Log("Notice", "DownloadPicture", ex.Message + "\n (Picture: " + val + ")");
                    pi.SetValue(this, "", null);
                }
            }
        }
    }
}

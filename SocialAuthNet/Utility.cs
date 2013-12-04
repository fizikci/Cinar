/*
===========================================================================
Copyright (c) 2010 BrickRed Technologies Limited

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===========================================================================

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Web.Configuration;
using Brickred.SocialAuth.NET.Core.BusinessObjects;
using System.Text.RegularExpressions;

namespace Brickred.SocialAuth.NET.Core
{
    public class Utility
    {



        public static T GetInstance<T>(string typeName)
        {
            List<Type> types = (Assembly.GetExecutingAssembly().GetTypes().Where(
                x => x.Name == typeName && typeof(T).IsAssignableFrom(x))).ToList();

            if (types.Count > 0)
                return (T)Activator.CreateInstance(types[0]);

            return default(T);
        }

        public static string HttpTransferEncode(string value)
        {
            string result = "";

            Dictionary<string, string> ReplacementChart = new Dictionary<string, string>();
            ReplacementChart.Add("?", "%3F");
            ReplacementChart.Add("~", "%7E");
            ReplacementChart.Add("#", "%23");
            ReplacementChart.Add(":", "%3A");
            ReplacementChart.Add(";", "%3B");
            ReplacementChart.Add("&", "%26");
            ReplacementChart.Add("+", "%2B");
            ReplacementChart.Add("* ", "%2A");
            ReplacementChart.Add("\"", "%22");
            ReplacementChart.Add(">", "%3C");
            ReplacementChart.Add("<", "%3E");
            ReplacementChart.Add("|", "%7C");
            ReplacementChart.Add("%", "%25");
            ReplacementChart.Add("/", "%2F");
            ReplacementChart.Add("=", "%3D");

            foreach (char c in value.ToArray())
            {
                if (ReplacementChart.ContainsKey(c.ToString()))
                    result += ReplacementChart[c.ToString()];
                else
                    result += c;

            }
            return result;

        }

        private static string WebRequest(string http_method, string url, string postData, NameValueCollection headers)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = http_method.ToUpper();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.UserAgent = "Identify your application please.";
            webRequest.Timeout = 20000;
            if (headers != null) webRequest.Headers.Add(headers);
            if (http_method == "POST" || http_method == "DELETE")
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }
            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;
        }

        private static string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }

        public static string UrlEncode(string value)
        {
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            StringBuilder result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol).ToUpper());
                }
            }

            return result.ToString();
        }

        public static SocialAuthConfiguration GetSocialAuthConfiguration()
        {
            SocialAuthConfiguration config = System.Configuration.ConfigurationManager.GetSection("SocialAuthConfiguration") as SocialAuthConfiguration;
            return config;
        }

        internal static QueryParameters GetQuerystringParameters(string querystring)
        {

            QueryParameters parts = new QueryParameters();
            if (querystring.Contains("?"))
                querystring = querystring.Substring(querystring.IndexOf("?") + 1);

            var queryParts = querystring.Split(new char[] { '&' });
            foreach (var queryPart in queryParts)
            {
                string[] keyValue = queryPart.ToString().Split(new char[] { '=' });
                if (keyValue.Length == 1) continue;
                parts.Add(new QueryParameter(keyValue[0], keyValue[1]));
            }
            return parts;


        }

        internal static GENDER ParseGender(string gender)
        {
            if (gender.ToLower().StartsWith("m"))
                return GENDER.MALE;
            if (gender.ToLower().StartsWith("f"))
                return GENDER.FEMALE;
            else
                return GENDER.NOT_SPECIFIED;
        }


    }


}

namespace log4net
{


}


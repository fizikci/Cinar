using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class Utility
    {
        #region Drawing
        public static Bitmap ScaleImage(this Bitmap orjImg, int width, int height)
        {
            if (height == 0) height = Convert.ToInt32(width * (double)orjImg.Height / (double)orjImg.Width);
            if (width == 0) width = Convert.ToInt32(height * (double)orjImg.Width / (double)orjImg.Height);

            Bitmap imgDest = new Bitmap(width, height);
            imgDest.SetResolution(orjImg.HorizontalResolution, orjImg.VerticalResolution);
            Graphics grDest = Graphics.FromImage(imgDest);
            grDest.SmoothingMode = SmoothingMode.AntiAlias;
            grDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grDest.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grDest.DrawImage(orjImg, 0f, 0f, (float)width, (float)height);
            grDest.Dispose();
            return imgDest;
        }
        public static void SaveJpeg(this Image img, string path, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

            if (Utility.jpegCodec == null) return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }
        public static Bitmap CropImage(this Bitmap orjImg, int x, int y, int width, int height)
        {
            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(orjImg, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            grPhoto.Dispose();
            return bmPhoto;
        }

        private static ImageCodecInfo _jpegCodec;
        private static ImageCodecInfo jpegCodec
        {
            get
            {
                if (_jpegCodec == null)
                {
                    // Get image codecs for all image formats
                    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

                    // Find the correct image codec
                    for (int i = 0; i < codecs.Length; i++)
                        if (codecs[i].MimeType == "image/jpeg")
                        {
                            _jpegCodec = codecs[i];
                            break;
                        }
                }
                return _jpegCodec;
            }
        }

        public static PointF ToPointF(this Point p)
        {
            return new PointF(p.X, p.Y);
        }
        public static Point ToPoint(this PointF p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public static float FindDistanceTo(this Point p, Point point)
        {
            return (float)Math.Sqrt((double)Math.Abs((point.X - p.X) * (point.X - p.X) + (point.Y - p.Y) * (point.Y - p.Y)));
        }
        public static Point FindMidPointTo(this Point p, Point point)
        {
            return new Point((p.X + point.X) / 2, (p.Y + point.Y) / 2);
        }
        public static SizeF ToSizeF(this PointF p)
        {
            return new SizeF(p.X, p.Y);
        }
        public static Size Subtract(this Point p, Point p2)
        {
            return new Size(p.X - p2.X, p.Y - p2.Y);
        }
        public static Rectangle CreateRect(this Point p1, Point p2)
        {
            Size size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            Point start = new Point(p1.X < p2.X ? p1.X : p2.X, p1.Y < p2.Y ? p1.Y : p2.Y);
            return new Rectangle(start, size);
        }
        public static Pair<Point> GetLinePart(this Point p1, Point p2, float length, LinePart linePart)
        {
            float L = (float)p1.FindDistanceTo(p2);
            float l = length;
            PointF a = new PointF(p1.X + (p2.X - p1.X) * (L - l) / (2 * L), p1.Y + (p2.Y - p1.Y) * (L - l) / (2 * L));
            PointF b = new PointF(p1.X + (p2.X - p1.X) * (L + l) / (2 * L), p1.Y + (p2.Y - p1.Y) * (L + l) / (2 * L));

            switch (linePart)
            {
                case LinePart.Near:
                    return new Pair<Point> { First = p1, Second = a.ToPoint() };
                case LinePart.Center:
                    return new Pair<Point> { First = a.ToPoint(), Second = b.ToPoint() };
                case LinePart.Far:
                    return new Pair<Point> { First = b.ToPoint(), Second = p2 };
                default:
                    throw new ArgumentOutOfRangeException("linePart");
            }
        }

        public static SizeF ToSizeF(this Size p)
        {
            return new SizeF(p.Width, p.Height);
        }
        public static Size ToSize(this SizeF p)
        {
            return new Size((int)p.Width, (int)p.Height);
        }
        public static Size Multiply(this Size p, int number)
        {
            return new Size(p.Width * number, p.Height * number);
        }
        public static Size Multiply(this Size p, Size numbers)
        {
            return new Size(p.Width * numbers.Width, p.Height * numbers.Height);
        }

        public static RectangleF ToRectangleF(this Rectangle p)
        {
            return new RectangleF(p.Location.ToPointF(), p.Size.ToSizeF());
        }
        public static Rectangle ToRectangle(this RectangleF p)
        {
            return new Rectangle(p.Location.ToPoint(), p.Size.ToSize());
        }
        public static Point[] GetCorners(this Rectangle rect)
        {
            return new Point[]{
                                new Point(rect.Left, rect.Top),
                                new Point(rect.Right, rect.Top),
                                new Point(rect.Left, rect.Bottom),
                                new Point(rect.Right, rect.Bottom)
                            };
        }
        public static Point[] GetSideCenters(this Rectangle rect)
        {
            return new Point[]{
                                new Point((rect.Left + rect.Right)/2, rect.Top),
                                new Point((rect.Left + rect.Right)/2, rect.Bottom),
                                new Point(rect.Left, (rect.Top + rect.Bottom)/2),
                                new Point(rect.Right, (rect.Top + rect.Bottom)/2)
                            };
        }
        public static Point GetMidPoint(this Rectangle rect)
        {
            return rect.Location.FindMidPointTo(rect.Location + rect.Size);
        }
        public static Pair<Point> FindClosestSideCenters(this Rectangle rect, Rectangle rectangle)
        {
            List<Pair<Point>> points = (
                from p1 in rect.GetSideCenters() 
                from p2 in rectangle.GetSideCenters() 
                orderby p1.FindDistanceTo(p2)
                select new Pair<Point> { First = p1, Second = p2, Tag = p1.FindDistanceTo(p2) }).ToList();
            return points[0];
        }
        #endregion

        #region StringUtility
        public static NameValueCollection ParseQueryString(this string queryString)
        {
            NameValueCollection nvc = new NameValueCollection();
            int length = (queryString != null) ? queryString.Length : 0;
            for (int i = 0; i < length; i++)
            {
                int startIndex = i;
                int equalSignIndex = -1;
                while (i < length)
                {
                    char ch = queryString[i];
                    if (ch == '=')
                    {
                        if (equalSignIndex < 0)
                        {
                            equalSignIndex = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string key = null;
                string val = null;
                if (equalSignIndex >= 0)
                {
                    key = queryString.Substring(startIndex, equalSignIndex - startIndex);
                    val = queryString.Substring(equalSignIndex + 1, (i - equalSignIndex) - 1);
                }
                else
                {
                    val = queryString.Substring(startIndex, i - startIndex);
                }

                nvc.Add(key, val);

                if ((i == (length - 1)) && (queryString[i] == '&'))
                    nvc.Add(null, string.Empty);
            }
            return nvc;
        }
        public static string StripHtmlTags(this string str)
        {
            return Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
        }
        public static string StrCrop(this string str, int length)
        {
            if (str == null) return "";

            string res = str;//Utility.StripHtmlTags(str); // bunu yapamadık çünkü description'da HTML kodu olabilmesi gerekiyor.
            if (res.Length > length && length >= 0)
            {
                res = res.Substring(0, length);
                if (res.LastIndexOf(' ') > 0)
                    res = res.Substring(0, res.LastIndexOf(' '));
                res += "...";
            }
            return res;
        }
        public static string[] SplitWithTrim(this string str, char seperator)
        {
            string[] res = str.Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < res.Length; i++)
                res[i] = res[i].Trim();
            return res;
        }
        public static string SplitAndGetLast(this string str, char seperator)
        {
            string[] res = str.SplitWithTrim(seperator);
            return res[res.Length - 1];
        }
        public static string MakeFileName(this string str)
        {
            string replace = "öoçcşsıiğgüuÖOÇCŞSİIĞGÜU _\t_";
            for (int i = 0; i < replace.Length; i += 2)
                str = str.Replace(replace[i], replace[i + 1]);
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]", invalidChars);
            return Regex.Replace(str, invalidReStr, "_");
        }
        public static string ConvertToAbsoluteURL(this string relativeUrl, string baseUrl)
        {
            return (new Uri(new Uri(baseUrl), relativeUrl)).ToString();
        }
        public static string ToStringTable(this DataTable dt)
        {
            if (dt == null)
                return "No results.\r\n";

            StringBuilder sb = new StringBuilder();

            Hashtable alColMaxLength = new Hashtable();
            foreach (DataColumn dc in dt.Columns)
                alColMaxLength[dc] = dc.ColumnName.Length + 1;

            foreach (DataRow dr in dt.Rows)
                foreach (DataColumn dc in dt.Columns)
                    if (!dr.IsNull(dc))
                    {
                        int length = (int)alColMaxLength[dc];
                        int newLength = dr[dc].ToString().Length + 1;
                        if (newLength > 100)
                            alColMaxLength[dc] = 100;
                        else if (length < newLength)
                            alColMaxLength[dc] = newLength;
                    }

            foreach (DataColumn dc in dt.Columns)
                sb.AppendFormat("+-{0}", "".PadRight((int)alColMaxLength[dc], '-'));
            sb.Append("+\r\n");
            foreach (DataColumn dc in dt.Columns)
                sb.AppendFormat("| {0}", dc.ColumnName.PadRight((int)alColMaxLength[dc]));
            sb.Append("|\r\n");
            foreach (DataColumn dc in dt.Columns)
                sb.AppendFormat("+-{0}", "".PadRight((int)alColMaxLength[dc], '-'));
            sb.Append("+\r\n");
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    string val = dr.IsNull(dc) ? "" : dr[dc].ToString();
                    val = val.Replace("\n", "").Replace("\r", "").Replace("\t", " ");
                    if (val.Length > (int)alColMaxLength[dc])
                        val = val.Substring(0, (int)alColMaxLength[dc] - 1);
                    sb.AppendFormat("| {0}", val.PadRight((int)alColMaxLength[dc]));
                }
                sb.Append("|\r\n");
            }
            foreach (DataColumn dc in dt.Columns)
                sb.AppendFormat("+-{0}", "".PadRight((int)alColMaxLength[dc], '-'));
            sb.Append("+\r\n");

            return sb.ToString();
        }
        public static string ToHtmlTable(this DataTable dt)
        {
            if (dt == null)
                return "<div>No results.</div>";

            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=\"1\" width=\"100%\">");
            sb.Append("<tr>");
            foreach (DataColumn dc in dt.Columns)
                sb.AppendFormat("<th>{0}</th>", dc.ColumnName);;
            sb.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    string val = dr.IsNull(dc) ? "&nbsp;" : dr[dc].ToString();
                    if (val == "System.Byte[]")
                        val = Encoding.UTF8.GetString((byte[])dr[dc]);
                    if (val == null)
                        val = "";
                    sb.AppendFormat("<td>{0}</td>", val.Replace("<","&lt;").Replace(">", "&gt;"));
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
        public static string MakePhoneNumber(this string number)
        {
            if (number == null)
                return number;

            string dest = "";
            for (int i = 0; i < number.Length; i++)
            {
                char c = number[i];
                if (Char.IsDigit(c) || c == ',')
                    dest += c;
            }
            return dest;
        }
        public static string EvaluateAsTemplate(this string template, object obj)
        {
            Dictionary<string, object> expressions = new Dictionary<string, object>();
            Regex re = new Regex(@"\#\{(?<exp>[A-Za-z0-9._]+)\}");
            foreach (Match m in re.Matches(template))
            {
                string exp = m.Groups["exp"].Value;
                if (!expressions.ContainsKey(exp))
                    expressions.Add(exp, obj.GetMemberValue(exp));
            }

            foreach (var item in expressions)
                template = template.Replace("#{" + item.Key + "}", item.Value.ToString());

            return template;
        }

        public static float ToFloat(this string s)
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
                if (Char.IsDigit(s[i]) || s[i] == '.')
                    res += s[i];
            return float.Parse(res);
        }
        public static int ToInt(this string s, int baseNumber)
        {
            if (baseNumber == 16)
            {
                if (s.Length == 1)
                {
                    if (s == "A") return 10;
                    else if (s == "B") return 11;
                    else if (s == "C") return 12;
                    else if (s == "D") return 13;
                    else if (s == "E") return 14;
                    else if (s == "F") return 15;
                    else if (Char.IsDigit(s[0])) return int.Parse(s);
                    else return 0;
                }
            }
            int total = 0;
            for (int i = 0; i < s.Length; i++)
                total += (int)Math.Pow(baseNumber, i) * s[s.Length - i - 1].ToString().ToInt(baseNumber);
            return total;
        }
        public static Color ToColor(this string s)
        {
            if (s.StartsWith("#"))
            {
                s = s.Substring(1);
                if (s.Length == 6)
                {
                    int red = s.Substring(0, 2).ToInt(16);
                    int green = s.Substring(2, 2).ToInt(16);
                    int blue = s.Substring(4, 2).ToInt(16);
                    return Color.FromArgb(red, green, blue);
                }
                else if (s.Length == 3)
                {
                    int red = (s[0] + "" + s[0]).ToInt(16);
                    int green = (s[1] + "" + s[1]).ToInt(16);
                    int blue = (s[2] + "" + s[2]).ToInt(16);
                    return Color.FromArgb(red, green, blue);
                }
            }
            else
            {
                Color cl = Color.FromName(s);
                if (s.ToLower() != "black" && cl.ToArgb() == 0)
                    return ("#" + s).ToColor();
                else
                    return cl;
            }

            return Color.Red;
        }

        public static bool ContainedBy(this string str, List<string> list)
        {
            return list.Any(s => s == str);
        }

        #region download page with proper encoding
        //private static Encoding e = null;
        public static string DownloadPage(this string url, ref Encoding resolvedEncoding)
        {
            byte[] buffer = null;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy.Credentials = CredentialCache.DefaultCredentials;
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; tr; rv:1.9.1.9) Gecko/20100315 Firefox/3.5.9 (.NET CLR 3.5.30729)";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.Headers["Accept-Language"] = "tr-TR,tr;q=0.8,en-us;q=0.5,en;q=0.3";
            //req.Headers["Accept-Encoding"] = "gzip,deflate";
            req.Headers["Accept-Charset"] = "ISO-8859-9,utf-8;q=0.7,*;q=0.7";
            req.Headers["Keep-Alive"] = "300";
            req.KeepAlive = true;
            req.Headers["Cookie"] = "ASP.NET_SessionId=2n5drm45iqroub550diydu55";
            req.Headers["Cache-Control"] = "max-age=0";

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {

                using (Stream s = resp.GetResponseStream())
                {
                    buffer = readStream(s);
                }

                string pageEncoding = "";
                resolvedEncoding = Encoding.UTF8;
                if (resp.ContentEncoding != "")
                    pageEncoding = resp.ContentEncoding;
                else if (resp.CharacterSet != "")
                    pageEncoding = resp.CharacterSet;
                else if (resp.ContentType != "")
                    pageEncoding = getCharacterSet(resp.ContentType);

                if (pageEncoding == "")
                    pageEncoding = getCharacterSet(buffer);

                if (pageEncoding != "")
                {
                    try
                    {
                        resolvedEncoding = Encoding.GetEncoding(pageEncoding);
                    }
                    catch
                    {
                        //throw new Exception("Invalid encoding: " + pageEncoding);
                    }
                }

                return resolvedEncoding.GetString(buffer);
            }
        }
        private static string getCharacterSet(string s)
        {
            s = s.ToUpperInvariant();
            int start = s.LastIndexOf("CHARSET");
            if (start == -1)
            {
                start = s.LastIndexOf("ENCODING");
                if (start == -1)
                    return "";
            }

            start = s.IndexOf("=", start);
            if (start == -1)
                return "";

            start++;
            s = s.Substring(start).Trim().Trim('"');
            int end = s.Length;

            int i = s.IndexOf(";");
            if (i != -1)
                end = i;
            i = s.IndexOf("\"");
            if (i != -1 && i < end)
                end = i;
            i = s.IndexOf("'");
            if (i != -1 && i < end)
                end = i;
            i = s.IndexOf("/");
            if (i != -1 && i < end)
                end = i;

            return s.Substring(0, end).Trim();
        }
        private static string getCharacterSet(byte[] data)
        {
            string s = Encoding.Default.GetString(data);
            return getCharacterSet(s);
        }
        private static byte[] readStream(Stream s)
        {
            long curLength = 0;
            try
            {
                byte[] buffer = new byte[8096];
                using (MemoryStream ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = s.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            curLength = 0;
                            return ms.ToArray();
                        }
                        ms.Write(buffer, 0, read);
                        curLength = ms.Length;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region ReflectionUtility
        public static object GetAttribute(this ICustomAttributeProvider mi, Type attributeType)
        {
            object[] attribs = mi.GetCustomAttributes(attributeType, true);
            object res = null;
            if (attribs.Length > 0) res = attribs[0]; else res = attributeType.GetConstructor(Type.EmptyTypes).Invoke(null);
            return res;
        }
        public static T GetAttribute<T>(this ICustomAttributeProvider mi)
        {
            return (T)GetAttribute(mi, typeof(T));
        }
        /// <summary>
        /// Returns all get & set properties
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(this Type type)
        {
            ArrayList al = new ArrayList();

            foreach (PropertyInfo pi in type.GetProperties())
                if (pi.GetSetMethod() != null)
                    al.Add(pi);

            return (PropertyInfo[])al.ToArray(typeof(PropertyInfo));
        }
        public static PropertyInfo[] GetProperties(this object obj)
        {
            Type type = obj.GetType();
            return Utility.GetProperties(type);
        }
        public static PropertyInfo GetProperty(this Type objectType, Type propertyType)
        {
            foreach (PropertyInfo pi in objectType.GetProperties())
                if (pi.PropertyType == propertyType)
                    return pi;
            return null;
        }


        private static PropertyInfo getStringIndexer(MemberInfo[] indexers)
        {
            foreach (PropertyInfo pi in indexers)
            {
                ParameterInfo[] indexerParams = pi.GetIndexParameters();
                if (indexerParams.Length == 1 && indexerParams[0].ParameterType.Name.EndsWith("String"))
                    return pi;
            }
            return null;
        }
        private static PropertyInfo getNonStringIndexer(MemberInfo[] indexers)
        {
            foreach (PropertyInfo pi in indexers)
            {
                ParameterInfo[] indexerParams = pi.GetIndexParameters();
                if (indexerParams.Length == 1 && !indexerParams[0].ParameterType.Name.EndsWith("String"))
                    return pi;
            }
            return null;
        }

        public static object GetMemberValue(this object obj, string memberName)
        {
            object member = obj;
            string remaining = null;
            string indexer = null; bool stringIndexer = false;
            // comboBox1.Items[5].Text'i gibi bir ifadeyi
            // memberName=comboBox1, remaining=Items[5].Text olarak inceliyoruz.
            // bir sonraki aşamada
            // membername=Items[5], remaining=Text olacak.
            if (memberName.Contains("."))
            {
                remaining = memberName.Substring(memberName.IndexOf('.') + 1);
                memberName = memberName.Substring(0, memberName.IndexOf('.'));
            }
            // membername indexer ise indexer kısmını ayıralım
            // membername = Items, indexer=5 olacak şekilde
            if (memberName.Contains("["))
            {
                stringIndexer = memberName.Contains("\"");
                indexer = memberName.Substring(memberName.IndexOf('[')).Trim('[', ']', '"');
                memberName = memberName.Substring(0, memberName.IndexOf('['));
            }
            MemberInfo[] mis = member.GetType().GetMember(memberName, MemberTypes.Field | MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (mis.Length != 1)
                throw new Exception(memberName + " isimli member bulunamadı ya da birden çok defa bulundu.");

            MemberInfo mi = mis[0];
            object result;
            if (mi is PropertyInfo)
            {
                PropertyInfo pi = mi as PropertyInfo;
                if (indexer != null)
                {
                    object collectionObject = pi.GetValue(member, null);
                    MemberInfo[] indexers = collectionObject.GetType().GetMember("Item");
                    pi = stringIndexer ? getStringIndexer(indexers) : getNonStringIndexer(indexers);
                    if (pi == null) throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
                    ParameterInfo[] indexerParams = pi.GetIndexParameters();
                    object indexerParam = System.Convert.ChangeType(indexer, indexerParams[0].ParameterType);
                    result = pi.GetValue(collectionObject, new object[] { indexerParam });
                }
                else
                    result = pi.GetValue(member, null);
            }
            else if (mi is FieldInfo)
            {
                result = (mi as FieldInfo).GetValue(member);
            }
            else
                throw new Exception(memberName + " property ya da field olmalıdır.");

            if (remaining != null)
            {
                if (result == null)
                    return null;
                else
                    result = GetMemberValue(result, remaining);
            }

            return result;
        }
        public static T GetMemberValue<T>(this object obj, string memberName)
        {
            return (T)GetMemberValue(obj, memberName);
        }

        public static object SetMemberValue(this object obj, string memberName, object val)
        {
            object member = obj;
            string remaining = null;
            string indexer = null; bool stringIndexer = false;
            // comboBox1.Items[5].Text'i gibi bir ifadeyi
            // memberName=comboBox1, remaining=Items[5].Text olarak inceliyoruz.
            // bir sonraki aşamada
            // membername=Items[5], remaining=Text olacak.
            if (memberName.Contains("."))
            {
                remaining = memberName.Substring(memberName.IndexOf('.') + 1);
                memberName = memberName.Substring(0, memberName.IndexOf('.'));
            }
            // membername indexer ise indexer kısmını ayıralım
            // membername = Items, indexer=5 olacak şekilde
            if (memberName.Contains("["))
            {
                stringIndexer = memberName.Contains("\"");
                indexer = memberName.Substring(memberName.IndexOf('[')).Trim('[', ']', '"');
                memberName = memberName.Substring(0, memberName.IndexOf('['));
            }
            MemberInfo[] mis = member.GetType().GetMember(memberName, MemberTypes.Field | MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (mis.Length != 1)
                throw new Exception(memberName + " isimli member bulunamadı ya da birden çok defa bulundu.");

            MemberInfo mi = mis[0];
            object result;
            if (mi is PropertyInfo)
            {
                PropertyInfo pi = mi as PropertyInfo;
                if (indexer != null)
                {
                    object collectionObject = pi.GetValue(obj, null);
                    result = GetIndexedValue(collectionObject, indexer, stringIndexer);
                }
                else
                {
                    result = pi.GetValue(member, null);
                }
            }
            else if (mi is FieldInfo)
            {
                result = (mi as FieldInfo).GetValue(member);
            }
            else
                throw new Exception(memberName + " property ya da field olmalıdır.");

            if (remaining != null)
            {
                return SetMemberValue(result, remaining, val);
            }
            else
            {
                if (mi is PropertyInfo)
                {
                    PropertyInfo pi = mi as PropertyInfo;
                    if (indexer != null)
                    {
                        object collectionObject = pi.GetValue(member, null);
                        SetIndexedValue(collectionObject, indexer, stringIndexer, val);
                    }
                    else
                    {
                        pi.SetValue(member, Convert.ChangeType(val, pi.PropertyType), null);
                    }
                }
                else if (mi is FieldInfo)
                {
                    FieldInfo fi = mi as FieldInfo;
                    fi.SetValue(member, Convert.ChangeType(val, fi.FieldType));
                }

                return val;
            }
        }

        public static void SetIndexedValue(this object obj, string indexer, bool stringIndexer, object val)
        {
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = stringIndexer ? getStringIndexer(indexers) : getNonStringIndexer(indexers);
            if (pi == null) throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
            ParameterInfo[] indexerParams = pi.GetIndexParameters();
            object indexerParam = System.Convert.ChangeType(indexer, indexerParams[0].ParameterType);
            pi.SetValue(obj, Convert.ChangeType(val, pi.PropertyType), new object[] { indexerParam });
        }

        public static void SetIndexedValue(this object obj, object indexer, object val)
        {
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = null;
            foreach (PropertyInfo pInfo in indexers)
            {
                ParameterInfo[] indexerParams = pInfo.GetIndexParameters();
                if (indexerParams.Length == 1 && (indexerParams[0].ParameterType==typeof(object) || indexerParams[0].ParameterType == indexer.GetType()))
                {
                    pi = pInfo;
                    break;
                }
            }
            if (pi == null)
                throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
            pi.SetValue(obj, Convert.ChangeType(val, pi.PropertyType), new object[] { indexer });
        }

        public static object GetIndexedValue(this object obj, string indexer, bool stringIndexer)
        {
            object result;
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = stringIndexer ? getStringIndexer(indexers) : getNonStringIndexer(indexers);
            if (pi == null) throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
            ParameterInfo[] indexerParams = pi.GetIndexParameters();
            object indexerParam = System.Convert.ChangeType(indexer, indexerParams[0].ParameterType);
            result = pi.GetValue(obj, new object[] { indexerParam });
            return result;
        }

        public static object GetIndexedValue(this object obj, object indexer)
        {
            object result;
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = null;
            foreach (PropertyInfo pInfo in indexers)
            {
                ParameterInfo[] indexerParams = pInfo.GetIndexParameters();
                if (indexerParams.Length == 1 && (indexerParams[0].ParameterType==typeof(object) || indexerParams[0].ParameterType == indexer.GetType()))
                {
                    pi = pInfo;
                    break;
                }
            }
            if (pi == null) 
                throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
            result = pi.GetValue(obj, new object[] { indexer });
            return result;
        }

        public static T ToEnum<T>(this int val)
        {
            foreach (object o in Enum.GetValues(typeof(T)))
                if (Convert.ToInt32(o) == val)
                    return (T)o;
            return (T)Enum.Parse(typeof(T), "");
        }

        public static string ToXML(this object obj)
        {
            if (obj == null)
                return "";

            throw new NotImplementedException();

            //StringBuilder sb = new StringBuilder();
            //DataContractSerializer ser = new DataContractSerializer(obj.GetType());
            //using (XmlWriter sw = XmlWriter.Create(sb))
            //{
            //    ser.WriteObject(sw, obj);
            //    sw.Flush();
            //    return sb.ToString();
            //}
        }

        public static string CompareFields(this object obj1, object obj2, Func<PropertyInfo, bool> predicate)
        {
            if (obj1 == null && obj2 == null)
                return "Objects are null";

            if (obj1 == null)
                return "Object 1 is null, object 2 is NOT null";

            if (obj2 == null)
                return "Object 1 is NOT null, object 2 is null";

            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo pi in obj1.GetProperties())
                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                {
                    object val1 = pi.GetValue(obj1, null) ?? "null";
                    object val2 = obj2.GetMemberValue(pi.Name) ?? "null";
                    if (!val1.Equals(val2))
                        sb.AppendLine(pi.Name + " : " + val1 + " ==>> " + val2);
                }
            return sb.ToString();
        }
        public static string CompareFields(this object obj1, object obj2)
        {
            return CompareFields(obj1, obj2, pi => true);
        }

        #endregion

        #region JSON
        public static string ToHTMLString(this string str)
        {
            if (str == null) return String.Empty;
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r");
        }
        public static string ToJS(this object val)
        {
            if (val == null) return "null";
            switch (val.GetType().Name)
            {
                case "String":
                    return "'" + Utility.ToHTMLString(val.ToString()) + "'";
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                    return val.ToString();
                case "DateTime":
                    if (val.Equals(DateTime.MinValue)) val = new DateTime(1970, 1, 1);
                    DateTime d = (DateTime)val;
                    return String.Format("new Date({0},{1},{2},{3},{4},{5})", d.Year, d.Month-1, d.Day, d.Hour, d.Minute, d.Second);
                    //return String.Format("'{0}-{1}-{2} {3}:{4}'", d.Day, d.Month, d.Year, d.Hour, d.Minute);
                case "Boolean":
                    return val.ToString().ToLower();
                default:
                    return ToJSON(val);
            }
        }
        public static string ToJSON(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            if (obj is IEnumerable)
            {
                sb.Append("[\n");
                foreach (var item in (IEnumerable)obj)
                    sb.Append(item.ToJSON() + ",\n");
                sb.Remove(sb.Length - 2, 2);
                sb.Append("]");
                return sb.ToString();
            }

            if (obj is DataRow)
                return (obj as DataRow).ToJSON();

            if (obj is Hashtable)
                return (obj as Hashtable).ToJSON();

            if (obj is DataTable)
                return (obj as DataTable).ToJSON();

            sb.Append("{\n");
            ArrayList res = new ArrayList();
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                res.Add("\t" + String.Format("{0}:{1}", pi.Name, Utility.ToJS(pi.GetValue(obj, null))));
            }
            foreach (MethodInfo mi in obj.GetType().GetMethods())
            {
                if (!mi.IsPublic) continue;
                if (!mi.GetAttribute<VisibleToClientAttribute>().Visible) continue;

                string strPrm = "", strPrmJson = "";
                foreach (var prm in mi.GetParameters())
                {
                    strPrm += ", " + prm.Name;
                    strPrmJson += prm.Name + ":" + prm.Name + ", ";
                }
                if (strPrmJson.Length >= 2)
                    strPrmJson = strPrmJson.Substring(0, strPrmJson.Length - 2);

                res.Add("\t" + String.Format("{0}: function(callback{1}) {{runWebPartMethod(this, '{0}', {{{2}}}, callback);}}", mi.Name, strPrm, strPrmJson));
            }
            res.Sort();
            sb.Append(String.Join(",\n", (string[])res.ToArray(typeof(string))));

            sb.Append("}");

            return sb.ToString();
        }
        public static string ToJSON(this DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");
            sb.AppendFormat("tableName:{0},", Utility.ToJS(dt.TableName));
            sb.Append("rows:[");

            foreach (DataRow dr in dt.Rows)
                sb.Append(Utility.ToJSON(dr) + ",");
            sb.Remove(sb.Length - 1, 1);

            sb.Append("]");
            sb.Append("}");

            return sb.ToString();
        }
        public static string ToJSON(this DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");

            foreach (DataColumn dc in dr.Table.Columns)
                sb.AppendFormat("{0}:{1},", Utility.ToJS(dc.ColumnName), Utility.ToJS(dr[dc]));
            sb.Remove(sb.Length - 1, 1);

            sb.Append("}");
            return sb.ToString();
        }
        public static string ToJSON(this Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");

            foreach (object key in ht.Keys)
                sb.AppendFormat("{0}:{1},", Utility.ToJS(key.ToString()), Utility.ToJS(ht[key]));
            sb.Remove(sb.Length - 1, 1);

            sb.Append("}");
            return sb.ToString();
        }
        #endregion

        public static object Clone(this object obj)
        {
            return Clone(obj, null);
        }
        public static object Clone(this object obj, Func<PropertyInfo, bool> predicate)
        {
            if (obj == null)
                return null;

            object res = Activator.CreateInstance(obj.GetType());

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                if (predicate != null && !predicate(pi))
                    continue;

                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                    pi.SetValue(res, pi.GetValue(obj, null), null);
                else 
                    pi.SetValue(res, Clone(pi.GetValue(obj, null)), null);
            }

            return res;
        }

        public static bool IsNumeric(this object o)
        {
            if (o is IConvertible)
            {
                TypeCode tc = ((IConvertible)o).GetTypeCode();
                if (TypeCode.Char <= tc && tc <= TypeCode.Decimal)
                    return true;
            }
            return false;
        }

        public static void CopyDirectory(string src, string dst)
        {
            String[] files;

            if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dst))
                Directory.CreateDirectory(dst);

            files = Directory.GetFileSystemEntries(src);

            foreach (string element in files)
            {

                if (Directory.Exists(element))
                {
                    // Sub directories
                    if (element + Path.DirectorySeparatorChar != dst)
                    {
                        CopyDirectory(element, dst + Path.GetFileName(element));
                    }
                }
                else
                {
                    // Files in directory
                    File.Copy(element, dst + Path.GetFileName(element), true);
                }
            }
        }
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            int i = 0;
            foreach (TSource local in source)
            {
                if (predicate(local))
                    return i;
                i++;
            }
            return -1;
        }
    }

    public class Pair<T>
    {
        public T First { get; set; }
        public T Second { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return string.Format("First: {0} Second: {1} Tag: {2}", First, Second, Tag);
        }
    }

    public class VisibleToClientAttribute : Attribute
    {
        public VisibleToClientAttribute()
        {
        }
        public VisibleToClientAttribute(bool visible)
        {
            Visible = visible;
        }

        public bool Visible { get; set; }
    }

    public enum LinePart
    {
        Near,
        Center,
        Far
    }
}

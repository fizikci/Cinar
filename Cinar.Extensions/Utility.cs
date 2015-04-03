using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace System
{
    public static class Utility
    {
        #region Drawing
        public static Image ScaleImage(this Image orjImg, int width, int height)
        {
            if (height == 0) height = Convert.ToInt32(width * (double)orjImg.Height / (double)orjImg.Width);
            if (width == 0) width = Convert.ToInt32(height * (double)orjImg.Width / (double)orjImg.Height);

            Bitmap imgDest = new Bitmap(width, height);
            imgDest.SetResolution(72, 72);
            using (Graphics grDest = Graphics.FromImage(imgDest))
            {
                grDest.SmoothingMode = SmoothingMode.AntiAlias;
                grDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grDest.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grDest.DrawImage(orjImg, 0f, 0f, (float)width, (float)height);
            }
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
        public static Image CropImage(this Image orjImg, int x, int y, int width, int height, bool whiteBackground = true)
        {
            Bitmap bmPhoto = new Bitmap(width, height, whiteBackground ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            if(whiteBackground)
                grPhoto.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            grPhoto.DrawImage(orjImg, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            grPhoto.Dispose();
            return bmPhoto;
        }
        public static void SavePng(this Image img, string path)
        {
            img.Save(path, ImageFormat.Png);
        }
        public static void SaveGif(this Image img, string path)
        {
            img.Save(path, ImageFormat.Gif);
        }
        public static void SaveImage(this Image img, string path, long quality)
        {
            string lowerPath = path.ToLowerInvariant();

            if (lowerPath.EndsWith(".png"))
                SavePng(img, path);
            else if (lowerPath.EndsWith(".gif"))
                SaveGif(img, path);
            else
                SaveJpeg(img, path, quality);

        }
        public static bool IsGifAnimated(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            byte[] netscape = bytes.Skip(0x310).Take(11).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var item in netscape)
            {
                sb.Append((char)item);
            }

            return sb.ToString() == "NETSCAPE2.0";
        }

        public static void DrawImage(this Image orjImg, Image imgToDraw, Alignment align) 
        {
            using (Graphics g = Graphics.FromImage(orjImg)) {
                int W = orjImg.Width, H = orjImg.Height, w = imgToDraw.Width, h = imgToDraw.Height;

                switch (align)
                {
                    case Alignment.TopCenter:
                        g.DrawImage(imgToDraw, new Rectangle((W - w) / 2, 0, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.TopRight:
                        g.DrawImage(imgToDraw, new Rectangle(W - w, 0, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.MiddleRight:
                        g.DrawImage(imgToDraw, new Rectangle(W - w, (H - h) / 2, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.BottomRight:
                        g.DrawImage(imgToDraw, new Rectangle(W - w, H - h, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.BottomCenter:
                        g.DrawImage(imgToDraw, new Rectangle((W - w) / 2, H - h, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.BottomLeft:
                        g.DrawImage(imgToDraw, new Rectangle(0, H - h, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.MiddleLeft:
                        g.DrawImage(imgToDraw, new Rectangle(0, (H - h) / 2, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.TopLeft:
                        g.DrawImage(imgToDraw, new Rectangle(0, 0, imgToDraw.Width, imgToDraw.Height));
                        break;
                    case Alignment.MiddleCenter:
                        g.DrawImage(imgToDraw, new Rectangle((W - w) / 2, (H - h) / 2, imgToDraw.Width, imgToDraw.Height));
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ConcatIconsIntoOnePngFile(string iconsFolder, string outFolder, int iconWidth = 16)
        {
            List<string> files = new List<string>();

            foreach (string file in Directory.GetFiles(iconsFolder).OrderBy(f => Path.GetFileName(f)))
            {
                using (Image img = Image.FromFile(file))
                {
                    if (img.Width == iconWidth && img.Height == iconWidth)
                        files.Add(file);
                }
            }

            using (Bitmap bmp = new Bitmap(iconWidth, files.Count * iconWidth))
            {
                string cssFileContent = ".cbtn {background-image:url('all_icons.png'); background-repeat:no-repeat; height:" + iconWidth + "px; width:" + iconWidth + "px; display:inline-block;}\r\n";
                string sampleHtmlFile = "<html><head><link rel=\"stylesheet\" type=\"text/css\" href=\"all_icons.css\" /></head><body>\r\n";

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string file = files[i];
                        using (Image img = Image.FromFile(file))
                            g.DrawImage(img, 0, i * iconWidth);

                        cssFileContent += string.Format(".c{0} {{background-position:0px {1}px;}}\r\n", Path.GetFileNameWithoutExtension(file), i * -iconWidth);
                        sampleHtmlFile += string.Format("<span class=\"cbtn c{0}\"></span> {0}\r\n", Path.GetFileNameWithoutExtension(file));
                    }
                }

                sampleHtmlFile += "</body></html>";

                File.WriteAllText(outFolder + @"\all_icons.css", cssFileContent, Encoding.UTF8);
                File.WriteAllText(outFolder + @"\all_icons.html", sampleHtmlFile, Encoding.UTF8);

                bmp.MakeTransparent();
                bmp.Save(outFolder + @"\all_icons.png", ImageFormat.Png);
            }

        }

        public static long CalculateDirectorySize(string path)
        {
            long folderSize = 0;
            FileInfo[] files = new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in files) folderSize += file.Length;
            return folderSize;
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
            if (str == null) return null;

            char[] array = new char[str.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < str.Length; i++)
            {
                char let = str[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
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
        public static string[] SplitWithTrim(this string str, string seperator)
        {
            string[] res = str.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < res.Length; i++)
                res[i] = res[i].Trim();
            return res;
        }
        public static string[] SplitWithTrim(this string str, string seperator, params char[] trimChars)
        {
            string[] res = str.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < res.Length; i++)
                res[i] = res[i].Trim(trimChars);
            return res;
        }
        public static string SplitAndGetLast(this string str, char seperator)
        {
            string[] res = str.SplitWithTrim(seperator);
            return res[res.Length - 1];
        }
        public static string MakeFileName(this string str)
        {
            if (str == null)
                return "_";
            str = str.RemoveDiacritics();
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            invalidChars += " \\&é\\!\\'\\^\\+\\%\\(\\)\\=\\-\\#\\$\\{\\]\\}\\¨\\~\\´\\`\\,\\;";
            string invalidReStr = string.Format(@"[{0}]", invalidChars);
            string res = Regex.Replace(str, invalidReStr, "_");
            res = res.Replace("ı","i");
            while (res.Contains("__")) res = res.Replace("__", "_");
            return res;
        }
        public static string RemoveDiacritics(this string str)
        {
            return string.Concat(str.Normalize(NormalizationForm.FormD).Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)).Normalize(NormalizationForm.FormC);
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
                    sb.AppendFormat("<td>{0}</td>", val);
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
        public static string ToHtmlTable(this object obj) {
            if (obj == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<table>\r\n");
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                object val = pi.GetValue(obj, null);
                string valStr = (val ?? "").ToString();
                sb.AppendFormat("<tr><td><b>{0} : </b></td><td>{1}</td></tr>\r\n", pi.Name, valStr);
            }
            sb.Append("</table>\r\n");
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
            Regex re = new Regex(@"\#\{(?<exp>[A-Za-z0-9._\[\]""]+)\}");
            foreach (Match m in re.Matches(template))
            {
                string exp = m.Groups["exp"].Value;
                if (!expressions.ContainsKey(exp))
                    try
                    {
                        expressions.Add(exp, obj.GetMemberValue(exp));
                    }catch
                    {
                        expressions.Add(exp, "");
                    }
            }

            foreach (var item in expressions)
                template = template.Replace("#{" + item.Key + "}", item.Value == null ? "" : item.Value.ToString());

            return template;
        }
        public static string CapitalizeInvariant(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToUpperInvariant(str[0]).ToString();

            str = str.Trim();
            if (!str.Contains(" "))
                return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();

            var parts = str.Split(' ');
            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].CapitalizeInvariant();

            return parts.StringJoin(" ");
        }
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();

            str = str.Trim();
            if (!str.Contains(" "))
                return char.ToUpper(str[0]) + str.Substring(1).ToLower();

            var parts = str.Split(' ');
            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].Capitalize();

            return parts.StringJoin(" ");
        }
        public static string CapitalizeFirstLetter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();

            str = str.Trim();
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
        public static string CapitalizeFirstLetterInvariant(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToUpperInvariant(str[0]).ToString();

            str = str.Trim();
            return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
        }
        public static string ToLowerFirstLetter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToLower(str[0]).ToString();

            str = str.Trim();
            return char.ToLower(str[0]) + str.Substring(1);
        }
        public static string ToLowerFirstLetterInvariant(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length == 1)
                return char.ToLowerInvariant(str[0]).ToString();

            str = str.Trim();
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        public static string HumanReadable(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (str.EndsWith("Id", StringComparison.InvariantCulture)) str = str.Substring(0, str.Length - 2);
            if (str.EndsWith("_id", StringComparison.InvariantCulture)) str = str.Substring(0, str.Length - 3);

            string res = str[0].ToString();

            for (int i = 1; i < str.Length; i++ )
            {
                char lastChar = str[i-1];
                char c = str[i];

                if (c == '_')
                {
                    res += " ";
                    continue;
                }

                if ((Char.IsLower(lastChar) && Char.IsUpper(c)) || (!Char.IsDigit(lastChar) && Char.IsDigit(c)) || (Char.IsDigit(lastChar) && !Char.IsDigit(c)))
                    res += " ";

                res += c;
            }

            return res;
        }

        public static bool IsEmail(this string str)
        {
            try
            {
                MailAddress m = new MailAddress(str);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static List<string> PascalCaseWords(this string str)
        {
            var res = new List<string>();
            if(string.IsNullOrWhiteSpace(str))
                return res;

            var word = str[0].ToString();
            for (int i = 1; i < str.Length; i++)
            {
                if (Char.IsLower(str[i]) || Char.IsDigit(str[i]))
                {
                    word += str[i];
                    continue;
                }
                res.Add(word);
                word = str[i].ToString();
            }
            res.Add(word);

            return res;
        }

        public static string PascalCaseRemoveFirstWord(this string str)
        {
            var res = str.PascalCaseWords();
            if (res.Count <= 1)
                return "";

            return res.Skip(1).StringJoin();
        }

        /// <summary>
        /// This method recursively searchs for file names and sticks them together with a seperator.
        /// It also has a substring from the start parameter for creating relative paths, 
        /// which is necessary for web.
        /// </summary>
        /// <param name="dir">Root directory, will be searched for full paths.</param>
        /// <param name="relativeDirectoryFileName">Usage: Give the "name" of the root directory here for relative paths.</param>
        /// <param name="seperator">Join value, defaults to "\r\n"</param>
        /// <returns> All file paths of sub directories joined together with a seperator. </returns>
        public static string GetFileNames(this string dir, string relativeDirectoryFileName = "", string seperator = "\r\n")
        {
            string output = Directory.GetFiles(dir).Select(x => x.Substring(x.IndexOf(relativeDirectoryFileName))).StringJoin(seperator);
            foreach (string d in Directory.GetDirectories(dir))
            {
                string result = GetFileNames(d, relativeDirectoryFileName);
                if (string.IsNullOrWhiteSpace(output)) output = result;
                else if (!string.IsNullOrWhiteSpace(result))
                    output = new string[] { output, result}.StringJoin(seperator);
            }
            return output;
        }

        public static string StringJoin<T>(this IEnumerable<T> source, string seperator)
        {
            if (source == null)
                return "";
            return string.Join(seperator, source.Select(t => t == null ? "" : t.ToString()).ToArray());
        }
        public static string StringJoin<T>(this IEnumerable<T> source)
        {
            return source.StringJoin("");
        }



        public static string ConvertEncoding(this string str, string srcEncodingName, string destEncodingName)
        {
            return str.ConvertEncoding(Encoding.GetEncoding(srcEncodingName), Encoding.GetEncoding(destEncodingName));
        }
        public static string ConvertEncoding(this string str, Encoding srcEncoding, Encoding destEncoding)
        {
            return destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(str)));
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
            s = s.ToUpperInvariant();

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

        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        public static bool ContainedBy(this string str, List<string> list)
        {
            return list.Any(s => s == str);
        }

        #region download page with proper encoding
        public static string DownloadPage(this string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                return wc.DownloadString(url);
            }
        }
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
            //req.Headers["Cookie"] = "ASP.NET_SessionId=2n5drm45iqroub550diydu55";
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

        public static string ToAgoString(this DateTime d)
        {
            TimeSpan s = DateTime.Now.Subtract(d);

            int secDiff = (int)s.TotalSeconds;
            int dayDiff = (int)s.TotalDays;
            int yearDiff = (int)(s.TotalDays / 365d);

            if (dayDiff == 0)
            {
                if (secDiff < 60)
                    return "just now";
                if (secDiff < 120)
                    return "1 minute ago";
                if (secDiff < 3600)
                    return string.Format("{0} minutes ago", Math.Floor((double)secDiff / 60));
                if (secDiff < 7200)
                    return "1 hour ago";
                if (secDiff < 86400)
                    return string.Format("{0} hours ago", Math.Floor((double)secDiff / 3600));
            }

            if (dayDiff == 1)
                return "yesterday";
            if (dayDiff < 7)
                return string.Format("{0} days ago", dayDiff);
            if (dayDiff < 31)
                return string.Format("{0} weeks ago", Math.Ceiling((double)dayDiff / 7));
            if (dayDiff < 365)
                return string.Format("{0} months ago", Math.Ceiling((double)dayDiff / 30));

            if (yearDiff == 1)
                return "last year";

            return string.Format("{0} years ago", yearDiff);
        }
        public static string ToStringSpan(this DateTime d1, DateTime d2, string monthFormat = "MMMM")
        {
            var min = d1 < d2 ? d1 : d2;
            var max = d1 < d2 ? d2 : d1;

            if (min.Year == max.Year && min.Month == max.Month && min.Day == max.Day)
                return min.ToString("dd " + monthFormat + " yyyy");
            else if (min.Year == max.Year && min.Month == max.Month)
                return min.ToString("dd") + " - " + max.ToString("dd") + " " + min.ToString(monthFormat) + (DateTime.Now.Year != min.Year ? min.ToString("yyyy") : "");
            else if (min.Year == max.Year && min.Month != max.Month)
                return min.ToString("dd ") + min.ToString(monthFormat) + " - " + max.ToString("dd ") + max.ToString(monthFormat) + (DateTime.Now.Year != min.Year ? min.ToString("yyyy") : "");
            else
                return min.ToString("dd " + monthFormat + " yyyy") + " - " + max.ToString("dd " + monthFormat + " yyyy");
        }
        public static string HumanReadableDay(this DateTime d)
        {
            if (DateTime.Now.Date == d.Date)
                return "Today";

            if (DateTime.Now.AddDays(-1).Date == d.Date)
                return "Yesterday";

            if(DateTime.Now.Year == d.Year)
                return d.ToString("dd MMMM");

            return d.ToString("dd MMM yyyy");
        }

        public static string ValidateTCKimlikNo(string tcKimlik)
        {
            if (string.IsNullOrWhiteSpace(tcKimlik))
                return "TC Kimlik numarası boş bırakılamaz.";
            if (tcKimlik.Length != 11)
                return "TC Kimlik numarası 11 haneli olmalıdır.";
            if (!Regex.IsMatch(tcKimlik, "^[1-9][0-9]{10}$"))
                return "TC Kimlik numarası geçersiz."; // bütün karakterler sayı olmalı, ilk hane 0 olamaz
            int tekler = 0, ciftler = 0;
            for (int i = 0; i < 10; i += 2)
            {
                tekler += int.Parse(tcKimlik[i].ToString());
                if (i < 8)
                    ciftler += int.Parse(tcKimlik[i + 1].ToString());
            }
            if ((tekler * 7 - ciftler) % 10 != int.Parse(tcKimlik[9].ToString()))
                return "TC Kimlik numarası geçersiz."; // 1. 3. 5. 7. ve 9. hanelerin toplamının 7 katından, 2. 4. 6. ve 8. hanelerin toplamı çıkartıldığında, elde edilen sonucun Mod10'u bize 10. haneyi verir.
            if ((tekler + ciftler + int.Parse(tcKimlik[9].ToString())) % 10 != int.Parse(tcKimlik[10].ToString()))
                return "TC Kimlik numarası geçersiz."; // ilk 10 hanenin toplamının Mod10'u bize 11. haneyi verir.

            return ""; // valid
        }


        public static bool IsUtf8(string path) {
            return new Cinar.Extensions.Utf8Checker().Check(path);
        }

        private const string digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string CreatePassword(int length)
        {
            if (length <= 0)
                length = 6;

            var pass = "";
            var r = new Random();
            for (int i = 0; i < length; i++)
                pass += digits[r.Next(digits.Length)];
            return pass;
        }

        #endregion

        #region ReflectionUtility
        public static object GetAttribute(this ICustomAttributeProvider mi, Type attributeType)
        {
            object[] attribs = mi.GetCustomAttributes(attributeType, true);
            object res = null;
            if (attribs.Length > 0) res = attribs[0]; 
            else if(attributeType.GetConstructor(Type.EmptyTypes)!=null)
                res = attributeType.GetConstructor(Type.EmptyTypes).Invoke(null);
            return res;
        }
        public static T GetAttribute<T>(this ICustomAttributeProvider mi)
        {
            return (T)GetAttribute(mi, typeof(T));
        }
        public static List<T> GetAttributes<T>(this ICustomAttributeProvider mi)
        {
            object[] attribs = mi.GetCustomAttributes(typeof(T), true);
            return attribs.Select(t => (T)t).ToList();
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
                if (memberName == "") memberName = "Item";
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
                    object collectionObject = obj;
                    if (memberName != "Item")
                    {
                        collectionObject = pi.GetValue(member, null);
                        MemberInfo[] indexers = collectionObject.GetType().GetMember("Item");
                        pi = stringIndexer ? getStringIndexer(indexers) : getNonStringIndexer(indexers);
                        if (pi == null)
                            throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
                    }
                    ParameterInfo[] indexerParams = pi.GetIndexParameters();
                    object indexerParam = indexer.ChangeType(indexerParams[0].ParameterType);
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
                return null;

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
                        SetIndexedValue(collectionObject, indexer, stringIndexer, val.ChangeType(pi.PropertyType));
                    }
                    else
                    {
                        pi.SetValue(member, val.ChangeType(pi.PropertyType), null);
                    }
                }
                else if (mi is FieldInfo)
                {
                    FieldInfo fi = mi as FieldInfo;
                    fi.SetValue(member, val.ChangeType(fi.FieldType));
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
            object indexerParam = indexer.ChangeType(indexerParams[0].ParameterType);
            pi.SetValue(obj, val.ChangeType(pi.PropertyType), new object[] { indexerParam });
        }

        public static void SetIndexedValue(this object obj, object indexer, object val)
        {
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = null;

            if (indexers.Length == 0 && obj is Array)
            {
                ((Array)obj).SetValue(val, (int)indexer);
                return;
            }
            
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
            pi.SetValue(obj, val.ChangeType(pi.PropertyType), new object[] { indexer });
        }

        public static object GetIndexedValue(this object obj, string indexer, bool stringIndexer)
        {
            object result;
            MemberInfo[] indexers = obj.GetType().GetMember("Item");
            PropertyInfo pi = stringIndexer ? getStringIndexer(indexers) : getNonStringIndexer(indexers);
            if (pi == null) throw new Exception("Belirtilen indexer bulunamadı. Not: Şu an için sadece tek parametreli indexerlar destekleniyor.");
            ParameterInfo[] indexerParams = pi.GetIndexParameters();
            object indexerParam = indexer.ChangeType(indexerParams[0].ParameterType);
            result = pi.GetValue(obj, new object[] { indexerParam });
            return result;
        }

        public static object GetIndexedValue(this object obj, object indexer)
        {
            object result;
            MemberInfo[] indexers = obj.GetType().GetMember("Item");

            if (indexers.Length == 0 && obj is Array)
                return ((Array)obj).GetValue((int)indexer);

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

        public static T ChangeType<T>(this object value, CultureInfo culture = null)
        {
            return (T)ChangeType(value, typeof(T), culture);
        }

        public static object ChangeType(this object value, Type conversion, CultureInfo culture = null)
        {
            if (culture == null) culture = CultureInfo.CurrentCulture;

            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value == DBNull.Value)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            if (value == DBNull.Value)
                return t.GetDefault();

            if (t.IsEnum) {
                if (value is string)
                    return Enum.Parse(t, value.ToString());
                else if (value.IsNumeric())
                    return Enum.ToObject(t, (int)value);
            }

            return Convert.ChangeType(value, t);
        }

        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static string SerializeToString(this object entity, Predicate<PropertyInfo> serializeThisProperty)
        {
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;
                if (!serializeThisProperty(pi)) continue;

                object val = pi.GetValue(entity, null);
                if (val != null)
                {
                    string valStr = val.ToString();
                    sb.AppendFormat("{0},{1},{2}", pi.Name, valStr.Length, valStr);
                }
            }
            return sb.ToString();
        }
        public static string SerializeToString(this object entity)
        {
            return entity.SerializeToString(pi => true);
        }
        public static void DeserializeFromString(this object entity, string data)
        {
            try
            {
                while (data.Length > 0)
                {
                    string propName = data.Substring(0, data.IndexOf(','));
                    data = data.Substring(propName.Length + 1);
                    string valLengthStr = data.Substring(0, data.IndexOf(','));
                    data = data.Substring(valLengthStr.Length + 1);
                    int length = Int32.Parse(valLengthStr);
                    string valStr = data.Substring(0, length);
                    data = data.Substring(length);

                    PropertyInfo pi = entity.GetType().GetProperty(propName);
                    object val = null;
                    if (pi.PropertyType.IsEnum)
                        val = Enum.Parse(pi.PropertyType, valStr);
                    else
                        val = valStr.ChangeType(pi.PropertyType);

                    pi.SetValue(entity, val, null);
                }
            }
            catch
            {
                throw new Exception("Error while deserializing the module. This may be because module changed or database charset problem.");
            }
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
                if ((pi.PropertyType.IsValueType || pi.PropertyType == typeof(string)) && pi.Name != "UpdateDate" && pi.Name != "UpdateUserId")
                {
                    object val1 = pi.GetValue(obj1, null) ?? "null";
                    object val2 = obj2.GetMemberValue(pi.Name) ?? "null";
                    
                    if (val1.Equals("")) val1 = "null";
                    if (val2.Equals("")) val2 = "null";

                    if (!val1.Equals(val2))
                        sb.AppendLine(pi.Name + " : " + val1 + " ==>> " + val2);
                }
            return sb.ToString();
        }
        public static string CompareFields(this object obj1, object obj2)
        {
            return CompareFields(obj1, obj2, pi => true);
        }

        public static bool CopyPropertiesWithSameName(this object obj1, object obj2)
        {
            if (obj1 == null || obj2 == null)
                return false;

            foreach (PropertyInfo pi1 in obj1.GetProperties())
            {
                PropertyInfo pi2 = obj2.GetType().GetProperty(pi1.Name);
                if (pi2 == null || pi2.GetSetMethod() == null || pi2.PropertyType != pi1.PropertyType)
                    continue;

                try
                {
                    pi2.SetValue(obj2, pi1.GetValue(obj1, null), null);
                }
                catch { }
            }

            return true;
        }

        public static string GetFriendlyName(this Type type)
        {
            using (var provider = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                string typeName = provider.GetTypeOutput(typeRef);
                if (typeName.Contains("."))
                {
                    if (typeName.Contains("<"))
                    {
                        string[] parts = typeName.Split('<');
                        string first = parts[0].SplitAndGetLast('.');
                        string rest = parts[1].Trim('>').SplitWithTrim(',').Select(s => s.SplitAndGetLast('.')).StringJoin(", ");
                        return first + "<" + rest + ">";
                    }

                    return typeName.SplitAndGetLast('.');
                }
                return typeName;
            }
        }

        public static List<T> ToEntityList<T>(this DataTable dt) where T : new()
        {
            if (dt == null) return null;

            var res = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                foreach (DataColumn dc in dt.Columns)
                    t.SetMemberValue(dc.ColumnName, dr[dc]);
                res.Add(t);
            }
            return res;
        }

        #endregion

        #region JSON
        public static string ToHTMLString(this string str)
        {
            if (str == null) return String.Empty;
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        public static string RemoveInvisibleCharacters(this string str)
        {
            if (str == null)
                return str;

            StringBuilder buffer = new StringBuilder(str.Length);

            foreach (char c in str)
                if (isInvisible(c.ToString()))
                    buffer.Append(c);

            return buffer.ToString();
        }
        private static bool isInvisible(string character)
        {
            return
            (
                 character == " " ||
                 character == "\t" ||
                 character == "\n" ||
                 character == "\r" ||
                 character.Trim() != ""
            );
        }

        public static string ToJS(this object val)
        {
            if (val == null) return "null";
            if (val.GetType().IsEnum) return "\"" + Enum.GetName(val.GetType(), val) + "\"";
            if (val == DBNull.Value) return "null";

            switch (val.GetType().Name)
            {
                case "Char":
                case "String":
                    return "\"" + Utility.ToHTMLString(val.ToString()) + "\"";
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Single":
                case "Double":
                case "Byte":
                case "SByte":
                case "UInt32":
                case "UInt64":
                case "UInt16":
                    return val.ToString().Replace(",", ".");
                case "DateTime":
                    if (val.Equals(DateTime.MinValue)) val = new DateTime(1970, 1, 1);
                    DateTime d = (DateTime)val;
                    return '"' + d.ToString("yyyy-MM-dd\"T\"HH:mm:ss") + '"';
                    //return String.Format("new Date({0},{1},{2},{3},{4},{5})", d.Year, d.Month-1, d.Day, d.Hour, d.Minute, d.Second);
                    //return String.Format("'{0}-{1}-{2} {3}:{4}'", d.Day, d.Month, d.Year, d.Hour, d.Minute);
                case "Boolean":
                    return val.ToString().ToLower();
                default:
                    return ToJSON(val);
            }
        }
        public static string ToJSON(this object obj)
        {
            if (obj == null)
                return "null";

            StringBuilder sb = new StringBuilder();
            if (obj is IEnumerable && obj.GetType()!=typeof(string))
            {
                sb.Append("[\n");
                foreach (var item in (IEnumerable)obj)
                    sb.Append(item.ToJS() + ",\n");
                if (sb.ToString().EndsWith(",\n"))
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
                if (pi.GetSetMethod() == null) continue; //SetMethod niye?

                res.Add("\t" + String.Format("\"{0}\": {1}", pi.Name, Utility.ToJS(pi.GetValue(obj, null))));
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

            sb.Append("\n}");

            return sb.ToString();
        }
        public static string ToJSON(this DataTable dt)
        {
            if (dt == null)
                return "null";

            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");
            sb.AppendFormat("tableName:{0},", Utility.ToJS(dt.TableName));
            sb.Append("rows:[");

            foreach (DataRow dr in dt.Rows)
                sb.Append(Utility.ToJSON(dr) + ",");
            //sb.Remove(sb.Length - 1, 1);

            sb.Append("]");
            sb.Append("}");

            return sb.ToString();
        }
        public static string ToJSON(this DataRow dr)
        {
            if (dr == null)
                return "null"; 
            
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");

            foreach (DataColumn dc in dr.Table.Columns)
                sb.AppendFormat("{0}:{1},", Utility.ToJS(dc.ColumnName), Utility.ToJS(dr[dc]));
            sb.Remove(sb.Length - 1, 1);

            sb.Append("}");
            return sb.ToString();
        }

        public static string ToJSON(this IDictionary ht)
        {
            if (ht == null)
                return "null";

            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");

            foreach (object key in ht.Keys)
                sb.AppendFormat("{0}:{1},", Utility.ToJS(key.ToString()), Utility.ToJS(ht[key]));
            sb.Remove(sb.Length - 1, 1);

            sb.Append("}");
            return sb.ToString();
        }

        #endregion

        public static string ValidateCreditCardNumber(CardType cardType, string cardNumber)
        {
            byte[] number = new byte[16]; // number to validate

            // Remove non-digits
            int len = 0;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (char.IsDigit(cardNumber, i))
                {
                    if (len == 16) return "Kart numarası çok fazla haneden oluşuyor."; // number has too many digits
                    number[len++] = byte.Parse(cardNumber[i].ToString());
                }
            }

            // Validate based on card type, first if tests length, second tests prefix
            switch (cardType)
            {
                case CardType.MASTERCARD:
                    if (len != 16)
                        return "MasterCard için uyumsuz kart numarası";
                    if (number[0] != 5 || number[1] == 0 || number[1] > 5)
                        return "MasterCard için uyumsuz kart numarası";
                    break;

                case CardType.VISA:
                    if (len != 16 && len != 13)
                        return "VISA için uyumsuz kart numarası";
                    if (number[0] != 4)
                        return "VISA için uyumsuz kart numarası";
                    break;
            }

            // Use Luhn Algorithm to validate
            int sum = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                if (i % 2 == len % 2)
                {
                    int n = number[i] * 2;
                    sum += (n / 10) + (n % 10);
                }
                else
                    sum += number[i];
            }
            return (sum % 10 == 0) ? "" : "Geçersiz kart numarası";
        }

        public static object Deserialize(this string xml, Type type)
        {
            if (xml == null)
                return null;

            object obj = null;
            XmlSerializer ser = new XmlSerializer(type);
            using (StringReader stringReader = new StringReader(xml))
            {
                using (XmlTextReader xmlReader = new XmlTextReader(stringReader))
                {
                    obj = ser.Deserialize(xmlReader);
                }
            }
            return obj;
        }
        public static T Deserialize<T>(this string xml)
        {
            return (T)Deserialize(xml, typeof(T));
        }

        public static string Serialize(this object obj)
        {
            if (obj == null) return null;

            var serializer = new XmlSerializer(obj.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }

            return sb.ToString();
        }

        public static object Clone(this object obj)
        {
            return Clone(obj, pi => pi.GetIndexParameters().Length == 0);
        }
        public static object Clone(this object obj, Func<PropertyInfo, bool> predicate)
        {
            if (obj == null) return null;
            if (obj.GetType().IsValueType || obj.GetType() == typeof(string))
                return obj;

            object res = null;
            try
            {
                res = Activator.CreateInstance(obj.GetType());
            }
            catch { return null; }

            if (obj is IEnumerable)
                foreach (var elm in obj as IEnumerable)
                    (res as IList).Add(elm.Clone());

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                if (predicate != null && !predicate(pi))
                    continue;

                if (pi.GetSetMethod() == null)
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

        public static bool CanConvertToInteger(this string str)
        {
            int number = 0;
            return int.TryParse(str, out number);
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

        public static long GetDirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(fi => fi.Length) +
                   dir.GetDirectories().Sum(di => GetDirSize(di));
        }
        public static long GetDirSize(string dirPath)
        {
            return GetDirSize(new DirectoryInfo(dirPath));
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

        public static T[] SafeCastToArray<T>(this IEnumerable source)
        {
            if (source == null)
                return null;
            return source.Cast<T>().ToArray();
        }


        public static string ToStringBetter(this Exception ex)
        {
            if (ex == null)
                return "";

            return ex.InnerException == null ? ex.Message : ex.InnerException.ToStringBetter();
        }


        public static bool DoResumableDownload(HttpContext httpContext, string filePath, long speed)
        {
            // Many changes: mostly declare variables near use
            // Extracted duplicate references to HttpContext.Response and .Request
            // also duplicate reference to .HttpMethod

            // Removed try/catch blocks which hid any problems
            var response = httpContext.Response;
            var request = httpContext.Request;
            var method = request.HttpMethod.ToUpper();
            if (method != "GET" &&
                method != "HEAD")
            {
                response.StatusCode = 501;
                return false;
            }

            if (!File.Exists(filePath))
            {
                response.StatusCode = 404;
                return false;
            }

            // Stream implements IDisposable so should be in a using block
            using (var myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var fileLength = myFile.Length;
                if (fileLength > Int32.MaxValue)
                {
                    response.StatusCode = 413;
                    return false;
                }

                var lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
                var fileName = Path.GetFileName(filePath);
                var fileNameUrlEncoded = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                var eTag = fileNameUrlEncoded + lastUpdateTiemStr;

                var ifRange = request.Headers["If-Range"];
                if (ifRange != null && ifRange.Replace("\"", "") != eTag)
                {
                    response.StatusCode = 412;
                    return false;
                }

                long startBytes = 0;

                // Just guessing, but I bet you want startBytes calculated before
                // using to calculate content-length
                var rangeHeader = request.Headers["Range"];
                if (rangeHeader != null)
                {
                    response.StatusCode = 206;
                    var range = rangeHeader.Split(new[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                    if (startBytes < 0 || startBytes >= fileLength)
                    {
                        // TODO: Find correct status code
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.StatusDescription = string.Format("Invalid start of range: {0}", startBytes);
                        return false;
                    }
                }

                response.Clear();
                response.Buffer = false;
                response.AddHeader("Content-MD5", GetMD5Hash(filePath));
                response.AddHeader("Accept-Ranges", "bytes");
                response.AppendHeader("ETag", string.Format("\"{0}\"", eTag));
                response.AppendHeader("Last-Modified", lastUpdateTiemStr);
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment;filename=" + fileNameUrlEncoded.Replace("+", "%20"));
                var remaining = fileLength - startBytes;
                response.AddHeader("Content-Length", remaining.ToString());
                response.AddHeader("Connection", "Keep-Alive");
                response.ContentEncoding = Encoding.UTF8;

                if (startBytes > 0)
                {
                    response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }

                // BinaryReader implements IDisposable so should be in a using block
                using (var br = new BinaryReader(myFile))
                {
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);

                    const int packSize = 1024 * 10; //read in block，every block 10K bytes
                    var maxCount = (int)Math.Ceiling((remaining + 0.0) / packSize); //download in block
                    for (var i = 0; i < maxCount && response.IsClientConnected; i++)
                    {
                        response.BinaryWrite(br.ReadBytes(packSize));
                        response.Flush();

                        // HACK: Unexplained sleep
                        var sleep = (int)Math.Ceiling(1000.0 * packSize / speed); //the number of millisecond
                        if (sleep > 1) Thread.Sleep(sleep);
                    }
                }
            }
            return true;
        }

        public static string GetMD5Hash(string pathName)
        {
            byte[] arrbytHashValue;
            using (FileStream oFileStream = new FileStream(pathName, FileMode.Open))
            {
                arrbytHashValue = new MD5CryptoServiceProvider().ComputeHash(oFileStream);
            }

            string strHashData = BitConverter.ToString(arrbytHashValue);
            strHashData = strHashData.Replace("-", "");
            string strResult = strHashData;

            return (strResult);
        }

        public static string MD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] btr = Encoding.UTF8.GetBytes(str);
            btr = md5.ComputeHash(btr);

            StringBuilder sb = new StringBuilder();
            foreach (byte ba in btr)
                sb.Append(ba.ToString("x2").ToLower());

            return sb.ToString();
        }

        public static void CopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public static void EncDecFile(string srcPath, string dstPath)
        {
            int start = 10;
            if (srcPath.ToLowerInvariant().EndsWith("jpg"))
                start = 10;
            if (srcPath.ToLowerInvariant().EndsWith("png"))
                start = 9;
            if (srcPath.ToLowerInvariant().EndsWith("swf"))
                start = 84;
            if (srcPath.ToLowerInvariant().EndsWith("etf"))
                start = 84;
            if (srcPath.ToLowerInvariant().EndsWith("flv"))
                start = 24;
            if (srcPath.ToLowerInvariant().EndsWith("f4v"))
                start = 24;

            byte[] bytes = File.ReadAllBytes(srcPath);

            for (int i = start; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte((int)(bytes[i] ^ 143));

            File.WriteAllBytes(dstPath + "tmp", bytes);
            File.Move(dstPath + "tmp", dstPath);
        }

        public static string GetProcessorId()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["ProcessorId"] != null)
                        return queryObj["ProcessorId"].ToString();
                }
            }
            catch (ManagementException e)
            {
            }

            return "unknown";
        }

        public static bool IsBinaryFile(string filePath, int sampleSize = 10240)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("File path is not valid", "filePath");

            var buffer = new char[sampleSize];
            string sampleContent;

            using (var sr = new StreamReader(filePath))
            {
                int length = sr.Read(buffer, 0, sampleSize);
                sampleContent = new string(buffer, 0, length);
            }

            //Look for 4 consecutive binary zeroes
            if (sampleContent.Contains("\0\0\0\0"))
                return true;

            return false;
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string ExecuteProcess(string filename, string arguments = null)
        {
            var process = new Process();

            process.StartInfo.FileName = filename;
            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.Append(args.Data);

            string stdError = null;
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                stdError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing " + filename + " "+ arguments + ": " + e.Message, e);
            }

            if (process.ExitCode == 0)
            {
                return stdOutput.ToString();
            }
            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(stdError))
                {
                    message.AppendLine(stdError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                throw new Exception(filename + " " + arguments + " finished with exit code = " + process.ExitCode + ": " + message);
            }
        }

        public static void SendMail(string from, string fromDisplayName, string to, string toDisplayName, string subject, string message, string host, int port, string userName, string password, string bcc = null)
        {
            MailAddress _from = new MailAddress(from, fromDisplayName);
            MailAddress _to = new MailAddress(to, toDisplayName);
            MailMessage mail = new MailMessage(_from, _to);
            mail.Subject = subject;
            if (bcc!=null)
                mail.Bcc.Add(new MailAddress(bcc, bcc));
            mail.IsBodyHtml = true;
            mail.Body = message;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = port;
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(userName, password);
            }

            smtp.Send(mail);
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
    public enum Alignment
    {
        TopCenter,
        TopRight,
        MiddleRight,
        BottomRight,
        BottomCenter,
        BottomLeft,
        MiddleLeft,
        TopLeft,
        MiddleCenter
    }

    public enum CardType
    {
        MASTERCARD, VISA
    };

    public interface IAPIProvider
    {
    }
}

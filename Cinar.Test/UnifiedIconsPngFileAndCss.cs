using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Scripting;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cinar.Test
{
    public class UnifiedIconsPngFileAndCss
    {
        public static void Run()
        {
            List<string> files = new List<string>();

            foreach (string file in Directory.GetFiles(@"E:\work\Cinar\Cinar.CMS.Web\external\icons").OrderBy(f=>Path.GetFileName(f)))
            {
                using (Image img = Image.FromFile(file))
                {
                    if (img.Width == 16 && img.Height == 16)
                        files.Add(file);
                }
            }

            using (Bitmap bmp = new Bitmap(16, files.Count * 16))
            {
                string cssFileContent = ".cbtn {background-image:url('all_icons.png'); background-repeat:no-repeat; height:16px; width:16px; display:inline-block;}\r\n";
                string sampleHtmlFile = "<html><head><link rel=\"stylesheet\" type=\"text/css\" href=\"all_icons.css\" /></head><body>\r\n";

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string file = files[i];
                        using (Image img = Image.FromFile(file))
                            g.DrawImage(img, 0, i * 16);

                        cssFileContent += string.Format(".c{0} {{background-position:0px {1}px;}}\r\n", Path.GetFileNameWithoutExtension(file), i * -16);
                        sampleHtmlFile += string.Format("<span class=\"cbtn c{0}\"></span> {0}\r\n", Path.GetFileNameWithoutExtension(file));
                    }
                }

                sampleHtmlFile += "</body></html>";

                File.WriteAllText(@"c:\temp\all_icons.css", cssFileContent, Encoding.UTF8);
                File.WriteAllText(@"c:\temp\all_icons.html", sampleHtmlFile, Encoding.UTF8);

                bmp.MakeTransparent();
                bmp.Save(@"c:\temp\all_icons.png", ImageFormat.Png);
            }

        }
    }
}

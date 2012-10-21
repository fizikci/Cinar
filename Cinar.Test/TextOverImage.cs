using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;
using System.Net;
using System.Diagnostics;
using System.Drawing.Text;

namespace Cinar.Test
{
    public class TextOverImage
    {
        public static void Run()
        {
            float fontSize = 54.0f;
            Font font = new Font("Arabic Typesetting", fontSize);
            int ayetNo = 0;

            StringBuilder sb = new StringBuilder(20000);
            sb.AppendLine("var quran_bg_size = {");
            foreach (var item in Kuran.kuran)
            {
                sb.Append("\t\""+item.Key+"\": [");
                ayetNo++;
                float ayetWidth = 0;
                float ayetHeight = 0;
                using (Bitmap a = new Bitmap(5000, (int)(fontSize + fontSize)))
                {
                    using (Graphics g = Graphics.FromImage(a))
                    {
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            if (i + 1 < item.Value.Length && item.Value[i + 1] == "ۖ")
                                item.Value[i] += item.Value[i + 1];
                            SizeF size = g.MeasureString(item.Value[i], font);
                            sb.Append(size.Width+",");
                            ayetWidth += size.Width;
                            ayetHeight = size.Height;
                            g.TextRenderingHint = TextRenderingHint.AntiAlias;
                            g.DrawString(item.Value[i], font, Brushes.Black, new PointF(5000 - ayetWidth, fontSize / 7f)); // requires font, brush etc
                            if (i + 1 < item.Value.Length && item.Value[i + 1] == "ۖ")
                                i++;
                        }
                        sb = sb.Remove(sb.Length-1, 1);
                        sb.AppendLine("],");
                        Bitmap b = (Bitmap)a.CropImage(5000-(int)ayetWidth, 0, (int)ayetWidth, (int)ayetHeight, false);
                        b.SavePng(@"C:\Users\android\Desktop\txt2img\" + item.Key + ".png");
                    }
                }
                Console.WriteLine(item.Key);
                //if(ayetNo==10) break;
            }

            File.WriteAllText(@"C:\Users\android\Desktop\txt2img\quran_bg_size.js", sb.ToString(), Encoding.UTF8);
        }


    }
}

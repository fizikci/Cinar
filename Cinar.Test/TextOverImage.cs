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
            float fontSize = 36.0f;
            //Font font = new Font("Arabic Typesetting", fontSize);
            string basePath = @"C:\Users\android\Desktop\txt2img\tesbihat\";
            Font font = new Font("Scheherazade", fontSize);
            var brush1 = new SolidBrush(Color.FromArgb(85, 0, 0));
            var brush2 = new SolidBrush(Color.FromArgb(0, 85, 0));
            var cuz = 0;

            /*
            foreach (var item in Kuran.kuran)
            {
                var cuzIndex = Kuran.cuzler.IndexOf(s => s == item.Key);
                if (cuzIndex > -1) cuz = cuzIndex + 1;
                if (!Directory.Exists(basePath + cuz)) Directory.CreateDirectory(basePath + cuz);

                using (Bitmap a = new Bitmap(5000, (int)(fontSize + fontSize)))
                {
                    using (Graphics g = Graphics.FromImage(a))
                    {
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            if (File.Exists(basePath + cuz + "\\" + item.Key + "_" + i + ".png")) continue;
                            SizeF size = g.MeasureString(item.Value[i], font);
                            g.TextRenderingHint = TextRenderingHint.AntiAlias;
                            g.Clear(Color.Transparent);
                            g.DrawString(item.Value[i], font, i%2==0?brush1:brush2, new PointF(0, fontSize / 7f)); // requires font, brush etc
                            Bitmap b = (Bitmap)a.CropImage(0, 0, (int)size.Width, (int)size.Height, false);
                            b.SavePng(basePath + cuz + "\\" + item.Key + "_" + i + ".png");
                        }
                    }
                }
                Console.WriteLine(item.Key);
                //if(ayetNo==10) break;
            }
             */

            using (Bitmap a = new Bitmap(5000, (int)(fontSize + fontSize)))
            {
                using (Graphics g = Graphics.FromImage(a))
                {
                    foreach(var item in Kuran.tesbihat)
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        SizeF size = g.MeasureString(item.Value[i], font);
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                        g.Clear(Color.Transparent);
                        g.DrawString(item.Value[i], font, i % 2 == 0 ? brush1 : brush2, new PointF(0, fontSize / 7f)); // requires font, brush etc
                        Bitmap b = (Bitmap)a.CropImage(0, 0, (int)size.Width, (int)size.Height, false);
                        b.SavePng(basePath + item.Key + "_" + i + ".png");
                    }
                }
            }
        }


    }
}

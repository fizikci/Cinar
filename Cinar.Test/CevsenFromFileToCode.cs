using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cinar.Test
{
    public class CevsenFromFileToCode
    {
        public static Dictionary<string, string[]> Run()
        {
            Dictionary<string, string[]> res = new Dictionary<string, string[]>();

            for (int i = 1; i <= 101; i++)
            {
                string pathAr = @"C:\Users\KESKIN\Desktop\cevsen\" + i + ".txt";
                string pathTr = @"C:\Users\KESKIN\Desktop\cevsen\meal" + i + ".txt";

                string strAr = File.ReadAllText(pathAr);
                string strTr = File.ReadAllText(pathTr);

                string[] arrAr = strAr.Replace(" *", "").Replace("\r", "").SplitWithTrim('\n');
                string[] arrTr = strTr.Replace("<div dir='ltr'>", "").Replace("<span style='color: #900'>", "").Replace("</span></div>", "").Replace("\r", "").SplitWithTrim('\n');

                for (int j = 0; j < arrTr.Length; j++)
                    if (arrTr[j].Contains(". "))
                        arrTr[j] = arrTr[j].Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries)[1];

                for (int k = 0; k < arrAr.Length; k++)
                {
                    res.Add(i + "_" + k, arrAr[k].SplitWithTrim(' '));
                }
            }

            return res;
        }
    }
}

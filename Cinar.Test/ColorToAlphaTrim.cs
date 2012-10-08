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

namespace Cinar.Test
{
    public class ColorToAlphaTrim
    {
        public static void Run()
        {
            string str = "";

            for (int i = 1; i < 101; i++) 
            {
                str += @"convert " + i + @".gif -transparent white -trim -trim ..\cevsen2\" + i + ".png" + Environment.NewLine;
                Console.WriteLine(i);
            }

            File.WriteAllText(@"C:\Users\android\Desktop\cevsen\run.bat", str, Encoding.UTF8);
        }
    }
}

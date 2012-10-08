using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;
using System.Net;

namespace Cinar.Test
{
    public class FileRenamer
    {
        public static void Run()
        {
            for (int i = 0; i < 102; i++) 
            {
                string src = @"C:\Users\android\Desktop\dua-ufku.com\cevsen\" + i.ToString().PadLeft(6, '0') + ".gif";
                string dst = @"C:\Users\android\Desktop\dua-ufku.com\cevsen\" + i.ToString() + ".gif";

                File.Move(src, dst);

                Console.WriteLine(i);
            }
        }
    }
}

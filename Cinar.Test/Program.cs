using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ContentExtractor.Run();

            Console.WriteLine("\r\n\r\n\r\nBİTTİ");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Musteri m = new Musteri();
            m.Ad = "ldsjfh";


        }
    }

    public class Musteri
    {
        public string Ad { get; set; }
        public string VergiNo { get; set; }
    }
}

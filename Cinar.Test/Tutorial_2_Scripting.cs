using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Scripting;
using System.IO;

namespace Cinar.Test
{
    public class Tutorial_2_Scripting
    {
        public static void Run()
        {
            Interpreter engine = new Interpreter(File.ReadAllText("d:\\deneme.csc"), null);
            engine.SetAttribute("musteri", new Musteri { Ad="Microsoft"});
            engine.Parse();
            engine.Execute();

            Console.WriteLine(engine.Output);
        }
    }

    public class Musteri
    {
        public string Ad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cinar.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            List<Data> list = new List<Data>() { new Data() { Name = "Bülent" }, new Data() { Name = "5463465" }, new Data() { Name = "sdfgsdfg" }, };
            Parallel.ForEach(list, i=>ParalelCalis(i));

            Tutorial_3_CreateTableAtRuntime.Run();

            Console.WriteLine("\r\n\r\n\r\nBİTTİ");
            Console.ReadLine();
        }

        private static void ParalelCalis(Data data)
        {
            data.Name = data.Name + "bitti";
        }
    }

    public class Data
    {
        public string   Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.Test
{
    public class Tutorial_1_Metadata
    {
        public static void Run()
        {
            Database.Database db = new Database.Database(DatabaseProvider.MySQL, "localhost", "issuetracking", "root", "bk", 30);
            foreach (var table in db.Tables)
            {
                Console.WriteLine(table.Name);
                foreach (var column in table.Columns)
                {
                    Console.WriteLine(" - " + column.Name);
                }
            }
        }
    }
}

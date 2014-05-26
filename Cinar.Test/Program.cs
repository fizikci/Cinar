using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            //List<Data> list = new List<Data>() { new Data() { Name = "Bülent" }, new Data() { Name = "5463465" }, new Data() { Name = "sdfgsdfg" }, };
            //Parallel.ForEach(list, i=>ParalelCalis(i));

            //Tutorial_3_CreateTableAtRuntime.Run();

            string csv_file_path = @"C:\Users\BulentKeskin\Desktop\dalist 29nisanV4.csv";
            DataTable csvData = GetDataTabletFromCSVFile(csv_file_path);

            Database.Database db = new Database.Database("Server=localhost;Database=cinarcms;Uid=root;Pwd=bk;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL);
            db.ExecuteNonQuery(@"delete from contact where Id>2;
                                    delete from Company where Id>3;
                                    delete from Definition where Id>12;");

            int counter = 0;
            foreach (DataRow dr in csvData.Rows)
            {
                counter++;
                Console.WriteLine(counter);
                try
                {
                    int kind1Id = 0;
                    if (dr["Kategori1"].ToString() != "")
                    {
                        var kategori1 = dr["Kategori1"].ToString().Trim();
                        kind1Id = db.GetInt("select Id from Definition where Kind='ContactKind1' AND Name={0}", kategori1);
                        if (kind1Id == 0)
                            kind1Id = db.GetInt("insert into Definition(Kind, Name, InsertDate, InsertUserId) values({0},{1},now(),1); SELECT LAST_INSERT_ID();", "ContactKind1", kategori1);
                    }
                    int kind2Id = 0;
                    if (dr["Kategori2"].ToString() != "")
                    {
                        var kategori2 = dr["Kategori2"].ToString().Trim();
                        kind2Id = db.GetInt("select Id from Definition where Kind='ContactKind2' AND Name={0}", kategori2);
                        if (kind2Id == 0)
                            kind2Id = db.GetInt("insert into Definition(Kind, Name, InsertDate, InsertUserId) values({0},{1},now(),1); SELECT LAST_INSERT_ID();", "ContactKind2", kategori2);
                    }
                    int companyId = 0;
                    if (dr["Company"].ToString() != "")
                    {
                        var company = dr["Company"].ToString().Trim();
                        companyId = db.GetInt("select Id from Company where Name={0}", company);
                        if (companyId == 0)
                            companyId = db.GetInt("insert into Company(Name,UserId,Email,InsertDate, InsertUserId) values({0},0,{1},now(),1); SELECT LAST_INSERT_ID();", company, dr["Email"].ToString());
                    }
                    int titleId = 0;
                    if (dr["Title2"].ToString() != "")
                    {
                        var title = dr["Title2"].ToString().Trim();
                        titleId = db.GetInt("select Id from Definition where Kind='Title' AND Name={0}", title);
                        if (titleId == 0)
                            titleId = db.GetInt("insert into Definition(Kind, Name, InsertDate, InsertUserId) values({0},{1},now(),1); SELECT LAST_INSERT_ID();", "Title", title);
                    }
                    int countryId = 0;
                    if (dr["Country"].ToString() != "")
                    {
                        var country = dr["Country"].ToString().Trim();
                        countryId = db.GetInt("select Id from Definition where Kind='Country' AND Name={0}", country);
                        if (countryId == 0)
                            countryId = db.GetInt("insert into Definition(Kind, Name, InsertDate, InsertUserId) values({0},{1},now(),1); SELECT LAST_INSERT_ID();", "Country", country);
                    }
                    db.ExecuteNonQuery(@"insert into contact(
	                                    Kind1Id,
	                                    Kind2Id,
	                                    Name,
	                                    CompanyId,
	                                    TitleId,
	                                    Description,
	                                    Email,
	                                    Phone,
	                                    Phone2,
	                                    Fax,
	                                    PhoneMobile,
	                                    Web,
	                                    AddressLine1,
	                                    City,
	                                    CountryId,
	                                    ExtraField1,
	                                    ExtraField2,
                                        UserId, InsertDate, InsertUserId
                                    ) values (
                                        {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, 0, now(), 1
                                    )
                                ",
                                        kind1Id, kind2Id, dr["Name"].ToString(), companyId, titleId, dr["Title1"].ToString(),
                                        dr["Email"].ToString(), dr["Phone"].ToString(), dr["Phone2"].ToString(), dr["Fax"].ToString(),
                                        dr["PhoneMobile"].ToString(), dr["Web"].ToString(), dr["AddressLine1"].ToString(), dr["City"].ToString(),
                                        countryId, dr["ExtraField1"].ToString(), dr["ExtraField2"].ToString()
                                        );
                }
                catch { }
            }

            Console.WriteLine("\r\n\r\n\r\nBİTTİ");
            Console.ReadLine();
        }

        private static void ParalelCalis(Data data)
        {
            data.Name = data.Name + "bitti";
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();

            try
            {

                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path, Encoding.GetEncoding("WINDOWS-1254")))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }
    }

    public class Data
    {
        public string   Name { get; set; }
    }
}

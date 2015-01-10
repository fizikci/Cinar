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
            //Tutorial_3_CreateTableAtRuntime.Run();


            string csv_file_path = @"C:\Users\Bülent\Downloads\yenidatabaseV3.csv";
            DataTable csvData = GetDataTabletFromCSVFile(csv_file_path);

            Database.Database db = new Database.Database("Server=94.73.149.239;Database=hsedbs;Uid=hsedbs;Pwd=545454Ll;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL);
            //Database.Database db = new Database.Database("Server=localhost;Database=hazarcrm;Uid=root;Pwd=bk;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL);
            db.ExecuteNonQuery(@"truncate table Contact;
                                    truncate table Company;
                                    truncate table EntityComment;
                                    truncate table EventContact;
                                    truncate table GenericNotification;
                                    truncate table Log;
                                    truncate table Tag;
                                    truncate table Definition;");

            int counter = 0;
            foreach (DataRow dr in csvData.Rows)
            {
                counter++;
                Console.Write(".");
                try
                {
                    int prefixId = definition("PrefixId", "Prefix", dr, db);
                    string name = dr["Name"].ToString();
                    string description = dr["Description1"] + (dr["Description2"].ToString() != "" ? Environment.NewLine + dr["Description2"] : "");
                    int relationWithUsId = definition("RelationWithUsId", "RelationWithUs", dr, db);
                    string email = dr["Email"].ToString();
                    string email2 = dr["Email2"].ToString();
                    definition("Tags1", "CompanyTag", dr, db);
                    definition("Tags2", "CompanyTag", dr, db);
                    definition("Tags3", "CompanyTag", dr, db);
                    definition("Tags4", "CompanyTag", dr, db);
                    definition("Tags5", "CompanyTag", dr, db);
                    definition("TagsVIP", "CompanyTag", dr, db);
                    string tags = joinStrings(dr, "Tags1", "Tags2", "Tags3", "Tags4", "Tags5", "TagsVIP");
                    int assistantTypeId = definition("AssistantTypeId", "AssistantType", dr, db);
                    string assistantName = dr["AssistantName"].ToString();
                    string assistantEmail = dr["AssistantEmail"].ToString();
                    int departmentId = definition("DepartmentId", "Department", dr, db);
                    string referenceBy = dr["ReferenceBy"].ToString();
                    int kind1Id = definition("Kind1Id", "ContactKind1", dr, db);
                    int kind2Id = definition("Kind2Id", "ContactKind2", dr, db);
                    string phone = dr["Phone"].ToString();
                    string interPhone = dr["Dahili"].ToString();
                    string phone2 = dr["Phone2"].ToString();
                    string fax = dr["Fax"].ToString();
                    string phoneMobile = dr["PhoneMobile"].ToString();
                    string web = dr["Web"].ToString();
                    string addressLine1 = dr["AddressLine1"].ToString();
                    string town = dr["Town"].ToString();
                    string city = dr["City"].ToString();
                    int countryId = definition("CountryId", "Country", dr, db);
                    int interestId1 = definition("InterestId1", "Interest", dr, db);
                    int interestId2 = definition("InterestId2", "Interest", dr, db);
                    int language1Id = definition("Language1Id", "Language", dr, db);
                    int language2Id = definition("Language2Id", "Language", dr, db);
                    definition("NewsletterMembership", "Newsletter", dr, db);
                    definition("NewsletterMembershipYBN", "Newsletter", dr, db);
                    string newsletterMembership = joinStrings(dr, "NewsletterMembership", "NewsletterMembershipYBN");
                    string extraField1 = dr["ExtraField1"].ToString();
                    string extraField2 = dr["ExtraField2"].ToString();
                    string extraField3 = dr["ExtraField3"].ToString();

                    int companyId = 0;
                    if (dr["CompanyId"].ToString() != "")
                    {
                        var company = dr["CompanyId"].ToString().Trim();
                        companyId = db.GetInt("select Id from Company where Name={0}", company);
                        if (companyId == 0)
                            companyId = db.GetInt(@"insert into Company(
                                            Name,UserId,Email,
                                            AddressLine1, City, CountryId,
                                            Phone, Fax, SectorId, Web,
                                            InsertDate,InsertUserId,Visible
                                        ) values(
                                            {0},0,{1},
                                            {2},{3},{4},{5},{6},{7},{8},
                                            now(),1,1
                                        ); SELECT LAST_INSERT_ID();", 
                                            company, dr["Email"].ToString(),
                                            addressLine1, city, countryId,
                                            phone, fax, kind1Id, web
                               );
                    }


                    int contactId = db.GetInt(@"insert into contact(
	                                    PrefixId, Name, Description, RelationWithUsId,
	                                    Email, Email2, Tags, AssistantTypeId,
	                                    AssistantName, AssistantEmail, CompanyId, DepartmentId,
	                                    ReferenceBy, Kind1Id, Kind2Id, Phone, InterPhone,
	                                    Phone2, Fax, PhoneMobile, Web,
                                        AddressLine1, Town, City, CountryId,
                                        InterestId1, InterestId2, Language1Id, Language2Id,
                                        NewsletterMembership, ExtraField1, ExtraField2, ExtraField3,
                                        UserId, InsertDate, InsertUserId, Visible
                                    ) values (
                                        {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24},
                                        {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32},
                                        0, now(), 1, 1
                                    ); SELECT LAST_INSERT_ID();
                                ",
	                                    prefixId, name, description, relationWithUsId,
	                                    email, email2, tags, assistantTypeId,
	                                    assistantName, assistantEmail, companyId, departmentId,
	                                    referenceBy, kind1Id, kind2Id, phone, interPhone,
	                                    phone2, fax, phoneMobile, web,
                                        addressLine1, town, city, countryId,
                                        interestId1, interestId2, language1Id, language2Id,
                                        newsletterMembership, extraField1, extraField2, extraField3                                        
                                        );
                    if (dr["EntityComment"].ToString() != "")
                        db.ExecuteNonQuery("insert into EntityComment(EntityName, EntityId, Details, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", "Contact", contactId, dr["EntityComment"]);
                    if (dr["EventInvite3"].ToString() != "")
                        db.ExecuteNonQuery("insert into EventContact(EventId, ContactId, State, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", 3, contactId, "Invited");
                    if (dr["EventJoining2"].ToString() != "")
                        db.ExecuteNonQuery("insert into EventContact(EventId, ContactId, State, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", 2, contactId, "Joining");
                    if (dr["EventJoining4"].ToString() != "")
                        db.ExecuteNonQuery("insert into EventContact(EventId, ContactId, State, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", 4, contactId, "Joining");
                    if (dr["EventJoining3"].ToString() != "")
                        db.ExecuteNonQuery("insert into EventContact(EventId, ContactId, State, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", 3, contactId, "Joining");
                    if (dr["EventJoining5"].ToString() != "")
                        db.ExecuteNonQuery("insert into EventContact(EventId, ContactId, State, InsertDate, InsertUserId,Visible) values({0},{1},{2},now(),1,1)", 5, contactId, "Joining");
                }
                catch(Exception ex) {
                    Console.WriteLine();
                    Console.WriteLine(counter+": "+ex.ToStringBetter());
                }
            }
             

            Console.WriteLine("\r\nBİTTİ");
            Console.ReadLine();
        }

        private static string joinStrings(DataRow dr, params string[] strs)
        {
            string res = "";
            foreach (var str in strs)
                if (dr[str].ToString() != "")
                    res += "," + dr[str];
            return res.Trim(',', ' ');
        }

        private static int definition(string csvColumnName, string kind, DataRow dr, Database.Database db) {
            int id = 0;
            if (dr[csvColumnName].ToString() != "")
            {
                var name = dr[csvColumnName].ToString().Trim();
                id = db.GetInt("select Id from Definition where Kind={1} AND Name={0}", name, kind);
                if (id == 0)
                    id = db.GetInt("insert into Definition(Kind, Name, InsertDate, InsertUserId,Visible) values({0},{1},now(),1,1); SELECT LAST_INSERT_ID();", kind, name);
            }
            return id;
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();

            try
            {

                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path, Encoding.GetEncoding("WINDOWS-1254")))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn dataColumn = new DataColumn(column);
                        dataColumn.AllowDBNull = true;
                        csvData.Columns.Add(dataColumn);
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

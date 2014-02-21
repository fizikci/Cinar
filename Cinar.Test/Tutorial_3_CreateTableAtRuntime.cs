using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Workflows;
using Cinar.Entities;

namespace Cinar.Test
{
    public class Tutorial_3_CreateTableAtRuntime
    {
        public static void Run()
        {
            // Eğer bu veritabanı serverda yoksa, otomatik create edilir.
            CinarContext.Db = new Database.Database(DatabaseProvider.MySQL, "localhost", "cinarcms", "root", "bk", 30);
            WFAracTalep aracTalep = new WFAracTalep { 
                Adres = "adres",
                Saat = "saat",
                State = WFS_AracTalep.New
            };
            aracTalep.Save();

            /*
            Table table = db.CreateTableMetadataForType(typeof(Ogrenci));

            Console.WriteLine(db.GetTableDDL(table));

            Ogrenci ogr = new Ogrenci();
            ogr.Ad = "Mehmet";
            ogr.TcKimlikNo = "987632454";
            db.Save(ogr);

            Veli v = new Veli();
            v.Ad = "Bülent";
            v.TcKimlikNo = "756867";
            db.Save(v);
            
            List<Ogrenci> list = db.ReadList<Ogrenci>("select *, '2. Dönem' as Donem, 2012 as Yil from student");
            */
        }
    }

    [PrimaryKeyConstraint(Name = "PK_Ogrenci", ConstraintColumnNames = "Id")]
    [Index(Name = "IND_Ad_DogumYili", ConstraintColumnNames = "Ad,dogum_yili")]
    //[CheckConstraint(Name = "CHK_DogumYili", Expression = "DogumYili > 1900")]
    [UniqueConstraint(Name = "UNQ_TcKimlikNo", ConstraintColumnNames = "TcKimlikNo")]
    [DefaultData(ColumnList = "TcKimlikNo, Ad", ValueList = "'12345678901', 'Ahmet'")]
    [TableDetail(Name="student")]
    public class Ogrenci : IDatabaseEntity
    {
        [ColumnDetail(IsAutoIncrement = true, IsNotNull = true)]
        public int Id { get; set; }

        [ColumnDetail(Length = 11)]
        public string TcKimlikNo { get; set; }

        [ColumnDetail(Length = 20)]
        public string Ad { get; set; }

        [ColumnDetail(IsNotNull = true, DefaultValue = "1980", Name="dogum_yili")]
        public int DogumYili { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 7)]
        public OgrenciTipi Tipi { get; set; }


        #region IDatabaseEntity members
        public void Initialize()
        {
        }

        public string GetNameColumn()
        {
            return "";
        }

        public string GetNameValue()
        {
            return "";
        }

        public object this[string key]
        {
            get { return ht[key]; }
            set { ht[key] = value; }
        }
        Hashtable ht = new Hashtable();
        public Hashtable GetOriginalValues()
        {
            return ht;
        }
        #endregion


        public void BeforeSave()
        {
            
        }

        public void AfterSave(bool isUpdate)
        {
            
        }
    }

    public class Veli : Ogrenci { }

    public enum OgrenciTipi { Sabahci, Oglenci }
}

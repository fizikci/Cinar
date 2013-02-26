using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.Test
{
    public class Tutorial_3_CreateTableAtRuntime
    {
        public static void Run()
        {
            // Eğer bu veritabanı serverda yoksa, otomatik create edilir.
            Database.Database db = new Database.Database(DatabaseProvider.MySQL, "localhost", "deneme", "root", "bk", 30, null, true);

            Table table = db.CreateTableMetadataForType(typeof(Ogrenci));

            Console.WriteLine(db.GetTableDDL(table));

            Ogrenci ogr = new Ogrenci();
            ogr.Ad = "Mehmet";
            ogr.TcKimlikNo = "98765432101";
            db.Save(ogr);

            Veli v = new Veli();
            v.Ad = "Bülent";
            v.TcKimlikNo = "756867";
            db.Save(v);
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
            get { return ""; }
            set { }
        }

        public Hashtable GetOriginalValues()
        {
            return new Hashtable();
        }
        #endregion
    }

    public class Veli : Ogrenci { }

    public enum OgrenciTipi { Sabahci, Oglenci }

}

Cinar.Database kütüphanesi veritabaný ile çalýþmayý daha fonksiyonel ve daha eðlenceli hale getirir.

Özellikler:
- Database abstraction (MySQL, MS SQL Server ve PostgreSQL ile çalýþabilir)
- Veritabaný metedatasýna eriþim saðlýyor (Tables, Columns, PrimaryKey, vs..)
- Metadatadan "create table", "insert", "update" gibi SQL scriptlerini üretebiliyor
- Transactionlý veya transactionsýz çalýþabilir (özellikle web uygulamalarýnda performansý arttýrmak için transaction'ý kapatmak gerekli olabilir)
- Çalýþtýrýlan SQL'lerin detaylý loglarýný tutuyor. (hangi kod satýrýndan hangi SQL çalýþtý?)
- Web uygulamalarýnda bir request için çalýþan Cache (önbellek) mekanizmasýna sahip
- Object Releational mapping yaklaþýmý
	- Nesnelerin sýnýf isimlerini tablo adý ve public {get; set;} imzasýna sahip propertilerini tablo sütunu olarak kabul edip database ile map ediyor
	- Nesneler üzerinde ColumnDetail attribute'u ile column metadatasý belirtilebiliyor
		(ColumnType, IsNotNull, DefaultValue, Length, IsPrimaryKey, IsAutoIncrement, References, ReferenceType)
	- Nesnesi olup kendisi olmayan tablolarýn runtime'da otomatik "create" edilmesi
	- Otomatik create edilen tablolara eklenmesi gereken default kayýtlar DefaultData attribute'u ile belirtilebiliyor
	- Yazýlýmdaki mevcut sýnýflarla çalýþabilmesi için yapýlmasý gereken tek þey bu nesnelere IDatabaseEntity interface'ini implement ettirmek
- ORM'in yaný sýra DataSet ve Hashtable ile çalýþabiliyor
- Execute metoduna parametre olarak geçilen bir "lambda ifadesini" bir transaction içerisinde çalýþtýrýp commit veya duruma göre rollback edebiliyor
- Hem object oriented hem de table oriented yaklaþýmlarý destekleyen kullanýþlý metodlara sahip
	SQL ile veri okuma metodlarý:
		DataTable GetDataTable(string sql, params object[] parameters)
		DataRow GetDataRow(string sql, params object[] parameters)
		object GetValue(string sql, params object[] parameters)
		    string GetString(..)
		    int GetInt(..)
		    bool GetBool(..)
		    DateTime GetDateTime(..)
		IDatabaseEntity Read(Type entityType, int id)
		    T Read<T>(int id)
		IDatabaseEntity Read(Type entityType, string where, params object[] parameters)
		    T Read<T>(string where, params object[] parameters))
		IDatabaseEntity[] ReadList(Type entityType, string selectSql, params object[] parameters)
		    List<T> ReadList<T>(string selectSql, params object[] parameters)
		DataTable ReadTable(Type entityType, string selectSql, params object[] parameters)
	SQL çalýþtýrma:
		int ExecuteNonQuery(IDbCommand cmd)
		int ExecuteNonQuery(string sql, params object[] parameters)
		int Insert(string tableName, Hashtable data)
		int Insert(string tableName, DataRow dataRow)
		int Update(string tableName, Hashtable data)
		int Update(string tableName, DataRow dataRow)
		void Save(IDatabaseEntity entity)
	DDL sorgularý için kullanýþlý metodlar:
		Table CreateTableForType(Type type)
		Table GetTableForType(Type type)
		DbType GetDbType(Type type)
		string GetTableDDL(Table table)
		string GetTableDDL(Table table, DatabaseProvider provider)
		string GetColumnDDL(Column column)
	Veri tiplerini dönüþtürme metodlarý:
		Hashtable EntityToHashtable(IDatabaseEntity entity)
		DataRow EntityToDataRow(IDatabaseEntity entity)
		IDatabaseEntity DataRowToEntity(Type entityType, DataRow dr)
		void FillEntity(IDatabaseEntity entity, DataRow dr)
		void FillEntity(IDatabaseEntity entity)
		void FillDataRow(IDatabaseEntity entity, DataRow dr)


VERÝTABANI METADATASINA ERÝÞÝM

Veritabaný metadatasýna eriþim pek çok þekilde gerekli olur:
- Veritabanýnýzdaki tablolara göre kod generate etmeniz gerektiðinde,
- Design time'da kullanýcýya bir tablo yada tablo alaný seçtirmeniz gerektiðinde,
- Generic insert, update SQLleri oluþturmayý arzu ettiðiniz durumlarda,
- ve benzeri..

Cinar.Database projesi bu tür durumlarda yardýmcý olacak bir kütüphane. Ýki satýr kod yazdýktan
sonra bütün veritabaný nesneleri hakkýnda bilgiye eriþebiliyorsunuz:

Database db = new Database("...connection string...", DatabaseProvider.SQLServer);

Yukarýdaki kod ile Microsoft SQL Server'a, belirttiðimiz connection string ile baðlanmasýný istedik.
Cinar.Database baþarýlý bir baðlantýnýn ardýndan tüm veritabaný nesneleri hakkýnda bilgi toplar ve bunu uygun sýnýflarýn, kolleksiyonlarýn
içine doldurur. Bu bilgiye aþaðýdaki gibi eriþilebilir:

// tablolarý listeleyelim:
foreach( Table tbl in db.Tables )
{
	Console.WriteLine("Tablo adý                         : " + tbl.Name);
	Console.WriteLine("Alan sayýsý                       : " + tbl.Columns.Count);
	Console.WriteLine("Primary key                       : " + tbl.PrimaryColumn);
	Console.WrileLine("String tipindeki ilk alan (varsa) : " + tbl.StringColumn);
	Console.WrileLine("Int tipindeki ilk alan (varsa)    : " + tbl.Columns.Find(DbType.Int32));
	Console.WriteLine("---------------------------------------------");
} 
Koddan da görüldüðü gibi db.Tables kolleksiyonu tablolar hakkýnda bilgi toplamak için kullanýlabilir.

Þimdi de, Musteri isimli tablonun alanlarýný listelemek istediðimizi farzedelim:
Table musteriTable = db.Tables["Musteri"];

if(musteriTable==null)
{
	Console.WriteLine("böyle bi tablo yok ki!");
}
else
{
	foreach( Column fld in musteriTable.Columns )
	{
		Console.WriteLine("Column adý                  : " + fld.Name);
		Console.WriteLine("Tipi                        : " + fld.ColumnType);
		Console.WriteLine("Null olabilir mi?           : " + fld.IsNullable);
		Console.WrileLine("Default deðeri              : " + fld.DefaultValue);
		Console.WrileLine("Primary key mi?             : " + fld.IsPrimaryKey);
		Console.WrileLine("Baþka bir tabloya referans? : " + fld.ReferenceColumn);
		Console.WriteLine("---------------------------------------------");
	} 
}

OBJECT RELATIONAL MAPPING


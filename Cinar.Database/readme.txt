Cinar.Database k�t�phanesi veritaban� ile �al��may� daha fonksiyonel ve daha e�lenceli hale getirir.

�zellikler:
- Database abstraction (MySQL, MS SQL Server ve PostgreSQL ile �al��abilir)
- Veritaban� metedatas�na eri�im sa�l�yor (Tables, Columns, PrimaryKey, vs..)
- Metadatadan "create table", "insert", "update" gibi SQL scriptlerini �retebiliyor
- Transactionl� veya transactions�z �al��abilir (�zellikle web uygulamalar�nda performans� artt�rmak i�in transaction'� kapatmak gerekli olabilir)
- �al��t�r�lan SQL'lerin detayl� loglar�n� tutuyor. (hangi kod sat�r�ndan hangi SQL �al��t�?)
- Web uygulamalar�nda bir request i�in �al��an Cache (�nbellek) mekanizmas�na sahip
- Object Releational mapping yakla��m�
	- Nesnelerin s�n�f isimlerini tablo ad� ve public {get; set;} imzas�na sahip propertilerini tablo s�tunu olarak kabul edip database ile map ediyor
	- Nesneler �zerinde ColumnDetail attribute'u ile column metadatas� belirtilebiliyor
		(ColumnType, IsNotNull, DefaultValue, Length, IsPrimaryKey, IsAutoIncrement, References, ReferenceType)
	- Nesnesi olup kendisi olmayan tablolar�n runtime'da otomatik "create" edilmesi
	- Otomatik create edilen tablolara eklenmesi gereken default kay�tlar DefaultData attribute'u ile belirtilebiliyor
	- Yaz�l�mdaki mevcut s�n�flarla �al��abilmesi i�in yap�lmas� gereken tek �ey bu nesnelere IDatabaseEntity interface'ini implement ettirmek
- ORM'in yan� s�ra DataSet ve Hashtable ile �al��abiliyor
- Execute metoduna parametre olarak ge�ilen bir "lambda ifadesini" bir transaction i�erisinde �al��t�r�p commit veya duruma g�re rollback edebiliyor
- Hem object oriented hem de table oriented yakla��mlar� destekleyen kullan��l� metodlara sahip
	SQL ile veri okuma metodlar�:
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
	SQL �al��t�rma:
		int ExecuteNonQuery(IDbCommand cmd)
		int ExecuteNonQuery(string sql, params object[] parameters)
		int Insert(string tableName, Hashtable data)
		int Insert(string tableName, DataRow dataRow)
		int Update(string tableName, Hashtable data)
		int Update(string tableName, DataRow dataRow)
		void Save(IDatabaseEntity entity)
	DDL sorgular� i�in kullan��l� metodlar:
		Table CreateTableForType(Type type)
		Table GetTableForType(Type type)
		DbType GetDbType(Type type)
		string GetTableDDL(Table table)
		string GetTableDDL(Table table, DatabaseProvider provider)
		string GetColumnDDL(Column column)
	Veri tiplerini d�n��t�rme metodlar�:
		Hashtable EntityToHashtable(IDatabaseEntity entity)
		DataRow EntityToDataRow(IDatabaseEntity entity)
		IDatabaseEntity DataRowToEntity(Type entityType, DataRow dr)
		void FillEntity(IDatabaseEntity entity, DataRow dr)
		void FillEntity(IDatabaseEntity entity)
		void FillDataRow(IDatabaseEntity entity, DataRow dr)


VER�TABANI METADATASINA ER���M

Veritaban� metadatas�na eri�im pek �ok �ekilde gerekli olur:
- Veritaban�n�zdaki tablolara g�re kod generate etmeniz gerekti�inde,
- Design time'da kullan�c�ya bir tablo yada tablo alan� se�tirmeniz gerekti�inde,
- Generic insert, update SQLleri olu�turmay� arzu etti�iniz durumlarda,
- ve benzeri..

Cinar.Database projesi bu t�r durumlarda yard�mc� olacak bir k�t�phane. �ki sat�r kod yazd�ktan
sonra b�t�n veritaban� nesneleri hakk�nda bilgiye eri�ebiliyorsunuz:

Database db = new Database("...connection string...", DatabaseProvider.SQLServer);

Yukar�daki kod ile Microsoft SQL Server'a, belirtti�imiz connection string ile ba�lanmas�n� istedik.
Cinar.Database ba�ar�l� bir ba�lant�n�n ard�ndan t�m veritaban� nesneleri hakk�nda bilgi toplar ve bunu uygun s�n�flar�n, kolleksiyonlar�n
i�ine doldurur. Bu bilgiye a�a��daki gibi eri�ilebilir:

// tablolar� listeleyelim:
foreach( Table tbl in db.Tables )
{
	Console.WriteLine("Tablo ad�                         : " + tbl.Name);
	Console.WriteLine("Alan say�s�                       : " + tbl.Columns.Count);
	Console.WriteLine("Primary key                       : " + tbl.PrimaryColumn);
	Console.WrileLine("String tipindeki ilk alan (varsa) : " + tbl.StringColumn);
	Console.WrileLine("Int tipindeki ilk alan (varsa)    : " + tbl.Columns.Find(DbType.Int32));
	Console.WriteLine("---------------------------------------------");
} 
Koddan da g�r�ld��� gibi db.Tables kolleksiyonu tablolar hakk�nda bilgi toplamak i�in kullan�labilir.

�imdi de, Musteri isimli tablonun alanlar�n� listelemek istedi�imizi farzedelim:
Table musteriTable = db.Tables["Musteri"];

if(musteriTable==null)
{
	Console.WriteLine("b�yle bi tablo yok ki!");
}
else
{
	foreach( Column fld in musteriTable.Columns )
	{
		Console.WriteLine("Column ad�                  : " + fld.Name);
		Console.WriteLine("Tipi                        : " + fld.ColumnType);
		Console.WriteLine("Null olabilir mi?           : " + fld.IsNullable);
		Console.WrileLine("Default de�eri              : " + fld.DefaultValue);
		Console.WrileLine("Primary key mi?             : " + fld.IsPrimaryKey);
		Console.WrileLine("Ba�ka bir tabloya referans? : " + fld.ReferenceColumn);
		Console.WriteLine("---------------------------------------------");
	} 
}

OBJECT RELATIONAL MAPPING


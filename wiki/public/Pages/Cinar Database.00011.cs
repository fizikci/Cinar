Cinar Database
admin|2011/04/28 17:42:31
##PAGE##
==Tanım==
Cinar.Database kütüphanesi veritabanı ile çalışmayı daha fonksiyonel ve daha eğlenceli hale getirir.

==Özellikler==
* '''Database abstraction''' (MySQL, MS SQL Server ve PostgreSQL ile çalışabilir)
* '''Veritabanı metedatasına erişim''' sağlıyor (Tables, Fields, PrimaryKey, vs..)
* '''SQL Generation''': Metadatadan "create table", "insert", "update" gibi SQL scriptlerini üretebiliyor
* '''Transaction'''lı veya transactionsız çalışabilir (özellikle web uygulamalarında performansı arttırmak için transaction'ı kapatmak gerekli olabilir)
* '''Loglama''': Çalıştırılan SQL'lerin detaylı loglarını tutuyor. (hangi kod satırından hangi SQL çalıştı?)
* '''Cache''': Web uygulamalarında bir request için çalışan Cache (önbellek) mekanizmasına sahip
* '''Object Relational Mapping''' yaklaşımı
** Nesnelerin sınıf isimlerini tablo adı ve public {get; set;} imzasına sahip propertilerini tablo fieldı olarak kabul edip database ile map ediyor
** Nesneler üzerinde FieldDetail ve TableDetail attribute'u ile ekstra mapping metadatası belirtilebiliyor (TableName, FieldName, FieldType, IsNotNull, DefaultValue, Length, IsPrimaryKey, IsAutoIncrement, References, ReferenceType)
** Nesnesi olup kendisi olmayan tabloların runtime'da otomatik "create" edilmesi
** Otomatik create edilen tablolara eklenmesi gereken default kayıtlar DefaultData attribute'u ile belirtilebiliyor
** Yazılımdaki mevcut sınıflarla çalışabilmesi için yapılması gereken tek şey bu nesnelere IDatabaseEntity interface'ini implement ettirmek
* '''Table Oriented''': ORM'in yanı sıra DataSet ve Hashtable ile çalışabiliyor
* Execute metoduna parametre olarak geçilen bir "lambda ifadesini" bir transaction içerisinde çalıştırıp commit veya duruma göre rollback edebiliyor

==Kod Örnekleri==

'''Veritabanına bağlanma'''
@@Database db = new Database(Provider.MySQL, host, dbName, userName, password, 30);
string version = db.GetString("select concat('MySQL version : ', version())");@@

@@Database db = new Database(Provider.PostgreSQL, host, dbName, userName, password, 30);
string version = db.GetString("select 'PostgreSQL version : ' || version()");@@

'''Metadataya erişim'''
@@// tabloları listeleyelim:
foreach( Table tbl in db.Tables )
{
	Console.WriteLine("Tablo adı                         : " + tbl.Name);
	Console.WriteLine("Alan sayısı                       : " + tbl.Fields.Count);
	Console.WriteLine("Primary key                       : " + tbl.PrimaryField);
	Console.WrileLine("String tipindeki ilk alan (varsa) : " + tbl.StringField);
	Console.WrileLine("Int tipindeki ilk alan (varsa)    : " + tbl.Fields.Find(DbType.Int32));
	Console.WriteLine("---------------------------------------------");
}@@

<nowiki>
<!--iframe title="YouTube video player" width="480" height="390" src="http://www.youtube.com/embed/fFzswnSpB64" frameborder="0" allowfullscreen></iframe-->
</nowiki>
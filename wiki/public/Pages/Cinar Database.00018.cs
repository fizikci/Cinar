Cinar Database
admin|2011/04/29 12:48:24
##PAGE##
==Tanım==
Cinar.Database kütüphanesi veritabanı ile çalışmayı daha fonksiyonel ve daha eğlenceli hale getirir.

==Özellikler==
* '''Database abstraction''' (MySQL, MS SQL Server ve PostgreSQL ile çalışabilir)
* '''Veritabanı metedatasına erişim''' sağlıyor (Tables, Fields, PrimaryKey, vs..)
* '''SQL Generation''': Metadatadan "create table", "insert", "update" gibi SQL scriptlerini üretebiliyor
* '''Transaction'''lı veya transactionsız çalışabilir (özellikle web uygulamalarında performansı arttırmak için transaction'ı kapatmak gerekli olabilir)
Execute metoduna parametre olarak geçilen bir "lambda ifadesini" bir transaction içerisinde çalıştırıp commit veya duruma göre rollback edebiliyor
* '''Loglama''': Çalıştırılan SQL'lerin detaylı loglarını tutuyor. (hangi kod satırından hangi SQL çalıştı?)
* '''Cache''': Web uygulamalarında bir request için çalışan Cache (önbellek) mekanizmasına sahip
* '''Object Relational Mapping''' yaklaşımı
** Nesnelerin sınıf isimlerini tablo adı ve public {get; set;} imzasına sahip propertilerini tablo fieldı olarak kabul edip database ile map ediyor
** Nesneler üzerinde FieldDetail ve TableDetail attribute'u ile ekstra mapping metadatası belirtilebiliyor (TableName, FieldName, FieldType, IsNotNull, DefaultValue, Length, IsPrimaryKey, IsAutoIncrement, References, ReferenceType)
** Nesnesi olup kendisi olmayan tabloların runtime'da otomatik "create" edilmesi
** Otomatik create edilen tablolara eklenmesi gereken default kayıtlar DefaultData attribute'u ile belirtilebiliyor
** Yazılımdaki mevcut sınıflarla çalışabilmesi için yapılması gereken tek şey bu nesnelere IDatabaseEntity interface'ini implement ettirmek
* '''Table Oriented''': ORM'in yanı sıra DataSet ve Hashtable ile çalışabiliyor

==Kod Örnekleri==

'''Veritabanına bağlanma'''

@@Database db = new Database(Provider.MySQL, host, dbName, userName, password, 30);@@

@@Database db = new Database(Provider.PostgreSQL, host, dbName, userName, password, 30);@@
Bu sayede MySQL, Postgre veya MS SQL'de "connection string nasıl yazılıyordu" diye düşünmenize gerek kalmıyor.

'''Basit sorgular'''

@@
string version = db.GetString("select version()");
DataTable dt = db.GetDataTable("select Id, Ad from Kisi");
@@

'''Insert'''

@@
Hashtable record = new Hashtable();
record["Ad"] = "Microsoft";
db.Insert("Kisi", record);
@@
Insert sql yazmak için uğraşmaya gerek yok, Insert metodu INSERT sorgusunu nasıl oluşturacağını biliyor.

'''Update'''

@@
DataRow dr = db.GetDataRow("select * from Kisi where Id=1");
dr["Ad"] = "Microsoft Coorporation";
db.Update("Kisi", dr);
@@
Update sorgusu da yazmaya gerek yok. Eğer ORM yöntemini kullanırsanız, DataRow veya Hashtable'lar ile de uğraşmanıza gerek yok.

'''Metadataya erişim'''

@@// tabloları listeleyelim:
foreach( Table tbl in db.Tables )
{
	Console.WriteLine("Tablo adı                         : " + tbl.Name);
	Console.WriteLine("Alan sayısı                       : " + tbl.Fields.Count);
	Console.WriteLine("Primary key                       : " + tbl.PrimaryField);
	Console.WriteLine("String tipindeki ilk alan (varsa) : " + tbl.StringField);
	Console.WriteLine("Int tipindeki ilk alan (varsa)    : " + tbl.Fields.Find(DbType.Int32));
}

// Şimdi de, Musteri isimli tablonun alanlarını listelemek istediğimizi farzedelim:

Table musteriTable = db.Tables["Musteri"];

if(musteriTable==null)
{
	Console.WriteLine("böyle bir tablo yok!");
}
else
{
	foreach( Field fld in musteriTable.Fields )
	{
		Console.WriteLine("Field adı                   : " + fld.Name);
		Console.WriteLine("Tipi                        : " + fld.FieldType);
		Console.WriteLine("Null olabilir mi?           : " + fld.IsNullable);
		Console.WriteLine("Default değeri              : " + fld.DefaultValue);
		Console.WriteLine("Primary key mi?             : " + fld.IsPrimaryKey);
		Console.WriteLine("Başka bir tabloya referans? : " + fld.ReferenceField);
	} 
}@@

<nowiki>
<!--iframe title="YouTube video player" width="480" height="390" src="http://www.youtube.com/embed/fFzswnSpB64" frameborder="0" allowfullscreen></iframe-->
</nowiki>
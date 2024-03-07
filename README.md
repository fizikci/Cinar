# Cinar
Cinar is a database-centric code library. It contains a framework named Cinar.Database and an SQL Client named Cinar.DBTools with many features that the creators needed in 20 years of their programming life.

# Cinar Database
## Definition
The Cinar.Database library enhances the functionality and enjoyment of working with databases by offering a comprehensive suite of features designed to simplify database operations.

## Features
- **Database abstraction:** Supports various databases, including MySQL, MS SQL Server, PostgreSQL, and SQLite, providing flexibility across different platforms.
- **Database Metadata Access:** Offers access to detailed information about database-defined objects like tables, fields, and indexes.
- **SQL Generation:** Automatically generates SQL scripts such as "create table", "insert", and "update" from metadata, streamlining the development process.
- **Transaction Management:** Facilitates both transactional and non-transactional operations, crucial for optimizing performance in web applications. Features functionality to execute lambda expressions within transactions, with automatic commit or rollback as appropriate.
- **Logging**: Maintains detailed logs of SQL queries executed, enhancing debugging and monitoring capabilities.
- **Caching:** Incorporates a caching mechanism for efficient handling of web application requests.
- **Object Relational Mapping**
  - Nesnelerin sınıf isimlerini tablo adı olarak ve public {get; set;} imzasına sahip propertilerini tablo fieldı olarak kabul edip database ile map ediyor.
  - Nesneler üzerinde FieldDetail ve TableDetail attribute'u ile ekstra mapping metadatası belirtilebiliyor (TableName, FieldName, FieldType, IsNotNull, DefaultValue, vb.)
  - Nesnesi olup kendisi olmayan tabloların runtime'da otomatik "create" edilmesini sağlıyor.
  - Otomatik create edilen tablolara eklenmesi gereken default kayıtlar DefaultData attribute'u ile belirtilebiliyor.
  - Yazılımdaki mevcut sınıflarla çalışabilmesi için yapılması gereken tek şey bu nesnelere IDatabaseEntity interface'ini implement ettirmek.
- **Table Oriented:** ORM'in yanı sıra DataSet ve Hashtable ile çalışabiliyor

## Kod Örnekleri
###### Veritabanına bağlanma
```
Database db = new Database(Provider.MySQL, host, dbName, userName, password, 30);

Database db = new Database(Provider.PostgreSQL, host, dbName, userName, password, 30);
```
Bu sayede "MySQL, Postgre veya MS SQL'de connection string nasıl yazılıyordu" diye düşünmemize gerek kalmıyor.

###### Basit sorgular
```
string version = db.GetString("select version()");
DataTable dt = db.GetDataTable("select Id, Ad from Kisi");
```
Veritabanından bir DataTable okumak için SQLConnection ve SQLDataAdapter kullanarak DataTable'i Fill() etmekle kim uğraşır?

###### Insert
```
Hashtable record = new Hashtable(); // Hashtable
record["Ad"] = "Ahmet";
db.Insert("Kisi", record);
```
veya
```
var record = new { Ad = "Ahmet" }; // Anonymous object
db.Insert("Kisi", record);
```
veya
```
DataRow record = dataTable.NewRow(); // DataRow
record["Ad"] = "Ahmet";
db.Insert("Kisi", record);
```
Insert sql yazmak için uğraşmaya gerek yok, Insert metodu INSERT sorgusunu nasıl oluşturacağını biliyor.

###### Update
```
DataRow dr = db.GetDataRow("select * from Kisi where Id=1");
dr["Ad"] = "Zeynep";
db.Update("Kisi", dr);
```
Update sorgusu da yazmaya gerek yok. (Eğer ORM yöntemini kullanırsak, DataRow veya Hashtable'lar ile de uğraşmaya gerek yok.)

###### Metadataya erişim
```
// tabloları listeleyelim:
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
}
```

# Cinar Database Tools
## Tanım
Çınar Database Tools, veritabanı programcısının hayatını kolaylaştırmak için geliştirilmiş bir yazılımdır.

## Özellikler
- Farklı veritabanı ürünleri ile aynı arayüzden çalışır: Microsoft SQL, MySQL, PostgreSQL
- Table, view, constraint, index gibi veritabanı nesnelerini listeler, değiştirmenize ve yeni nesneler eklemenize yardımcı olur.
- Verilerinizi listeleme, güncelleme, silme işlemlerinizi kolaylaştırır.
- Bir veritabanından diğerine (mesela MySQL'den MS SQL'e) veri ve schema transferi yapmanızı sağlar.
- Bir veritabanının backup'ını (SQL dump) desteklediği veritabanı ürünlerinden herhangi birinin formatında almanızı sağlar.
- SQL sorguları yazmanız için gelişmiş bir editöre sahiptir. (syntax highlighting ve code completion)
- Çınar Script ile çok hızlı kompleks sorgular üretmenize yardımcı olur.
- Mevcut bir veritabanını kolayca keşfetmenizi (anlamanızı) sağlayacak araç ve özelliklere sahiptir.
- Diagramlarla çalışmanızı sağlar.
- Veritabanınızın normalizasyonunu test eder ve düzeltmenize yardımcı olur.
- Bir .Net dll'indeki sınıflara uygun tablolar oluşturmanıza yardımcı olur.
- Scheduled Tasks (zamanlanmış görevler) oluşturmanızı sağlar. Bu araç ile veritabanı replikasyonu bile yapmak mümkündür.
- İki veritabanını (diff tarzında) karşılaştırmanıza yardımcı olur.
- Veritabanınız hakkında ekstra metadata belirtmenize imkan sağlar.
- Kod üretmek için projeler oluşturabilirsiniz.
- Visual Studio kullanıcıları için çok tanıdık bir arayüze sahiptir.

# SQL Statements for different vendors
## Renaming a Table
###### Microsoft SQL
```EXEC sp_rename products, items```
###### MySQL, PostgreSQL
```ALTER TABLE products RENAME TO items```



## Adding a Column
###### Microsoft SQL, MySQL, PostgreSQL
```ALTER TABLE products ADD description text```

## Removing a Column
###### Microsoft SQL, MySQL, PostgreSQL
```ALTER TABLE products DROP COLUMN description```

## Renaming a Column
###### Microsoft SQL
```EXEC sp_rename @objname = 'products.product_no', @newname = 'product_number', @objtype = 'COLUMN'```
###### MySQL
```ALTER TABLE product CHANGE product_no product_number INT NOT NULL DEFAULT 0; // omit PRIMARY KEY and UNIQUE```
###### PostgreSQL
```ALTER TABLE products RENAME COLUMN product_no TO product_number```

## Changing a Column's Data Type
###### Microsoft SQL
```ALTER TABLE products ALTER COLUMN price DECIMAL(10, 2)```
###### MySQL
```ALTER TABLE products MODIFY COLUMN price DECIMAL(10, 2)```
###### PostgreSQL
```ALTER TABLE products ALTER COLUMN price TYPE numeric(10,2)```



## Reading a Column's Default Value
###### Microsoft SQL
```SELECT * FROM sys.default_constraints```
can be used for removing

## Changing a Column's Default Value
###### Microsoft SQL
```ALTER TABLE products ADD CONSTRAINT some_name DEFAULT 7.77 FOR price```
###### MySQL, PostgreSQL
```ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77```

## Removing a Column's Default Value
###### Microsoft SQL
```ALTER TABLE products DROP CONSTRAINT default_name```
###### MySQL, PostgreSQL
```ALTER TABLE products ALTER COLUMN price DROP DEFAULT```



## Reading Constraints
###### Microsoft SQL Server, PostgreSQL
```
select distinct
	Con.Name, Con.TableName,Con.Type, Col.ColumnName, Col.Position, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
from
	(select c.CONSTRAINT_NAME as Name, c.TABLE_NAME as TableName, c.CONSTRAINT_TYPE as Type, r.UNIQUE_CONSTRAINT_NAME as RefConstraintName, r.UPDATE_RULE AS UpdateRule, R.DELETE_RULE as DeleteRule from INFORMATION_SCHEMA.TABLE_CONSTRAINTS c left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON r.CONSTRAINT_NAME=c.CONSTRAINT_NAME where c.table_catalog='$=db.Name$') as Con,
	(select CONSTRAINT_NAME as ConstraintName, TABLE_NAME as TableName, COLUMN_NAME as ColumnName, ORDINAL_POSITION as Position from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_catalog='$=db.Name$') as Col
where
	Con.Name = Col.ConstraintName
```
###### MySQL
```
select distinct
	Con.Name, Con.Type, Con.TableName, Col.ColumnName, Col.Position, Con.RefTableName, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
from
	(select c.CONSTRAINT_NAME as Name, c.TABLE_NAME as TableName, c.CONSTRAINT_TYPE as Type, r.REFERENCED_TABLE_NAME as RefTableName, r.UNIQUE_CONSTRAINT_NAME as RefConstraintName, r.UPDATE_RULE AS UpdateRule, R.DELETE_RULE as DeleteRule from INFORMATION_SCHEMA.TABLE_CONSTRAINTS c left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON r.CONSTRAINT_NAME=c.CONSTRAINT_NAME where c.table_schema='$=db.Name$') as Con,
	(select CONSTRAINT_NAME as ConstraintName, TABLE_NAME as TableName, COLUMN_NAME as ColumnName, ORDINAL_POSITION as Position from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_schema='$=db.Name$') as Col
where
	Con.Name = Col.ConstraintName
```

## Removing a Constraint
###### Microsoft SQL, PostgreSQL
```ALTER TABLE products DROP CONSTRAINT some_name```
###### MySQL
```Not Available```

## Adding a Check Constraint
###### Microsoft SQL
```ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '') // before add: WITH NOCHECK```
###### MySQL
```Not Available```
###### PostgreSQL
```ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '')```

## Adding a Unique Constraint
###### Microsoft SQL, PostgreSQL
```ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)```
###### MySQL
```CREATE UNIQUE INDEX some_name ON products (product_no)```

## Adding a FK Constraint
###### Microsoft SQL, MySQL, PostgreSQL
```ALTER TABLE products ADD CONSTRAINT some_name FOREIGN KEY (group_id) REFERENCES groups (id)```

## Adding a NotNull Constraint
###### Microsoft SQL
```ALTER TABLE product ALTER COLUMN product_no INT NOT NULL // not full field DDL```
###### MySQL
```ALTER TABLE product MODIFY COLUMN product_no INT NOT NULL DEFAULT 0 // full field DDL```
###### PostgreSQL
```ALTER TABLE products ALTER COLUMN product_no SET NOT NULL```

## Removing a NotNull Constraint
###### Microsoft SQL, MySQL
```ALTER TABLE products CHANGE product_no product_no INT NULL```
###### PostgreSQL
```ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL```

## Adding a Primary Key Constraint
###### Microsoft SQL, PostgreSQL
```ALTER TABLE products ADD CONSTRAINT PK_products PRIMARY KEY (id);```
###### MySQL
```ALTER TABLE products ADD PRIMARY KEY (id)```

## Removing a Primary Key Constraint
###### Microsoft SQL, PostgreSQL
```ALTER TABLE products DROP CONSTRAINT PK_products```
###### MySQL
```ALTER TABLE products DROP PRIMARY KEY```



## Set Field as Auto Increment
###### Microsoft SQL
```Not Available```
###### MySQL
```ALTER TABLE products CHANGE id id INT NOT NULL AUTO_INCREMENT```
###### PostgreSQL
```
DECLARE
    seq_name VARCHAR(100);
BEGIN
    SELECT pg_get_serial_sequence('product', 'id') INTO seq_name;
    IF seq_name IS NOT NULL THEN RAISE 'it is already auto increment'; END IF;

    CREATE SEQUENCE seq_product_id;
    SELECT setval('seq_product_id', max(id)) FROM products;
    ALTER TABLE products ALTER COLUMN id DROP DEFAULT;
    ALTER TABLE products ALTER COLUMN id SET DEFAULT NEXTVAL('seq_product_id');
END
```

## Removing Auto Increment
###### Microsoft SQL
```Not Available```
###### MySQL
```ALTER TABLE products CHANGE id id INT NOT NULL```
###### PostgreSQL
```
DECLARE
    seq_name VARCHAR(100);
BEGIN
    SELECT pg_get_serial_sequence('product', 'id') INTO seq_name;
    IF seq_name IS NULL THEN RAISE 'product.id is already NOT auto increment'; END IF;

    ALTER TABLE "product" ALTER COLUMN "id" DROP DEFAULT;
    EXECUTE 'DROP SEQUENCE ""$1""' USING seq_name;
END;
```



## Reading other indexes
###### Microsoft SQL Server
```EXEC sp_helpindex 'products'```
###### MySQL
```show index from products```
###### PostgreSQL
```
select
    t.relname as table_name,
    i.relname as index_name,
    array_to_string(array_agg(a.attname), ', ') as column_names
from
    pg_class t,
    pg_class i,
    pg_index ix,
    pg_attribute a
where
    t.oid = ix.indrelid
    and i.oid = ix.indexrelid
    and a.attrelid = t.oid
    and a.attnum = ANY(ix.indkey)
    and t.relkind = 'r'
    and t.relname like 'products%'
group by
    t.relname,
    i.relname
order by
    t.relname,
    i.relname;
```

## Adding an Index
###### Microsoft SQL, MySQL, PostgreSQL
```CREATE INDEX some_name ON products (product_no [ASC|DESC], ...)```

## Removing an Index
###### Microsoft SQL, MySQL
```DROP INDEX some_name ON products```
###### PostgreSQL
```DROP INDEX some_name```

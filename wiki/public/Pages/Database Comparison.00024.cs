Database Comparison
admin|2011/05/04 12:18:45
##PAGE##
===Renaming a Table===
Microsoft SQL
@@EXEC sp_rename products, items@@
MySQL, PostgreSQL
@@ALTER TABLE products RENAME TO items@@




===Adding a Column===
Microsoft SQL, MySQL, PostgreSQL
@@ALTER TABLE products ADD description text@@

===Removing a Column===
Microsoft SQL, MySQL, PostgreSQL
@@ALTER TABLE products DROP COLUMN description@@

===Renaming a Column===
Microsoft SQL
@@EXEC sp_rename @objname = 'products.product_no', @newname = 'product_number', @objtype = 'COLUMN'@@
MySQL
@@ALTER TABLE product CHANGE product_no product_number INT NOT NULL DEFAULT 0; // omit PRIMARY KEY and UNIQUE@@
PostgreSQL
@@ALTER TABLE products RENAME COLUMN product_no TO product_number@@

===Changing a Column's Data Type===
Microsoft SQL
@@ALTER TABLE products ALTER COLUMN price DECIMAL(10, 2)@@
MySQL
@@ALTER TABLE products MODIFY COLUMN price DECIMAL(10, 2)@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price TYPE numeric(10,2)@@




===Reading a Column's Default Value===
Microsoft SQL
@@SELECT * FROM sys.default_constraints@@
can be used for removing

===Changing a Column's Default Value===
Microsoft SQL
@@ALTER TABLE products ADD CONSTRAINT some_name DEFAULT 7.77 FOR price@@
MySQL, PostgreSQL
@@ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77@@

===Removing a Column's Default Value===
Microsoft SQL
@@ALTER TABLE products DROP CONSTRAINT default_name@@
MySQL, PostgreSQL
@@ALTER TABLE products ALTER COLUMN price DROP DEFAULT@@




===Reading Constraints===
Microsoft SQL Server, PostgreSQL
@@
// only primary key and unique
select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where table_name='contact' AND table_catalog='$=db.Name$'
select * from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where table_name='contact' AND table_catalog='$=db.Name$'
@@
MySQL
@@
select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where table_name='contact' AND table_schema='$=db.Name$';
select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_name='contact' AND table_schema='$=db.Name$';
@@

===Removing a Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@
MySQL
@@Not Available@@

===Adding a Check Constraint===
Microsoft SQL
@@ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '') // before add: WITH NOCHECK@@
MySQL
@@Not Available@@
PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '')@@

===Adding a Unique Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)@@
MySQL
@@CREATE UNIQUE INDEX some_name ON products (product_no)@@

===Adding a FK Constraint===
Microsoft SQL, MySQL, PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name FOREIGN KEY (group_id) REFERENCES groups (id)@@

===Adding a NotNull Constraint===
Microsoft SQL
@@ALTER TABLE product ALTER COLUMN product_no INT NOT NULL // not full field DDL@@
MySQL
@@ALTER TABLE product MODIFY COLUMN product_no INT NOT NULL DEFAULT 0 // full field DDL@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no SET NOT NULL@@

===Removing a NotNull Constraint===
Microsoft SQL, MySQL
@@ALTER TABLE products CHANGE product_no product_no INT NULL@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL@@

===Adding a Primary Key Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT PK_products PRIMARY KEY (id);@@
MySQL
@@ALTER TABLE products ADD PRIMARY KEY (id)@@

===Removing a Primary Key Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT PK_products@@
MySQL
@@ALTER TABLE products DROP PRIMARY KEY@@




===Set Field as Auto Increment===
Microsoft SQL
@@Not Available@@
MySQL
@@ALTER TABLE products CHANGE id id INT NOT NULL AUTO_INCREMENT@@
PostgreSQL
@@
DROP SEQUENCE IF EXISTS seq_product_id;
CREATE SEQUENCE seq_product_id;
ALTER TABLE products ALTER COLUMN id SET DEFAULT NEXTVAL('seq_product_id');
SELECT setval('seq_product_id', max(id)) FROM products;
@@

===Removing Auto Increment===
Microsoft SQL
@@Not Available@@
MySQL
@@ALTER TABLE products CHANGE id id INT NOT NULL@@
PostgreSQL
@@
ALTER TABLE products ALTER COLUMN id DROP DEFAULT;
DROP SEQUENCE IF EXISTS seq_product_id;
@@



===Adding an Index===
Microsoft SQL, MySQL, PostgreSQL
@@CREATE INDEX some_name ON products (product_no [ASC|DESC], ...)@@

===Removing an Index===
Microsoft SQL, MySQL
@@DROP INDEX some_name ON products@@
PostgreSQL
@@DROP INDEX some_name@@


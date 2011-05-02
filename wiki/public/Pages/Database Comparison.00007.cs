Database Comparison
admin|2011/05/01 21:43:22
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

===Changing a Column's Default Value===
Microsoft SQL
@@ALTER TABLE products ADD CONSTRAINT some_name DEFAULT 7.77 FOR price@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77@@

===Removing a Column's Default Value===
Microsoft SQL
@@?@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price DROP DEFAULT@@

===Changing a Column's Data Type===
Microsoft SQL
@@ALTER TABLE products ALTER COLUMN price DECIMAL(10, 2)@@
MySQL
@@ALTER TABLE products MODIFY COLUMN price DECIMAL(10, 2)@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price TYPE numeric(10,2)@@

===Renaming a Column===
Microsoft SQL
@@EXEC sp_rename @objname = 'products.product_no', @newname = 'product_number', @objtype = 'COLUMN'@@
PostgreSQL
@@ALTER TABLE products RENAME COLUMN product_no TO product_number@@

===Adding a Check Constraint===
Microsoft SQL
@@ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '') // before add: WITH NOCHECK@@
PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name CHECK (name <> '')@@

===Adding a Unique Constraint===
Microsoft SQL
@@ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)@@
PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)@@

===Adding a FK Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name FOREIGN KEY (group_id) REFERENCES groups (id)@@

===Adding a NotNull Constraint===
Microsoft SQL
@@?@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no SET NOT NULL@@

===Removing a Constraint===
Microsoft SQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@
PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Removing a NotNull Constraint===
Microsoft SQL
@@?@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL@@
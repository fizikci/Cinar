Database Comparison
admin|2011/05/02 17:46:30
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
MySQL, PostgreSQL
@@ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77@@

===Removing a Column's Default Value===
Microsoft SQL
@@ALTER TABLE products DROP CONSTRAINT default_name@@
MySQL, PostgreSQL
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
MySQL
@@ALTER TABLE product CHANGE product_no product_number INT NOT NULL DEFAULT 0;@@
PostgreSQL
@@ALTER TABLE products RENAME COLUMN product_no TO product_number@@

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
@@Not Available@@

===Adding a FK Constraint===
Microsoft SQL, MySQL, PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name FOREIGN KEY (group_id) REFERENCES groups (id)@@

===Adding a NotNull Constraint===
Microsoft SQL
@@ALTER TABLE product ALTER COLUMN product_no product_number INT NOT NULL DEFAULT 0@@
MySQL
@@ALTER TABLE product CHANGE product_no product_number INT NOT NULL DEFAULT 0@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no SET NOT NULL@@

===Removing a NotNull Constraint===
Microsoft SQL, MySQL
@@ALTER TABLE products CHANGE product_no product_no INT NULL@@
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL@@

===Removing a Constraint===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Set Field as Primary Key===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Removing Primary Key===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Set Field as Auto Increment===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Removing Auto Increment===
Microsoft SQL, PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

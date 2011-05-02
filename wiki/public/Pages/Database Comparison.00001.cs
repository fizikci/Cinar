Database Comparison
admin|2011/05/01 20:37:09
##PAGE##
===Adding a Column===
Microsoft SQL
@@ALTER TABLE dnm ADD ad text@@
PostgreSQL
@@ALTER TABLE dnm ADD COLUMN ad text@@

===Removing a Column===
PostgreSQL
@@ALTER TABLE dnm DROP COLUMN ad@@

===Adding a Check Constraint===
PostgreSQL
@@ALTER TABLE products ADD CHECK (name <> '')@@

===Adding a Unique Constraint===
PostgreSQL
@@ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)@@

===Adding a FK Constraint===
PostgreSQL
@@ALTER TABLE products ADD FOREIGN KEY (product_group_id) REFERENCES product_groups@@

===Adding a NotNull Constraint===
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no SET NOT NULL@@

===Removing a Constraint===
PostgreSQL
@@ALTER TABLE products DROP CONSTRAINT some_name@@

===Removing a NotNull Constraint===
PostgreSQL
@@ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL@@

===Changing a Column's Default Value===
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77@@

===Removing a Column's Default Value===
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price DROP DEFAULT@@

===Changing a Column's Data Type===
PostgreSQL
@@ALTER TABLE products ALTER COLUMN price TYPE numeric(10,2)v

===Renaming a Column===
PostgreSQL
@@ALTER TABLE products RENAME COLUMN product_no TO product_number@@

===Renaming a Table===
ALTER TABLE products RENAME TO items@@
Database Comparison
admin|2011/05/01 20:30:49
##PAGE##
<table>
<tr><td>Operation</td><td>MS SQL Server</td><td>PostgreSQL</td></tr>
<tr><td>Adding a Column</td><td>ALTER TABLE dnm ADD ad text</td><td>ALTER TABLE dnm ADD COLUMN ad text</td></tr>
<tr><td>Removing a Column</td><td></td><td>ALTER TABLE dnm DROP COLUMN ad</td></tr>
<tr><td>Adding a Check Constraint</td><td></td><td>ALTER TABLE products ADD CHECK (name <> '')</td></tr>
<tr><td>Adding a Unique Constraint</td><td></td><td>ALTER TABLE products ADD CONSTRAINT some_name UNIQUE (product_no)</td></tr>
<tr><td>Adding a FK Constraint</td><td></td><td>ALTER TABLE products ADD FOREIGN KEY (product_group_id) REFERENCES product_groups</td></tr>
<tr><td>Adding a NotNull Constraint</td><td></td><td>ALTER TABLE products ALTER COLUMN product_no SET NOT NULL</td></tr>
<tr><td>Removing a Constraint</td><td></td><td>ALTER TABLE products DROP CONSTRAINT some_name</td></tr>
<tr><td>Removing a NotNull Constraint</td><td></td><td>ALTER TABLE products ALTER COLUMN product_no DROP NOT NULL</td></tr>
<tr><td>Changing a Column's Default Value</td><td></td><td>ALTER TABLE products ALTER COLUMN price SET DEFAULT 7.77</td></tr>
<tr><td>Removing a Column's Default Value</td><td></td><td>ALTER TABLE products ALTER COLUMN price DROP DEFAULT</td></tr>
<tr><td>Changing a Column's Data Type</td><td></td><td>ALTER TABLE products ALTER COLUMN price TYPE numeric(10,2)</td></tr>
<tr><td>Renaming a Column</td><td></td><td>ALTER TABLE products RENAME COLUMN product_no TO product_number</td></tr>
<tr><td>Renaming a Table</td><td></td><td>ALTER TABLE products RENAME TO items</td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
<tr><td></td><td></td><td></td></tr>
</table>
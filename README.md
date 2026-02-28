
# Cinar Database Library and Tools

## Overview

Cinar is a comprehensive, database-centric library that includes a powerful framework named `Cinar.Database` and an SQL client, `Cinar.DBTools`. These tools were created based on over 20 years of software development experience, providing a range of functionalities tailored to streamline and enhance database management.

---

## Cinar Database

### Introduction

The `Cinar.Database` library brings an enriched experience to database handling by simplifying complex tasks, making database management both efficient and enjoyable.

### Key Features

- **Database Abstraction**: Supports multiple databases, including MySQL, MS SQL Server, PostgreSQL, and SQLite, enabling smooth cross-platform integration.
- **Database Metadata Access**: Provides detailed access to database metadata, including tables, fields, and indexes.
- **SQL Generation**: Automatically generates essential SQL scripts, like `CREATE TABLE`, `INSERT`, and `UPDATE`, based on metadata, significantly accelerating development.
- **Transaction Management**: Supports both transactional and non-transactional operations, crucial for web applications, with built-in support for lambda expressions in transactions to automate commits or rollbacks.
- **Logging**: Logs SQL queries to aid in debugging and monitoring.
- **Caching**: Contains a caching system that enhances response handling, especially for web applications.
- **Object-Relational Mapping (ORM)**:
  - Automatically maps classes to database tables by considering public properties with `{get; set;}` signatures as table fields.
  - Allows additional metadata through attributes like `TableName`, `FieldName`, `FieldType`, `IsNotNull`, and `DefaultValue`.
  - Supports automatic runtime creation of tables if they are not present.
  - Facilitates default data insertion in auto-created tables using `DefaultData` attributes.
  - Integrates with existing class models by implementing the `IDatabaseEntity` interface.
- **Table-Oriented Operations**: Alongside ORM, it supports working with `DataSet` and `Hashtable` for flexibility.

### Code Examples

#### Connecting to a Database
```csharp
Database db = new Database(Provider.MySQL, host, dbName, userName, password, 30);
Database db = new Database(Provider.PostgreSQL, host, dbName, userName, password, 30);
```
This simplifies connections without needing to remember the syntax for different databases.

#### Simple Queries
```csharp
string version = db.GetString("select version()");
DataTable dt = db.GetDataTable("select Id, Name from Person");
```

#### Insert
```csharp
Hashtable record = new Hashtable(); 
record["Name"] = "Ahmet";
db.Insert("Person", record);
```
Alternatively:
```csharp
var record = new { Name = "Ahmet" };
db.Insert("Person", record);
```
The `Insert` method builds the SQL `INSERT` query automatically.

#### Update
```csharp
DataRow dr = db.GetDataRow("select * from Person where Id=1");
dr["Name"] = "Zeynep";
db.Update("Person", dr);
```

#### Metadata Access
```csharp
foreach(Table tbl in db.Tables) {
    Console.WriteLine("Table name: " + tbl.Name);
    Console.WriteLine("Field count: " + tbl.Fields.Count);
    Console.WriteLine("Primary key: " + tbl.PrimaryField);
}
Table customerTable = db.Tables["Customer"];
if (customerTable == null) {
    Console.WriteLine("Table not found!");
} else {
    foreach(Field fld in customerTable.Fields) {
        Console.WriteLine("Field name: " + fld.Name);
        Console.WriteLine("Type: " + fld.FieldType);
    }
}
```

---

## Cinar Database Tools

### Overview

`Cinar.DBTools` is designed to simplify database management with a broad range of functions for database professionals.

### Key Features

- **Unified Interface**: Works seamlessly with Microsoft SQL, MySQL, and PostgreSQL.
- **Object Management**: Lists, modifies, and adds database objects like tables, views, constraints, and indexes.
- **Data Management**: Facilitates listing, updating, and deleting records.
- **Cross-Database Transfer**: Allows schema and data transfer between databases (e.g., MySQL to MS SQL).
- **Backup Support**: Creates SQL dumps in formats compatible with supported databases.
- **Advanced SQL Editor**: Includes syntax highlighting and code completion.
- **Fast Scripting with Cinar Script**: Supports complex query generation.
- **Database Discovery Tools**: Offers tools to easily explore and understand an existing database.
- **Diagram Support**: Allows users to create database diagrams.
- **Normalization Testing**: Assists in testing and correcting database normalization.
- **Table Generation**: Helps create tables that align with classes in a .NET DLL.
- **Scheduled Tasks**: Enables creating tasks for database replication and more.
- **Database Comparison**: Provides a tool to compare two databases (diff-style).
- **Extended Metadata**: Allows for adding extra metadata to databases.
- **Code Generation Projects**: Supports creating projects for code generation.
- **Familiar Interface**: Provides an interface similar to Visual Studio, making it user-friendly for .NET developers.

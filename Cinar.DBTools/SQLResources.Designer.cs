﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinar.DBTools {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SQLResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SQLResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cinar.DBTools.SQLResources", typeof(SQLResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Class {
            get {
                object obj = ResourceManager.GetObject("Class", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Interface {
            get {
                object obj = ResourceManager.GetObject("Interface", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap LocalVariable {
            get {
                object obj = ResourceManager.GetObject("LocalVariable", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Method {
            get {
                object obj = ResourceManager.GetObject("Method", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Property {
            get {
                object obj = ResourceManager.GetObject("Property", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot;?&gt;
        ///
        ///&lt;SyntaxDefinition name =&quot;SQL&quot; extensions = &quot;.sql&quot;&gt;
        ///	
        ///	&lt;Properties&gt;
        ///		&lt;Property name=&quot;LineComment&quot; value=&quot;--&quot;/&gt;
        ///	&lt;/Properties&gt;
        ///
        ///	&lt;Digits name =&quot;Digits&quot; bold =&quot;false&quot; italic =&quot;false&quot; color =&quot;DarkBlue&quot;/&gt;
        ///
        ///	&lt;RuleSets&gt;
        ///		&lt;RuleSet ignorecase = &quot;true&quot;&gt;
        ///			&lt;Delimiters&gt;=!&amp;gt;&amp;lt;+-/*%&amp;amp;|^~.}{,;][?:()&lt;/Delimiters&gt;
        ///			
        ///			&lt;Span name =&quot;LineComment&quot; bold =&quot;false&quot; italic =&quot;false&quot; color =&quot;DarkSlateGray&quot; stopateol =&quot;true&quot;&gt;
        ///				&lt;Begin&gt;-- &lt;/Begin&gt;
        ///			&lt;/Span&gt;
        ///					
        ///			&lt;Span name =&quot;B [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SQL {
            get {
                return ResourceManager.GetString("SQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*
        ///This &quot;query by script&quot; calculates optimal data lengths for the (var)char columns.
        ///To achive this, it finds the max length of the data and compare it to the defined length.
        ///This is useful to decrease both memory &amp; disk consumption.
        ///*/
        ///$
        ///bool firstUnionSkipped = false;
        ///foreach(var t in db.Tables)
        ///	foreach(var f in t.Columns){
        ///		if(f.IsStringType()==false) continue;
        ///		if(firstUnionSkipped) echo(&quot;UNION&quot;);
        ///		firstUnionSkipped = true;
        ///		$
        ///		select &apos;$=t.Name$&apos; as table_name, &apos;$=f.Name$&apos; as column_n [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SQLCalculateOptimalDataLength {
            get {
                return ResourceManager.GetString("SQLCalculateOptimalDataLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///var dt = db.GetDataTable(&quot;DECLARE @collate SYSNAME
        ///SELECT @collate = &apos;Turkish_CI_AS&apos;
        ///
        ///SELECT 
        ///    &apos;ALTER TABLE [&apos; + SCHEMA_NAME(o.[schema_id]) + &apos;].[&apos; + o.name + &apos;]
        ///        ALTER COLUMN [&apos; + c.name + &apos;] &apos; +
        ///        UPPER(t.name) + 
        ///        CASE WHEN t.name NOT IN (&apos;ntext&apos;, &apos;text&apos;) 
        ///            THEN &apos;(&apos; + 
        ///                CASE 
        ///                    WHEN t.name IN (&apos;nchar&apos;, &apos;nvarchar&apos;) AND c.max_length != -1 
        ///                        THEN CAST(c.max_length / 2 AS VARCHAR(10))
        ///                     [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SQLChangeCollationSQLServer {
            get {
                return ResourceManager.GetString("SQLChangeCollationSQLServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///var db2 = Provider.GetConnection(&quot;DealerSafe (232.124 SQLServer)&quot;).Database;
        ///
        ///foreach(var t in db.Tables){
        ///	if(db2.Tables[t.Name]==null){
        ///		echo(&quot;Fazla &quot;+(t.IsView ? &quot;view&quot;:&quot;tablo&quot;)+&quot;: &quot; + t.Name + &quot;\r\n&quot;);
        ///		continue;
        ///	}
        ///	foreach(var c in t.Columns){
        ///		if(db2.Tables[t.Name].Columns[c.Name]==null)
        ///			echo(&quot;Fazla alan: &quot; + t.Name + &quot;.&quot;+c.Name+&quot;\r\n&quot;);
        ///	}	
        ///}
        ///
        ///foreach(var t in db2.Tables){
        ///	if(db.Tables[t.Name]==null){
        ///		echo(&quot;Eksik &quot;+(t.IsView ? &quot;view&quot;:&quot;tablo&quot;)+&quot;: &quot; + t.Name + &quot;\r\n&quot;);
        ///		co [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SQLCompareDatabases {
            get {
                return ResourceManager.GetString("SQLCompareDatabases", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELIMITER $$
        ///
        ///CREATE FUNCTION &lt;function_name&gt;()
        ///    RETURNS TYPE
        ///    BEGIN
        ///
        ///    END$$
        ///
        ///DELIMITER ;.
        /// </summary>
        internal static string SQLCreateFunctionMySQL {
            get {
                return ResourceManager.GetString("SQLCreateFunctionMySQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE FUNCTION function_name
        ///  (input_parameter_name datatype, ...)
        ///RETURNS datatype
        ///AS $$
        ///DECLARE
        ///  variable_name datatype;
        ///BEGIN
        ///  /* SQL code */
        ///END;
        ///$$ LANGUAGE plpgsql;.
        /// </summary>
        internal static string SQLCreateFunctionPostgreSQL {
            get {
                return ResourceManager.GetString("SQLCreateFunctionPostgreSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELIMITER $$
        ///
        ///CREATE PROCEDURE &lt;procedure_name&gt;()
        ///    BEGIN
        ///
        ///    END$$
        ///
        ///DELIMITER ;.
        /// </summary>
        internal static string SQLCreateSProcMySQL {
            get {
                return ResourceManager.GetString("SQLCreateSProcMySQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE FUNCTION function_name
        ///  (input_parameter_name datatype, ...)
        ///RETURNS return_type
        ///AS $$
        ///DECLARE
        ///  variable_name datatype;
        ///BEGIN
        ///  /* SQL code */
        ///END;
        ///$$ LANGUAGE plpgsql;.
        /// </summary>
        internal static string SQLCreateSProcPostgreSQL {
            get {
                return ResourceManager.GetString("SQLCreateSProcPostgreSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE PROCEDURE nameprocedure
        ///  (input_parameter_name datatype, ... )
        ///AS
        ///  /* SQL code */
        ///GO.
        /// </summary>
        internal static string SQLCreateSProcSQLServer {
            get {
                return ResourceManager.GetString("SQLCreateSProcSQLServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELIMITER $$
        ///
        ///CREATE
        ///    TRIGGER &lt;trigger_name&gt; BEFORE/AFTER INSERT/UPDATE/DELETE
        ///    ON &lt;table_name&gt;
        ///    FOR EACH ROW BEGIN
        ///
        ///    END$$
        ///
        ///DELIMITER ;.
        /// </summary>
        internal static string SQLCreateTriggerMySQL {
            get {
                return ResourceManager.GetString("SQLCreateTriggerMySQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE VIEW &lt;view_name&gt; 
        ///    AS
        ///(SELECT * FROM ...);.
        /// </summary>
        internal static string SQLCreateViewMySQL {
            get {
                return ResourceManager.GetString("SQLCreateViewMySQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///var dt = db.GetDataTable(&quot;_select_statement_&quot;);
        ///echo(Utility.ToStringTable(dt));
        ///$.
        /// </summary>
        internal static string SQLDataTableToStringTable {
            get {
                return ResourceManager.GetString("SQLDataTableToStringTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(table in db.Tables)
        ///    echo(&apos;truncate table [&apos; + table.Name + &apos;];\r\n&apos;);
        ///$.
        /// </summary>
        internal static string SQLDeleteFromTables {
            get {
                return ResourceManager.GetString("SQLDeleteFromTables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(table in db.Tables)
        ///	foreach(c in table.Constraints)
        ///		if(c.Type == &apos;ForeignKey&apos;)
        ///    		echo(db.GetSQLConstraintRemove(c) + &apos;;\r\n&apos;);
        ///$.
        /// </summary>
        internal static string SQLDropAllForeignKeys {
            get {
                return ResourceManager.GetString("SQLDropAllForeignKeys", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(var t in db.Tables)
        ///	foreach(var i in t.Constraints)
        ///		if(i.Type==&quot;ForeignKey&quot;)
        ///		{
        ///			var x = t.Name+&quot;.FK_&quot;+t.Name+&quot;_&quot;+i.ConstraintColumnNames+&quot;_&quot;+i.RefTableName+&quot; (&quot;+i.ConstraintColumnNames+&quot;)&quot;;
        ///			var y = i.ToString();
        ///			
        ///			if(x!=y) echo(x+&quot;\r\n&quot;+y+&quot;\r\n\r\n&quot;);
        ///		}
        ///$.
        /// </summary>
        internal static string SQLFindIncorrectForeignKeys {
            get {
                return ResourceManager.GetString("SQLFindIncorrectForeignKeys", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(var t in db.Tables){
        ///	if(!t.IsView){
        ///		foreach(var c in t.Columns){
        ///			if(c.Name.Contains(&apos;field_name_to_search&apos;)){
        ///	    		echo(t.Name + &apos;.&apos; + c.Name + &apos;\r\n&apos;);
        ///	    	}
        ///	    }
        ///	}
        ///}
        ///$.
        /// </summary>
        internal static string SQLForEachColumn {
            get {
                return ResourceManager.GetString("SQLForEachColumn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(table in db.Tables)
        ///    echo(table.Name + &apos;\r\n&apos;);
        ///$.
        /// </summary>
        internal static string SQLForEachTable {
            get {
                return ResourceManager.GetString("SQLForEachTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(var t in db.Tables)
        ///	foreach(var f in t.Columns){
        ///		if(f.IsStringType()==false) continue;
        ///$
        ///select &apos;$=t.Name$&apos; as table_name, &apos;$=f.Name$&apos; as column_name, Id, $=f.Name$ as column_val from $=t.Name$ 
        ///where ($=f.Name$ like &apos;%WRITE_SEARCH_KEYWORD_HERE%&apos;)
        ///UNION
        ///$
        ///	}
        ///$
        ///order by table_name, column_name.
        /// </summary>
        internal static string SQLSearhAllStringFields {
            get {
                return ResourceManager.GetString("SQLSearhAllStringFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///for(int i=0; i&lt;db.Tables.Count; i++)
        ///{
        ///	var table = db.Tables[i];
        ///    echo(&quot;select &apos;&quot; + table.Name + &quot;&apos;, count(*) from [&quot; + table.Name + &quot;]&quot;);
        ///    if(i &lt; db.Tables.Count-1)
        ///		echo(&quot;&quot; UNION \r\n&quot;&quot;);
        ///}
        ///$.
        /// </summary>
        internal static string SQLSelectCountsFromTables {
            get {
                return ResourceManager.GetString("SQLSelectCountsFromTables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///foreach(var t in db.Tables)
        ///	if(t.IsView){
        ///		foreach(var dr in db.GetDataTable(&quot;sp_helptext &apos;&quot;+t.Name+&quot;&apos;&quot;).Rows)
        ///			echo(dr[0]);
        ///		echo(&apos;GO\r\n\r\n\r\n&apos;);
        ///	}
        ///$.
        /// </summary>
        internal static string SQLSPHelpText {
            get {
                return ResourceManager.GetString("SQLSPHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $
        ///var limit = 1000;
        ///var tableName = &quot;synonym&quot;;
        ///var dt = db.GetDataTable(&quot;select Id, Word1, Word2 from &quot;+tableName+&quot; order by 1 limit &quot;+limit+&quot; offset 0&quot;);
        ///var bulk = 0;
        ///
        ///while(dt!=null &amp;&amp; dt.Rows.Count&gt;0){
        ///	echo(&quot;insert into &quot;+tableName+&quot; values &quot;);
        ///	for(int i=0; i&lt;dt.Rows.Count; i++){
        ///		var dr = dt.Rows[i];
        ///		echo(&quot;(&quot;+dr.Id+&quot;, &apos;&quot;+dr.Word1+&quot;&apos;, &apos;&quot;+dr.Word2+&quot;&apos;)&quot;);
        ///		if(i&lt;dt.Rows.Count-1) echo(&apos;, &apos;);
        ///	}
        ///	echo(&quot;;\r\n&quot;);
        ///	bulk++;
        ///	dt = db.GetDataTable(&quot;select Id, Word1, Word2 from &quot;+tableName+&quot; or [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SQLTransferTableSQLGenerator {
            get {
                return ResourceManager.GetString("SQLTransferTableSQLGenerator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Struct {
            get {
                object obj = ResourceManager.GetObject("Struct", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}

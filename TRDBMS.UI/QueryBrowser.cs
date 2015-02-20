﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TRDBMS.Implementation;

namespace TRDBMS.UI
{
    public partial class QueryBrowser : Form
    {
        public QueryBrowser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CREATE table (field type{, field type}) 
            string tableName = "Employee"; // tablename
            TableDefinition employee = new TableDefinition(tableName); //Initialize the table definition with the table name and add the fields.

            //Dictionary of fields and datatype pairs defining the schema of the table
            employee.AddField("Id", 'I'); 
            employee.AddField("Name", 'S');
            employee.AddField("Designation", 'S');

            try
            {
                //A directory is created if does not exist, a data dictionary is created if does not exist, entry for the created table is added 
                //and a file for the table is created in the same directory.
                //Take a look at Constants.cs for the directory name and PathUtil.cs for the path for directory.
                SchemaManager.CreateTable(employee); 
            }
            catch (Exception)
            {
            }

            //Get table definition
            TableDefinition definition = SchemaManager.GetTableDefinition("Employee");

            //Insert values into table
            //INSERT INTO table (value{, value}) 
            //Create Data Access manager for a table that accepts table name in a constructor.
            TableDataAccessManager dataAccessManager = new TableDataAccessManager("Employee");

            //Create a record that has to be added in the table
            List<string> values = new List<string>();
            values.Add("1");
            values.Add("John");
            values.Add("Senior Software Developer");

            //Inserting the created record into the table
            dataAccessManager.Insert(values);

            //Create another record that has to be added in the table
            values = new List<string>();
            values.Add("2");
            values.Add("Swara");
            values.Add("Software Engineer");

            //Inserting the created record into the table
            dataAccessManager.Insert(values);

            /*ReadData takes input parameters as (List<string> fields, Dictionary<string, string> fieldConst) is a generic function for query types:
             1.SELECT * FROM table
             2.SELECT field {, field} FROM table
             3.SELECT * FROM table WHERE field1 = constant1, field2 = constant2,...
             4.SELECT field {, field} FROM table WHERE field1 = constant1, field2 = constant2,...*/

            // 1.SELECT * FROM table 
            //Pass null as parameters to select the entire table.
            List<List<string>> lst = dataAccessManager.ReadData(null,null);

            //2.SELECT field {, field} FROM table
            //Pass fields and null as parameters to ReadData to select a list of fields from the table.
            List<string> fields = new List<string>();
            fields.Add("Id");
            fields.Add("Name");
            List<List<string>> fieldList = dataAccessManager.ReadData(fields, null);

            //3.SELECT * FROM table WHERE field1 = constant1, field2 = constant2,...
            //Pass null and filters as parameters to ReadData to select all fields from table satisfing the WHERE conditions.
            Dictionary<string, string> fieldConst = new Dictionary<string,string>();
            fieldConst.Add("Name","John");
            List<List<string>> filtersConditions = dataAccessManager.ReadData(null,fieldConst);

            //4.SELECT field {, field} FROM table WHERE field1 = constant1, field2 = constant2,...*/
            //Pass list of fields and filters as parameters to ReadData function to select the list fields from table satisfing the WHERE conditions.
            List<List<string>> fieldsWithfilterConditions = dataAccessManager.ReadData(fields, fieldConst);




        }
    }
}

using System;
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
            string tableName = "Employee";
            TableDefinition employee = new TableDefinition(tableName);
            employee.AddField("Id", 'I');
            employee.AddField("Name", 'S');
            employee.AddField("Designation", 'S');

            try
            {
                SchemaManager.CreateTable(employee);
            }
            catch (Exception)
            {
            }

            TableDefinition definition = SchemaManager.GetTableDefinition("Employee");

            TableDataAccessManager dataAccessManager = new TableDataAccessManager("Employee");

            List<string> values = new List<string>();
            values.Add("1");
            values.Add("Manik");
            values.Add("Senior Software Developer");

            dataAccessManager.Insert(values);


            values = new List<string>();
            values.Add("2");
            values.Add("Swara");
            values.Add("Software Engineer");

            dataAccessManager.Insert(values);

            List<List<string>> lst = dataAccessManager.ReadAllData();
        }
    }
}

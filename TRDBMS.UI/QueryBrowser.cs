using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TRDBMS.Implementation;
using TRDBMS.Implementation.Commands;

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
            string query = QueryTextBox.Text;
            string[] sqlSplit = query.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            lblErrorQuery.Text = string.Empty;
            lblErrorText.Text = string.Empty;

            foreach (string query1 in sqlSplit)
            {
                try
                {
                    CommandBase cmdBase = QueryManager.GetCommand(query1);
                    List<List<string>> resultList = cmdBase.ExecuteCommand();
                    displayGridView.DataSource=ListExtentions.ToDataTable(resultList);
                }

                catch(Exception err)
                {
                    lblErrorQuery.Text = query1;
                    lblErrorText.Text = err.Message;
                }
            }

        }

    }
}

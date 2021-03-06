﻿using System;
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
    public enum StatusType
    {
        SUCCESS = 0,
        ERROR = 1
    }
    public partial class QueryBrowser : Form
    {
        List<Control> ctrls;
        public QueryBrowser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrls = new List<Control>();
            panel1.Controls.Clear();
            string query = QueryTextBox.Text;
            string[] sqlSplit = query.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string query1 in sqlSplit)
            {
                ExecuteQuery(query1);
            }
            Render();
        }

        private void ExecuteQuery(String query)
        {
            CommandBase cmdBase = null;
            try
            {
                cmdBase = QueryManager.GetCommand(query);
                List<List<string>> resultList = cmdBase.ExecuteCommand();
                if (!cmdBase.IsNonQuery)
                {
                    AddControls(cmdBase, resultList);
                }
                else
                {
                    String statusMessage = "Command executed successsfully";
                    AddControls(cmdBase, statusMessage, StatusType.SUCCESS);
                }
            }
            catch (Exception err)
            {
                AddControls(cmdBase == null ? new NullCommand(query) : cmdBase, err.Message, StatusType.ERROR);
            }

        }

        private void Render()
        {
            ctrls.Reverse();
            panel1.Controls.AddRange(ctrls.ToArray());

        }

        private void AddControls(CommandBase cmdBase, List<List<string>> resultList)
        {
            Label lblQuery = new Label();
            DataGridView resultTable = new DataGridView();
            lblQuery.AutoSize = true;
            lblQuery.Dock = DockStyle.Top;
            lblQuery.Text = cmdBase.GetQuery();
            lblQuery.Font = new Font("Arial",9);
            resultTable.DataSource = ListExtentions.ToDataTable(resultList,new List<string>( cmdBase.GetFieldNames()));
            resultTable.DataBindingComplete += resultTable_DataBindingComplete;
            resultTable.AutoSize = false;
            resultTable.Dock = DockStyle.Top;
            resultTable.Height = 300;
            resultTable.Width = 630;
            resultTable.AllowUserToAddRows = false;
            Label lblNewLine = new Label();
            lblNewLine.AutoSize = true;
            lblNewLine.Dock = DockStyle.Top;
            lblNewLine.Text = "----------------------------------------------------------------------------------------------";
            ctrls.Add(lblQuery);
            ctrls.Add(resultTable);
            ctrls.Add(lblNewLine);
        }

        void resultTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).AutoResizeColumns();
            ((DataGridView)sender).AutoResizeColumnHeadersHeight();
        }

        private void AddControls(CommandBase cmdBase, String message, StatusType type)
        {
            Label lblMessage = new Label();
            Label lblQuery = new Label();
            if (type == StatusType.SUCCESS)
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            else
                lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = message;
            lblMessage.Font = new Font("Arial", 9);
            lblQuery.Font = new Font("Arial", 9);
            lblQuery.Text = cmdBase.GetQuery();
            lblQuery.AutoSize = true;
            lblQuery.Dock = DockStyle.Top;
            lblMessage.AutoSize = true;
            lblMessage.Dock = DockStyle.Top;
            Label lblNewLine = new Label();
            lblNewLine.AutoSize = true;
            lblNewLine.Dock = DockStyle.Top;
            lblNewLine.Text = "----------------------------------------------------------------------------------------------";
            ctrls.Add(lblQuery);
            ctrls.Add(lblMessage);
             ctrls.Add(lblNewLine);
       }

    }
}

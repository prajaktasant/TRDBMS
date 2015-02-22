using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    class SqlParse
    {
        class SqlParse
        {
            string checkSql;
            //public SqlParse(string sql)         //construtor get the user input
            //{
            //    checkSql = sql;
            //}
            public void Parse(string sql)
            {
                checkSql = sql;
                switch (checkSql[0])             // category the query to create, insert, select
                {
                    case 'C':
                        checkCreate(checkSql);
                        break;
                    case 'I':
                        checkInsert(checkSql);
                        break;
                    case 'S':
                        checkSelect(checkSql);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            }

            public Boolean checkCreate(string sqlQuery)     //function for checking create statement
            {

                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                //for (int i = 0; i < sqlSplit.Length; i++)
                //{
                //    Console.Write(sqlSplit[i]);
                //}
                // store table name field name and type
                CreateData createNew = new CreateData();
                createNew.tableName = sqlSplit[1];
                for (int i = 2; i < sqlSplit.Length; i = i + 2)
                {
                    createNew.setFieldData(sqlSplit[i], sqlSplit[i + 1]);
                }
                return true;

            }


            public Boolean checkInsert(string sqlQuery)     //function for checking insert statement
            {
                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                InsertData insertNew = new InsertData();
                insertNew.tableName = sqlSplit[2];
                for (int i = 3; i < sqlSplit.Length; i++)
                {
                    insertNew.setValueList(sqlSplit[i]);
                }
                return true;
            }

            public Boolean checkSelect(string sqlQuery)         //function for checking select statement
            {
                string[] sqlSplit = checkSql.Split(new char[5] { ' ', '(', ',', ')', '=' }, StringSplitOptions.RemoveEmptyEntries);
                //for (int i = 0; i < sqlSplit.Length; i++)
                //{
                //    Console.Write(sqlSplit[i]);
                //}
                SelectData selectNew = new SelectData();
                if (sqlSplit[1] != "*")       // case "SELECT field FROM table"
                {
                    selectNew.oneField = sqlSplit[1];   //set the field  
                    selectNew.oneTable = sqlSplit[3];   // set the table
                }
                else if (sqlSplit[4] == "WHERE") // case "SELECT * FROM table WHERE field = constant"
                {
                    selectNew.tableName = sqlSplit[3];

                    selectNew.setForOneTable(sqlSplit[5], sqlSplit[6]);

                }
                else
                {
                    selectNew.setForTwoTable(sqlSplit[3], sqlSplit[4], sqlSplit[6], sqlSplit[7]);
                }
                return true;
            }
        }
    }
}

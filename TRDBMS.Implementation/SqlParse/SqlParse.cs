﻿using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.Commands;

namespace TRDBMS.Implementation.SqlParse
{
    public class SqlParse
    {
           string checkSql;
           public SqlParse(string sql)         //construtor get the user input
           {
               checkSql = sql.Trim();
           }
           public CommandBase Parse()
            {
                if(checkSql.StartsWith("CREATE"))
                {
                    return new CreateCommand(checkCreate(checkSql));

                }
                else if (checkSql.StartsWith("INSERT"))
                {
                    return new InsertCommand(checkInsert(checkSql));

                }
               else if (checkSql.StartsWith("SELECT"))
                {
                    Object obj = checkSelect(checkSql);
                    if (obj is SelectData)
                        return new SelectCommand(obj as SelectData);
                    else if (obj is JoinData)
                        return new JoinCommand(obj as JoinData);
                    else
                        throw new InvalidOperationException();

                }
                else
                {
                    throw new Exception("Invalid Query");                        

                }
            }

           public CreateData checkCreate(string sqlQuery)     //function for checking create statement
            {
               
                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                CreateData createNew = new CreateData();
                createNew.tableName = sqlSplit[1];
                for (int i = 2; i < sqlSplit.Length; i = i + 2)
                {
                    createNew.setFieldData(sqlSplit[i].Trim(), sqlSplit[i + 1].Trim());
                }
                return createNew;

            }


           public InsertData checkInsert(string sqlQuery)     //function for checking insert statement
            {
               
                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                InsertData insertNew = new InsertData();
                insertNew.tableName = sqlSplit[2];
                for (int i = 3; i < sqlSplit.Length; i++)
                {
                    insertNew.setValueList(sqlSplit[i].Replace("\"", "").Trim());
                }
                return insertNew;
            }

           public Object checkSelect(string sqlQuery)         //function for checking select statement
            {
                
                string[] sqlSplit = checkSql.Split(new char[5] { ' ', '(', ',', ')', '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (sqlSplit[1] != "*")       // case "SELECT field FROM table"
                {
                    SelectData selectNew = new SelectData();
                    string fields = sqlQuery.Substring(6, sqlQuery.IndexOf("FROM")-6);
                    string[] fieldSplit = fields.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for(int i = 0; i<fieldSplit.Length; i++)
                    {
                        selectNew.AddField(fieldSplit[i].Trim());

                    }

                    selectNew.tableName = sqlQuery.Substring(sqlQuery.IndexOf("FROM") + 4, sqlQuery.Length - (sqlQuery.IndexOf("FROM") + 4)).Trim();
                    return selectNew;
                }
                else if (sqlQuery.Contains("WHERE")) // case "SELECT * FROM table WHERE field = constant"
                {
                    if(sqlQuery.Contains(","))
                    {
                        JoinData joinData = new JoinData();

                        string tablenames = sqlQuery.Substring(sqlQuery.IndexOf("FROM") + 4, sqlQuery.IndexOf("WHERE") - (sqlQuery.IndexOf("FROM") + 4));
                        string[] tableSplit = tablenames.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if(tableSplit.Length!=2)
                        {
                            throw new Exception("Invalid table count in select");
                        }
                        joinData.tableName1 = tableSplit[0].Trim();
                        joinData.tableName2 = tableSplit[1].Trim();

                        string filters = sqlQuery.Substring(sqlQuery.IndexOf("WHERE") + 5, sqlQuery.Length - (sqlQuery.IndexOf("WHERE") + 5));
                        string[] filterSplit = filters.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (filterSplit.Length != 2)
                        {
                            throw new Exception("Invalid where clause in select");
                        }
                        joinData.field1 = filterSplit[0].Trim();
                        joinData.field2 = filterSplit[1].Trim();

                        return joinData;

                    }
                    
                    else
                    {
                        SelectData selectWhereClause = new SelectData();
                        string filters = sqlQuery.Substring(sqlQuery.IndexOf("WHERE") + 5, sqlQuery.Length - (sqlQuery.IndexOf("WHERE") + 5));
                        string[] filterSplit = filters.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (filterSplit.Length != 2)
                        {
                            throw new Exception("Invalid where clause in select");
                        }
                        selectWhereClause.AddFilters(filterSplit[0].Trim(), filterSplit[1].Replace("\"", "").Trim());
                        selectWhereClause.tableName = sqlSplit[3].Trim();
                        return selectWhereClause;
                    }
                

                }
                else
                {
                    throw new InvalidOperationException();
                }
               
            }
        }
}

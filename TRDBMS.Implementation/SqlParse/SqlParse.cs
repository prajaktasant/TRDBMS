using System;
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
                if(checkSql.StartsWith("CREATE"))               // "create" sql statement
                {
                    return new CreateCommand(checkCreate(checkSql),checkSql);

                }
                else if (checkSql.StartsWith("INSERT"))         // "insert" sql statement
                {
                    return new InsertCommand(checkInsert(checkSql), checkSql);

                }
               else if (checkSql.StartsWith("SELECT"))         // "select" sql statement
                {
                    Object obj = checkSelect(checkSql);
                    if (obj is SelectData)
                        return new SelectCommand(obj as SelectData, checkSql);
                    else if (obj is JoinData)
                        return new JoinCommand(obj as JoinData, checkSql);
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
               
                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries); //split the statement using delimelators 
                CreateData createNew = new CreateData();
                createNew.tableName = sqlSplit[1];    // store the table name  
                if (sqlSplit[1].ToUpper() == "CREATE" || sqlSplit[1].ToUpper() == "INSERT" || sqlSplit[1].ToUpper() == "SELECT") //table name can not be key word
                {
                    Console.WriteLine("Invalid table name. Keyword is used");
                }

                for (int i = 2; i < sqlSplit.Length; i = i + 2)
                {
                    if (sqlSplit[i].ToUpper() == "CREATE" || sqlSplit[i].ToUpper() == "INSERT" || sqlSplit[i].ToUpper() == "SELECT")
                    {
                        Console.WriteLine("Invalid field. Keyword is used");
                    }
                    if (sqlSplit[i + 1] == "INT" || sqlSplit[i + 1] == "STRING")        // field type can only be INT OR STRING
                    {
                        createNew.setFieldData(sqlSplit[i].Trim(), sqlSplit[i + 1].Trim());
                    }
                    else
                    {
                        Console.WriteLine("Invalid type");
                    }
                }
                return createNew;
            }


           public InsertData checkInsert(string sqlQuery)     //function for checking insert statement
            {

                string[] sqlSplit = checkSql.Split(new char[4] { ' ', '(', ',', ')'}, StringSplitOptions.RemoveEmptyEntries);
                InsertData insertNew = new InsertData();
                insertNew.tableName = sqlSplit[2];
                if (sqlSplit[1] != "INTO")          // INSERT MUST HAVE INTO
                {
                    Console.WriteLine("Invalid insert, missing INTO.");      
                }

                if (sqlSplit[2].ToUpper() == "CREATE" || sqlSplit[2].ToUpper() == "INSERT" || sqlSplit[2].ToUpper() == "SELECT")    // table name can not be the key word
                {
                    Console.WriteLine("Invalid table name. Keyword is used");
                }

                for (int i = 3; i < sqlSplit.Length; i++)
                {
                    insertNew.setValueList(sqlSplit[i].Replace("\"", "").Trim());   //store the insert value into list
                }
                return insertNew;
            }

           public Object checkSelect(string sqlQuery)         //function for checking select statement
            {
                
                string[] sqlSplit = checkSql.Split(new char[5] { ' ', '(', ',', ')', '=' }, StringSplitOptions.RemoveEmptyEntries); // split the string using the delimilator
                
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

                        string tablenames = sqlQuery.Substring(sqlQuery.IndexOf("FROM") + 4, sqlQuery.IndexOf("WHERE") - (sqlQuery.IndexOf("FROM") + 4)); // extract the table name between From and where
                        string[] tableSplit = tablenames.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if(tableSplit.Length!=2)
                        {
                            throw new Exception("Invalid table count in select"); // only two tables allowed
                        }
                        joinData.tableName1 = tableSplit[0].Trim();
                        joinData.tableName2 = tableSplit[1].Trim();

                        string filters = sqlQuery.Substring(sqlQuery.IndexOf("WHERE") + 5, sqlQuery.Length - (sqlQuery.IndexOf("WHERE") + 5));  // get the where clause
                        string[] filterSplit = filters.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (filterSplit.Length != 2)
                        {
                            throw new Exception("Invalid where clause in select");  // only a field and a constant allowed
                        }
                        joinData.field1 = filterSplit[0].Trim();   //store the field
                        joinData.field2 = filterSplit[1].Trim();    // store the constant

                        return joinData;

                    }
                    
                    else
                    {
                        SelectData selectWhereClause = new SelectData();        // select from two tables
                        string filters = sqlQuery.Substring(sqlQuery.IndexOf("WHERE") + 5, sqlQuery.Length - (sqlQuery.IndexOf("WHERE") + 5)); // get the where clause
                        string[] filterSplit = filters.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (filterSplit.Length != 2)
                        {
                            throw new Exception("Invalid where clause in select");      // only two tables allowed
                        }
                        selectWhereClause.AddFilters(filterSplit[0].Trim(), filterSplit[1].Replace("\"", "").Trim());
                        selectWhereClause.tableName = sqlSplit[3].Trim();  
                        return selectWhereClause;
                    }
                

                }
                else if (sqlSplit[1] == "*" && !sqlQuery.Contains("WHERE"))     // case select * from table
                {
                    SelectData selectNew = new SelectData();
                    selectNew.tableName = sqlSplit[3].Trim();
                    return selectNew;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
}

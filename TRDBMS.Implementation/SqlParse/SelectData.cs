using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    class SelectData
    {
        public string tableName { get; set; }    //store the tablename of "select * from table where field = constant"
        string[] whereClauseWithConstant;  // store the field and constant of "select * from table where field = constant"
        string[] tableNameArray;    //store the two table names of "SELECT * FROM table1,table2 where field1 = field2"
        string[] whereClauseforTwoTable; // store the two fields of "SELECT * FROM table1,table2 where field1 = field2"
        public string oneField { get; set; }    //store the fieldname for "SELECT field FROM table"
        public string oneTable { get; set; }    //store the tablename for "SELECT field FROM table"

        public SelectData()
        {
            whereClauseforTwoTable = new string[2];
            whereClauseWithConstant = new string[2];
            tableNameArray = new string[2];
        }

        public void setForTwoTable(string table1, string table2, string field1, string field2) //set the value for "SELECT * FROM table1,table2 where field1 = field2"
        {
            tableNameArray[0] = table1;
            tableNameArray[1] = table2;
            whereClauseforTwoTable[0] = field1;
            whereClauseforTwoTable[1] = field2;
        }

        public void setForOneTable(string field, string constant) //set the value for "select * from table where field = constant"
        {
            whereClauseWithConstant[0] = field;
            whereClauseWithConstant[1] = constant;
        }

    }
}

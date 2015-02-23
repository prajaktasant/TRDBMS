using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    public class SelectData
    {
        public string tableName { get; set; }    //store the tablename of "select * from table where field = constant"
        public Dictionary<string,string> whereClauseWithConstant;  // store the field and constant of "select * from table where field = constant"
        public List<string> fields { get; set; }    //store the fieldname for "SELECT field,field,.. FROM table"

        public SelectData()
        {
            whereClauseWithConstant = new Dictionary<string,string>();
            fields = new List<string>();
        }

        public void AddField(string field)
        {
            fields.Add(field);
        }

        public void AddFilters(string fieldName, string constant)
        {
            whereClauseWithConstant.Add(fieldName, constant);
        }

    }


}

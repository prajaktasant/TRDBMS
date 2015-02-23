using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    public class CreateData
    {
        public string tableName { get; set; }
        public Dictionary<string, string> columeValue;

        public CreateData()
        {
            columeValue = new Dictionary<string, string>();
        }
        public void setFieldData(string fieldName, string fieldType)
        {
            columeValue.Add(fieldName, fieldType);
        }
    }
}

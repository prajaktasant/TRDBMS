using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    /// <summary>
    /// This is a Initialization Data Structure for Create Command.
    /// </summary>
    public class CreateData
    {
        public string tableName { get; set; }   //set and get tablename
        public Dictionary<string, string> columeValue;  //Dictionary to hold the pair of field name and field type.

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

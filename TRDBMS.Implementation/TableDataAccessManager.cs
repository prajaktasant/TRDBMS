using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TRDBMS.Implementation.Serializable;

namespace TRDBMS.Implementation
{
    /// <summary>
    /// This class provides functions that are used for selection and insertion opertion over tables. 
    /// The constructor initializes the table name, table definition and the table file path.
    /// </summary>
    public class TableDataAccessManager
    {
        private readonly string _tableName;
        private readonly string _dataFilePath;
        TableDefinition _definition;
        public TableDataAccessManager(string tableName)
        {
            _tableName = tableName;
            _definition = SchemaManager.GetTableDefinition(this._tableName);
            _dataFilePath = Path.Combine(DatabaseUtil.GetDatabaseDirectory(), _tableName);
            if (!File.Exists(_dataFilePath))
            {
                throw new Exception("Invalid not found");
            }
        }
        /// <summary>
        /// This function inserts the list of values into the table. 
        /// It first validates that the number of values should match the number of fields in the table definition else throws an exception.
        /// It then validates the data types of the values with the table definition data types and throws exception if the data type is invalid.
        /// A BinaryFormatter serializer is used to write the values in to the corresponding table file stored in MyDatabase directory on the file system.
        /// </summary>
        /// <param name="values"></param>
        public void Insert(List<string> values)
        {
            if (_definition.Fields.Count != values.Count)   //Validates the number if values with the field count in the table.
            {
                throw new Exception("Number of values do not match the number of columns");
            }

            int i = 0;

            //Validates the datatype of the values to be inserted in the table.
            foreach (KeyValuePair<string, char> field in _definition.Fields)
            {
                if (field.Value == 'I' || field.Value == 'i')   
                {
                    try
                    {
                        int.Parse(values[i++]);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid data type");
                    }

                }
                else if (field.Value == 'S' || field.Value == 's')
                {
                    if (string.IsNullOrEmpty(values[i++]))
                    {
                        throw new Exception("Invalid data type");
                    }
                }

            }

            Table_Data data = new Table_Data(values); // An object (tuple) for the values to be inserted of class Table_Data is created.

            using (Stream stream = File.Open(_dataFilePath, FileMode.Append))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, data); //The object (tuple) is serialized (written) to the table data file
            }
        }
        /// <summary>
        /// This function is called by the ReadData function to extract the data from the table one tuple at a time for the list of fields mention in the 
        /// "SELECT field {, field} FROM table" query.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="newObj"></param>
        /// <returns></returns>
        List<string> extractFieldValues(List<string> fields, Table_Data newObj)
        {
            List<string> retfields = new List<string>();
            foreach (string field in fields)
            {
                if (!_definition.Fields.ContainsKey(field))
                {
                    throw new Exception("Invalid field name");
                }
                else
                {
                    int idx = getIndex(field);
                    retfields.Add(newObj.Fields[idx]);

                }
            }

            return retfields;   //Returns a single tuple of field values.
        }

        /// <summary>
        /// ReadData takes input parameters as (List<string> fields, Dictionary<string, string> fieldConst) is a generic function for query types:
        /// 1.SELECT * FROM table: Accepts null as parameters to select the entire table.
        /// 2.SELECT field {, field} FROM table: Accepts list of fields as first parammeter and null as second parameter
        /// to ReadData to select a list of fields from the table.
        /// 3.SELECT * FROM table WHERE field1 = constant1, field2 = constant2,...: Accepts null as first parameter and 
        /// key value pairs of field name and constant as second parameter to ReadData to select all tuples from table satisfing the WHERE conditions.
        /// 4.SELECT field {, field} FROM table WHERE field1 = constant1, field2 = constant2,...: Accepts list of fields as first parameter and
        /// key value pairs of field name and constant as second parameter to ReadData function to select the tuple with the list fields from table satisfing the WHERE conditions.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldConst"></param>
        /// <returns></returns>
        public List<List<string>> ReadData(List<string> fields, Dictionary<string, string> fieldConst)
        {
            List<List<string>> retList = new List<List<string>>();

            using (Stream stream = File.Open(_dataFilePath, FileMode.Open))
            {
                while (stream.Position != stream.Length)
                {
                    Table_Data newObj = null;

                    BinaryFormatter bformatter = new BinaryFormatter();

                    bformatter = new BinaryFormatter();

                    newObj = (Table_Data)bformatter.Deserialize(stream);

                    bool valueFound = fieldConst == null ? true : findFilters(fieldConst, newObj);
                    if (valueFound)
                    {
                        retList.Add(fields == null ? new List<string>(newObj.Fields) : extractFieldValues(fields, newObj));
                    }
                }

            }
            return retList;
        }

        private bool findFilters(Dictionary<string, string> fieldConst, Table_Data newObj)
        {
            bool retval = false;
            foreach( KeyValuePair<string,string> fld in fieldConst)
            {
                int idx = getIndex(fld.Key);
                retval = newObj.Fields[idx].ToLower() == fld.Value.ToLower();
                if (retval == false)
                    break;
            }
            
            return retval;
        }

        private int getIndex(string field)
        {
            int i = 0;

            foreach (KeyValuePair<string, char> fld in _definition.Fields)
            {
                if (fld.Key.ToLower() == field.ToLower())
                {
                    return i;
                }

                i++;
            }

            return 0;
        }

        public string TableName
        {
            get { return _tableName; }
        }

        public static List<List<string>> GetJoin(string table1, string table2, List<string> field1, List<string> field2)
        {
            TableDataAccessManager table1DataAccessManager = new TableDataAccessManager(table1);
            TableDataAccessManager table2AccessManager = new TableDataAccessManager(table2);  
            List<List<string>> table1Records = table1DataAccessManager.ReadData(field1, null);
            List<List<string>> table2Records = table2AccessManager.ReadData(field2, null);
            List<string> commonRecord = new List<string>();

            foreach(List<string> table1Record in table1Records)
            {
                foreach(List<string> table2Record in table2Records)
                {
                    if(table1Record.ToArray()[0].ToLower() == table2Record.ToArray()[0].ToLower())
                    {
                        if (!commonRecord.Contains(table1Record.ToArray()[0].ToLower()))
                        commonRecord.Add(table1Record.ToArray()[0].ToLower());
                    }
                        
                }

            }
            List<List<string>> joinTuples = new List<List<string>>();
            List<List<string>> table1Tuples = new List<List<string>>();
            List<List<string>> table2Tuples = new List<List<string>>();
            foreach(string record in commonRecord)
            {
                Dictionary<string, string> field1Record = new Dictionary<string, string>();
                field1Record.Add(field1.ToArray()[0], record);
                table1Tuples = table1DataAccessManager.ReadData(null, field1Record);

                Dictionary<string, string> field2Record = new Dictionary<string, string>();
                field2Record.Add(field2.ToArray()[0], record);
                table2Tuples = table2AccessManager.ReadData(null, field2Record);
                foreach(List<string> table1tuple in table1Tuples)
                {
                    List<string> t = table1tuple;
                    foreach(List<string> table2tuple in table2Tuples)
                    {
                        List<string> lst = new List<string>();
                        lst.AddRange(t);
                        lst.AddRange(table2tuple);
                        joinTuples.Add(lst);
                    }
                }
            }

            return joinTuples;
        }
    }
}

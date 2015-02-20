using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TRDBMS.Implementation.Serializable;

namespace TRDBMS.Implementation
{
    public class TableDataAccessManager
    {
        private readonly string _tableName;
        private readonly string _dataFilePath;

        public TableDataAccessManager(string tableName)
        {
            _tableName = tableName;
            _dataFilePath = Path.Combine(DatabaseUtil.GetDatabaseDirectory(), _tableName);
            if (!File.Exists(_dataFilePath))
            {
                throw new Exception("Invalid not found");
            }
        }

        public void Insert(List<string> values)
        {
            TableDefinition definition = SchemaManager.GetTableDefinition(this._tableName);
            if (definition.Fields.Count != values.Count)
            {
                throw new Exception("Number of values do not match the number of columns");
            }

            int i = 0;


            foreach (KeyValuePair<string, char> field in definition.Fields)
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

            Table_Data data = new Table_Data(values);

            using (Stream stream = File.Open(_dataFilePath, FileMode.Append))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, data);
            }
        }

        //public List<List<string>> ReadAllData()
        //{
        //    List<List<string>> retList = new List<List<string>>();

        //    //Open the file written above and read values from it.
        //    using (Stream stream = File.Open(_dataFilePath, FileMode.Open))
        //    {
        //        while (stream.Position != stream.Length)
        //        {
        //            Table_Data newObj = null;

        //            BinaryFormatter bformatter = new BinaryFormatter();

        //            bformatter = new BinaryFormatter();

        //            newObj = (Table_Data)bformatter.Deserialize(stream);

        //            retList.Add(new List<string>(newObj.Fields));
        //        }

        //    }

        //    return retList;
        //}

        List<string> extractFieldValues(List<string> fields, Table_Data newObj)
        {
            List<string> retfields = new List<string>();
            TableDefinition definition = SchemaManager.GetTableDefinition(this._tableName);
            foreach (string field in fields)
            {
                if (!definition.Fields.ContainsKey(field))
                {
                    throw new Exception("Invalid field type");
                }
                else
                {
                    int idx = getIndex(field);
                    retfields.Add(newObj.Fields[idx]);

                }
            }

            return retfields;
        }

        public List<List<string>> ReadData(List<string> fields, Dictionary<string, string> fieldConst)
        {
            List<List<string>> retList = new List<List<string>>();

            TableDefinition definition = SchemaManager.GetTableDefinition(this._tableName);
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
            TableDefinition definition = SchemaManager.GetTableDefinition(this._tableName);

            foreach (KeyValuePair<string, char> fld in definition.Fields)
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

        public static List<List<string>> GetJoin(string table1, string table2, List<string> commonField)
        {
            TableDataAccessManager table1DataAccessManager = new TableDataAccessManager(table1);
            TableDataAccessManager table2AccessManager = new TableDataAccessManager(table2);
            List<List<string>> table1Records = table1DataAccessManager.ReadData(commonField, null);
            List<List<string>> table2Records = table2AccessManager.ReadData(commonField, null);
            List<string> commonRecord = new List<string>();

            foreach(List<string> table1Record in table1Records)
            {
                foreach(List<string> table2Record in table2Records)
                {
                    if(table1Record.ToArray()[0].ToLower() == table2Record.ToArray()[0].ToLower())
                    {
                        commonRecord.Add(table1Record.ToArray()[0].ToLower());
                    }
                        
                }

            }
            List<List<string>> table1Tuples = new List<List<string>>();
            List<List<string>> table2Tuples = new List<List<string>>();
            foreach(string record in commonRecord)
            {
                Dictionary<string, string> fieldRecord = new Dictionary<string, string>();
                fieldRecord.Add(commonField.ToArray()[0], record);
                table1Tuples = table1DataAccessManager.ReadData(null, fieldRecord);
                table2Tuples = table2AccessManager.ReadData(null, fieldRecord);
                //table1Tuples.AddRange(table2Tuples);

            }

            foreach(List<string> table1tuple in table1Tuples)
            {
                foreach(List<string> table2tuple in table2Tuples)
                {
                    table1tuple.AddRange(table2tuple);
                }
            }

            return table1Tuples;
        }
    }
}

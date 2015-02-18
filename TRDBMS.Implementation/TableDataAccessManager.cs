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

        public List<List<string>> ReadAllData()
        {
            List<List<string>> retList = new List<List<string>>();

            //Open the file written above and read values from it.
            using (Stream stream = File.Open(_dataFilePath, FileMode.Open))
            {
                while (stream.Position != stream.Length)
                {
                    Table_Data newObj = null;

                    BinaryFormatter bformatter = new BinaryFormatter();

                    bformatter = new BinaryFormatter();

                    newObj = (Table_Data)bformatter.Deserialize(stream);

                    retList.Add(newObj.Fields);
                }

            }

            return retList;
        }

        public string TableName
        {
            get { return _tableName; }
        }
    }
}

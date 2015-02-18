using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TRDBMS.Implementation.Serializable;


namespace TRDBMS.Implementation
{
    public class SchemaManager
    {
        private readonly string _schemaFilePath = Path.Combine(DatabaseUtil.GetDatabaseDirectory(), Constants.TABLE_SCHEMA_FILE_NAME);
        private void IntitializeSchemaStore()
        {
            if (!File.Exists(_schemaFilePath))
            {
                using (File.Create(_schemaFilePath))
                {
                }
            }
        }

        public SchemaManager()
        {
            IntitializeSchemaStore();
        }

        private List<string> Tables
        {
            get
            {
                List<string> retList = new List<string>();

                using (Stream stream = File.Open(_schemaFilePath, FileMode.Open))
                {
                    while (stream.Position != stream.Length)
                    {
                        Table_Schema newObj = null;

                        BinaryFormatter bformatter = new BinaryFormatter();

                        bformatter = new BinaryFormatter();

                        newObj = (Table_Schema)bformatter.Deserialize(stream);
                        if (!retList.Contains(newObj.TableName))
                            retList.Add(newObj.TableName);
                    }

                }

                return retList;
            }
        }

        public bool IsTableExists(string tableName)
        {
            return Tables.Contains(tableName);
        }

        public TableDefinition ReadTableDefinition(string tableName)
        {
            if (!IsTableExists(tableName))
            {
                throw new Exception("Table not found");
            }

            TableDefinition table = new TableDefinition(tableName);

            using (Stream stream = File.Open(_schemaFilePath, FileMode.Open))
            {
                while (stream.Position != stream.Length)
                {
                    Table_Schema newObj = null;

                    BinaryFormatter bformatter = new BinaryFormatter();

                    bformatter = new BinaryFormatter();

                    newObj = (Table_Schema)bformatter.Deserialize(stream);
                    if (newObj.TableName.ToLower() == tableName.ToLower())
                    {
                        table.AddField(newObj.FieldName, newObj.DataType);
                    }
                }
            }

            return table;
        }

        private string CreateTableStore(string tableName)
        {
            string tableFilePath = Path.Combine(DatabaseUtil.GetDatabaseDirectory(), tableName);
            if (File.Exists(tableFilePath))
            {
                throw new Exception("File with the same name already exists");
            }

            using (File.Create(tableFilePath))
            {
            }

            return tableFilePath;
        }

        public void AddTable(TableDefinition tableDefinition)
        {
            if (IsTableExists(tableDefinition.Name))
            {
                throw new Exception("Table with the same name already exists");
            }

            string path = CreateTableStore(tableDefinition.Name);

            using (Stream stream = File.Open(_schemaFilePath, FileMode.Append))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                foreach (KeyValuePair<string, char> keyValuePair in tableDefinition.Fields)
                {
                    Table_Schema schema = new Table_Schema(tableDefinition.Name, keyValuePair.Key, keyValuePair.Value, path);
                    bformatter.Serialize(stream, schema);
                }
            }

        }

        public static void CreateTable(TableDefinition tableDefinition)
        {
            SchemaManager manager = new SchemaManager();
            manager.AddTable(tableDefinition);
        }

        public static TableDefinition GetTableDefinition(string tableName)
        {
            SchemaManager manager = new SchemaManager();
            return manager.ReadTableDefinition(tableName);
        }
    }
}

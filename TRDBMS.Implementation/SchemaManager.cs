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
        /// <summary>
        /// _schemaFilePath contains the path of the Data Dictionary(database schema). A data dictionary file is created if does not exist.
        /// The folder "MyDatabase is created on the local file system in the AppData Directory. The Data Dictionary file and the all the file for each table 
        /// is created in the same directory "MyDatabase".
        /// </summary>
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

        /// <summary>
        /// Opens the Data Dictionary file, reads the tables one by one, and returns a list of tables names. 
        /// A Binary formatter is used to read data from the Data Dictionary file.
        /// </summary>
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
        /// <summary>
        /// Validates if the table exists in the data dictionary file.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsTableExists(string tableName)
        {
            return Tables.Contains(tableName);
        }

        /// <summary>
        /// Opens the data dictionary file from "MyDatabase" directory and reads the table definition of the specified table one tuple at a time using
        /// Binary formatter deserializer that deserializes a stream into an object graph.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
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

                    newObj = (Table_Schema)bformatter.Deserialize(stream); //Read a tuple from a stream and deserialize it into a Table_Schema type.
                    if (newObj.TableName.ToLower() == tableName.ToLower())
                    {
                        table.AddField(newObj.FieldName, newObj.DataType);
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// Creates a file path for a new table entry in the Data Dictionary file. The file has same name as the table name.
        /// Throws exception if the file already exists else creates a file and returns the file path.
        /// The file is stored in the same directory as that of the Data Dictionary "MyDatabase". 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// A table definition is added to the Data Dictionary when a new table is created. An exception occurs if the same table name exists in the Data Dictionary file.
        /// A Binary Formatter serializer is used to write in the Data dictionary file stored on the file system directory "MyDatabase"
        /// one tuple at a time (i.e. table name, field name, field type,  the file path for the table) for every field name and field type pairs.
        /// A Binary Formatter serializer serializes the object to the given stream.
        /// </summary>
        /// <param name="tableDefinition"></param>
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

                //An object of type Table_Schema is created for every field name and type pair and the object is serialized to the Data dictionary file
                //using BinaryFormatter serializer.

                foreach (KeyValuePair<string, string> keyValuePair in tableDefinition.Fields)
                {
                    Table_Schema schema = new Table_Schema(tableDefinition.Name, keyValuePair.Key, keyValuePair.Value, path); 
                    bformatter.Serialize(stream, schema); 
                }
            }

        }
        /// <summary>
        /// Creates a new table passing the the table definition to the AddTable function.
        /// </summary>
        /// <param name="tableDefinition"></param>
        public static void CreateTable(TableDefinition tableDefinition)
        {
            SchemaManager manager = new SchemaManager();
            manager.AddTable(tableDefinition);
        }

        /// <summary>
        /// Returns the table definition by passing the table name to ReadTableDefinition function.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static TableDefinition GetTableDefinition(string tableName)
        {
            SchemaManager manager = new SchemaManager();
            return manager.ReadTableDefinition(tableName);
        }
    }
}

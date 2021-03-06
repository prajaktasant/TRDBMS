﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TRDBMS.Implementation.Serializable
{
    /// <summary>
    /// This class is responsible for serializing and deserializing the data tuples containing table definition for the data dictionary.
    /// </summary>
    [Serializable]
    internal class Table_Schema : ISerializable
    {
        private string _tableName;
        private string _fieldName;
        private string _dataType;
        private string _tableFilePath;

        public string TableName
        {
            get { return _tableName; }
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public string DataType
        {
            get { return _dataType; }
        }

        public string TableFilePath
        {
            get { return _tableFilePath; }
        }

        public Table_Schema()
        {
        }

        public Table_Schema(string tableName, string fieldName, string dataType, string tableFilePath)
        {
            _tableName = tableName;
            _fieldName = fieldName;
            _dataType = dataType;
            _tableFilePath = tableFilePath;
        }

        //Deserialization constructor.
        public Table_Schema(SerializationInfo info, StreamingContext ctxt)
        {
            _tableName = (string)info.GetValue("_tableName", typeof(string));
            _fieldName = (string)info.GetValue("_fieldName", typeof(string));
            _dataType = (string)info.GetValue("_dataType", typeof(string));
            _tableFilePath = (string)info.GetValue("_tableFilePath", typeof(string));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_tableName", _tableName);
            info.AddValue("_fieldName", _fieldName);
            info.AddValue("_dataType", _dataType);
            info.AddValue("_tableFilePath", _tableFilePath);
        }
    }
}

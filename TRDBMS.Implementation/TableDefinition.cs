using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation
{
    /// <summary>
    /// This class defines the table schema. It assigns the table name, field name and field types. 
    /// This class is also used to extract the table information for table manipulations and validations.
    /// </summary>
    public class TableDefinition
    {
        private string _name;
        private Dictionary<string, char> _fields;

        public TableDefinition(string name)
        {
            _name = name;
            _fields = new Dictionary<string, char>();
        }

        public TableDefinition(string name, Dictionary<string, char> fields)
        {
            _name = name;
            _fields = fields;
        }

        public void AddField(string name, char datatype)
        {
            if (_fields == null)
            {
                _fields = new Dictionary<string, char>();
            }

            _fields.Add(name, datatype);
        }

        public Dictionary<string, char> Fields
        {
            get { return _fields; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}

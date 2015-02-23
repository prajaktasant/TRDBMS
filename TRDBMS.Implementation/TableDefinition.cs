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
        private Dictionary<string, string> _fields;

        public TableDefinition(string name)
        {
            _name = name;
            _fields = new Dictionary<string, string>();
        }

        public TableDefinition(string name, Dictionary<string, string> fields)
        {
            _name = name;
            _fields = fields;
        }

        public void AddField(string name, string datatype)
        {
            if (_fields == null)
            {
                _fields = new Dictionary<string, string>();
            }

            _fields.Add(name, datatype);
        }

        public Dictionary<string, string> Fields
        {
            get { return _fields; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation
{
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

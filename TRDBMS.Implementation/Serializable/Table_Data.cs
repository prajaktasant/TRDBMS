using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TRDBMS.Implementation.Serializable
{
    [Serializable]
    public class Table_Data : ISerializable
    {

        private List<string> lst = new List<string>();

        public string [] Fields
        {
            get { return lst.ToArray(); }
        }
        public Table_Data()
        { }

        public Table_Data(List<string> fields)
        {
            lst = fields;
        }

        //Deserialization constructor.
        public Table_Data(SerializationInfo info, StreamingContext ctxt)
        {
            lst = (List<string>)info.GetValue("fields", typeof(List<string>));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("fields", lst);
        }
    }
}

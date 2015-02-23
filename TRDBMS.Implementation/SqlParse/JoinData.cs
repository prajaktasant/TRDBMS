using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.SqlParse
{
    public class JoinData
    {
        public string tableName1 { get; set; }
        public string tableName2 { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }   
     
        public JoinData()
        {
  
        }


    }
}

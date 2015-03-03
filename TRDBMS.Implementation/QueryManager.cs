using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.Commands;
using TRDBMS.Implementation.SqlParse;


namespace TRDBMS.Implementation
{
    public class QueryManager
    {
        public static CommandBase GetCommand(String query)
        {
            SqlParse.SqlParse sqlParse = new SqlParse.SqlParse(query);
            return sqlParse.Parse();
        }
    }
}

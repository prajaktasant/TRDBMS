using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    class JoinCommand : CommandBase
    {
        JoinData _joinData = null;
        String _sqlQuery = null;
        public override bool IsNonQuery { get { return false; } }
        public JoinCommand(JoinData joinData, String sqlQuery)
        {
            _joinData = joinData;
            _sqlQuery = sqlQuery;
        }
        public override string GetQuery()
        {
            return _sqlQuery;
        }

        public override List<List<string>> ExecuteCommand()
        {
            List<string> field1 = new List<string>();
            field1.Add(_joinData.field1);
            List<string> field2 = new List<string>();
            field2.Add(_joinData.field2);
            return TableDataAccessManager.GetJoin(_joinData.tableName1, _joinData.tableName2, field1, field2);
        }
    }
}

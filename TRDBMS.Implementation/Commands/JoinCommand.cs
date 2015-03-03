using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    /// <summary>
    /// Responsible for executing the SELECT command involving two table.
    /// </summary>
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

        public override IEnumerable<string> GetFieldNames()
        {
            foreach (var tmp in SchemaManager.GetTableDefinition(_joinData.tableName1).Fields)
            {
                yield return string.Format("{0}.{1}", _joinData.tableName1, tmp.Key)  ;
            }

            foreach (var tmp in SchemaManager.GetTableDefinition(_joinData.tableName2).Fields)
            {
                yield return string.Format("{0}.{1}", _joinData.tableName2, tmp.Key);
            }
        }
    }
}

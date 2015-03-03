using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    /// <summary>
    /// Responsible for executing the INSERT query.
    /// </summary>
    public class InsertCommand : CommandBase
    {
        InsertData _insertData = null;
        String _sqlQuery = null;
        public InsertCommand(InsertData insertData, String sqlQuery)
        {
            _insertData = insertData;
            _sqlQuery = sqlQuery;
        }
        public override string GetQuery()
        {
            return _sqlQuery;
        }
        public override List<List<string>> ExecuteCommand()
        {
            TableDataAccessManager dataAccessManager = new TableDataAccessManager(_insertData.tableName);
            dataAccessManager.Insert(_insertData.valueList);
            return new List<List<string>>();

        }

        public override IEnumerable<string> GetFieldNames()
        {
            throw new NotImplementedException();
        }
    }
}

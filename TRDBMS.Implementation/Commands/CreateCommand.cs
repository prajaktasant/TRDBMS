using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    public class CreateCommand : CommandBase
    {
        CreateData _createData = null;
        String _sqlQuery = null;
        public CreateCommand(CreateData createData, String sqlQuery)
        {
            _createData = createData;
            _sqlQuery = sqlQuery;
        }
        public override string GetQuery()
        {
            return _sqlQuery;
        }
        public override List<List<string>> ExecuteCommand()
        {
            TableDefinition table = new TableDefinition(_createData.tableName,_createData.columeValue);
            SchemaManager.CreateTable(table);
            return new List<List<string>>();

        }
    }
}

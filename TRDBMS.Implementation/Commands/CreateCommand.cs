using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    public class CreateCommand : CommandBase
    {
        CreateData _createData = null;

        public CreateCommand(CreateData createData)
        {
            _createData = createData;
        }

        public override void ExecuteCommand()
        {
            TableDefinition table = new TableDefinition(_createData.tableName,_createData.columeValue);
            SchemaManager.CreateTable(table);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    public class InsertCommand : CommandBase
    {
        InsertData _insertData = null;
        public InsertCommand(InsertData insertData)
        {
            _insertData = insertData;
        }
        public override void ExecuteCommand()
        {
            TableDataAccessManager dataAccessManager = new TableDataAccessManager(_insertData.tableName);
            dataAccessManager.Insert(_insertData.valueList);

        }
    }
}

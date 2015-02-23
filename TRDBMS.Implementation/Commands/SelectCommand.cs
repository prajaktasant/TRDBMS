using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    class SelectCommand : CommandBase
    {
        SelectData _selectData = null;

        public SelectCommand(SelectData selectData)
        {
            _selectData = selectData;
        }
        public override void ExecuteCommand()
        {
           
                 TableDataAccessManager tableDataAccessManager = new TableDataAccessManager(_selectData.tableName);
                 List<List<string>> selectedDatalst = tableDataAccessManager.ReadData(_selectData.fields,_selectData.whereClauseWithConstant.Count == 0 ? null: _selectData.whereClauseWithConstant);
           
           
        }
    }
}

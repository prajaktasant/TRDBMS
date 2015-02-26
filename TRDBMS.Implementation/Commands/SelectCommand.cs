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
        public override List<List<string>> ExecuteCommand()
        {
                 TableDataAccessManager tableDataAccessManager = new TableDataAccessManager(_selectData.tableName);
                 return tableDataAccessManager.ReadData(_selectData.fields.Count == 0?null: _selectData.fields,_selectData.whereClauseWithConstant.Count == 0 ? null: _selectData.whereClauseWithConstant);           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    class SelectCommand : CommandBase
    {
        SelectData _selectData = null;
        String _sqlQuery = null;
        public override bool IsNonQuery { get { return false; } }
        public SelectCommand(SelectData selectData, String sqlQuery)
        {
            _selectData = selectData;
            _sqlQuery = sqlQuery;
        }
        public override string GetQuery()
        {
            return _sqlQuery;
        }

        public override List<List<string>> ExecuteCommand()
        {
                 TableDataAccessManager tableDataAccessManager = new TableDataAccessManager(_selectData.tableName);
                 return tableDataAccessManager.ReadData(_selectData.fields.Count == 0?null: _selectData.fields,_selectData.whereClauseWithConstant.Count == 0 ? null: _selectData.whereClauseWithConstant);           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    /// <summary>
    /// Responsible for executing all the SELECT queries excluding the SELECT query for two tables.
    /// </summary>
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

        public override IEnumerable<string> GetFieldNames()
        {
            List<string> retval = new List<string>();
            if(_selectData.fields.Count>0)
                return _selectData.fields;
            
            foreach (var tmp in SchemaManager.GetTableDefinition(_selectData.tableName).Fields)
            {
                retval.Add(tmp.Key);
            }
            return retval;
        }

    }
}

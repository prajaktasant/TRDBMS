using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.Commands
{
    public class NullCommand : CommandBase
    {
        String _sqlQuery;

        public NullCommand(String query)
        {
            _sqlQuery = query;
        }
        public override string GetQuery()
        {
            return _sqlQuery; 
        }

        public override List<List<string>> ExecuteCommand()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetFieldNames()
        {
            throw new NotImplementedException();
        }
    }
}

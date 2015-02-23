﻿using System;
using System.Collections.Generic;
using System.Text;
using TRDBMS.Implementation.SqlParse;

namespace TRDBMS.Implementation.Commands
{
    class JoinCommand : CommandBase
    {
        JoinData _joinData = null;
        public JoinCommand(JoinData joinData)
        {
            _joinData = joinData;
        }

        public override void ExecuteCommand()
        {
                  List<string> field1 = new List<string>();
                field1.Add(_joinData.field1);
                List<string> field2 = new List<string>();
                field2.Add(_joinData.field2);
                 List<List<string>> joinResult 
                    = TableDataAccessManager.GetJoin(_joinData.tableName1,_joinData.tableName2,field1,field2);
        }
    }
}
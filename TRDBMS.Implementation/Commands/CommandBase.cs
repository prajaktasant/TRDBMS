using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.Commands
{
    public abstract class CommandBase
    {
        public abstract List<List<string>> ExecuteCommand();
     
    }
}

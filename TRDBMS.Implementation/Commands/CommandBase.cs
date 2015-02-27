using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.Commands
{
    public abstract class CommandBase
    {
        public abstract string GetQuery();
        public virtual bool IsNonQuery { get { return true; } }
        public abstract List<List<string>> ExecuteCommand();
     
    }
}

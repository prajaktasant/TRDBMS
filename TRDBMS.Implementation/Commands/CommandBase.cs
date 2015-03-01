using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation.Commands
{
    /// <summary>
    /// Abstract class creates object of the base class depending on the type of Query.
    /// </summary>
    public abstract class CommandBase
    {
        public abstract string GetQuery();
        public virtual bool IsNonQuery { get { return true; } }
        public abstract List<List<string>> ExecuteCommand(); //Call to Execute Command of the respective child class depending on the type of Query.
     
    }
}

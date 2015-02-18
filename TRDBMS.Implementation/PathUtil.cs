using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation
{
    public static class PathUtil
    {
        public static string GetAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }


    }
}

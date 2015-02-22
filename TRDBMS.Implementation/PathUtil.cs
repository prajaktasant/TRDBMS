using System;
using System.Collections.Generic;
using System.Text;

namespace TRDBMS.Implementation
{
    public static class PathUtil
    {
        /// <summary>
        /// Returnss the path of the AppData folder on the local file System.
        /// </summary>
        /// <returns></returns>
        public static string GetAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }


    }
}

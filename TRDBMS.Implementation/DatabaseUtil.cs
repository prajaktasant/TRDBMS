using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TRDBMS.Implementation
{
    /// <summary>
    /// This class creates a manages the data base directory "MyDatabase". 
    /// The directory is created in the AppData folder on the local file System.
    /// The Data dictionary file and files for all the table data are stored in this directory.
    /// </summary>
    public static class DatabaseUtil
    {
        private static bool IsDatabaseDirectoryExits(string name)
        {
            return Directory.Exists(Path.Combine(PathUtil.GetAppDataFolder(), name).ToString());
        }

        private static string CreateDatabaseDirectory(string name)
        {
            string path = Path.Combine(PathUtil.GetAppDataFolder(), name).ToString();
            if (IsDatabaseDirectoryExits(path))
            {
                return path;
            }
            return Directory.CreateDirectory(path).FullName;
        }

        public static string GetDatabaseDirectory()
        {
            return CreateDatabaseDirectory(Constants.DBNAME);
        }

    }
}

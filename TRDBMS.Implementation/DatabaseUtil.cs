using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TRDBMS.Implementation
{
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TRDBMS.Implementation
{
    public static class ListExtentions
    {
        /// <summary>
        /// Helper Function to convert a result from List<List<string>> to a Data table 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(List<List<string>> list, List<string> headers)
        {
            DataTable tmp = new DataTable();

            foreach (var item in headers)
            {

                tmp.Columns.Add(item);

            }
            foreach (List<string> row in list)
            {

                tmp.Rows.Add(row.ToArray());
            }
            return tmp;
        }

    }
}

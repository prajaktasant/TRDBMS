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
            public static  DataTable ToDataTable( List<List<string>> list)
            {
                DataTable tmp = new DataTable();
                int i = 0;
                foreach (List<string> row in list)
                {
                    if (i==0)
                    {
                        i = 1;
                        foreach(string s in row)
                        {
                            tmp.Columns.Add(""+i++);
                        }
                    }
                    tmp.Rows.Add(row.ToArray());
                }
                return tmp;
            }

    }
}

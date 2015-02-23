using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TRDBMS.Implementation
{
    public static class ListExtentions
    {
            public static DataTable ToDataTable(List<List<string>> list)
            {
                DataTable tmp = new DataTable();
                foreach (List<string> row in list)
                {
                    tmp.Rows.Add(row.ToArray());
                }
                return tmp;
            }
       
    }
}

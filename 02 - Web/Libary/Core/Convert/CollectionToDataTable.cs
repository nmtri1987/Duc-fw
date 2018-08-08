using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Core.Converts
{
    public static class CollectionToDataTable
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            for (int i = 0; i < pia.Length; i++)
            {
                if (pia[i].Name.Equals("ExtensionData")) continue;
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[pia[i].Name].AllowDBNull = true;
            }

            //Populate the table
            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    if (pia[i].Name.Equals("ExtensionData")) continue;
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}

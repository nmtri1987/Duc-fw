using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
namespace Biz.Core.Converts
{
    public enum ObjectState : int
    {
        unchange = 0,
        add = 1,
        update = 2,
        delete = 3,
    }
    public static class DataTableToCollection
    {
        public static string ConvertDataTableToHTMLString(System.Data.DataTable dt, string filter, string sort, string fontsize, string border, bool headers, bool useCaptionForHeaders)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='" + border + "'b>");
            if (headers)
            {
                //write column headings
                sb.Append("<tr>");
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    if (useCaptionForHeaders)
                        sb.Append("<td><b><font face=Arial size=2>" + dc.Caption + "</font></b></td>");
                    else
                        sb.Append("<td><b><font face=Arial size=2>" + dc.ColumnName + "</font></b></td>");
                }
                sb.Append("</tr>");
            }

            //write table data
            foreach (System.Data.DataRow dr in dt.Select(filter, sort))
            {
                sb.Append("<tr>");
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append("<td><font face=Arial size=" + fontsize + ">" + dr[dc].ToString() + "</font></td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
        public static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            //try
            //{
            string columnname = "";
            string value = "";
            PropertyInfo[] Properties;
            Properties = typeof(T).GetProperties();
            foreach (PropertyInfo objProperty in Properties)
            {
                columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                if (!string.IsNullOrEmpty(columnname))
                {
                    value = row[columnname].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!objProperty.PropertyType.IsEnum)
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                        else
                        {
                            value = row[columnname].ToString().Replace("%", "");
                            objProperty.SetValue(obj, (ObjectState)Enum.Parse(typeof(ObjectState), value), null);
                        }

                    }
                }
            }
            return obj;
            //}
            //catch
            //{
            //    return obj;
            //}
        }
        //#region "getobject filled object with property reconized"

        //public static List<T> ConvertTo<T>(DataTable datatable) where T : new()
        //{
        //    List<T> Temp = new List<T>();
        //    try
        //    {
        //        List<string> columnsNames = new List<string>();
        //        foreach (DataColumn DataColumn in datatable.Columns)
        //            columnsNames.Add(DataColumn.ColumnName);
        //        Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
        //        return Temp;
        //    }
        //    catch
        //    {
        //        return Temp;
        //    }

        //}
        //public static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        //{
        //    T obj = new T();
        //    //try
        //    //{
        //        string columnname = "";
        //        string value = "";
        //        PropertyInfo[] Properties;
        //        Properties = typeof(T).GetProperties();
        //        foreach (PropertyInfo objProperty in Properties)
        //        {
        //            columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
        //            if (!string.IsNullOrEmpty(columnname))
        //            {
        //                value = row[columnname].ToString();
        //                if (!string.IsNullOrEmpty(value))
        //                {
        //                    if (!objProperty.PropertyType.IsEnum)
        //                    {
        //                        if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
        //                        {
        //                            value = row[columnname].ToString().Replace("$", "").Replace(",", "");
        //                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
        //                        }
        //                        else
        //                        {
        //                            value = row[columnname].ToString().Replace("%", "");
        //                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        value = row[columnname].ToString().Replace("%", "");
        //                        objProperty.SetValue(obj, (ObjectState) Enum.Parse(typeof(ObjectState), value), null);
        //                    }
                            
        //                }
        //            }
        //        }
        //        return obj;
        //    //}
        //    //catch
        //    //{
        //    //    return obj;
        //    //}
        //}

        //#endregion

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static IEnumerable<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ToDataTable(this ExcelWorksheet ws, bool hasHeaderRow = true)
        {
            var tbl = new DataTable();
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column]) tbl.Columns.Add(hasHeaderRow ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            var startRow = hasHeaderRow ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var row = tbl.NewRow();
                foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
                tbl.Rows.Add(row);
            }
            return tbl;
        }


        public static List<T> getClassFromExcel<T>(string path, int fromRow, int fromColumn, int toColumn = 0) where T : class
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                List<T> retList = new List<T>();

                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                toColumn = toColumn == 0 ? typeof(T).GetProperties().Count() : toColumn;

                for (var rowNum = fromRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    T objT = Activator.CreateInstance<T>();
                    Type myType = typeof(T);
                    PropertyInfo[] myProp = myType.GetProperties();

                    var wsRow = ws.Cells[rowNum, fromColumn, rowNum, toColumn];

                    for (int i = 0; i < myProp.Count(); i++)
                    {
                        myProp[i].SetValue(objT, wsRow[rowNum, fromColumn + i].Text);
                    }
                    retList.Add(objT);
                }
                return retList;
            }
        }
        public static List<T> getClassFromExcelPackage<T>(ExcelPackage objEx, int fromRow, int fromColumn, int toColumn = 0) where T : BaseEntity
        {
            using (var pck = objEx)
            {
                List<T> retList = new List<T>();
                var ws = pck.Workbook.Worksheets.First();
                toColumn = toColumn == 0 ? typeof(T).GetProperties().Count() : toColumn;

                for (var rowNum = fromRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    T objT = Activator.CreateInstance<T>();
                    Type myType = typeof(T);
                    PropertyInfo[] myProp = myType.GetProperties();

                    var wsRow = ws.Cells[rowNum, fromColumn, rowNum, toColumn];

                    for (int i = 0; i < myProp.Count(); i++)
                    {
                        myProp[i].SetValue(objT, wsRow[rowNum, fromColumn + i].Text);
                    }
                    retList.Add(objT);
                }
                return retList;
            }
        }


    }
}

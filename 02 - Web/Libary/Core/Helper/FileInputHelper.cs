using ClosedXML.Excel;
using Biz.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biz.Core.Helpers
{
    public static class FileInputHelper
    {
        public static string UploadFile(string path, string fileName, HttpPostedFileBase file)
        {
            string result = string.Empty;
            try
            {
                bool exists = Directory.Exists(HttpContext.Current.Server.MapPath(path));
                if (!exists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                path = HttpContext.Current.Server.MapPath(path);
                //var fileName = Path.GetFileName(file.FileName);
                
                path = Path.Combine(path, fileName);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                file.SaveAs(path);
                result = fileName;
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public static void ExportExcel(System.Data.DataTable dt, string fileName, string sheetName,bool isCustom=false, List<ExcelCellModel> customTemp =null)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, sheetName);
                //define colum style - DNH code
                
                if (isCustom)
                {
                    int count = 0;
                    if (customTemp != null)
                    {
                        foreach (ExcelCellModel item in customTemp)
                        {
                            
                            if (item.FontInfo.FontColor != null)
                            {
                                if (count == 0)
                                {
                                    ws.Row(item.RowInsert).InsertRowsAbove(item.RowInsertNumber);
                                }
                                ws.Cell(item.RowIndex, item.ColumnIndex).Value = item.CellValue;
                                ws.Cell(item.RowIndex, item.ColumnIndex).Style.Font.Bold = item.FontInfo.IsBold;
                                ws.Cell(item.RowIndex, item.ColumnIndex).Style.Font.FontColor = item.FontInfo.FontColor;
                                ws.Cell(item.RowIndex, item.ColumnIndex).Style.Font.FontSize = item.FontInfo.FontSize;
                            }
                           
                            if (item.BackgroundColorInfo != null)
                            {
                                foreach(int ColIndex in item.BackgroundColorInfo.ColunmIndex)
                                {
                                    ws.Column(ColIndex).Style.Fill.BackgroundColor = item.BackgroundColorInfo.BackgorundColor;
                                    ws.Column(ColIndex).Style.Font.FontColor = item.BackgroundColorInfo.FontColor;
                                }
                                
                            }
                            count++;
                            
                        }
                    }
                    
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    string ColumnType = "";
                    string ColumName = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ColumnType = dt.Columns[j].DataType.Name.ToString().ToLower();
                        ColumName = dt.Columns[j].ColumnName.ToString().ToLower();
                        if (ColumnType == "datetime" || ColumName == "enddate")
                        {
                            //format datetime for this column
                            //dt.Columns[j].DataType = typeof(System.DateTime);// Type.GetType("");
                            ws.Column(j+1).Style.DateFormat.Format = "dd-mmm-yyyy";
                        }else if (ColumnType == "decimal")
                        {
                            ws.Column(j + 1).Style.DateFormat.Format = "#,##0.00";
                        }
                    }
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static void ExportMultiSheetExcel(System.Data.DataSet ds, string[] sheetName, string fileName)
        {
            try
            {


                using (XLWorkbook wb = new XLWorkbook())
                {

                    for (int i = 0; i < sheetName.Length; i++)
                    {
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            ds.Tables[i].TableName = sheetName[i];
                            wb.Worksheets.Add(ds.Tables[i]);
                        }
                    }

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch (Exception ex) { }
        }
        //System.Data.DataSet ds, string[] sheetName, string fileName, List<DropdownModelExcel> list,int[] DateArry
        public static void ExportMultiSheetExcelExtend(ExcelPara  Para)
        {
            try
            {


                using (XLWorkbook wb = new XLWorkbook())
                {

                    for (int i = 0; i < Para.sheetName.Length; i++)
                    {
                        if (Para.ds.Tables[i].Rows.Count > 0)
                        {
                            Para.ds.Tables[i].TableName = Para.sheetName[i];
                            wb.Worksheets.Add(Para.ds.Tables[i]);
                        }
                    }
                    IXLWorksheet worksheet = null; ;
                    if (Para.list != null)
                    {
                        foreach (DropdownModelExcel item in Para.list)
                        {
                            worksheet = wb.Worksheet(item.SheetShowDrop);
                            if (item.IsHideSheetDataDrop)
                            {
                                wb.Worksheet(item.SheetDataDrop).Hide();
                            }
                            var worksheet2 = wb.Worksheet(item.SheetDataDrop);
                            worksheet.Column(item.ColumnShowDropData).SetDataValidation().List(worksheet2.Range(item.RangeSheetData), true);


                        }
                    }else
                    {
                        worksheet = wb.Worksheet(1);
                    }
                    if (Para.DateColumns.Length > 0)
                    {
                        for(int i = 0; i < Para.DateColumns.Length; i++)
                        {
                            worksheet.Column(Para.DateColumns[i]).Style.DateFormat.Format = Para.DateFormat;
                        }
                        
                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Para.fileName + ".xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
    
}
public class DropdownModelExcel
{
    public int SheetShowDrop { get; set; }
    public int SheetDataDrop { get; set; }
    public int ColumnShowDropData { get; set; }
    public string RangeSheetData { get; set; }

    public bool IsHideSheetDataDrop { get; set; }
}

public class ExcelPara
{
    public   System.Data.DataSet ds { get; set; }
    public string[] sheetName { get; set; }
    public string fileName { get; set; }
    public List<DropdownModelExcel> list { get; set; }
    public int[] DateColumns { get; set; }

    /// <summary>
    /// "dd-mmm-yyyy"
    /// </summary>
    public string DateFormat { get; set; }

}
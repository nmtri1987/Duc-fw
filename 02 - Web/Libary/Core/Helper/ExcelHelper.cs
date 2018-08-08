using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using ClosedXML;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI;

using System.Web.UI.WebControls;
using Biz.Core.Converts;
using System.Reflection;
namespace Helpers
{
    public class ExcelHelper
    {
        public static void ExportToMemory<T>(IEnumerable<T> dt, HttpResponseBase Response)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                using (ExcelPackage pck = new ExcelPackage(MyMemoryStream))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromCollection(dt);
                    pck.Save();



                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        public static void ExportPDF(DataTable dt, HttpResponseBase Response)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    GridView b = new GridView();
                    b.DataSource = dt;
                    b.DataBind();
                    b.RenderControl(hw);
                    //string mystring =  DataTableToCollection.ConvertDataTableToHTMLString(dt,"","","","",true,true);
                    StringReader sr = new StringReader(hw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Records.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        //public void HtmlToPdf(string html, HttpResponseBase Response)
        //{

        //    // Create a byte array that will eventually hold our final PDF
        //    Byte[] bytes;

        //    // Boilerplate iTextSharp setup here

        //    // Create a stream that we can write to, in this case a MemoryStream
        //    using (var ms = new MemoryStream())
        //    {
        //        // Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
        //        using (var doc = new Document())
        //        {
        //            // Create a writer that's bound to our PDF abstraction and our stream
        //            using (var writer = PdfWriter.GetInstance(doc, ms))
        //            {
        //                // Open the document for writing
        //                doc.Open();

        //                string finalHtml = string.Empty;

        //                // Read your html by database or file here and store it into finalHtml e.g. a string
        //                // XMLWorker also reads from a TextReader and not directly from a string
        //                using (var srHtml = new StringReader(finalHtml))
        //                {
        //                    // Parse the HTML

        //                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
        //                }

        //                doc.Close();
        //            }
        //        }

        //        // After all of the PDF "stuff" above is done and closed but **before** we
        //        // close the MemoryStream, grab all of the active bytes from the stream
        //        bytes = ms.ToArray();
        //    }

        //    // Clear the response
        //    Response.Clear();
        //    MemoryStream mstream = new MemoryStream(bytes);

        //    // Define response content type
        //    Response.ContentType = "application/pdf";

        //    // Give the name of file of pdf and add in to header
        //    Response.AddHeader("content-disposition", "attachment;filename=invoice.pdf");
        //    Response.Buffer = true;
        //    mstream.WriteTo(Response.OutputStream);
        //    Response.End();

        //}
        private MemoryStream createPDF(string html)
        {
            MemoryStream msOutput = new MemoryStream();
            TextReader reader = new StringReader(html);

            // step 1: creation of a document-object
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);

            // step 2:
            // we create a writer that listens to the document
            // and directs a XML-stream to a file
            PdfWriter writer = PdfWriter.GetInstance(document, msOutput);

            // step 3: we create a worker parse the document
            HTMLWorker worker = new HTMLWorker(document);

            // step 4: we open document and start the worker on the document
            document.Open();
            worker.StartDocument();

            // step 5: parse the html into the document
            worker.Parse(reader);

            // step 6: close the document and the worker
            worker.EndDocument();
            worker.Close();
            document.Close();

            return msOutput;
        }
        public static void ExportExcel(DataTable dt, HttpResponseBase Response, string FileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= " + FileName + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public static void ConvertHTMLToPDF(string HTMLCode,string strFileName)
        {
            HttpContext context = HttpContext.Current;

            //Render PlaceHolder to temporary stream
            System.IO.StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            /********************************************************************************/
            //Try adding source strings for each image in content
            string tempPostContent = getImage(HTMLCode);
            /*********************************************************************************/

            StringReader reader = new StringReader(tempPostContent);

            //Create PDF document
            Document doc = new Document(PageSize.A4);
            HTMLWorker parser = new HTMLWorker(doc);
            PdfWriter.GetInstance(doc, new FileStream(context.Server.MapPath("~") + "/" + strFileName+".pdf",

            FileMode.Create));
            doc.Open();

            try
            {
                //Parse Html and dump the result in PDF file
                parser.Parse(reader);
            }
            catch (Exception ex)
            {
                //Display parser errors in PDF.
                Paragraph paragraph = new Paragraph("Error!" + ex.Message);
                Chunk text = paragraph.Chunks[0] as Chunk;
                if (text != null)
                {
                    text.Font.Color = BaseColor.RED;
                }
                doc.Add(paragraph);
            }
            finally
            {
                doc.Close();
            }
        }

        public static string getImage(string input)
        {
            if (input == null)
                return string.Empty;
            string tempInput = input;
            string pattern = @"<img(.|\n)+?>";
            string src = string.Empty;
            HttpContext context = HttpContext.Current;

            //Change the relative URL's to absolute URL's for an image, if any in the HTML code.
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline |

            RegexOptions.RightToLeft))
            {
                if (m.Success)
                {
                    string tempM = m.Value;
                    string pattern1 = "src=[\'|\"](.+?)[\'|\"]";
                    Regex reImg = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match mImg = reImg.Match(m.Value);

                    if (mImg.Success)
                    {
                        src = mImg.Value.ToLower().Replace("src=", "").Replace("\"", "");

                        if (src.ToLower().Contains("http://") == false)
                        {
                            //Insert new URL in img tag
                            src = "src=\"" + context.Request.Url.Scheme + "://" +
                            context.Request.Url.Authority + src + "\"";
                            try
                            {
                                tempM = tempM.Remove(mImg.Index, mImg.Length);
                                tempM = tempM.Insert(mImg.Index, src);

                                //insert new url img tag in whole html code
                                tempInput = tempInput.Remove(m.Index, m.Length);
                                tempInput = tempInput.Insert(m.Index, tempM);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }
            return tempInput;
        }

        string getSrc(string input)
        {
            string pattern = "src=[\'|\"](.+?)[\'|\"]";
            System.Text.RegularExpressions.Regex reImg = new System.Text.RegularExpressions.Regex(pattern,
            System.Text.RegularExpressions.RegexOptions.IgnoreCase |

            System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Match mImg = reImg.Match(input);
            if (mImg.Success)
            {
                return mImg.Value.Replace("src=", "").Replace("\"", ""); ;
            }

            return string.Empty;
        }

        public static DataTable getClassFromExcelPackage<T>(Stream FileStream, int fromRow, int fromColumn, int toColumn = 0) where T : BaseEntity
        {
            using (var pck = new ExcelPackage(FileStream))
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
                        string excelValue = wsRow[rowNum, fromColumn + i].Text;
                        Type columnType = myProp[i].PropertyType;
                        if (!string.IsNullOrEmpty(excelValue))
                        {
                            if (columnType == typeof(int))
                            {
                                myProp[i].SetValue(objT, int.Parse(excelValue));
                            }
                            else if (columnType == typeof(DateTime))
                            {
                                // myProp[i].SetValue(objT, Convert.ToDateTime((DateTime)excelValue));

                            }
                            else if (myProp[i].PropertyType == typeof(Decimal))
                            {
                                myProp[i].SetValue(objT, Convert.ToDecimal(excelValue));
                            }
                            else
                            {
                                myProp[i].SetValue(objT, excelValue);
                            }
                        }
                        
                    }
                    retList.Add(objT);
                }
                return retList.ToDataTable();
            }
        }

        public static DataTable ToDataTable(Stream FileStream)
        {
            ExcelPackage package = new ExcelPackage(FileStream);
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    try
                    {
                        if (cell.StyleID == (int)ExcelCellStyle.Datetime)
                        {

                            newRow[cell.Start.Column - 1] = DateTime.FromOADate(double.Parse(cell.Value.ToString())).ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            newRow[cell.Start.Column - 1] = cell.Text;
                        }
                    }catch(Exception objEx)
                    {
                        newRow[cell.Start.Column - 1] = cell.Value;
                    }
                    //newRow[cell.Start.Column - 1] = cell.Value;

                }
                table.Rows.Add(newRow);
            }
          //  DataTable dataTable = table.Rows.Cast<table>().Where(row => !row.ItemArray.All(field =>field is System.DBNull || string.Compare((field as string).Trim(),string.Empty) == 0)).CopyToDataTable();
            return table;
        }

        public static DataTable ToDataTableExcel( ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
namespace Helpers
{
    public class PdfHelper
    {
        public static void CreatePDFFromHTMLFile(string HtmlStream, string Destinationpath, string FileNamePDF)
        {
            try
            {
                object TargetFile = FileNamePDF;
                string ModifiedFileName = string.Empty;
                string FinalFileName = string.Empty;


                ExportHtmltoPdf.HtmlToPdfBuilder builder = new ExportHtmltoPdf.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);
                ExportHtmltoPdf.HtmlPdfPage first = builder.AddPage();
                first.AppendHtml(HtmlStream);
                byte[] file = builder.RenderPdf();
                File.WriteAllBytes(TargetFile.ToString(), file);

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
                ModifiedFileName = TargetFile.ToString();
                ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");
                iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);

                reader.Close();

                if (File.Exists(TargetFile.ToString()))
                    File.Delete(TargetFile.ToString());
                FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
                File.Copy(ModifiedFileName, FinalFileName);
                if (File.Exists(ModifiedFileName))
                    File.Delete(ModifiedFileName);

                // string path = Request.PhysicalApplicationPath + "\\files\\" + "ConvertHTMLToPDF.pdf";

                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(Destinationpath);


                if (buffer != null)
                {
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileNamePDF);
                    HttpContext.Current.Response.TransmitFile(Destinationpath);
                    HttpContext.Current.Response.End();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HtmlTemp"></param>
        /// <param name="FileName"></param>
        public static void MyPaymentReport(string FileName, string UserImg,string mycontent)
        {
            // Create a Document object
            HttpContext context = HttpContext.Current;
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            HTMLWorker worker = new HTMLWorker(document);
            // Create a new PdfWriter object, specifying the output stream
          //  var output = new FileStream(context.Server.MapPath("~/MyFirstPDF.pdf"), FileMode.Create);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            try
            {
                //string arialuniTff = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                //iTextSharp.text.FontFactory.Register(arialuniTff);

                string fontpath = System.Web.HttpContext.Current.Server.MapPath("~/Content/ARIALUNI.ttf");
              //  BaseFont bf = BaseFont.CreateFont(fontpath + "ARIALUNI.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.FontFactory.Register(fontpath, "CustomAriel");
             //   Font f = new Font(bf, 10, Font.NORMAL);
            }catch(Exception objEx){
               
            }
          
            // Open the Document for writing
            document.Open();

            //... Step 3: Add elements to the document! ...


            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

            //document.Add(new Paragraph("IFind - Asia", titleFont));
            iTextSharp.text.html.simpleparser.StyleSheet ST = new iTextSharp.text.html.simpleparser.StyleSheet();
            //Set the default body font to our registered font's internal name
            ST.LoadTagStyle("body", "face", "Arial Unicode MS");
            //Set the default encoding to support Unicode characters
            ST.LoadTagStyle("body", "encoding", "Identity-H");
            

            // step 4.3: assign the style sheet to the html parser
            worker.SetStyleSheet(ST);
            //List<IElement> list = HTMLWorker.ParseToList(new StringReader(mycontent), ST);
           

            //add logo
            //var logo = iTextSharp.text.Image.GetInstance(context.Server.MapPath("~/Img/Users/ducnguyen.jpg"));
            //Image logo = iTextSharp.text.Image.GetInstance(context.Server.MapPath(UserImg));
            //logo.ScaleToFit(80, 80);
            ////logo.SetAbsolutePosition(50, 700);
            //document.Add(logo);

            // Read in the contents of the Receipt.htm file...
            //string contents = File.ReadAllText(context.Server.MapPath("~/html/" + HtmlTemp + ".html"));
            //string contents = LoadWebtoHTML(context.Server.MapPath("~/PersonInfo/MyPdfReport"));
            string contents = getImage(mycontent);

            // Step 4: Parse the HTML string into a collection of elements...
            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), ST);

            // Enumerate the elements, adding each one to the Document...
            foreach (var htmlElement in parsedHtmlElements)
                document.Add(htmlElement as IElement);


            // Close the Document - this saves the document contents to the output stream
            document.Close();

            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=MyPayment-{0}.pdf", FileName));
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.HeaderEncoding = Encoding.UTF8;
            context.Response.BinaryWrite(output.ToArray());
            context.Response.Flush();
            context.Response.End();
            
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
        public static string LoadWebtoHTML(string Link)
        {
            string html = new WebClient().DownloadString(Link);
            return html;
        }
    }
}

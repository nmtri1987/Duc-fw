using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace daitiphu.common.tinhnang
{
    public class HtmlTag
    {
        /// <summary>
        /// Get the Domain name and port of the current URL
        /// </summary>
        /// <returns>Domain name and port</returns>
        public static string ClientDomainName()
        {
            string domainNameAndPort = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.Length - HttpContext.Current.Request.Url.PathAndQuery.Length);
            return domainNameAndPort;
        }
        public static string GetRootOfDomain()
        {
            return ClientDomainName() + ApplicationVRoot() + "/";
        }
        public static string Strip(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
        public static string StripHtmlTag(string tagName, string html)
        {
            Regex reg = new Regex(string.Format("<{0}[^>]*?>[\\w|\\t|\\r|\\W]*?</{0}>", tagName), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Regex regt = new Regex("\\n");
            Match m = reg.Match(html);
            while (m.Success)
            {
                html = html.Replace(m.ToString(), new string((char)10, regt.Matches(m.ToString()).Count));
                m = m.NextMatch();
            }
            return html;
        }
        public static string FormatN(object ob, int decimals)
        {
            float num;
            long num2;
            string str;
            string str2 = "";
            switch (decimals)
            {
                case 1:
                    str = "0.0";
                    break;

                case 2:
                    str = "0.00";
                    break;

                case 3:
                    str = "0.000";
                    break;

                default:
                    str = "0.";
                    break;
            }
            try
            {
                num = float.Parse(ob.ToString());
            }
            catch
            {
                num = 0f;
            }
            try
            {
                num2 = (long)num;
            }
            catch
            {
                num2 = 0L;
            }
            if (num2.ToString().Length <= 3)
            {
                str2 = "";
            }
            else if (num2.ToString().Length <= 6)
            {
                str2 = "0,00";
            }
            else if (num2.ToString().Length <= 9)
            {
                str2 = "0,000,00";
            }
            else if (num2.ToString().Length <= 12)
            {
                str2 = "0,000,000,00";
            }
            return num.ToString(str2 + str);
        }
        public static string ApplicationVRoot()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return "";
            }
            return HttpContext.Current.Request.ApplicationPath;
        }
        public static string LinkMapping(string page,string ID, string Title)
        {
            string url = "#";
            url = ApplicationVRoot() + "/" + page + "?id=" + ID + "&tl=" + daitiphu.common.tinhnang.xulyquery.GetSEName(FilterVietkey(Title));
            return url;
        }
        public static string FilterVietkey(string strSource)
        {
            int num;
            string str = "\x00e1 \x00e0 ả \x00e3 ạ \x00c1 \x00c0 Ả \x00c3 Ạ ă ắ ằ ẳ ẵ ặ Ă Ắ Ằ Ẳ Ẵ Ặ \x00e2 ấ ầ ẩ ẫ ậ \x00c2 Ấ Ầ Ẩ Ẫ Ậ đ Đ \x00e9 \x00e8 ẻ ẽ ẹ \x00c9 \x00c8 Ẻ Ẽ Ẹ \x00ea ế ề ể ễ ệ \x00ca Ế Ề Ể Ễ Ệ \x00ed \x00ec ỉ ĩ ị \x00cd \x00cc Ỉ Ĩ Ị \x00f3 \x00f2 ỏ \x00f5 ọ \x00d3 \x00d2 Ỏ \x00d5 Ọ \x00f4 ố ồ ổ ỗ ộ \x00d4 Ố Ồ Ổ Ỗ Ộ ơ ớ ờ ở ỡ ợ Ơ Ớ Ờ Ở Ỡ Ợ \x00fa \x00f9 ủ ũ ụ \x00da \x00d9 Ủ Ũ Ụ ư ứ ừ ử ữ ự Ư Ứ Ừ Ử Ữ Ự \x00fd ỳ ỷ ỹ ỵ \x00dd Ỳ Ỷ Ỹ Ỵ";
            string str2 = "a a a a a A A A A A a a a a a a A A A A A A a a a a a a A A A A A A d d e e e e e E E E E E e e e e e e E E E E E E i i i i i I I I I I o o o o o O O O O O o o o o o o O O O O O O o o o o o o O O O O O O u u u u u U U U U U u u u u u u U U U U U U y y y y y Y Y Y Y Y";
            string[] strArray = str.Split(" ".ToCharArray());
            string[] strArray2 = str2.Split(" ".ToCharArray());
            string str3 = strSource;
            for (num = 0; num < strArray.Length; num++)
            {
                str3 = str3.Replace(strArray[num], strArray2[num]);
            }
            str = "\x00c0 \x00c1 \x00c2 \x00c3 \x00c4 \x00c5 \x00c6 \x00c7 \x00c8 \x00c9 \x00ca \x00cb \x00cc \x00cd \x00ce \x00cf \x00d0 \x00d1 \x00d2 \x00d3 \x00d4 \x00d5 \x00d6 \x00d8 \x00d9 \x00da \x00db \x00dc \x00dd \x00de \x00df \x00e0 \x00e1 \x00e2 \x00e3 \x00e4 \x00e5 \x00e6 \x00e7 \x00e8 \x00e9 \x00ea \x00eb \x00ec \x00ed \x00ee \x00ef \x00f0 \x00f1 \x00f2 \x00f3 \x00f4 \x00f5 \x00f6 \x00f8 \x00f9 \x00fa \x00fb \x00fc \x00fd \x00fe \x00ff";
            str2 = "A A A A A A \x00c6 \x00c7 E E E E I I I I D N O O O O O \x00d8 U U U U Y \x00de \x00df a a a a a a \x00e6 \x00e7 e e e e i i i i \x00f0 n o o o o o \x00f8 u u u u y \x00fe y";
            string[] strArray3 = str.Split(" ".ToCharArray());
            string[] strArray4 = str2.Split(" ".ToCharArray());
            for (num = 0; num < strArray3.Length; num++)
            {
                str3 = str3.Replace(strArray3[num], strArray4[num]);
            }
            return str3.Replace("\0", "");
        }
        public static string rutGon(string Input, int SoLuong)
        {
            if (Input.Length > SoLuong)
            {
                return Input.Substring(0, SoLuong) + "...";
            }
            return Input;
        }
        public static string StripHTML(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }
    }
}

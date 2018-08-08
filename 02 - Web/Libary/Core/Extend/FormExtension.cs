using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


    public static class FormExtension
    {
        public static void AppendUrlEncoded(this StringBuilder sb, string name, string value, bool moreValues = true)
        {
            sb.Append(HttpUtility.UrlEncode(name));
            sb.Append("=");
            sb.Append(HttpUtility.UrlEncode(value));
            if (moreValues)
                sb.Append("&");
        }
    }

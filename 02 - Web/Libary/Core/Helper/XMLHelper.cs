using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
namespace Helpers
{

    public class XMLHelper
    {
        private const string SETTINGS_ALL_KEY = "Cached.apilink.{0}";

        public static string WebApiReturnConfig(string strName, bool isRoot = false)
        {
            try
            {
                string key = String.Format(SETTINGS_ALL_KEY, isRoot);
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (string)obj2;
                }
                XDocument config = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Data/SystemConfig.xml"));
                string strEndpoint = config.Document.Root.Elements("api").FirstOrDefault(c => c.Attribute("name").Value == strName).Attribute("url").Value;

                strEndpoint = isRoot ? strEndpoint : strEndpoint + "api/";
                HttpCache.Max(key, strEndpoint);
                return strEndpoint;
            }
            catch (Exception ObjEx)
            {
                string message = ObjEx.Message;
            }
            return "";
        }
    }
}
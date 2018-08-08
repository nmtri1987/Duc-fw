using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RBVH.TMS.Ext
{
    public class ModuleConfig
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["HRConnection"].ToString();
        public static string MyConnection
        {
            get
            {
                if (ConnectionString != null)
                {
                    return DTP.Core.CryptoUtil.Decrypt(ConnectionString, true);
                    //return ConfigurationManager.ConnectionStrings["HRConnection"].ToString();
                }
                return null;
            }
        }
        //public const string HRConnection = ConfigurationManager.ConnectionStrings["iWebConnect"].ToString();
    }


}
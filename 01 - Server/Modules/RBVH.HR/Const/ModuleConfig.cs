using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


public class QTConfig
{
    private static string ConnectionString = ConfigurationManager.ConnectionStrings["LVConnection"].ToString();
    public static string MyConnection
    {
        get
        {
            if (ConnectionString != null)
            {
                //return ConnectionString;
                return DTP.Core.CryptoUtil.Decrypt(ConnectionString, true);
            }
            return null;
        }
    }
}


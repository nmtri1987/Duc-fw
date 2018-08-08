using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using dtp.Web;
using System.Xml;
namespace DTP.Object
{
    [DataContract]
    public class MenuModel : BaseDBEntity
    {
        [DataMember]
        public string MenuID { get; set; }

        [DataMember]
        public string MenuDesc { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public string ParentMenuID { get; set; }
    }

    [CollectionDataContract]
    public class MenuCollection : BaseDBEntityCollection<MenuModel>
    {
    }

    public class MenuManager
    {
        public static MenuCollection GetMenuCollection()
        {
            string _Folder = daitiphu.common.tinhnang.IOHelper.GetDirectory("~/App_Data/XML/");
            string fileName = _Folder;
            MenuCollection menus = new MenuCollection();
            try
            {
                 fileName = _Folder + ConfigurationManager.AppSettings["xmlMenuFile"].ToString();
                
                if (File.Exists(fileName))
                {
                 
                    DataSet ds = new DataSet();
                    ds.ReadXml(fileName);
                    int i = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        menus.Add(new MenuModel()
                        {
                            MenuID = dr["MenuID"].ToString(),
                            MenuDesc = dr["MenuDesc"].ToString(),
                            Path = dr["Path"].ToString(),
                            ParentMenuID = dr["ParentMenuID"].ToString()
                        });
                        i++;
                    }
                }

            }
            catch (Exception objEx)
            {
                menus.Add(new MenuModel()
                {
                    MenuID = "0",
                    MenuDesc = objEx.Message + "/" + fileName,
                    //Path = dr["Path"].ToString(),
                  //  ParentMenuID = dr["ParentMenuID"].ToString()
                });
            }
            return menus;
        }
    }
}
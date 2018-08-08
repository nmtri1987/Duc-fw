using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace daitiphu.common.tinhnang
{
    public class IOHelper
    {
      
      
        public static string GetDirectory(string strPath)
        {
          
            string dir = HttpContext.Current.Server.MapPath(strPath);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            return dir;
        }
        public static bool delFile(string strFilename)
        {
            bool flag = false;
            if (File.Exists(strFilename))
            {
                File.Delete(strFilename);
                flag = true;
            }
            return flag;
        }
        public static bool RemoveImagesFile(string Path,string strFileName,bool hasPhotoFile)
        {
            bool flag = false;

            string dir = HttpContext.Current.Server.MapPath(Path) + "/icons/" + strFileName;
            if (File.Exists(dir))
            {
                File.Delete(dir);
                
                if (hasPhotoFile)
                {
                    dir = HttpContext.Current.Server.MapPath(Path) + "/photo/" + strFileName;
                    if (File.Exists(dir))
                    {
                        File.Delete(dir);

                    }
                }
                flag = true;
            }
            return flag;
        }
        public static string ImageUrl(string Path)
        {
            string strImg = Path;
            try
            {
              if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(strImg)))
              {
                strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + "/images/no_image.gif";
              }
              else
              {
                strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + strImg.Replace("~/", "/");
              }
            }
            catch (Exception objEx)
            {
              strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + "/images/no_image.gif";
            }
            return strImg;
        }
    }
}

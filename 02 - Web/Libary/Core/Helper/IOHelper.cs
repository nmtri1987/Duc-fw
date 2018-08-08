using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
    public static bool HasFile(string strFileName)
    {
        return File.Exists(strFileName);
    }
    public static bool hasFile(string strFileName)
    {
        return File.Exists(strFileName);
    }
    public static bool HasFile(string Folder,string strFilename)
    {
        bool flag = false;
        string FullPath = GetDirectory(Folder) + "\\" + strFilename;
        if (File.Exists(FullPath))
        {
            flag = true;
        }
        return flag;
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
    static public bool CheckImageSize(string strFilePath, int intWidth, int intHeight)
    {

        bool bolResult = true;
        if (File.Exists(strFilePath))
        {
            System.Drawing.Image imgPicture = System.Drawing.Image.FromFile(strFilePath);
            if ((imgPicture.Width >= intWidth) || (imgPicture.Height >= intHeight))
            {
                bolResult = false;
            }
            imgPicture.Dispose();
        }
        return bolResult;

    }

    static public void DownloadFile(string localFile, string downloadUrl)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(downloadUrl);
        req.Method = "GET";

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

        // Retrieve response stream and wrap in StreamReader
        Stream respStream = resp.GetResponseStream();
        StreamReader rdr = new StreamReader(respStream);

        // Create the local file
        StreamWriter wrtr = new StreamWriter(localFile);

        // loop through response stream reading each line 
        //  and writing to the local file
        string inLine = rdr.ReadLine();
        while (inLine != null)
        {
            wrtr.WriteLine(inLine);
            inLine = rdr.ReadLine();
        }

        rdr.Close();
        wrtr.Close();
    }
    public static bool ResizeImage(string strResourceFile, int intHeight, int intWidth)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(strResourceFile);
        if ((image.Height > intHeight) || (image.Width > intWidth))
        {
            float num = ((float)image.Height) / ((float)intHeight);
            float num2 = ((float)image.Width) / ((float)intWidth);
            int height = Math.Min(intHeight, image.Height);
            int width = (image.Width * height) / image.Height;
            if (num < num2)
            {
                width = Math.Min(intWidth, image.Width);
                height = (image.Height * width) / image.Width;
            }
            Bitmap bitmap = new Bitmap(width, height);
            Graphics.FromImage(bitmap).DrawImage(image, 0, 0, width, height);
            image.Dispose();
            bitmap.Save(strResourceFile);
            bitmap.Dispose();
        }
        return true;
    }
    public static bool RemoveImagesFile(string Path, string strFileName, bool hasPhotoFile)
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
    //public static string ImageUrl(string Path)
    //{
    //    string strImg = Path;
    //    try
    //    {
    //        if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(strImg)))
    //        {
    //            strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + "/images/no_image.gif";
    //        }
    //        else
    //        {
    //            strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + strImg.Replace("~/", "/");
    //        }
    //    }
    //    catch (Exception objEx)
    //    {
    //        strImg = daitiphu.common.tinhnang.HtmlTag.GetRootOfDomain() + "/images/no_image.gif";
    //    }
    //    return strImg;
    //}

    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        try
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        catch { }
    }

    /// <summary>
    /// Reads an object instance from a binary file.
    /// </summary>
    /// <typeparam name="T">The type of object to read from the XML.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }

    }

    /// <summary>
    /// Writes the given object instance to an XML file.
    /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
    /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
    /// <para>Object type must have a parameterless constructor.</para>
    /// </summary>
    /// <typeparam name="T">The type of object being written to the file.</typeparam>
    /// <param name="filePath">The file path to write the object instance to.</param>
    /// <param name="objectToWrite">The object instance to write to the file.</param>
    /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
    public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter writer = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            string file = GetDirectory("App_Data") + "\\" + filePath;
            writer = new StreamWriter(file, append);
            serializer.Serialize(writer, objectToWrite);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    /// <summary>
    /// Reads an object instance from an XML file.
    /// <para>Object type must have a parameterless constructor.</para>
    /// </summary>
    /// <typeparam name="T">The type of object to read from the file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the XML file.</returns>
    public static T ReadFromXmlFile<T>(string filePath) where T : new()
    {
        TextReader reader = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            string file = GetDirectory("App_Data") + "\\" + filePath;
            reader = new StreamReader(file);
            return (T)serializer.Deserialize(reader);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }

    //    // Write the list of salesman objects to file.
    //    WriteToXmlFile<List<salesman>>("C:\salesmen.txt", salesmanList);

    //// Read the list of salesman objects from the file back into a variable.
    //List<salesman> salesmanList = ReadFromXmlFile<List<salesman>>("C:\salesmen.txt");

    /// <summary>
    /// Writes the given object instance to a Json file.
    /// <para>Object type must have a parameterless constructor.</para>
    /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
    /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
    /// </summary>
    /// <typeparam name="T">The type of object being written to the file.</typeparam>
    /// <param name="filePath">The file path to write the object instance to.</param>
    /// <param name="objectToWrite">The object instance to write to the file.</param>
    /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
    public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter writer = null;
        try
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    /// <summary>
    /// Reads an object instance from an Json file.
    /// <para>Object type must have a parameterless constructor.</para>
    /// </summary>
    /// <typeparam name="T">The type of object to read from the file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the Json file.</returns>
    public static T ReadFromJsonFile<T>(string filePath) where T : new()
    {
        TextReader reader = null;
        try
        {
            reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }
}


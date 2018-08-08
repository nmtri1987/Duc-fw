using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
using System.Runtime.Serialization;
namespace DTP.Object
{
    [DataContract]
    public class DBgl_Country : BaseDBEntity
    {
         [DataMember]
        public int CountryID { get; set; }

         [DataMember]
        public string CountryName { get; set; }
         [DataMember]
        public bool isPublished { get; set; }
         [DataMember]
        public int Orders { get; set; }
         [DataMember]
        public string ImgUrl { get; set; }
         [DataMember]
        public string Description { get; set; }
         [DataMember]
        public string Code { get; set; }
    }

     [CollectionDataContract]
    public class DBgl_CountryCollection : BaseDBEntityCollection<DBgl_Country> { }
    public class DBgl_CountryManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "DTP.Object.gl_Country.all";
        private const string SETTINGS_ID_KEY = "DTP.Object.gl_Country.{0}";

        #endregion
        private static void RemoveCache(DBgl_Country objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.CountryID));
        }
        private static DBgl_Country GetItemFromReader(IDataReader dataReader)
        {
            DBgl_Country objItem = new DBgl_Country();
            objItem.CountryID = SqlHelper.GetInt(dataReader, "CountryID");

            objItem.CountryName = SqlHelper.GetString(dataReader, "CountryName");

            objItem.isPublished = SqlHelper.GetBoolean(dataReader, "isPublished");

            objItem.Orders = SqlHelper.GetInt(dataReader, "Orders");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.Code = SqlHelper.GetString(dataReader, "Code");
            return objItem;
        }
        public static DBgl_Country GetItemByID(Int32 CountryID)
        {
            string key = String.Format(SETTINGS_ID_KEY, CountryID);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_Country)obj2; }


            DBgl_Country item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_GetByID");
            db.AddInParameter(dbCommand, "CountryID", DbType.Int32, CountryID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            dtpCache.Max(key, item);
            return item;

        }
        public static DBgl_Country AddItem(string CountryName, bool isPublished, int Orders, string ImgUrl, string Description, string Code)
        {
            DBgl_Country item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_Add");
            db.AddOutParameter(dbCommand, "CountryID", DbType.Int32, 0);

            db.AddInParameter(dbCommand, "CountryName", DbType.String, CountryName);

            db.AddInParameter(dbCommand, "isPublished", DbType.Boolean, isPublished);

            db.AddInParameter(dbCommand, "Orders", DbType.Int32, Orders);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);

            db.AddInParameter(dbCommand, "Description", DbType.String, Description);

            db.AddInParameter(dbCommand, "Code", DbType.String, Code);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                int itemID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@CountryID"));
                item = GetItemByID(itemID);

                if (item != null)
                {
                    RemoveCache(item);
                }
            }
            return item;
        }
        public static DBgl_Country UpdateItem(int CountryID, string CountryName, bool isPublished, int Orders, string ImgUrl, string Description, string Code)
        {
            DBgl_Country item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_Update");
            db.AddInParameter(dbCommand, "CountryID", DbType.Int32, CountryID);

            db.AddInParameter(dbCommand, "CountryName", DbType.String, CountryName);

            db.AddInParameter(dbCommand, "isPublished", DbType.Boolean, isPublished);

            db.AddInParameter(dbCommand, "Orders", DbType.Int32, Orders);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);

            db.AddInParameter(dbCommand, "Description", DbType.String, Description);

            db.AddInParameter(dbCommand, "Code", DbType.String, Code);
            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(CountryID);
            if (item != null)
            {
                RemoveCache(item);
            }
            return item;
        }
        public static DBgl_CountryCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBgl_CountryCollection ItemCollection = new DBgl_CountryCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_Country item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBgl_CountryCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_CountryCollection)obj2;
            }
            DBgl_CountryCollection ItemCollection = new DBgl_CountryCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_Country item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
        public static string GetJson(DBgl_CountryCollection itemCollection)
        {
            StringBuilder builder = new StringBuilder();
            if (itemCollection.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("results");
                    jsonWriter.WriteStartArray();
                    itemCollection.ForEach(delegate(DBgl_Country objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("CountryID");
                        jsonWriter.WriteValue(objItem.CountryID.ToString());
                        jsonWriter.WritePropertyName("CountryName");
                        jsonWriter.WriteValue(objItem.CountryName.ToString());
                        jsonWriter.WritePropertyName("isPublished");
                        jsonWriter.WriteValue(objItem.isPublished.ToString());
                        jsonWriter.WritePropertyName("Orders");
                        jsonWriter.WriteValue(objItem.Orders.ToString());
                        jsonWriter.WritePropertyName("ImgUrl");
                        jsonWriter.WriteValue(objItem.ImgUrl.ToString());
                        jsonWriter.WritePropertyName("Description");
                        jsonWriter.WriteValue(objItem.Description.ToString());
                        jsonWriter.WritePropertyName("Code");
                        jsonWriter.WriteValue(objItem.Code.ToString());

                        jsonWriter.WriteEndObject();
                    });
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteEndObject();
                    builder.AppendLine(sw.ToString());

                }
            }
            else
            {
              //  builder.AppendLine(@"{""results"":[{""id"":""-1"",""myvalue"":""" + MainFunction.ngonngu("gl.nodata") + @"""}]}");

            }
            return builder.ToString();
        }


        public static int DeleteItem(int ItemId)
        {
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Country_Delete");
            db.AddInParameter(dbCommand, "CountryID", DbType.Int32, ItemId);
            DBgl_Country item = GetItemByID(ItemId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return db.ExecuteNonQuery(dbCommand);
        }
    }
}

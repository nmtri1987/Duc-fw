using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
namespace DTP.Object
{
    public class DBgl_UserInfo : BaseDBEntity
    {
        public int UserInfId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Mobile { get; set; }

        public int CountryId { get; set; }
        //public string CountryName
        //{
        //    get
        //    {
        //        DBgl_Country obItem = DBgl_CountryManager.GetItemByID(CountryId);
        //        return (obItem != null ? obItem.CountryName : "");
        //    }
        //}

        public int LocationId { get; set; }

        //public string LocationName
        //{
        //    get
        //    {
        //        DBgl_Location obItem = DBgl_LocationManager.GetItemByID(LocationId);
        //        return (obItem != null ? obItem.LocName : "");
        //    }
        //}

        public string Address { get; set; }

        public int Rating { get; set; }

        public int MyLegs { get; set; }

        public int EnjoyLegs { get; set; }

        public bool Sex { get; set; }

        public string createdUser { get; set; }
    }
    public class DBgl_UserInfoCollection : BaseDBEntityCollection<DBgl_UserInfo> { }
    public class DBgl_UserInfoManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "dtp.Web.Website.GL.gl_UserInfo.all";
        private const string SETTINGS_ID_KEY = "dtp.Web.Website.GL.gl_UserInfo.{0}";
        private const string SETTINGS_User_KEY = "dtp.Web.Website.GL.gl_UserInfo.UserName{0}";

        #endregion
        private static void RemoveCache(DBgl_UserInfo objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.UserInfId));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.UserName));
        }
        private static DBgl_UserInfo GetItemFromReader(IDataReader dataReader)
        {
            DBgl_UserInfo objItem = new DBgl_UserInfo();
            objItem.UserInfId = SqlHelper.GetInt(dataReader, "UserInfId");

            objItem.UserId = SqlHelper.GetInt(dataReader, "UserId");

            objItem.UserName = SqlHelper.GetString(dataReader, "UserName");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.Mobile = SqlHelper.GetString(dataReader, "Mobile");

            objItem.CountryId = SqlHelper.GetInt(dataReader, "CountryId");

            objItem.LocationId = SqlHelper.GetInt(dataReader, "LocationId");

            objItem.Address = SqlHelper.GetString(dataReader, "Address");

            objItem.Rating = SqlHelper.GetInt(dataReader, "Rating");

            objItem.MyLegs = SqlHelper.GetInt(dataReader, "MyLegs");

            objItem.EnjoyLegs = SqlHelper.GetInt(dataReader, "EnjoyLegs");

            objItem.Sex = SqlHelper.GetBoolean(dataReader, "Sex");

            objItem.createdUser = SqlHelper.GetString(dataReader, "createdUser");
            return objItem;
        }
        public static DBgl_UserInfo GetItemByID(Int64 UserInfId)
        {
            string key = String.Format(SETTINGS_ID_KEY, UserInfId);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_UserInfo)obj2; }


            DBgl_UserInfo item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_GetByID");
            db.AddInParameter(dbCommand, "UserInfId", DbType.Int64, UserInfId);
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
        public static DBgl_UserInfo GetItemByUserName(string UserName)
        {
            string key = String.Format(SETTINGS_User_KEY, UserName);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_UserInfo)obj2; }


            DBgl_UserInfo item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_GetByUserName");
            db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
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
        public static DBgl_UserInfo AddItem(int UserId, string UserName, DateTime CreatedDate, string Mobile, int CountryId, int LocationId, string Address, int Rating, int MyLegs, int EnjoyLegs, bool Sex, string createdUser)
        {
            DBgl_UserInfo item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_Add");
            db.AddOutParameter(dbCommand, "UserInfId", DbType.Int64, 0);

            db.AddInParameter(dbCommand, "UserId", DbType.Int64, UserId);

            db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "Mobile", DbType.String, Mobile);

            db.AddInParameter(dbCommand, "CountryId", DbType.Int32, CountryId);

            db.AddInParameter(dbCommand, "LocationId", DbType.Int32, LocationId);

            db.AddInParameter(dbCommand, "Address", DbType.String, Address);

            db.AddInParameter(dbCommand, "Rating", DbType.Int32, Rating);

            db.AddInParameter(dbCommand, "MyLegs", DbType.Int32, MyLegs);

            db.AddInParameter(dbCommand, "EnjoyLegs", DbType.Int32, EnjoyLegs);

            db.AddInParameter(dbCommand, "Sex", DbType.Boolean, Sex);

            db.AddInParameter(dbCommand, "createdUser", DbType.String, createdUser);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                int itemID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@UserInfId"));
                item = GetItemByID(itemID);

                if (item != null)
                {
                    RemoveCache(item);
                }
            }
            return item;
        }
        public static DBgl_UserInfo UpdateLegs(string strUserName)
        {
            DBgl_UserInfo objInfo = DBgl_UserInfoManager.GetItemByUserName(strUserName);
            if (objInfo != null)
            {
                int joinlegs = objInfo.EnjoyLegs + 1;
                objInfo = DBgl_UserInfoManager.UpdateItem(objInfo.UserInfId, objInfo.UserId,
                objInfo.UserName, objInfo.CreatedDate, objInfo.Mobile, objInfo.CountryId, objInfo.LocationId, objInfo.Address,
                objInfo.Rating, objInfo.MyLegs, joinlegs, objInfo.Sex, objInfo.createdUser);
            }
            return objInfo;
        }
        public static DBgl_UserInfo UpdateItem(int UserInfId, int UserId, string UserName, DateTime CreatedDate, string Mobile, int CountryId, int LocationId, string Address, int Rating, int MyLegs, int EnjoyLegs, bool Sex, string createdUser)
        {
            DBgl_UserInfo item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_Update");
            db.AddInParameter(dbCommand, "UserInfId", DbType.Int64, UserInfId);

            db.AddInParameter(dbCommand, "UserId", DbType.Int64, UserId);

            db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "Mobile", DbType.String, Mobile);

            db.AddInParameter(dbCommand, "CountryId", DbType.Int32, CountryId);

            db.AddInParameter(dbCommand, "LocationId", DbType.Int32, LocationId);

            db.AddInParameter(dbCommand, "Address", DbType.String, Address);

            db.AddInParameter(dbCommand, "Rating", DbType.Int32, Rating);

            db.AddInParameter(dbCommand, "MyLegs", DbType.Int32, MyLegs);

            db.AddInParameter(dbCommand, "EnjoyLegs", DbType.Int32, EnjoyLegs);

            db.AddInParameter(dbCommand, "Sex", DbType.Boolean, Sex);

            db.AddInParameter(dbCommand, "createdUser", DbType.String, createdUser);
            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(UserInfId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return item;
        }
        public static DBgl_UserInfoCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBgl_UserInfoCollection ItemCollection = new DBgl_UserInfoCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_UserInfo item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBgl_UserInfoCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_UserInfoCollection)obj2;
            }
            DBgl_UserInfoCollection ItemCollection = new DBgl_UserInfoCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_UserInfo item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }

        public static string GetJson(DBgl_UserInfoCollection itemCollection)
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
                    itemCollection.ForEach(delegate(DBgl_UserInfo objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("UserInfId");
                        jsonWriter.WriteValue(objItem.UserInfId.ToString());
                        jsonWriter.WritePropertyName("UserId");
                        jsonWriter.WriteValue(objItem.UserId.ToString());
                        jsonWriter.WritePropertyName("UserName");
                        jsonWriter.WriteValue(objItem.UserName.ToString());
                        jsonWriter.WritePropertyName("CreatedDate");
                        jsonWriter.WriteValue(objItem.CreatedDate.ToString());
                        jsonWriter.WritePropertyName("Mobile");
                        jsonWriter.WriteValue(objItem.Mobile.ToString());
                        jsonWriter.WritePropertyName("CountryId");
                        jsonWriter.WriteValue(objItem.CountryId.ToString());
                        //jsonWriter.WritePropertyName("LocationId");
                        //jsonWriter.WriteValue(objItem.LocationId.ToString());
                        jsonWriter.WritePropertyName("Address");
                        jsonWriter.WriteValue(objItem.Address.ToString());
                        jsonWriter.WritePropertyName("Rating");
                        jsonWriter.WriteValue(objItem.Rating.ToString());
                        jsonWriter.WritePropertyName("MyLegs");
                        jsonWriter.WriteValue(objItem.MyLegs.ToString());
                        jsonWriter.WritePropertyName("EnjoyLegs");
                        jsonWriter.WriteValue(objItem.EnjoyLegs.ToString());
                        jsonWriter.WritePropertyName("Sex");
                        jsonWriter.WriteValue(objItem.Sex.ToString());


                        jsonWriter.WritePropertyName("UserImg");
                        DBtblUser myUser;
                        string img = "no_image.gif";
                        myUser = DBtblUserManager.GetItemByID(objItem.UserId);
                        if (myUser != null)
                        {
                            img = myUser.ImgUrl;
                        }
                        jsonWriter.WriteValue(img);

                        jsonWriter.WritePropertyName("createdUser");
                        jsonWriter.WriteValue(objItem.createdUser.ToString());

                        jsonWriter.WriteEndObject();
                    });
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteEndObject();
                    builder.AppendLine(sw.ToString());

                }
            }
            else
            {
             //   builder.AppendLine(@"{""results"":[{""id"":""-1"",""myvalue"":""" + MainFunction.ngonngu("gl.nodata") + @"""}]}");

            }
            return builder.ToString();
        }



        public static int DeleteItem(int ItemId)
        {
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_UserInfo_Delete");
            db.AddInParameter(dbCommand, "UserInfId", DbType.Int32, ItemId);
            DBgl_UserInfo item = GetItemByID(ItemId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return db.ExecuteNonQuery(dbCommand);
        }
    }
}

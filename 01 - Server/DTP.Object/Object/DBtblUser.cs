using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
using System.Runtime.Serialization;
using System.Web.Security;
namespace DTP.Object
{
     [DataContract]
    public class DBtblUser : BaseDBEntity
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public DateTime Birthday { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool Sex { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }

        [DataMember]
        public string SaltKey { get; set; }
    }

    [CollectionDataContract]
    public class DBtblUserCollection : BaseDBEntityCollection<DBtblUser> { }

    public class DBtblUserManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "DTP.Object.tblUser.all";
        private const string SETTINGS_ID_KEY = "DTP.Object.tblUser.{0}";

        #endregion
        private static void RemoveCache(DBtblUser objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.UserID));
        }
        private static DBtblUser GetItemFromReader(IDataReader dataReader)
        {
            DBtblUser objItem = new DBtblUser();
            objItem.UserID = SqlHelper.GetInt(dataReader, "UserID");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.Username = SqlHelper.GetString(dataReader, "Username");

            objItem.Address = SqlHelper.GetString(dataReader, "Address");

            objItem.Birthday = SqlHelper.GetDateTime(dataReader, "Birthday");

            objItem.Email = SqlHelper.GetString(dataReader, "Email");

            objItem.password = SqlHelper.GetString(dataReader, "password");

            objItem.Website = SqlHelper.GetString(dataReader, "Website");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.Sex = SqlHelper.GetBoolean(dataReader, "Sex");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");

            objItem.SaltKey = SqlHelper.GetString(dataReader, "SaltKey");
            return objItem;
        }
        public static DBtblUser GetItemByID(Int64 UserID)
        {
            string key = String.Format(SETTINGS_ID_KEY, UserID);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBtblUser)obj2; }


            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_GetByID");
            db.AddInParameter(dbCommand, "UserID", DbType.Int64, UserID);
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
        public static DBtblUser AddItem(string Name, string Username, string Address, DateTime Birthday, string Email, string password, string Website, DateTime CreatedDate, bool Sex, bool Active, string ImgUrl, string SaltKey)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_Add");
            db.AddOutParameter(dbCommand, "UserID", DbType.Int64, 0);

            db.AddInParameter(dbCommand, "Name", DbType.String, Name);

            db.AddInParameter(dbCommand, "Username", DbType.String, Username);

            db.AddInParameter(dbCommand, "Address", DbType.String, Address);

            db.AddInParameter(dbCommand, "Birthday", DbType.DateTime, Birthday);

            db.AddInParameter(dbCommand, "Email", DbType.String, Email);

            db.AddInParameter(dbCommand, "password", DbType.String, password);

            db.AddInParameter(dbCommand, "Website", DbType.String, Website);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "Sex", DbType.Boolean, Sex);

            db.AddInParameter(dbCommand, "Active", DbType.Boolean, Active);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);

            db.AddInParameter(dbCommand, "SaltKey", DbType.String, SaltKey);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                int itemID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@UserID"));
                item = GetItemByID(itemID);

                if (item != null)
                {
                    RemoveCache(item);
                }
            }
            return item;
        }
        public static DBtblUser UpdateItem(int UserID, string Name, string Username, string Address, DateTime Birthday, string Email, string password, string Website, DateTime CreatedDate, bool Sex, bool Active, string ImgUrl, string SaltKey)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_Update");
            db.AddInParameter(dbCommand, "UserID", DbType.Int64, UserID);

            db.AddInParameter(dbCommand, "Name", DbType.String, Name);

            db.AddInParameter(dbCommand, "Username", DbType.String, Username);

            db.AddInParameter(dbCommand, "Address", DbType.String, Address);

            db.AddInParameter(dbCommand, "Birthday", DbType.DateTime, Birthday);

            db.AddInParameter(dbCommand, "Email", DbType.String, Email);

            db.AddInParameter(dbCommand, "password", DbType.String, password);

            db.AddInParameter(dbCommand, "Website", DbType.String, Website);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "Sex", DbType.Boolean, Sex);

            db.AddInParameter(dbCommand, "Active", DbType.Boolean, Active);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);

            db.AddInParameter(dbCommand, "SaltKey", DbType.String, SaltKey);
            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(UserID);
            if (item != null)
            {
                RemoveCache(item);
            }
            return item;
        }
        public static DBtblUserCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBtblUserCollection ItemCollection = new DBtblUserCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBtblUser item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBtblUserCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBtblUserCollection)obj2;
            }
            DBtblUserCollection ItemCollection = new DBtblUserCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("dtp_tblUser_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBtblUser item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }

        private static string CreatePasswordHash(string Password, string Salt)
        {
            //MD5, SHA1
            string passwordFormat = "";
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";

            return FormsAuthentication.HashPasswordForStoringInConfigFile(Password + Salt, passwordFormat);
        }

        public static DBtblUser GetItemByUser(string Username)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_GetByUser");
            db.AddInParameter(dbCommand, "Username", DbType.String, Username);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            return item;
        }

        public static DBtblUser GetItemByEmail(string email, string password)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_GetByemail");
            db.AddInParameter(dbCommand, "Email", DbType.String, email);
            db.AddInParameter(dbCommand, "password", DbType.String, password);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            return item;
        }
        public static DBtblUser GetItemByEmailOrUser(string UsOrEmail)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("dtp_tblUser_ByEmailOrUserName");
            db.AddInParameter(dbCommand, "UsOrEmail", DbType.String, UsOrEmail);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            return item;
        }
        public static DBtblUser GetItemByEmailOrUser(string Email, string UserName)
        {
            DBtblUser item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("dtp_tblUser_ByEmail_UserName");
            db.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
            db.AddInParameter(dbCommand, "Email", DbType.String, Email);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            return item;
        }
        public static DBtblUser Login(string email, string Password, bool isCookie)
        {
            DBtblUser customer = GetItemByEmailOrUser(email);
            if (customer != null)
            {
                string passwordHash = CreatePasswordHash(Password, customer.SaltKey);
                bool result = customer.password.Equals(passwordHash);
                if (result)
                {
                    return customer;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string GetJson(DBtblUserCollection itemCollection)
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
                    itemCollection.ForEach(delegate(DBtblUser objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("UserID");
                        jsonWriter.WriteValue(objItem.UserID.ToString());
                        jsonWriter.WritePropertyName("Name");
                        jsonWriter.WriteValue(objItem.Name.ToString());
                        jsonWriter.WritePropertyName("Username");
                        jsonWriter.WriteValue(objItem.Username.ToString());
                        jsonWriter.WritePropertyName("Address");
                        jsonWriter.WriteValue(objItem.Address.ToString());
                        jsonWriter.WritePropertyName("Birthday");
                        jsonWriter.WriteValue(objItem.Birthday.ToString());
                        jsonWriter.WritePropertyName("Email");
                        jsonWriter.WriteValue(objItem.Email.ToString());
                        jsonWriter.WritePropertyName("password");
                        jsonWriter.WriteValue(objItem.password.ToString());
                        jsonWriter.WritePropertyName("Website");
                        jsonWriter.WriteValue(objItem.Website.ToString());
                        jsonWriter.WritePropertyName("CreatedDate");
                        jsonWriter.WriteValue(objItem.CreatedDate.ToString());
                        jsonWriter.WritePropertyName("Sex");
                        jsonWriter.WriteValue(objItem.Sex.ToString());
                        jsonWriter.WritePropertyName("Active");
                        jsonWriter.WriteValue(objItem.Active.ToString());
                        jsonWriter.WritePropertyName("ImgUrl");
                        jsonWriter.WriteValue(objItem.ImgUrl.ToString());
                        jsonWriter.WritePropertyName("SaltKey");
                        jsonWriter.WriteValue(objItem.SaltKey.ToString());

                        jsonWriter.WriteEndObject();
                    });
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteEndObject();
                    builder.AppendLine(sw.ToString());

                }
            }
            else
            {
                builder.AppendLine(@"{""results"":[{""id"":""-1"",""myvalue"":""" + "gl.nodata" + @"""}]}");

            }
            return builder.ToString();
        }


        public static int DeleteItem(int ItemId)
        {
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblUser_Delete");
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, ItemId);
            DBtblUser item = GetItemByID(ItemId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return db.ExecuteNonQuery(dbCommand);
        }
    }

    public enum MemberError
    {

        // Summary:
        //     The user was successfully created.
        Success = 0,
        //
        // Summary:
        //     The user name was not found in the database.
        InvalidUserName = 1,
        //
        // Summary:
        //     The password is not formatted correctly.
        InvalidPassword = 2,
        //
        // Summary:
        //     The password question is not formatted correctly.
        InvalidQuestion = 3,
        //
        // Summary:
        //     The password answer is not formatted correctly.
        InvalidAnswer = 4,
        //
        // Summary:
        //     The e-mail address is not formatted correctly.
        InvalidEmail = 5,
        //
        // Summary:
        //     The user name already exists in the database for the application.
        DuplicateUserName = 6,
        //
        // Summary:
        //     The e-mail address already exists in the database for the application.
        DuplicateEmail = 7,
        //
        // Summary:
        //     The user was not created, for a reason defined by the provider.
        UserRejected = 8,
        //
        // Summary:
        //     The provider user key is of an invalid type or format.
        InvalidProviderUserKey = 9,
        //
        // Summary:
        //     The provider user key already exists in the database for the application.
        DuplicateProviderUserKey = 10,
        //
        // Summary:
        //     The provider returned an error that is not described by other System.Web.Security.MembershipCreateStatus
        //     enumeration values.
        ProviderError = 11,

    }
}

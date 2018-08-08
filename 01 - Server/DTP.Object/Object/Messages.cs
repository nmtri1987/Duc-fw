using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace ifinds.Object.CS
{
    [DataContract]
    public class Messages : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Int64 MessageID { get; set; }

        [DataMember]
        public string FromUser { get; set; }

        [DataMember]
        public string FromUImage { get; set; }

        [DataMember]
        public string ToUser { get; set; }

        [DataMember]
        public string ToUImage { get; set; }

        [DataMember]
        public string MesContent { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }


        [DataMember]
        public bool IsRead { get; set; }

        
    }
    public class MessagesCollection : BaseDBEntityCollection<Messages> { }
    public class MessagesManager
    {
        private static Messages GetItemFromReader(IDataReader dataReader)
        {
            Messages objItem = new Messages();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.MessageID = SqlHelper.GetInt64(dataReader, "MessageID");

            objItem.FromUser = SqlHelper.GetString(dataReader, "FromUser");

            objItem.FromUImage = SqlHelper.GetString(dataReader, "FromUImage");

            objItem.ToUser = SqlHelper.GetString(dataReader, "ToUser");

            objItem.ToUImage = SqlHelper.GetString(dataReader, "ToUImage");

            objItem.MesContent = SqlHelper.GetString(dataReader, "MesContent");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.IsRead = SqlHelper.GetBoolean(dataReader, "IsRead");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Messages GetItemByID(Int64 MessageID, int CompanyID)
        {
            Messages item = new Messages();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@MessageID", MessageID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Messages_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Messages AddItem(Messages model)
        {
            Int64 result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Messages_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (Int64)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static Messages UpdateItem(Messages model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Messages_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.MessageID, model.CompanyID);

        }
        public static MessagesCollection GetAllItem(int CompanyID)
        {
            MessagesCollection collection = new MessagesCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Messages_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Messages obj = new Messages();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static MessagesCollection Search(SearchFilter SearchKey)
        {
            MessagesCollection collection = new MessagesCollection();
            using (var reader = SqlHelper.ExecuteReader("Messages_Search", SearchFilterManager.SqlSearchDynParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Messages obj = new Messages();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static MessagesCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            MessagesCollection collection = new MessagesCollection();
            Messages obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("Messages_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static int UpdateStatus(string Fromuser,string Touser, int CompanyID)
        {
            var sqlParams = new SqlParameter[]
              {
                   new SqlParameter("@Fromuser", Fromuser),
                            new SqlParameter("@Touser", Touser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("Messages_UpdateStatus", sqlParams))
            {
               
            }
            return 1;
        }

        public static DataTable GetbyUserName(string UserName, int CompanyID)
        {
            DataTable collection = new DataTable();
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@UserName", UserName),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            DataSet ds = SqlHelper.ExecuteDataset("Messages_GetbyUserName", sqlParams);
            if (ds.Tables.Count > 0)
            {
                collection = ds.Tables[0];
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Messages model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@MessageID", model.MessageID),
                    new SqlParameter("@FromUser", model.FromUser),
                    new SqlParameter("@FromUImage", model.FromUImage),
                    new SqlParameter("@ToUser", model.ToUser),
                    new SqlParameter("@ToUImage", model.ToUImage),
                    new SqlParameter("@MesContent", model.MesContent),
                    new SqlParameter("@IsRead", model.IsRead),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(Int64 itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("Messages_Delete", new SqlParameter[]
            {
                new SqlParameter(@"MessageID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
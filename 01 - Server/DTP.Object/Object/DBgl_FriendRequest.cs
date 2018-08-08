using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
namespace DTP.Object
{
    public class DBgl_FriendRequest : BaseDBEntity
    {
        public int RequestId { get; set; }

        public string FromUser { get; set; }

        public string toUser { get; set; }

        public string Email { get; set; }

        public Guid requesttoken { get; set; }

        public DateTime createdDate { get; set; }

        public string createdUser { get; set; }

        public Guid WorkflowId { get; set; }

        public bool isClick { get; set; }

        public bool IsApprove { get; set; }

        public bool isCancel { get; set; }
        public decimal BudgetAmt { get; set; }

        public decimal ReceivedAmt { get; set; }
        public string comment { get; set; }

        public int RType { get; set; }
    }
    public class DBgl_FriendRequestCollection : BaseDBEntityCollection<DBgl_FriendRequest> { }
    public class DBgl_FriendRequestManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "dtp.Web.Website.GL.gl_FriendRequest.all";
        private const string SETTINGS_ID_KEY = "dtp.Web.Website.GL.gl_FriendRequest.{0}";
        private const string SETTINGS_WorkflowId_KEY = "dtp.Web.Website.GL.gl_FriendRequest.WfID{0}";
        private const string SETTINGS_Approve_WorkflowId_KEY = "dtp.Web.Website.GL.gl_FriendRequestApprove.WfID{0}";
        private const string SETTINGS_Email_User_KEY = "dtp.Web.Website.GL.gl_FriendRequest.WfID{0}{1}{2}";
        private const string SETTINGS_ALL_toUser_KEY = "dtp.Web.Website.GL.gl_FriendRequest.ALLToUser{0}";
        private const string SETTINGS_ALL_FrmUser_KEY = "dtp.Web.Website.GL.gl_FriendRequest.ALLFrmUser{0}";

        #endregion
        private static void RemoveCache(DBgl_FriendRequest objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.RequestId));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ALL_toUser_KEY, objItem.toUser));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ALL_FrmUser_KEY, objItem.FromUser));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_WorkflowId_KEY, objItem.WorkflowId));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_Approve_WorkflowId_KEY, objItem.WorkflowId));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_Email_User_KEY, objItem.Email, objItem.toUser, objItem.WorkflowId));

        }
        private static DBgl_FriendRequest GetItemFromReader(IDataReader dataReader)
        {
            DBgl_FriendRequest objItem = new DBgl_FriendRequest();
            objItem.RequestId = SqlHelper.GetInt(dataReader, "RequestId");

            objItem.FromUser = SqlHelper.GetString(dataReader, "FromUser");

            objItem.toUser = SqlHelper.GetString(dataReader, "toUser");

            objItem.Email = SqlHelper.GetString(dataReader, "Email");

            objItem.requesttoken = SqlHelper.GetGuid(dataReader, "requesttoken");

            objItem.createdDate = SqlHelper.GetDateTime(dataReader, "createdDate");

            objItem.createdUser = SqlHelper.GetString(dataReader, "createdUser");

            objItem.WorkflowId = SqlHelper.GetGuid(dataReader, "WorkflowId");

            objItem.isClick = SqlHelper.GetBoolean(dataReader, "isClick");

            objItem.IsApprove = SqlHelper.GetBoolean(dataReader, "IsApprove");

            objItem.isCancel = SqlHelper.GetBoolean(dataReader, "isCancel");
            objItem.BudgetAmt = SqlHelper.GetDecimal(dataReader, "BudgetAmt");

            objItem.ReceivedAmt = SqlHelper.GetDecimal(dataReader, "ReceivedAmt");
            objItem.comment = SqlHelper.GetString(dataReader, "comment");

            objItem.RType = SqlHelper.GetInt(dataReader, "RType");
            return objItem;
        }
        public static DBgl_FriendRequest GetItemByID(Int64 RequestId)
        {
            string key = String.Format(SETTINGS_ID_KEY, RequestId);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_FriendRequest)obj2; }


            DBgl_FriendRequest item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetByID");
            db.AddInParameter(dbCommand, "RequestId", DbType.Int64, RequestId);
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

        public static DBgl_FriendRequest GetUserInRequestList(Guid WorkflowId, string UserName)
        {
            DBgl_FriendRequest objItem = null;
            DBgl_FriendRequestCollection objcol = DBgl_FriendRequestManager.GetAllItemByWfId(WorkflowId);
            for (int i = 0; i < objcol.Count; i++)
            {
                if (objcol[i].toUser == UserName)
                {
                    objItem = objcol[i];
                    break;
                }
            }
            return objItem;
        }
        public static DBgl_FriendRequest GetUserInRequestList(Guid WorkflowId, Guid RequestTokenId)
        {
            DBgl_FriendRequest objItem = null;
            DBgl_FriendRequestCollection objcol = DBgl_FriendRequestManager.GetAllItemByWfId(WorkflowId);
            for (int i = 0; i < objcol.Count; i++)
            {
                if (objcol[i].requesttoken == RequestTokenId)
                {
                    objItem = objcol[i];
                    break;
                }
            }
            return objItem;
        }
        public static DBgl_FriendRequest GetItemByWorkflowId_UserOrEmail(string Email, string UserName, Guid WorkflowId)
        {
            string key = String.Format(SETTINGS_Email_User_KEY, Email, UserName, WorkflowId);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_FriendRequest)obj2; }


            DBgl_FriendRequest item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_EmailOrUserNameWfId");
            db.AddInParameter(dbCommand, "toUser", DbType.String, UserName);
            db.AddInParameter(dbCommand, "Email", DbType.String, Email);
            db.AddInParameter(dbCommand, "WorkflowId", DbType.Guid, WorkflowId);
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
        public static DBgl_FriendRequest AddItem(string FromUser, string toUser, string Email,
            Guid requesttoken, DateTime createdDate, string createdUser, Guid WorkflowId, bool isClick,
            bool IsApprove, bool isCancel, decimal BudgetAmt, decimal ReceivedAmt, string comment, int RType)
        {
            DBgl_FriendRequest item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_Add");
            db.AddOutParameter(dbCommand, "RequestId", DbType.Int64, 0);

            db.AddInParameter(dbCommand, "FromUser", DbType.String, FromUser);

            db.AddInParameter(dbCommand, "toUser", DbType.String, toUser);

            db.AddInParameter(dbCommand, "Email", DbType.String, Email);

            db.AddInParameter(dbCommand, "requesttoken", DbType.Guid, requesttoken);

            db.AddInParameter(dbCommand, "createdDate", DbType.DateTime, createdDate);

            db.AddInParameter(dbCommand, "createdUser", DbType.String, createdUser);

            db.AddInParameter(dbCommand, "WorkflowId", DbType.Guid, WorkflowId);

            db.AddInParameter(dbCommand, "isClick", DbType.Boolean, isClick);

            db.AddInParameter(dbCommand, "IsApprove", DbType.Boolean, IsApprove);

            db.AddInParameter(dbCommand, "isCancel", DbType.Boolean, isCancel);
            db.AddInParameter(dbCommand, "BudgetAmt", DbType.Decimal, BudgetAmt);

            db.AddInParameter(dbCommand, "ReceivedAmt", DbType.Decimal, ReceivedAmt);
            db.AddInParameter(dbCommand, "comment", DbType.String, comment);

            db.AddInParameter(dbCommand, "RType", DbType.Int32, RType);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                int itemID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@RequestId"));
                item = GetItemByID(itemID);

                if (item != null)
                {
                    RemoveCache(item);
                }
            }
            return item;
        }
        public static DBgl_FriendRequest UpdateItem(int RequestId, string FromUser, string toUser, string Email,
            Guid requesttoken, DateTime createdDate, string createdUser, Guid WorkflowId, bool isClick,
            bool IsApprove, bool isCancel, decimal BudgetAmt, decimal ReceivedAmt, string comment, int RType)
        {
            DBgl_FriendRequest item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_Update");
            db.AddInParameter(dbCommand, "RequestId", DbType.Int64, RequestId);

            db.AddInParameter(dbCommand, "FromUser", DbType.String, FromUser);

            db.AddInParameter(dbCommand, "toUser", DbType.String, toUser);

            db.AddInParameter(dbCommand, "Email", DbType.String, Email);

            db.AddInParameter(dbCommand, "requesttoken", DbType.Guid, requesttoken);

            db.AddInParameter(dbCommand, "createdDate", DbType.DateTime, createdDate);

            db.AddInParameter(dbCommand, "createdUser", DbType.String, createdUser);

            db.AddInParameter(dbCommand, "WorkflowId", DbType.Guid, WorkflowId);

            db.AddInParameter(dbCommand, "isClick", DbType.Boolean, isClick);

            db.AddInParameter(dbCommand, "IsApprove", DbType.Boolean, IsApprove);

            db.AddInParameter(dbCommand, "isCancel", DbType.Boolean, isCancel);
            db.AddInParameter(dbCommand, "BudgetAmt", DbType.Decimal, BudgetAmt);

            db.AddInParameter(dbCommand, "ReceivedAmt", DbType.Decimal, ReceivedAmt);
            db.AddInParameter(dbCommand, "comment", DbType.String, comment);

            db.AddInParameter(dbCommand, "RType", DbType.Int32, RType);
            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(RequestId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return item;
        }
        public static DBgl_FriendRequestCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBgl_FriendRequestCollection GetAllApproveItemByWfId(Guid WorkflowId)
        {
            string key = String.Format(SETTINGS_Approve_WorkflowId_KEY, WorkflowId);
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_FriendRequestCollection)obj2;
            }
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetAllApproveByWfId");
            db.AddInParameter(dbCommand, "WorkflowID", DbType.Guid, WorkflowId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
        public static DBgl_FriendRequestCollection GetAllItemByWfId(Guid WorkflowId)
        {
            string key = String.Format(SETTINGS_WorkflowId_KEY, WorkflowId);
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_FriendRequestCollection)obj2;
            }
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetAllByWfId");
            db.AddInParameter(dbCommand, "WorkflowID", DbType.Guid, WorkflowId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
        public static DBgl_FriendRequestCollection GetAllItem(string toUser)
        {
            string key = string.Format(SETTINGS_ALL_toUser_KEY, toUser);
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_FriendRequestCollection)obj2;
            }
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetAllApproveBytoUser");
            db.AddInParameter(dbCommand, "toUser", DbType.String, toUser);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }

        public static DBgl_FriendRequestCollection GetAllItemFromUser(string fromUser)
        {
            string key = string.Format(SETTINGS_ALL_FrmUser_KEY, fromUser);
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_FriendRequestCollection)obj2;
            }
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetAllApproveByFromUser");
            db.AddInParameter(dbCommand, "FromUser", DbType.String, fromUser);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
        public static DBgl_FriendRequestCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_FriendRequestCollection)obj2;
            }
            DBgl_FriendRequestCollection ItemCollection = new DBgl_FriendRequestCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_FriendRequest item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
        public static string GetJson(DBgl_FriendRequestCollection itemCollection)
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
                    itemCollection.ForEach(delegate(DBgl_FriendRequest objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("RequestId");
                        jsonWriter.WriteValue(objItem.RequestId.ToString());
                        jsonWriter.WritePropertyName("FromUser");
                        jsonWriter.WriteValue(objItem.FromUser.ToString());
                        jsonWriter.WritePropertyName("toUser");
                        jsonWriter.WriteValue(objItem.toUser.ToString());
                        jsonWriter.WritePropertyName("Email");
                        jsonWriter.WriteValue(objItem.Email.ToString());
                        jsonWriter.WritePropertyName("requesttoken");
                        jsonWriter.WriteValue(objItem.requesttoken.ToString());
                        jsonWriter.WritePropertyName("createdDate");
                        jsonWriter.WriteValue(objItem.createdDate.ToString());
                        jsonWriter.WritePropertyName("createdUser");
                        jsonWriter.WriteValue(objItem.createdUser.ToString());
                        jsonWriter.WritePropertyName("WorkflowId");
                        jsonWriter.WriteValue(objItem.WorkflowId.ToString());
                        jsonWriter.WritePropertyName("isClick");
                        jsonWriter.WriteValue(objItem.isClick.ToString());
                        jsonWriter.WritePropertyName("IsApprove");
                        jsonWriter.WriteValue(objItem.IsApprove.ToString());
                        jsonWriter.WritePropertyName("isCancel");
                        jsonWriter.WriteValue(objItem.isCancel.ToString());
                        jsonWriter.WritePropertyName("comment");
                        jsonWriter.WriteValue(objItem.comment.ToString());
                        jsonWriter.WritePropertyName("RType");
                        jsonWriter.WriteValue(objItem.RType.ToString());

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
            DbCommand dbCommand = db.GetStoredProcCommand("gl_FriendRequest_Delete");
            db.AddInParameter(dbCommand, "RequestId", DbType.Int32, ItemId);
            DBgl_FriendRequest objItem = DBgl_FriendRequestManager.GetItemByID(ItemId);
            if (objItem != null) { RemoveCache(objItem); };
            return db.ExecuteNonQuery(dbCommand);
        }
    }
    public enum FriendRequestEnum
    {
        gl_Invited = 2,
        gl_Request = 1
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
using Newtonsoft.Json;
using daitiphu.common.tinhnang;
using DTP.Object;
using System.Runtime.Serialization;
namespace DTP.Object
{
    public enum WFstatus : int
    {
        Detail = 1,
        Location = 2,
        TimeLine = 3,
        Services = 4,
        Published = 5
    }
    [DataContract]
    public class DBgl_Workflow : BaseDBEntity
    {
        [DataMember]
        public Guid WorkflowID { get; set; }

        [DataMember]
        public int WStatus { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public string WorkflowName { get; set; }

        
        public string WorkflowNameTrim
        {
            get
            {
                return HtmlTag.FilterVietkey(WorkflowName.ToString().Replace(" ", "").Replace(":", ""));
            }
        }
        [DataMember]
        public string SmallDesc { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        
        public double DateSpend
        {
            get
            {
                return (ToDate - FromDate).TotalDays;
            }
        }

        [DataMember]
        public decimal BudgetAmt { get; set; }

        [DataMember]
        public decimal ReceiveAmt { get; set; }

        [DataMember]
        public decimal PaymentAmnt { get; set; }

        [DataMember]
        public decimal CashOnHandAmt { get; set; }

        [DataMember]
        public int Members { get; set; }

        [DataMember]
        public bool isClose { get; set; }

        [DataMember]
        public int Ratting { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }

        [DataMember]
        public int SharingType { get; set; }

        [DataMember]
        public int WViews { get; set; }

    }

    [CollectionDataContract]
    public class DBgl_WorkflowCollection : BaseDBEntityCollection<DBgl_Workflow> { }


    [DataContract]
    public class DBgl_WorkflowGroup : BaseDBEntity
    {
        [DataMember]
        public string CreatedUser { get; set; }
        
        [DataMember]
        public int Legs { get; set; }
    }

    [CollectionDataContract]
    public class DBgl_WorkflowGroupCollection : BaseDBEntityCollection<DBgl_WorkflowGroup> { }

    public class DBgl_WorkflowManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "dtp.Web.Website.GL.WF.all";
        private const string SETTINGS_ID_KEY = "dtp.Web.Website.GL.WF.{0}";
        private const string SETTINGS_CreatedUser_KEY = "dtp.Web.Website.GL.WF.User{0}";


        #endregion
        private static void RemoveCache(DBgl_Workflow objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.WorkflowID));
            dtpCache.RemoveByPattern(string.Format(SETTINGS_CreatedUser_KEY, objItem.CreatedUser));
        }
        private static DBgl_WorkflowGroup GetItemGroupFromReader(IDataReader dataReader)
        {
            DBgl_WorkflowGroup objItem = new DBgl_WorkflowGroup();


            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.Legs = SqlHelper.GetInt(dataReader, "Legs");

           
            return objItem;
        }
        public static DBgl_WorkflowGroupCollection GetItemGroupPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBgl_WorkflowGroupCollection ItemCollection = new DBgl_WorkflowGroupCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_WorkflowGroup_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_WorkflowGroup item = GetItemGroupFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        private static DBgl_Workflow GetItemFromReader(IDataReader dataReader)
        {
            DBgl_Workflow objItem = new DBgl_Workflow();
            objItem.WorkflowID = SqlHelper.GetGuid(dataReader, "WorkflowID");

            objItem.WStatus = SqlHelper.GetInt(dataReader, "WStatus");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.Published = SqlHelper.GetBoolean(dataReader, "Published");

            objItem.WorkflowName = SqlHelper.GetString(dataReader, "WorkflowName");

            objItem.SmallDesc = SqlHelper.GetString(dataReader, "SmallDesc");

            objItem.FromDate = SqlHelper.GetDateTime(dataReader, "FromDate");

            objItem.ToDate = SqlHelper.GetDateTime(dataReader, "ToDate");
            objItem.BudgetAmt = SqlHelper.GetDecimal(dataReader, "BudgetAmt");

            objItem.ReceiveAmt = SqlHelper.GetDecimal(dataReader, "ReceiveAmt");

            objItem.PaymentAmnt = SqlHelper.GetDecimal(dataReader, "PaymentAmnt");

            objItem.CashOnHandAmt = SqlHelper.GetDecimal(dataReader, "CashOnHandAmt");
            objItem.Members = SqlHelper.GetInt(dataReader, "Members");

            objItem.isClose = SqlHelper.GetBoolean(dataReader, "isClose");

            objItem.Ratting = SqlHelper.GetInt(dataReader, "Ratting");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");
            objItem.SharingType = SqlHelper.GetInt(dataReader, "SharingType");
            objItem.WViews =  SqlHelper.GetInt(dataReader, "WViews");
            return objItem;
        }
        public static DBgl_WorkflowCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBgl_WorkflowCollection ItemCollection = new DBgl_WorkflowCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_Workflow item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBgl_Workflow GetItemByID(Guid WorkflowID)
        {
            string key = String.Format(SETTINGS_ID_KEY, WorkflowID);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBgl_Workflow)obj2; }


            DBgl_Workflow item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_GetByID");
            db.AddInParameter(dbCommand, "WorkflowID", DbType.Guid, WorkflowID);
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

        public static DBgl_Workflow AddItem(int WStatus,
            DateTime CreatedDate,
            string CreatedUser,
            bool Published,
            string WorkflowName,
            string SmallDesc,
            DateTime FromDate,
            DateTime ToDate, decimal BudgetAmt, decimal ReceiveAmt, decimal PaymentAmnt, decimal CashOnHandAmt, int Members, 
            bool isClose, int Ratting, string ImgUrl,int SharingType)
        {
            DBgl_Workflow item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_Add");
            db.AddOutParameter(dbCommand, "WorkflowID", DbType.Guid, 0);

            db.AddInParameter(dbCommand, "WStatus", DbType.Int32, WStatus);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "CreatedUser", DbType.String, CreatedUser);

            db.AddInParameter(dbCommand, "Published", DbType.Boolean, Published);

            db.AddInParameter(dbCommand, "WorkflowName", DbType.String, HtmlTag.Strip(WorkflowName));

            db.AddInParameter(dbCommand, "SmallDesc", DbType.String, SmallDesc);

            db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);

            db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
            db.AddInParameter(dbCommand, "BudgetAmt", DbType.Decimal, BudgetAmt);

            db.AddInParameter(dbCommand, "ReceiveAmt", DbType.Decimal, ReceiveAmt);

            db.AddInParameter(dbCommand, "PaymentAmnt", DbType.Decimal, PaymentAmnt);

            db.AddInParameter(dbCommand, "CashOnHandAmt", DbType.Decimal, CashOnHandAmt);
            db.AddInParameter(dbCommand, "Members", DbType.Int32, Members);

            db.AddInParameter(dbCommand, "isClose", DbType.Boolean, isClose);

            db.AddInParameter(dbCommand, "Ratting", DbType.Int32, Ratting);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);
            db.AddInParameter(dbCommand, "SharingType", DbType.Int32, SharingType);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                Guid itemID = (Guid)(db.GetParameterValue(dbCommand, "@WorkflowID"));
                item = GetItemByID(itemID);
                if (item != null)
                {
                    //add this user to Friend Request
                    DBtblUser objUser = DBtblUserManager.GetItemByUser(CreatedUser);
                    DBgl_FriendRequest objRequest = DBgl_FriendRequestManager.AddItem(objUser.Username, objUser.Username, 
                        objUser.Email, Guid.NewGuid(), DateTime.Now,
                    objUser.Username, item.WorkflowID, true, true, false, 0, 0, "", (int)FriendRequestEnum.gl_Invited);
                    //UPdate USerInfo Legs
                    DBgl_UserInfo objInfo = DBgl_UserInfoManager.GetItemByUserName(CreatedUser);
                    if (objInfo != null)
                    {
                        int joinlegs = objInfo.MyLegs + 1;
                        int myenjoyLegs = objInfo.EnjoyLegs + 1;
                        objInfo = DBgl_UserInfoManager.UpdateItem(objInfo.UserInfId, objInfo.UserId,
                        objInfo.UserName, objInfo.CreatedDate, objInfo.Mobile, objInfo.CountryId, objInfo.LocationId, objInfo.Address,
                        objInfo.Rating, joinlegs, myenjoyLegs, objInfo.Sex, objInfo.createdUser);
                    }
                    RemoveCache(item);
                }
            }
            return item;
        }
        public static DBgl_Workflow UpdateItem(Guid WorkflowID, int WStatus, DateTime CreatedDate, string CreatedUser, bool Published, string WorkflowName,
            string SmallDesc, DateTime FromDate, DateTime ToDate, decimal BudgetAmt, decimal ReceiveAmt, decimal PaymentAmnt,
            decimal CashOnHandAmt, int Members, bool isClose, int Ratting, string ImgUrl, int SharingType)
        {
            DBgl_Workflow item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_Update");
            db.AddInParameter(dbCommand, "WorkflowID", DbType.Guid, WorkflowID);

            db.AddInParameter(dbCommand, "WStatus", DbType.Int32, WStatus);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, CreatedDate);

            db.AddInParameter(dbCommand, "CreatedUser", DbType.String, CreatedUser);

            db.AddInParameter(dbCommand, "Published", DbType.Boolean, Published);

            db.AddInParameter(dbCommand, "WorkflowName", DbType.String, WorkflowName);

            db.AddInParameter(dbCommand, "SmallDesc", DbType.String, SmallDesc);

            db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);

            db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
            db.AddInParameter(dbCommand, "BudgetAmt", DbType.Decimal, BudgetAmt);

            db.AddInParameter(dbCommand, "ReceiveAmt", DbType.Decimal, ReceiveAmt);

            db.AddInParameter(dbCommand, "PaymentAmnt", DbType.Decimal, PaymentAmnt);

            db.AddInParameter(dbCommand, "CashOnHandAmt", DbType.Decimal, CashOnHandAmt);
            db.AddInParameter(dbCommand, "Members", DbType.Int32, Members);

            db.AddInParameter(dbCommand, "isClose", DbType.Boolean, isClose);

            db.AddInParameter(dbCommand, "Ratting", DbType.Int32, Ratting);

            db.AddInParameter(dbCommand, "ImgUrl", DbType.String, ImgUrl);
            db.AddInParameter(dbCommand, "SharingType", DbType.Int32, SharingType);
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {
                item = GetItemByID(WorkflowID);
                if (item != null)
                {
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static DBgl_WorkflowCollection GetAllItemByCreatedUser(string createdUser)
        {

            string key = String.Format(SETTINGS_CreatedUser_KEY, createdUser);
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_WorkflowCollection)obj2;
            }
            DBgl_WorkflowCollection ItemCollection = new DBgl_WorkflowCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_GetAllByCreatedUser");
            db.AddInParameter(dbCommand, "CreatedUser", DbType.String, createdUser);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_Workflow item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }


        public static string GetJson(DBgl_WorkflowCollection itemCollection,bool showAmount)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
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
                    itemCollection.ForEach(delegate(DBgl_Workflow objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("WorkflowID");
                        jsonWriter.WriteValue(objItem.WorkflowID.ToString());
                        jsonWriter.WritePropertyName("WStatus");
                        jsonWriter.WriteValue(objItem.WStatus.ToString());
                        jsonWriter.WritePropertyName("CreatedDate");
                        jsonWriter.WriteValue(objItem.CreatedDate.ToString());
                        jsonWriter.WritePropertyName("CreatedUser");
                        jsonWriter.WriteValue(objItem.CreatedUser.ToString());
                        jsonWriter.WritePropertyName("Published");
                        jsonWriter.WriteValue(objItem.Published.ToString());
                        jsonWriter.WritePropertyName("WorkflowName");
                        jsonWriter.WriteValue(objItem.WorkflowName.ToString());
                        jsonWriter.WritePropertyName("Linkname");
                        jsonWriter.WriteValue(HtmlTag.FilterVietkey(objItem.WorkflowName.ToString().Replace(" ", "").Replace(":", "")));
                        jsonWriter.WritePropertyName("SmallDesc");
                        jsonWriter.WriteValue(objItem.SmallDesc.ToString());
                        jsonWriter.WritePropertyName("FromDate");
                        jsonWriter.WriteValue(objItem.FromDate.ToShortDateString());
                        jsonWriter.WritePropertyName("ToDate");
                        jsonWriter.WriteValue(objItem.ToDate.ToShortDateString());
                        //jsonWriter.WritePropertyName("DFromDate");
                        //jsonWriter.WriteValue(MainFunction.ShowDayTime(objItem.FromDate));
                        //jsonWriter.WritePropertyName("DToDate");
                        //jsonWriter.WriteValue(MainFunction.ShowDayTime(objItem.ToDate));

                        if (showAmount)
                        {
                            jsonWriter.WritePropertyName("BudgetAmt");
                            jsonWriter.WriteValue(objItem.BudgetAmt.ToString());
                            jsonWriter.WritePropertyName("ReceiveAmt");
                            jsonWriter.WriteValue(objItem.ReceiveAmt.ToString());
                            jsonWriter.WritePropertyName("PaymentAmnt");
                            jsonWriter.WriteValue(objItem.PaymentAmnt.ToString());
                            jsonWriter.WritePropertyName("CashOnHandAmt");
                            jsonWriter.WriteValue(objItem.CashOnHandAmt.ToString());
                        }
                        jsonWriter.WritePropertyName("Members");
                        jsonWriter.WriteValue(objItem.Members.ToString());
                        jsonWriter.WritePropertyName("isClose");
                        jsonWriter.WriteValue(objItem.isClose.ToString());
                        jsonWriter.WritePropertyName("Ratting");
                        jsonWriter.WriteValue(objItem.Ratting.ToString());
                        jsonWriter.WritePropertyName("ImgUrl");
                        jsonWriter.WriteValue(objItem.ImgUrl.ToString());
                        jsonWriter.WritePropertyName("SharingType");
                        jsonWriter.WriteValue(objItem.SharingType.ToString());

                        jsonWriter.WriteEndObject();
                    });
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteEndObject();
                    builder.AppendLine(sw.ToString());

                }
            }
            else
            {
                //builder.AppendLine(@"{""results"":[{""id"":""-1"",""myvalue"":""" + MainFunction.ngonngu("gl.nodata") + @"""}]}");
            }
            return builder.ToString();
        }


        public static DBgl_WorkflowCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBgl_WorkflowCollection)obj2;
            }
            DBgl_WorkflowCollection ItemCollection = new DBgl_WorkflowCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("gl_Workflow_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBgl_Workflow item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }

            dtpCache.Max(key, ItemCollection);

            return ItemCollection;
        }
    }
}

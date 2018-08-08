using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object
{
    [DataContract]
    public class CompanyEvent : BaseDBEntity
    {
        [DataMember]
        public string EventCode { get; set; }

        [DataMember]
        public int EventType { get; set; }

        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public String EventDescription { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public decimal EstimateAmount { get; set; }

        [DataMember]
        public decimal DonateAmount { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public bool isClose { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string updateUser { get; set; }
    }
    public class CompanyEventCollection : BaseDBEntityCollection<CompanyEvent> { }
    public class CompanyEventManager
    {
        private static CompanyEvent GetItemFromReader(IDataReader dataReader)
        {
            CompanyEvent objItem = new CompanyEvent();
            objItem.EventCode = SqlHelper.GetString(dataReader, "EventCode");

            objItem.EventType = SqlHelper.GetInt(dataReader, "EventType");

            objItem.EventName = SqlHelper.GetString(dataReader, "EventName");

            objItem.EventDescription = SqlHelper.GetString(dataReader, "EventDescription");

            objItem.FromDate = SqlHelper.GetDateTime(dataReader, "FromDate");

            objItem.ToDate = SqlHelper.GetDateTime(dataReader, "ToDate");

            objItem.GroupCode = SqlHelper.GetString(dataReader, "GroupCode");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.LocationId = SqlHelper.GetInt(dataReader, "LocationId");

            objItem.LocationName = SqlHelper.GetString(dataReader, "LocationName");

            objItem.Currency = SqlHelper.GetString(dataReader, "Currency");

            objItem.EstimateAmount = SqlHelper.GetDecimal(dataReader, "EstimateAmount");

            objItem.DonateAmount = SqlHelper.GetDecimal(dataReader, "DonateAmount");

            objItem.Balance = SqlHelper.GetDecimal(dataReader, "Balance");

            objItem.isClose = SqlHelper.GetBoolean(dataReader, "isClose");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.UpdateDate = SqlHelper.GetDateTime(dataReader, "UpdateDate");

            objItem.updateUser = SqlHelper.GetString(dataReader, "updateUser");
            return objItem;
        }
        public static CompanyEvent GetItemByID(String eventCode)
        {
            CompanyEvent item = new CompanyEvent();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EventCode", eventCode);
            using (var reader = SqlHelper.ExecuteReader("tblCompanyEvent_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static CompanyEvent AddItem(CompanyEvent model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblCompanyEvent_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }
            catch (Exception objEx)
            {
                result = objEx.Message;
            }
            return GetItemByID(result);

        }
        public static CompanyEvent UpdateItem(CompanyEvent model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblCompanyEvent_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
       
        public static CompanyEventCollection GetAllItem()
        {
            CompanyEventCollection collection = new CompanyEventCollection();
            using (var reader = SqlHelper.ExecuteReader("tblCompanyEvent_GetAll", null))
            {
                while (reader.Read())
                {
                    CompanyEvent obj = new CompanyEvent();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(CompanyEvent model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@EventCode", model.EventCode),
					new SqlParameter("@EventType", model.EventType),
					new SqlParameter("@EventName", model.EventName),
					new SqlParameter("@EventDescription", model.EventDescription),
					new SqlParameter("@FromDate", model.FromDate),
					new SqlParameter("@ToDate", model.ToDate),
					new SqlParameter("@GroupCode", model.GroupCode),
					new SqlParameter("@Active", model.Active),
					new SqlParameter("@LocationId", model.LocationId),
					new SqlParameter("@LocationName", model.LocationName),
					new SqlParameter("@Currency", model.Currency),
					new SqlParameter("@EstimateAmount", model.EstimateAmount),
					new SqlParameter("@DonateAmount", model.DonateAmount),
					new SqlParameter("@Balance", model.Balance),
					new SqlParameter("@isClose", model.isClose),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@UpdateDate", model.UpdateDate),
					new SqlParameter("@updateUser", model.updateUser),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblCompanyEvent_Delete", itemID);
        }

        #region NewMethod

        public static CompanyEventCollection GetbyUser(string CreatedUser)
        {
            CompanyEventCollection collection = new CompanyEventCollection();
            //CompanyEvent obj;
            using (var reader = SqlHelper.ExecuteReader("tblCompanyEvent_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    collection.Add(GetItemFromReader(reader));
                }
            }
            return collection;
        }

        public static CompanyEventCollection GetAllByGroupCode(string GroupCode)
        {
            CompanyEventCollection collection = new CompanyEventCollection();
            using (var reader = SqlHelper.ExecuteReader("tblCompanyEvent_GetAllbyGroupCode", new SqlParameter("@GroupCode", GroupCode)))
            {
                while (reader.Read())
                {
                    CompanyEvent obj = new CompanyEvent();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        #endregion
    }
}

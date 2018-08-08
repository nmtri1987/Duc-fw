using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Server.DAC;
//using Server.Helpers;
namespace DTP.Object
{
    [DataContract]
    public class UserHistory : BaseDBEntity
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CuryId { get; set; }

        [DataMember]
        public string ToCuryId { get; set; }

        [DataMember]
        public string FinPeriod { get; set; }

        [DataMember]
        public decimal Payment { get; set; }

        [DataMember]
        public decimal CuryPayment { get; set; }

        [DataMember]
        public decimal ReceiveAmt { get; set; }

        [DataMember]
        public decimal CuryReceiveAmt { get; set; }

        [DataMember]
        public decimal SaleAmt { get; set; }

        [DataMember]
        public decimal CurySaleAmt { get; set; }

        [DataMember]
        public decimal PurchaseAmt { get; set; }

        [DataMember]
        public decimal CuryPurchaseAmt { get; set; }

        [DataMember]
        public decimal BalanceAmt { get; set; }

        [DataMember]
        public decimal CuryBalanceAmt { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }
    }
    public class UserHistoryCollection : BaseDBEntityCollection<UserHistory> { }
    public class UserHistoryManager
    {
        private static UserHistory GetItemFromReader(IDataReader dataReader)
        {
            UserHistory objItem = new UserHistory();
            objItem.UserName = SqlHelper.GetString(dataReader, "UserName");

            objItem.CuryId = SqlHelper.GetString(dataReader, "CuryId");

            objItem.ToCuryId = SqlHelper.GetString(dataReader, "ToCuryId");

            objItem.FinPeriod = SqlHelper.GetString(dataReader, "FinPeriod");

            objItem.Payment = SqlHelper.GetDecimal(dataReader, "Payment");

            objItem.CuryPayment = SqlHelper.GetDecimal(dataReader, "CuryPayment");

            objItem.ReceiveAmt = SqlHelper.GetDecimal(dataReader, "ReceiveAmt");

            objItem.CuryReceiveAmt = SqlHelper.GetDecimal(dataReader, "CuryReceiveAmt");

            objItem.SaleAmt = SqlHelper.GetDecimal(dataReader, "SaleAmt");

            objItem.CurySaleAmt = SqlHelper.GetDecimal(dataReader, "CurySaleAmt");

            objItem.PurchaseAmt = SqlHelper.GetDecimal(dataReader, "PurchaseAmt");

            objItem.CuryPurchaseAmt = SqlHelper.GetDecimal(dataReader, "CuryPurchaseAmt");

            objItem.BalanceAmt = SqlHelper.GetDecimal(dataReader, "BalanceAmt");

            objItem.CuryBalanceAmt = SqlHelper.GetDecimal(dataReader, "CuryBalanceAmt");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.UpdatedDate = SqlHelper.GetDateTime(dataReader, "UpdatedDate");
            return objItem;
        }
        public static UserHistory GetItemByID(String userName)
        {
            UserHistory item = new UserHistory();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            using (var reader = SqlHelper.ExecuteReader("tblUserHistory_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static UserHistory AddItem(UserHistory model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserHistory_Add", CreateSqlParameter(model)))
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
        public static UserHistory UpdateItem(UserHistory model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserHistory_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static UserHistoryCollection GetAllItem()
        {
            UserHistoryCollection collection = new UserHistoryCollection();
            using (var reader = SqlHelper.ExecuteReader("tblUserHistory_GetAll", null))
            {
                while (reader.Read())
                {
                    UserHistory obj = new UserHistory();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static UserHistoryCollection GetbyUser(string CreatedUser)
        {
            UserHistoryCollection collection = new UserHistoryCollection();
            UserHistory obj;
            using (var reader = SqlHelper.ExecuteReader("tblUserHistory_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(UserHistory model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@CuryId", model.CuryId),
					new SqlParameter("@ToCuryId", model.ToCuryId),
					new SqlParameter("@FinPeriod", model.FinPeriod),
					new SqlParameter("@Payment", model.Payment),
					new SqlParameter("@CuryPayment", model.CuryPayment),
					new SqlParameter("@ReceiveAmt", model.ReceiveAmt),
					new SqlParameter("@CuryReceiveAmt", model.CuryReceiveAmt),
					new SqlParameter("@SaleAmt", model.SaleAmt),
					new SqlParameter("@CurySaleAmt", model.CurySaleAmt),
					new SqlParameter("@PurchaseAmt", model.PurchaseAmt),
					new SqlParameter("@CuryPurchaseAmt", model.CuryPurchaseAmt),
					new SqlParameter("@BalanceAmt", model.BalanceAmt),
					new SqlParameter("@CuryBalanceAmt", model.CuryBalanceAmt),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@UpdatedDate", model.UpdatedDate),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblUserHistory_Delete", itemID);
        }
    }
}
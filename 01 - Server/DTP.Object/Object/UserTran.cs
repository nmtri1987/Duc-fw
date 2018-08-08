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
    public class UserTran : BaseDBEntity
    {
        [DataMember]
        public string TranCode { get; set; }

        [DataMember]
        public string TranType { get; set; }

        [DataMember]
        public string RefNbr { get; set; }

        [DataMember]
        public string TranDesc { get; set; }

        [DataMember]
        public decimal Amt { get; set; }

        [DataMember]
        public decimal CuryAmt { get; set; }

        [DataMember]
        public string CuryId { get; set; }

        [DataMember]
        public DateTime PayDate { get; set; }

        [DataMember]
        public DateTime TranDate { get; set; }


        [DataMember]
        public string FinPeriod { get; set; }

        [DataMember]
        public bool Release { get; set; }

        [DataMember]
        public bool IsIncome { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string PaymentName { get; set; }

        [DataMember]
        public string RevieveName { get; set; }

        [DataMember]
        public decimal Rate { get; set; }
    }
    public class PaymentTran : BaseDBEntity
    {
         [DataMember]
        public string RefNbr { get; set; }
         [DataMember]
        public decimal PayAmount { get; set; }
         [DataMember]
        public DateTime PayDate { get; set; }
         [DataMember]
        public string CreatedUser { get; set; }
         [DataMember]
        public string CuryId { get; set; }
    }

    public class PaymentTranCollection : BaseDBEntityCollection<PaymentTran> { }
    public class PaymentTranManager
    {
        private static PaymentTran GetItemFromReader(IDataReader dataReader)
        {
            PaymentTran objItem = new PaymentTran();

            objItem.RefNbr = SqlHelper.GetString(dataReader, "RefNbr");

            objItem.PayAmount = SqlHelper.GetDecimal(dataReader, "PayAmount");


            objItem.CuryId = SqlHelper.GetString(dataReader, "CuryId");

            objItem.PayDate = SqlHelper.GetDateTime(dataReader, "PayDate");


            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            return objItem;
        }

        public static PaymentTranCollection GetbyUserPaymentTran(UserTranFilter model)
        {
            PaymentTranCollection collection = new PaymentTranCollection();
            PaymentTran obj;
            try
            {
                using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetPaymentTran", new SqlParameter[]
                {
                new SqlParameter("@CreatedUser",model.CreatedUser),
					new SqlParameter("@DateFrom",model.FromDate),
					new SqlParameter("@DateTo", model.ToDate)}))
                {
                    while (reader.Read())
                    {
                        obj = GetItemFromReader(reader);
                        collection.Add(obj);
                    }
                }
            }
            catch (Exception objEx)
            {

            }
            return collection;
        }
        public static PaymentTran GetItemByRefNbr(String RefNbr)
        {
            PaymentTran item = new PaymentTran();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RefNbr", RefNbr);
            using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetByRefNbr", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
    }
    public class UserTranCollection : BaseDBEntityCollection<UserTran> { }
    public class UserTranManager
    {
      

        private static UserTran GetItemFromReader(IDataReader dataReader)
        {
            UserTran objItem = new UserTran();
            objItem.TranCode = SqlHelper.GetString(dataReader, "TranCode");

            objItem.TranType = SqlHelper.GetString(dataReader, "TranType");

            objItem.RefNbr = SqlHelper.GetString(dataReader, "RefNbr");

            objItem.TranDesc = SqlHelper.GetString(dataReader, "TranDesc");

            objItem.Amt = SqlHelper.GetDecimal(dataReader, "Amt");

            objItem.CuryAmt = SqlHelper.GetDecimal(dataReader, "CuryAmt");

            objItem.CuryId = SqlHelper.GetString(dataReader, "CuryId");

            objItem.PayDate = SqlHelper.GetDateTime(dataReader, "PayDate");
            objItem.TranDate = SqlHelper.GetDateTime(dataReader, "TranDate");

            objItem.FinPeriod = SqlHelper.GetString(dataReader, "FinPeriod");

            objItem.Release = SqlHelper.GetBoolean(dataReader, "Release");

            objItem.IsIncome = SqlHelper.GetBoolean(dataReader, "IsIncome");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.PaymentName = SqlHelper.GetString(dataReader, "PaymentName");

            objItem.RevieveName = SqlHelper.GetString(dataReader, "RevieveName");

            objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");
            return objItem;
        }
        public static UserTran GetItemByID(String tranCode)
        {
            UserTran item = new UserTran();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TranCode", tranCode);
            using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        
        public static UserTran AddItem(UserTran model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserTran_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }
            catch (Exception Objex)
            {

            }
            return GetItemByID(result);

        }
        public static UserTran UpdateItem(UserTran model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserTran_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static UserTranCollection GetAllItem()
        {
            UserTranCollection collection = new UserTranCollection();
            using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetAll", null))
            {
                while (reader.Read())
                {
                    UserTran obj = new UserTran();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static UserTranCollection GetbyUser(string CreatedUser)
        {
            UserTranCollection collection = new UserTranCollection();
            UserTran obj;
            using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static UserTranCollection GetbyUserDate(UserTranFilter model)
        {
            UserTranCollection collection = new UserTranCollection();
            UserTran obj;
            try
            {
                using (var reader = SqlHelper.ExecuteReader("tblUserTran_GetAll_byUserByDate", new SqlParameter[]
                {
                new SqlParameter("@CreatedUser",model.CreatedUser),
					new SqlParameter("@DateFrom",model.FromDate),
					new SqlParameter("@DateTo", model.ToDate)}))
                {
                    while (reader.Read())
                    {
                        obj = GetItemFromReader(reader);
                        collection.Add(obj);
                    }
                }
            }
            catch (Exception objEx)
            {

            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(UserTran model)
        {

            return new SqlParameter[]
                {
                new SqlParameter("@TranCode", model.TranCode),
					new SqlParameter("@TranType", model.TranType),
					new SqlParameter("@RefNbr", model.RefNbr),
					new SqlParameter("@TranDesc", model.TranDesc),
					new SqlParameter("@Amt", model.Amt),
					new SqlParameter("@CuryAmt", model.CuryAmt),
					new SqlParameter("@CuryId", model.CuryId),
					new SqlParameter("@PayDate", model.PayDate),
					new SqlParameter("@TranDate", model.TranDate),
					new SqlParameter("@FinPeriod", model.FinPeriod),
					new SqlParameter("@Release", model.Release),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblUserTran_Delete", itemID);
        }
    }
}

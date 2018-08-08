using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object
{
    [DataContract]
    public class EmpBudgetSchedule : BaseDBEntity
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Empcode { get; set; }

        [DataMember]
        public bool IsEmp { get; set; }

        [DataMember]
        public decimal Amt { get; set; }

        [DataMember]
        public DateTime PayDate { get; set; }

        [DataMember]
        public int PayType { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CurrencyID { get; set; }
        [DataMember]
        public int frequency { get; set; }

        [DataMember]
        public bool isExpense { get; set; }

        [DataMember]
        public string TranDesc { get; set; }

        [DataMember]
        public bool Release { get; set; }
    }
    public class EmpBudgetScheduleCollection : BaseDBEntityCollection<EmpBudgetSchedule> { }
    public class EmpBudgetScheduleManager
    {
        private static EmpBudgetSchedule GetItemFromReader(IDataReader dataReader)
        {
            EmpBudgetSchedule objItem = new EmpBudgetSchedule();
            objItem.Code = SqlHelper.GetString(dataReader, "Code");

            objItem.Empcode = SqlHelper.GetString(dataReader, "Empcode");

            objItem.IsEmp = SqlHelper.GetBoolean(dataReader, "IsEmp");

            objItem.Amt = SqlHelper.GetDecimal(dataReader, "Amt");

            objItem.PayDate = SqlHelper.GetDateTime(dataReader, "PayDate");

            objItem.PayType = SqlHelper.GetInt(dataReader, "PayType");

            objItem.EndDate = SqlHelper.GetDateTime(dataReader, "EndDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CurrencyID = SqlHelper.GetString(dataReader, "CurrencyID");
            objItem.frequency = SqlHelper.GetInt(dataReader, "frequency");

            objItem.isExpense = SqlHelper.GetBoolean(dataReader, "isExpense");

            objItem.TranDesc = SqlHelper.GetString(dataReader, "TranDesc");


            objItem.Release = SqlHelper.GetBoolean(dataReader, "Release");

            return objItem;
        }
        public static EmpBudgetSchedule GetItemByID(String code)
        {
            EmpBudgetSchedule item = new EmpBudgetSchedule();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Code", code);
            using (var reader = SqlHelper.ExecuteReader("tblEmpBudgetSchedule_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static EmpBudgetSchedule AddItem(EmpBudgetSchedule model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpBudgetSchedule_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EmpBudgetSchedule UpdateItem(EmpBudgetSchedule model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpBudgetSchedule_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EmpBudgetScheduleCollection GetAllItem()
        {
            EmpBudgetScheduleCollection collection = new EmpBudgetScheduleCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEmpBudgetSchedule_GetAll", null))
            {
                while (reader.Read())
                {
                    EmpBudgetSchedule obj = new EmpBudgetSchedule();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static EmpBudgetScheduleCollection GetAllByUser(string CreatedUser, int PayType=0)
        {
            EmpBudgetScheduleCollection collection = new EmpBudgetScheduleCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEmpBudgetSchedule_GetAllByUser", new SqlParameter("@CreatedUser", CreatedUser),
                    new SqlParameter("@PayType", PayType)))
            {
                while (reader.Read())
                {
                    EmpBudgetSchedule obj = new EmpBudgetSchedule();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(EmpBudgetSchedule model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@Code", model.Code),
					new SqlParameter("@Empcode", model.Empcode),
					new SqlParameter("@IsEmp", model.IsEmp),
					new SqlParameter("@Amt", model.Amt),
					new SqlParameter("@PayDate", model.PayDate),
					new SqlParameter("@PayType", model.PayType),
					new SqlParameter("@EndDate", model.EndDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CurrencyID", model.CurrencyID),
                    new SqlParameter("@frequency", model.frequency),
                    new SqlParameter("@isExpense", model.isExpense),
					new SqlParameter("@TranDesc", model.TranDesc),
                    new SqlParameter("@Release", model.Release),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblEmpBudgetSchedule_Delete", itemID);
        }
    }
}
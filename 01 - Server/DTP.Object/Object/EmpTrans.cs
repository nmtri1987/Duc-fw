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
public class EmpTrans :BaseDBEntity
{
[DataMember]
public string TranCode{ get; set; }

[DataMember]
public string EmpCode{ get; set; }

[DataMember]
public decimal Amt{ get; set; }

[DataMember]
public decimal CuryAmt{ get; set; }

[DataMember]
public decimal Rate{ get; set; }

[DataMember]
public string TranDesc{ get; set; }

[DataMember]
public string PaymentName{ get; set; }

[DataMember]
public string RevieveName{ get; set; }

[DataMember]
public DateTime CreatedDate{ get; set; }

[DataMember]
public string CreatedUser{ get; set; }

[DataMember]
public bool IsEmp{ get; set; }

[DataMember]
public string RefNbr{ get; set; }

[DataMember]
public DateTime Paydate{ get; set; }

[DataMember]
public int PayFinPeriod{ get; set; }

[DataMember]
public bool Posted{ get; set; }
 }
public class EmpTransCollection : BaseDBEntityCollection<EmpTrans> { }
public class EmpTransManager
{
private static EmpTrans GetItemFromReader(IDataReader dataReader)
{
EmpTrans objItem = new EmpTrans();
objItem.TranCode = SqlHelper.GetString(dataReader, "TranCode");

objItem.EmpCode = SqlHelper.GetString(dataReader, "EmpCode");

objItem.Amt = SqlHelper.GetDecimal(dataReader, "Amt");

objItem.CuryAmt = SqlHelper.GetDecimal(dataReader, "CuryAmt");

objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");

objItem.TranDesc = SqlHelper.GetString(dataReader, "TranDesc");

objItem.PaymentName = SqlHelper.GetString(dataReader, "PaymentName");

objItem.RevieveName = SqlHelper.GetString(dataReader, "RevieveName");

objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

objItem.IsEmp = SqlHelper.GetBoolean(dataReader, "IsEmp");

objItem.RefNbr = SqlHelper.GetString(dataReader, "RefNbr");

objItem.Paydate = SqlHelper.GetDateTime(dataReader, "Paydate");

objItem.PayFinPeriod = SqlHelper.GetInt(dataReader, "PayFinPeriod");

objItem.Posted = SqlHelper.GetBoolean(dataReader, "Posted");
 return objItem;
}
 public static EmpTrans GetItemByID(String tranCode)
        {
            EmpTrans item = new EmpTrans();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TranCode",tranCode);
            using (var reader = SqlHelper.ExecuteReader("tblEmpTrans_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static EmpTrans AddItem(EmpTrans model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpTrans_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static EmpTrans UpdateItem(EmpTrans model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpTrans_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static EmpTransCollection GetAllItem()
        {
            EmpTransCollection collection = new EmpTransCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEmpTrans_GetAll", null))
            {
                while (reader.Read())
                {
                    EmpTrans obj = new EmpTrans();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }
 public static EmpTransCollection GetbyUser(string CreatedUser)
        {
            EmpTransCollection collection = new EmpTransCollection();
            EmpTrans obj ;
            using (var reader = SqlHelper.ExecuteReader("tblEmpTrans_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(EmpTrans model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@TranCode", model.TranCode),
					new SqlParameter("@EmpCode", model.EmpCode),
					new SqlParameter("@Amt", model.Amt),
					new SqlParameter("@CuryAmt", model.CuryAmt),
					new SqlParameter("@Rate", model.Rate),
					new SqlParameter("@TranDesc", model.TranDesc),
					new SqlParameter("@PaymentName", model.PaymentName),
					new SqlParameter("@RevieveName", model.RevieveName),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@IsEmp", model.IsEmp),
					new SqlParameter("@RefNbr", model.RefNbr),
					new SqlParameter("@Paydate", model.Paydate),
					new SqlParameter("@PayFinPeriod", model.PayFinPeriod),
					new SqlParameter("@Posted", model.Posted),
					
                };
        }

            public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblEmpTrans_Delete", itemID);
        }
}
}
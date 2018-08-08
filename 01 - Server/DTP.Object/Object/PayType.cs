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
public class PayType :BaseDBEntity
{
[DataMember]
public int PayTypeId{ get; set; }

[DataMember]
public string Name{ get; set; }

[DataMember]
public bool isPayment{ get; set; }

[DataMember]
public bool Active{ get; set; }
 }
public class PayTypeCollection : BaseDBEntityCollection<PayType> { }
public class PayTypeManager
{
private static PayType GetItemFromReader(IDataReader dataReader)
{
PayType objItem = new PayType();
objItem.PayTypeId = SqlHelper.GetInt(dataReader, "PayTypeId");

objItem.Name = SqlHelper.GetString(dataReader, "Name");

objItem.isPayment = SqlHelper.GetBoolean(dataReader, "isPayment");

objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");
 return objItem;
}
 public static PayType GetItemByID(Int32 payTypeId)
        {
            PayType item = new PayType();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@PayTypeId",payTypeId);
            using (var reader = SqlHelper.ExecuteReader("tblPayType_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static PayType AddItem(PayType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblPayType_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static PayType UpdateItem(PayType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblPayType_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static PayTypeCollection GetAllItem()
        {
            PayTypeCollection collection = new PayTypeCollection();
            using (var reader = SqlHelper.ExecuteReader("tblPayType_GetAll", null))
            {
                while (reader.Read())
                {
                    PayType obj = new PayType();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(PayType model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@PayTypeId", model.PayTypeId),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@isPayment", model.isPayment),
					new SqlParameter("@Active", model.Active),
					
                };
        }

            public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblPayType_Delete", itemID);
        }
}
}
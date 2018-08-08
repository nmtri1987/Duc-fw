using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Server.DAC;
//using Server.Helpers;
namespace DTP.Object.GSM
{
[DataContract]
public class PhoneSetup :BaseDBEntity
{
[DataMember]
public int Id{ get; set; }

[DataMember]
public string Port{ get; set; }

[DataMember]
public string PhoneNo{ get; set; }

[DataMember]
public bool Active{ get; set; }
 }
public class PhoneSetupCollection : BaseDBEntityCollection<PhoneSetup> { }
public class PhoneSetupManager
{
private static PhoneSetup GetItemFromReader(IDataReader dataReader)
{
PhoneSetup objItem = new PhoneSetup();
objItem.Id = SqlHelper.GetInt(dataReader, "Id");

objItem.Port = SqlHelper.GetString(dataReader, "Port");

objItem.PhoneNo = SqlHelper.GetString(dataReader, "PhoneNo");

objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");
 return objItem;
}
 public static PhoneSetup GetItemByID(Int32 id)
        {
            PhoneSetup item = new PhoneSetup();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Id",id);
            using (var reader = SqlHelper.ExecuteReader("tblPhoneSetup_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static PhoneSetup AddItem(PhoneSetup model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblPhoneSetup_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static PhoneSetup UpdateItem(PhoneSetup model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblPhoneSetup_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static PhoneSetupCollection GetAllItem()
        {
            PhoneSetupCollection collection = new PhoneSetupCollection();
            using (var reader = SqlHelper.ExecuteReader("tblPhoneSetup_GetAll", null))
            {
                while (reader.Read())
                {
                    PhoneSetup obj = new PhoneSetup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

        public static PhoneSetupCollection GetSimPort()
        {
            PhoneSetupCollection collection = new PhoneSetupCollection();
            using (var reader = SqlHelper.ExecuteReader("ContactsBackup_GetComPort", null))
            {
                while (reader.Read())
                {
                    PhoneSetup obj = new PhoneSetup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static PhoneSetupCollection GetbyUser(string CreatedUser)
        {
            PhoneSetupCollection collection = new PhoneSetupCollection();
            PhoneSetup obj ;
            using (var reader = SqlHelper.ExecuteReader("tblPhoneSetup_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(PhoneSetup model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@Id", model.Id),
					new SqlParameter("@Port", model.Port),
					new SqlParameter("@PhoneNo", model.PhoneNo),
					new SqlParameter("@Active", model.Active),
					
                };
        }

            public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblPhoneSetup_Delete", itemID);
        }
}
}
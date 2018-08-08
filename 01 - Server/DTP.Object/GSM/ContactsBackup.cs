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
public class ContactsBackup :BaseDBEntity
{
[DataMember]
public int Id{ get; set; }

[DataMember]
public string PhoneNumber{ get; set; }

[DataMember]
public string ComPort{ get; set; }

[DataMember]
public DateTime CreatedDate{ get; set; }

[DataMember]
public string SimNumber{ get; set; }
 }
public class ContactsBackupCollection : BaseDBEntityCollection<ContactsBackup> { }
public class ContactsBackupManager
{
private static ContactsBackup GetItemFromReader(IDataReader dataReader)
{
ContactsBackup objItem = new ContactsBackup();
objItem.Id = SqlHelper.GetInt(dataReader, "Id");

objItem.PhoneNumber = SqlHelper.GetString(dataReader, "PhoneNumber");

objItem.ComPort = SqlHelper.GetString(dataReader, "ComPort");

objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

objItem.SimNumber = SqlHelper.GetString(dataReader, "SimNumber");
 return objItem;
}
 public static ContactsBackup GetItemByID(Int32 id)
        {
            ContactsBackup item = new ContactsBackup();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Id",id);
            using (var reader = SqlHelper.ExecuteReader("ContactsBackup_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static ContactsBackup AddItem(ContactsBackup model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ContactsBackup_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static ContactsBackup UpdateItem(ContactsBackup model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ContactsBackup_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static ContactsBackupCollection GetAllItem()
        {
            ContactsBackupCollection collection = new ContactsBackupCollection();
            using (var reader = SqlHelper.ExecuteReader("ContactsBackup_GetAll", null))
            {
                while (reader.Read())
                {
                    ContactsBackup obj = new ContactsBackup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

        public static ContactsBackupCollection GetSimPort()
        {
            ContactsBackupCollection collection = new ContactsBackupCollection();
            using (var reader = SqlHelper.ExecuteReader("ContactsBackup_GetComPort", null))
            {
                while (reader.Read())
                {
                    ContactsBackup obj = new ContactsBackup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ContactsBackupCollection GetbyUser(string CreatedUser)
        {
            ContactsBackupCollection collection = new ContactsBackupCollection();
            ContactsBackup obj ;
            using (var reader = SqlHelper.ExecuteReader("ContactsBackup_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(ContactsBackup model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@Id", model.Id),
					new SqlParameter("@PhoneNumber", model.PhoneNumber),
					new SqlParameter("@ComPort", model.ComPort),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@SimNumber", model.SimNumber),
					
                };
        }

            public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("ContactsBackup_Delete", itemID);
        }
}
}
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
public class ItemClass :BaseDBEntity
{
[DataMember]
public string ClassCode{ get; set; }

[DataMember]
public string ClassName{ get; set; }

[DataMember]
public string ClassDesc{ get; set; }

[DataMember]
public DateTime CreatedDate{ get; set; }

[DataMember]
public string CreatedUser{ get; set; }

[DataMember]
public string GroupCode{ get; set; }
 }
public class ItemClassCollection : BaseDBEntityCollection<ItemClass> { }
public class ItemClassManager
{
private static ItemClass GetItemFromReader(IDataReader dataReader)
{
ItemClass objItem = new ItemClass();
objItem.ClassCode = SqlHelper.GetString(dataReader, "ClassCode");

objItem.ClassName = SqlHelper.GetString(dataReader, "ClassName");

objItem.ClassDesc = SqlHelper.GetString(dataReader, "ClassDesc");

objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

objItem.GroupCode = SqlHelper.GetString(dataReader, "GroupCode");
 return objItem;
}
 public static ItemClass GetItemByID(String classCode)
        {
            ItemClass item = new ItemClass();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ClassCode",classCode);
            using (var reader = SqlHelper.ExecuteReader("tblItemClass_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static ItemClass AddItem(ItemClass model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemClass_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static ItemClass UpdateItem(ItemClass model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemClass_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static ItemClassCollection GetAllItem()
        {
            ItemClassCollection collection = new ItemClassCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemClass_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemClass obj = new ItemClass();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }
 public static ItemClassCollection GetbyUser(string CreatedUser)
        {
            ItemClassCollection collection = new ItemClassCollection();
            ItemClass obj ;
            using (var reader = SqlHelper.ExecuteReader("tblItemClass_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(ItemClass model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ClassCode", model.ClassCode),
					new SqlParameter("@ClassName", model.ClassName),
					new SqlParameter("@ClassDesc", model.ClassDesc),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@GroupCode", model.GroupCode),
					
                };
        }

            public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemClass_Delete", itemID);
        }
}
}
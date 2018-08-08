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
public class ItemEventMapping :BaseDBEntity
{
[DataMember]public string ItemCode{ get; set; }
[DataMember]public string EventCode{ get; set; }
[DataMember]public string ItemName{ get; set; }
[DataMember]public string EventName{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public string CreatedUser{ get; set; }
[DataMember]public string Noted{ get; set; }

}
public class ItemEventMappingCollection : BaseDBEntityCollection<ItemEventMapping> { }
public class ItemEventMappingManager
{
private static ItemEventMapping GetItemFromReader(IDataReader dataReader)
{
ItemEventMapping objItem = new ItemEventMapping();
objItem.ItemCode = SqlHelper.GetString(dataReader, "ItemCode");
objItem.EventCode = SqlHelper.GetString(dataReader, "EventCode");
objItem.ItemName = SqlHelper.GetString(dataReader, "ItemName");
objItem.EventName = SqlHelper.GetString(dataReader, "EventName");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");
objItem.Noted = SqlHelper.GetString(dataReader, "Noted");

return objItem;
}
 public static ItemEventMapping GetItemByID(String itemCode)
        {
            ItemEventMapping item = new ItemEventMapping();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ItemCode",itemCode);
            using (var reader = SqlHelper.ExecuteReader("tblItemEventMapping_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static ItemEventMapping AddItem(ItemEventMapping model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemEventMapping_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static ItemEventMapping UpdateItem(ItemEventMapping model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemEventMapping_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static ItemEventMappingCollection GetAllItem()
        {
            ItemEventMappingCollection collection = new ItemEventMappingCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemEventMapping_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemEventMapping obj = new ItemEventMapping();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }
 public static ItemEventMappingCollection GetbyUser(string CreatedUser)
        {
            ItemEventMappingCollection collection = new ItemEventMappingCollection();
            ItemEventMapping obj ;
            using (var reader = SqlHelper.ExecuteReader("tblItemEventMapping_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(ItemEventMapping model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ItemCode", model.ItemCode),
					new SqlParameter("@EventCode", model.EventCode),
					new SqlParameter("@ItemName", model.ItemName),
					new SqlParameter("@EventName", model.EventName),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@Noted", model.Noted),
					
                };
        }

            public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemEventMapping_Delete", itemID);
        }
}
}
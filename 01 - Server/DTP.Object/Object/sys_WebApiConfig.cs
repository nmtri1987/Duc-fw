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
public class sys_WebApiConfig :BaseDBEntity
{
[DataMember]public string apino{ get; set; }
[DataMember]public string Webiste{ get; set; }

}
public class sys_WebApiConfigCollection : BaseDBEntityCollection<sys_WebApiConfig> { }
public class sys_WebApiConfigManager
{
private static sys_WebApiConfig GetItemFromReader(IDataReader dataReader)
{
sys_WebApiConfig objItem = new sys_WebApiConfig();
objItem.apino = SqlHelper.GetString(dataReader, "apino");
objItem.Webiste = SqlHelper.GetString(dataReader, "Webiste");

return objItem;
}
 public static sys_WebApiConfig GetItemByID(String apino)
        {
            sys_WebApiConfig item = new sys_WebApiConfig();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@apino",apino);
            using (var reader = SqlHelper.ExecuteReader("sys_WebApiConfig_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
            
           
        }
public static sys_WebApiConfig AddItem(sys_WebApiConfig model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "sys_WebApiConfig_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
public static sys_WebApiConfig UpdateItem(sys_WebApiConfig model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "sys_WebApiConfig_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                     result = (String)reader[0];
                }
            }
            return GetItemByID(result);
            
        }
 public static sys_WebApiConfigCollection GetAllItem()
        {
            sys_WebApiConfigCollection collection = new sys_WebApiConfigCollection();
            using (var reader = SqlHelper.ExecuteReader("sys_WebApiConfig_GetAll", null))
            {
                while (reader.Read())
                {
                    sys_WebApiConfig obj = new sys_WebApiConfig();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }
 public static sys_WebApiConfigCollection GetbyUser(string CreatedUser)
        {
            sys_WebApiConfigCollection collection = new sys_WebApiConfigCollection();
            sys_WebApiConfig obj ;
            using (var reader = SqlHelper.ExecuteReader("sys_WebApiConfig_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;     
        }

            private static SqlParameter[] CreateSqlParameter(sys_WebApiConfig model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@apino", model.apino),
					new SqlParameter("@Webiste", model.Webiste),
					
                };
        }

            public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("sys_WebApiConfig_Delete", itemID);
        }
}
}
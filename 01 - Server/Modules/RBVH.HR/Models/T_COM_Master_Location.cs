using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.OG
{
	[DataContract]
    public class T_COM_Master_Location : BaseDBEntity
    {
       
	   [DataMember]public int LocationID{ get; set; }
[DataMember]public string LocationName{ get; set; }
[DataMember]public string LocationShortName{ get; set; }
[DataMember]public string Address_EN{ get; set; }
[DataMember]public string Address_VN{ get; set; }
[DataMember]public int EntityID{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsActive{ get; set; }


    }
    public class T_COM_Master_LocationCollection : BaseDBEntityCollection<T_COM_Master_Location> { }
	 public class T_COM_Master_LocationManager
		{
			private static T_COM_Master_Location GetItemFromReader(IDataReader dataReader)
			{
				T_COM_Master_Location objItem = new T_COM_Master_Location();
			
			     objItem.LocationID = SqlHelper.GetInt(dataReader, "LocationID");
objItem.LocationName = SqlHelper.GetString(dataReader, "LocationName");
objItem.LocationShortName = SqlHelper.GetString(dataReader, "LocationShortName");
objItem.Address_EN = SqlHelper.GetString(dataReader, "Address_EN");
objItem.Address_VN = SqlHelper.GetString(dataReader, "Address_VN");
objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");
objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_COM_Master_Location GetItemByID(int LocationID)
			{
				T_COM_Master_Location item = new T_COM_Master_Location();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@LocationID", LocationID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Location_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_COM_Master_Location AddItem(T_COM_Master_Location model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Location_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_Location UpdateItem(T_COM_Master_Location model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Location_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_LocationCollection GetAllItem()
			{
				T_COM_Master_LocationCollection collection = new T_COM_Master_LocationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Location_GetAll", null))
				{
					while (reader.Read())
					{
						T_COM_Master_Location obj = new T_COM_Master_Location();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

        public static T_COM_Master_LocationCollection GetAllItemByEnity(int EntityID)
        {
            T_COM_Master_LocationCollection collection = new T_COM_Master_LocationCollection();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EntityID", EntityID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Location_GetAllByEntityID", sqlParams))
            {
                while (reader.Read())
                {
                    T_COM_Master_Location obj = new T_COM_Master_Location();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_COM_Master_LocationCollection Search(SearchFilter SearchKey)
			{
				T_COM_Master_LocationCollection collection = new T_COM_Master_LocationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Location_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COM_Master_Location obj = new T_COM_Master_Location();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COM_Master_LocationCollection GetbyUser(string CreatedUser)
			{
				T_COM_Master_LocationCollection collection = new T_COM_Master_LocationCollection();
				T_COM_Master_Location obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Location_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_COM_Master_Location model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@LocationID", model.LocationID),
					new SqlParameter("@LocationName", model.LocationName),
					new SqlParameter("@LocationShortName", model.LocationShortName),
					new SqlParameter("@Address_EN", model.Address_EN),
					new SqlParameter("@Address_VN", model.Address_VN),
					new SqlParameter("@EntityID", model.EntityID),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsActive", model.IsActive),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COM_Master_Location_Delete", itemID);
			}
		}
	}
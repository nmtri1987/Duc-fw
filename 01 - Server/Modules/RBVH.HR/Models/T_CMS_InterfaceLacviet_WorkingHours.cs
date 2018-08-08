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
    public class T_CMS_InterfaceLacviet_WorkingHours : BaseDBEntity
    {
       
	   [DataMember]public int WorkingId{ get; set; }
[DataMember]public int WorkingHours{ get; set; }
[DataMember]public int EntityID{ get; set; }
[DataMember]public string Note{ get; set; }
[DataMember]public int LacvietID{ get; set; }
[DataMember]public int DefaultShiftId{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreateDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_InterfaceLacviet_WorkingHoursCollection : BaseDBEntityCollection<T_CMS_InterfaceLacviet_WorkingHours> { }
	 public class T_CMS_InterfaceLacviet_WorkingHoursManager
		{
			private static T_CMS_InterfaceLacviet_WorkingHours GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_InterfaceLacviet_WorkingHours objItem = new T_CMS_InterfaceLacviet_WorkingHours();
			
			     objItem.WorkingId = SqlHelper.GetInt(dataReader, "WorkingId");
objItem.WorkingHours = SqlHelper.GetInt(dataReader, "WorkingHours");
objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
objItem.Note = SqlHelper.GetString(dataReader, "Note");
objItem.LacvietID = SqlHelper.GetInt(dataReader, "LacvietID");
objItem.DefaultShiftId = SqlHelper.GetInt(dataReader, "DefaultShiftId");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreateDate = SqlHelper.GetDateTime(dataReader, "CreateDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_CMS_InterfaceLacviet_WorkingHours GetItemByID(int WorkingId)
			{
				T_CMS_InterfaceLacviet_WorkingHours item = new T_CMS_InterfaceLacviet_WorkingHours();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@WorkingId", WorkingId);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_InterfaceLacviet_WorkingHours_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_InterfaceLacviet_WorkingHours AddItem(T_CMS_InterfaceLacviet_WorkingHours model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_InterfaceLacviet_WorkingHours_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_InterfaceLacviet_WorkingHours UpdateItem(T_CMS_InterfaceLacviet_WorkingHours model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_InterfaceLacviet_WorkingHours_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_InterfaceLacviet_WorkingHoursCollection GetAllItem()
			{
				T_CMS_InterfaceLacviet_WorkingHoursCollection collection = new T_CMS_InterfaceLacviet_WorkingHoursCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_InterfaceLacviet_WorkingHours_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_InterfaceLacviet_WorkingHours obj = new T_CMS_InterfaceLacviet_WorkingHours();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_InterfaceLacviet_WorkingHoursCollection Search(SearchFilter SearchKey)
			{
				T_CMS_InterfaceLacviet_WorkingHoursCollection collection = new T_CMS_InterfaceLacviet_WorkingHoursCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_InterfaceLacviet_WorkingHours_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_InterfaceLacviet_WorkingHours obj = new T_CMS_InterfaceLacviet_WorkingHours();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_InterfaceLacviet_WorkingHoursCollection GetbyUser(string CreatedUser)
			{
				T_CMS_InterfaceLacviet_WorkingHoursCollection collection = new T_CMS_InterfaceLacviet_WorkingHoursCollection();
				T_CMS_InterfaceLacviet_WorkingHours obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_InterfaceLacviet_WorkingHours_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_InterfaceLacviet_WorkingHours model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@WorkingId", model.WorkingId),
					new SqlParameter("@WorkingHours", model.WorkingHours),
					new SqlParameter("@EntityID", model.EntityID),
					new SqlParameter("@Note", model.Note),
					new SqlParameter("@LacvietID", model.LacvietID),
					new SqlParameter("@DefaultShiftId", model.DefaultShiftId),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreateDate", model.CreateDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_InterfaceLacviet_WorkingHours_Delete", itemID);
			}
		}
	}
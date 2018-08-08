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
    public class T_LMS_Master_AnnualLeave : BaseDBEntity
    {
       
	   [DataMember]public int Grade_Id{ get; set; }
[DataMember]public int NoOfDays{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public string Description{ get; set; }
[DataMember]public bool IsManualEntry{ get; set; }
[DataMember]public bool IsAcitve{ get; set; }


    }
    public class T_LMS_Master_AnnualLeaveCollection : BaseDBEntityCollection<T_LMS_Master_AnnualLeave> { }
	 public class T_LMS_Master_AnnualLeaveManager
		{
			private static T_LMS_Master_AnnualLeave GetItemFromReader(IDataReader dataReader)
			{
				T_LMS_Master_AnnualLeave objItem = new T_LMS_Master_AnnualLeave();
			
			     objItem.Grade_Id = SqlHelper.GetInt(dataReader, "Grade_Id");
objItem.NoOfDays = SqlHelper.GetInt(dataReader, "NoOfDays");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");
objItem.Description = SqlHelper.GetString(dataReader, "Description");
objItem.IsManualEntry = SqlHelper.GetBoolean(dataReader, "IsManualEntry");
objItem.IsAcitve = SqlHelper.GetBoolean(dataReader, "IsAcitve");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_LMS_Master_AnnualLeave GetItemByID(int Grade_Id)
			{
				T_LMS_Master_AnnualLeave item = new T_LMS_Master_AnnualLeave();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@Grade_Id", Grade_Id);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_LMS_Master_AnnualLeave_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_LMS_Master_AnnualLeave AddItem(T_LMS_Master_AnnualLeave model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_LMS_Master_AnnualLeave_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_LMS_Master_AnnualLeave UpdateItem(T_LMS_Master_AnnualLeave model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_LMS_Master_AnnualLeave_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_LMS_Master_AnnualLeaveCollection GetAllItem()
			{
				T_LMS_Master_AnnualLeaveCollection collection = new T_LMS_Master_AnnualLeaveCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_LMS_Master_AnnualLeave_GetAll", null))
				{
					while (reader.Read())
					{
						T_LMS_Master_AnnualLeave obj = new T_LMS_Master_AnnualLeave();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_LMS_Master_AnnualLeaveCollection Search(SearchFilter SearchKey)
			{
				T_LMS_Master_AnnualLeaveCollection collection = new T_LMS_Master_AnnualLeaveCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_LMS_Master_AnnualLeave_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_LMS_Master_AnnualLeave obj = new T_LMS_Master_AnnualLeave();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_LMS_Master_AnnualLeaveCollection GetbyUser(string CreatedUser)
			{
				T_LMS_Master_AnnualLeaveCollection collection = new T_LMS_Master_AnnualLeaveCollection();
				T_LMS_Master_AnnualLeave obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_LMS_Master_AnnualLeave_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_LMS_Master_AnnualLeave model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@Grade_Id", model.Grade_Id),
					new SqlParameter("@NoOfDays", model.NoOfDays),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@IsManualEntry", model.IsManualEntry),
					new SqlParameter("@IsAcitve", model.IsAcitve),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_LMS_Master_AnnualLeave_Delete", itemID);
			}
		}
	}
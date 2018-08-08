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
    public class T_CMS_Master_Salutation : BaseDBEntity
    {
       
	   [DataMember]public int SalutationID{ get; set; }
[DataMember]public string Salutation_EN{ get; set; }
[DataMember]public string Salutation_VN{ get; set; }
[DataMember]public string Gender{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_Master_SalutationCollection : BaseDBEntityCollection<T_CMS_Master_Salutation> { }
	 public class T_CMS_Master_SalutationManager
		{
			private static T_CMS_Master_Salutation GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Master_Salutation objItem = new T_CMS_Master_Salutation();
			
			     objItem.SalutationID = SqlHelper.GetInt(dataReader, "SalutationID");
objItem.Salutation_EN = SqlHelper.GetString(dataReader, "Salutation_EN");
objItem.Salutation_VN = SqlHelper.GetString(dataReader, "Salutation_VN");
objItem.Gender = SqlHelper.GetString(dataReader, "Gender");
objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_CMS_Master_Salutation GetItemByID(int SalutationID)
			{
				T_CMS_Master_Salutation item = new T_CMS_Master_Salutation();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@SalutationID", SalutationID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_Salutation_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_Master_Salutation AddItem(T_CMS_Master_Salutation model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_Salutation_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_Salutation UpdateItem(T_CMS_Master_Salutation model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_Salutation_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_SalutationCollection GetAllItem()
			{
				T_CMS_Master_SalutationCollection collection = new T_CMS_Master_SalutationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_Salutation_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Master_Salutation obj = new T_CMS_Master_Salutation();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_Master_SalutationCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Master_SalutationCollection collection = new T_CMS_Master_SalutationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_Salutation_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Master_Salutation obj = new T_CMS_Master_Salutation();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Master_SalutationCollection GetbyUser(string CreatedUser)
			{
				T_CMS_Master_SalutationCollection collection = new T_CMS_Master_SalutationCollection();
				T_CMS_Master_Salutation obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_Salutation_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Master_Salutation model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@SalutationID", model.SalutationID),
					new SqlParameter("@Salutation_EN", model.Salutation_EN),
					new SqlParameter("@Salutation_VN", model.Salutation_VN),
					new SqlParameter("@Gender", model.Gender),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Master_Salutation_Delete", itemID);
			}
		}
	}
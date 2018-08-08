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
    public class T_CMS_Master_EmploymentSubType : BaseDBEntity
    {
       
	   [DataMember]public int EmpTypeID{ get; set; }
[DataMember]public int EmpSubTypeID{ get; set; }
[DataMember]public string EmpSubTypeDescription{ get; set; }
[DataMember]public int LacvietContractType{ get; set; }
[DataMember]public bool IsExpat{ get; set; }
[DataMember]public bool IsMlevel{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_Master_EmploymentSubTypeCollection : BaseDBEntityCollection<T_CMS_Master_EmploymentSubType> { }
	 public class T_CMS_Master_EmploymentSubTypeManager
		{
			private static T_CMS_Master_EmploymentSubType GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Master_EmploymentSubType objItem = new T_CMS_Master_EmploymentSubType();
			
			     objItem.EmpTypeID = SqlHelper.GetInt(dataReader, "EmpTypeID");
objItem.EmpSubTypeID = SqlHelper.GetInt(dataReader, "EmpSubTypeID");
objItem.EmpSubTypeDescription = SqlHelper.GetString(dataReader, "EmpSubTypeDescription");
objItem.LacvietContractType = SqlHelper.GetInt(dataReader, "LacvietContractType");
objItem.IsExpat = SqlHelper.GetBoolean(dataReader, "IsExpat");
objItem.IsMlevel = SqlHelper.GetBoolean(dataReader, "IsMlevel");
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
			public static T_CMS_Master_EmploymentSubType GetItemByID(int EmpSubTypeID)
			{
				T_CMS_Master_EmploymentSubType item = new T_CMS_Master_EmploymentSubType();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@EmpSubTypeID", EmpSubTypeID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentSubType_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_Master_EmploymentSubType AddItem(T_CMS_Master_EmploymentSubType model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_EmploymentSubType_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_EmploymentSubType UpdateItem(T_CMS_Master_EmploymentSubType model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_EmploymentSubType_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_EmploymentSubTypeCollection GetAllItem()
			{
				T_CMS_Master_EmploymentSubTypeCollection collection = new T_CMS_Master_EmploymentSubTypeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentSubType_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Master_EmploymentSubType obj = new T_CMS_Master_EmploymentSubType();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_Master_EmploymentSubTypeCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Master_EmploymentSubTypeCollection collection = new T_CMS_Master_EmploymentSubTypeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentSubType_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Master_EmploymentSubType obj = new T_CMS_Master_EmploymentSubType();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Master_EmploymentSubTypeCollection GetAllByEmpTypeID(int EmpTypeID)
			{
				T_CMS_Master_EmploymentSubTypeCollection collection = new T_CMS_Master_EmploymentSubTypeCollection();
				T_CMS_Master_EmploymentSubType obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_EmploymentSubType_GetAll_EmpTypeID", new SqlParameter("@EmpTypeID", EmpTypeID)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Master_EmploymentSubType model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@EmpTypeID", model.EmpTypeID),
					new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
					new SqlParameter("@EmpSubTypeDescription", model.EmpSubTypeDescription),
					new SqlParameter("@LacvietContractType", model.LacvietContractType),
					new SqlParameter("@IsExpat", model.IsExpat),
					new SqlParameter("@IsMlevel", model.IsMlevel),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Master_EmploymentSubType_Delete", itemID);
			}
		}
	}
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
    public class T_CMS_Master_EmploymentType : BaseDBEntity
    {
       
	   [DataMember]public int EmpTypeID{ get; set; }
[DataMember]public string EmpTypeDescription{ get; set; }
[DataMember]public string EmpTypeCode{ get; set; }
[DataMember]public int EntityID{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_Master_EmploymentTypeCollection : BaseDBEntityCollection<T_CMS_Master_EmploymentType> { }
	 public class T_CMS_Master_EmploymentTypeManager
		{
			private static T_CMS_Master_EmploymentType GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Master_EmploymentType objItem = new T_CMS_Master_EmploymentType();
			
			     objItem.EmpTypeID = SqlHelper.GetInt(dataReader, "EmpTypeID");
objItem.EmpTypeDescription = SqlHelper.GetString(dataReader, "EmpTypeDescription");
objItem.EmpTypeCode = SqlHelper.GetString(dataReader, "EmpTypeCode");
objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
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
			public static T_CMS_Master_EmploymentType GetItemByID(int EmpTypeID)
			{
				T_CMS_Master_EmploymentType item = new T_CMS_Master_EmploymentType();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@EmpTypeID", EmpTypeID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentType_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_Master_EmploymentType AddItem(T_CMS_Master_EmploymentType model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_EmploymentType_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_EmploymentType UpdateItem(T_CMS_Master_EmploymentType model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_EmploymentType_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_EmploymentTypeCollection GetAllItem()
			{
				T_CMS_Master_EmploymentTypeCollection collection = new T_CMS_Master_EmploymentTypeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentType_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Master_EmploymentType obj = new T_CMS_Master_EmploymentType();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

        public static T_CMS_Master_EmploymentTypeCollection GetAllItemByEnity(int EntityID)
        {
            T_CMS_Master_EmploymentTypeCollection collection = new T_CMS_Master_EmploymentTypeCollection();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EntityID", EntityID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_EmploymentType_GetAllByEntityID", sqlParams))
            {
                while (reader.Read())
                {
                    T_CMS_Master_EmploymentType obj = new T_CMS_Master_EmploymentType();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_CMS_Master_EmploymentTypeCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Master_EmploymentTypeCollection collection = new T_CMS_Master_EmploymentTypeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentType_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Master_EmploymentType obj = new T_CMS_Master_EmploymentType();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Master_EmploymentTypeCollection GetbyUser(string CreatedUser)
			{
				T_CMS_Master_EmploymentTypeCollection collection = new T_CMS_Master_EmploymentTypeCollection();
				T_CMS_Master_EmploymentType obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_EmploymentType_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Master_EmploymentType model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@EmpTypeID", model.EmpTypeID),
					new SqlParameter("@EmpTypeDescription", model.EmpTypeDescription),
					new SqlParameter("@EmpTypeCode", model.EmpTypeCode),
					new SqlParameter("@EntityID", model.EntityID),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Master_EmploymentType_Delete", itemID);
			}
		}
	}
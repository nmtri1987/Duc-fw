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
    public class T_CMS_Static_ContractTerm : BaseDBEntity
    {
       
	   [DataMember]public int ID{ get; set; }
[DataMember]public int EmpSubTypeID{ get; set; }
[DataMember]public int ContractTermMonths{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public string PeriodDescription{ get; set; }


    }
    public class T_CMS_Static_ContractTermCollection : BaseDBEntityCollection<T_CMS_Static_ContractTerm> { }
	 public class T_CMS_Static_ContractTermManager
		{
			private static T_CMS_Static_ContractTerm GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Static_ContractTerm objItem = new T_CMS_Static_ContractTerm();
			
			     objItem.ID = SqlHelper.GetInt(dataReader, "ID");
objItem.EmpSubTypeID = SqlHelper.GetInt(dataReader, "EmpSubTypeID");
objItem.ContractTermMonths = SqlHelper.GetInt(dataReader, "ContractTermMonths");
objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");
objItem.PeriodDescription = SqlHelper.GetString(dataReader, "PeriodDescription");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_CMS_Static_ContractTerm GetItemByID(int ID)
			{
				T_CMS_Static_ContractTerm item = new T_CMS_Static_ContractTerm();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@ID", ID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_ContractTerm_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_Static_ContractTerm AddItem(T_CMS_Static_ContractTerm model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Static_ContractTerm_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Static_ContractTerm UpdateItem(T_CMS_Static_ContractTerm model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Static_ContractTerm_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Static_ContractTermCollection GetAllItem()
			{
				T_CMS_Static_ContractTermCollection collection = new T_CMS_Static_ContractTermCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_ContractTerm_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Static_ContractTerm obj = new T_CMS_Static_ContractTerm();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_Static_ContractTermCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Static_ContractTermCollection collection = new T_CMS_Static_ContractTermCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_ContractTerm_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Static_ContractTerm obj = new T_CMS_Static_ContractTerm();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Static_ContractTermCollection GetbyUser(string CreatedUser)
			{
				T_CMS_Static_ContractTermCollection collection = new T_CMS_Static_ContractTermCollection();
				T_CMS_Static_ContractTerm obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_ContractTerm_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Static_ContractTerm model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@ID", model.ID),
					new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
					new SqlParameter("@ContractTermMonths", model.ContractTermMonths),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@PeriodDescription", model.PeriodDescription),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Static_ContractTerm_Delete", itemID);
			}
		}
	}
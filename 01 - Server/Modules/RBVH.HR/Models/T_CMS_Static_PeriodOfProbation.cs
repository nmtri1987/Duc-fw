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
    public class T_CMS_Static_PeriodOfProbation : BaseDBEntity
    {
       
	   [DataMember]public int ID{ get; set; }
[DataMember]public int EmpSubTypeID{ get; set; }
[DataMember]public int Period{ get; set; }
[DataMember]public string PeriodDescription{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_Static_PeriodOfProbationCollection : BaseDBEntityCollection<T_CMS_Static_PeriodOfProbation> { }
	 public class T_CMS_Static_PeriodOfProbationManager
		{
			private static T_CMS_Static_PeriodOfProbation GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Static_PeriodOfProbation objItem = new T_CMS_Static_PeriodOfProbation();
			
			     objItem.ID = SqlHelper.GetInt(dataReader, "ID");
objItem.EmpSubTypeID = SqlHelper.GetInt(dataReader, "EmpSubTypeID");
objItem.Period = SqlHelper.GetInt(dataReader, "Period");
objItem.PeriodDescription = SqlHelper.GetString(dataReader, "PeriodDescription");
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
			public static T_CMS_Static_PeriodOfProbation GetItemByID(int ID)
			{
				T_CMS_Static_PeriodOfProbation item = new T_CMS_Static_PeriodOfProbation();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@ID", ID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_PeriodOfProbation_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_CMS_Static_PeriodOfProbation AddItem(T_CMS_Static_PeriodOfProbation model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Static_PeriodOfProbation_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Static_PeriodOfProbation UpdateItem(T_CMS_Static_PeriodOfProbation model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Static_PeriodOfProbation_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Static_PeriodOfProbationCollection GetAllItem()
			{
				T_CMS_Static_PeriodOfProbationCollection collection = new T_CMS_Static_PeriodOfProbationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_PeriodOfProbation_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Static_PeriodOfProbation obj = new T_CMS_Static_PeriodOfProbation();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_Static_PeriodOfProbationCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Static_PeriodOfProbationCollection collection = new T_CMS_Static_PeriodOfProbationCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_PeriodOfProbation_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Static_PeriodOfProbation obj = new T_CMS_Static_PeriodOfProbation();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Static_PeriodOfProbationCollection GetbyUser(string CreatedUser)
			{
				T_CMS_Static_PeriodOfProbationCollection collection = new T_CMS_Static_PeriodOfProbationCollection();
				T_CMS_Static_PeriodOfProbation obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Static_PeriodOfProbation_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Static_PeriodOfProbation model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@ID", model.ID),
					new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
					new SqlParameter("@Period", model.Period),
					new SqlParameter("@PeriodDescription", model.PeriodDescription),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Static_PeriodOfProbation_Delete", itemID);
			}
		}
	}
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
    public class T_COM_Master_PlaceOfIssue : BaseDBEntity
    {
       
	   [DataMember]public int POI_ID{ get; set; }
[DataMember]public string POI_Name_EN{ get; set; }
[DataMember]public string POI_Name_VN{ get; set; }
[DataMember]public int POI_SICode{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsActive{ get; set; }


    }
    public class T_COM_Master_PlaceOfIssueCollection : BaseDBEntityCollection<T_COM_Master_PlaceOfIssue> { }
	 public class T_COM_Master_PlaceOfIssueManager
		{
			private static T_COM_Master_PlaceOfIssue GetItemFromReader(IDataReader dataReader)
			{
				T_COM_Master_PlaceOfIssue objItem = new T_COM_Master_PlaceOfIssue();
			
			     objItem.POI_ID = SqlHelper.GetInt(dataReader, "POI_ID");
objItem.POI_Name_EN = SqlHelper.GetString(dataReader, "POI_Name_EN");
objItem.POI_Name_VN = SqlHelper.GetString(dataReader, "POI_Name_VN");
objItem.POI_SICode = SqlHelper.GetInt(dataReader, "POI_SICode");
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
			public static T_COM_Master_PlaceOfIssue GetItemByID(int POI_ID)
			{
				T_COM_Master_PlaceOfIssue item = new T_COM_Master_PlaceOfIssue();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@POI_ID", POI_ID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_PlaceOfIssue_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
        public static T_COM_Master_PlaceOfIssue GetItemByPOI_Name_VN(string POI_Name_VN)
        {
            T_COM_Master_PlaceOfIssue item = new T_COM_Master_PlaceOfIssue();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@POI_Name_VN", POI_Name_VN);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_PlaceOfIssue_GetByPOI_Name_VN", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        
            public static T_COM_Master_PlaceOfIssue AddItem(T_COM_Master_PlaceOfIssue model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_PlaceOfIssue_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_PlaceOfIssue UpdateItem(T_COM_Master_PlaceOfIssue model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_PlaceOfIssue_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_PlaceOfIssueCollection GetAllItem()
			{
				T_COM_Master_PlaceOfIssueCollection collection = new T_COM_Master_PlaceOfIssueCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_PlaceOfIssue_GetAll", null))
				{
					while (reader.Read())
					{
						T_COM_Master_PlaceOfIssue obj = new T_COM_Master_PlaceOfIssue();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_COM_Master_PlaceOfIssueCollection Search(SearchFilter SearchKey)
			{
				T_COM_Master_PlaceOfIssueCollection collection = new T_COM_Master_PlaceOfIssueCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_PlaceOfIssue_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COM_Master_PlaceOfIssue obj = new T_COM_Master_PlaceOfIssue();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COM_Master_PlaceOfIssueCollection GetbyUser(string CreatedUser)
			{
				T_COM_Master_PlaceOfIssueCollection collection = new T_COM_Master_PlaceOfIssueCollection();
				T_COM_Master_PlaceOfIssue obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_PlaceOfIssue_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_COM_Master_PlaceOfIssue model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@POI_ID", model.POI_ID),
					new SqlParameter("@POI_Name_EN", model.POI_Name_EN),
					new SqlParameter("@POI_Name_VN", model.POI_Name_VN),
					new SqlParameter("@POI_SICode", model.POI_SICode),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsActive", model.IsActive),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COM_Master_PlaceOfIssue_Delete", itemID);
			}
		}
	}
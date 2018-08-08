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
    public class T_CMS_Master_WorkHours : BaseDBEntity
    {
       
	   [DataMember]public int WorkHoursID{ get; set; }
[DataMember]public int WorkHours{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public bool IsPartTime{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }


    }
    public class T_CMS_Master_WorkHoursCollection : BaseDBEntityCollection<T_CMS_Master_WorkHours> { }
    public class T_CMS_Master_WorkHoursResponse : SearchResponse<T_CMS_Master_WorkHours>
    {
    }
    public class T_CMS_Master_WorkHoursManager
		{
			private static T_CMS_Master_WorkHours GetItemFromReader(IDataReader dataReader)
			{
				T_CMS_Master_WorkHours objItem = new T_CMS_Master_WorkHours();
			
			     objItem.WorkHoursID = SqlHelper.GetInt(dataReader, "WorkHoursID");
objItem.WorkHours = SqlHelper.GetInt(dataReader, "WorkHours");
objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");
objItem.IsPartTime = SqlHelper.GetBoolean(dataReader, "IsPartTime");
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
			public static T_CMS_Master_WorkHours GetItemByID(int WorkHoursID)
			{
				T_CMS_Master_WorkHours item = new T_CMS_Master_WorkHours();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@WorkHoursID", WorkHoursID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_WorkHours_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}

        public static T_CMS_Master_WorkHours GetItemByWorkHour(string WorkHours)
        {
            T_CMS_Master_WorkHours item = new T_CMS_Master_WorkHours();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@WorkHours", WorkHours);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_WorkHours_GetByWorkHours", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_CMS_Master_WorkHours AddItem(T_CMS_Master_WorkHours model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_WorkHours_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_WorkHours UpdateItem(T_CMS_Master_WorkHours model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_CMS_Master_WorkHours_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_CMS_Master_WorkHoursCollection GetAllItem()
			{
				T_CMS_Master_WorkHoursCollection collection = new T_CMS_Master_WorkHoursCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_WorkHours_GetAll", null))
				{
					while (reader.Read())
					{
						T_CMS_Master_WorkHours obj = new T_CMS_Master_WorkHours();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_CMS_Master_WorkHoursCollection Search(SearchFilter SearchKey)
			{
				T_CMS_Master_WorkHoursCollection collection = new T_CMS_Master_WorkHoursCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_WorkHours_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_CMS_Master_WorkHours obj = new T_CMS_Master_WorkHours();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_CMS_Master_WorkHoursCollection GetbyUser(string CreatedUser)
			{
				T_CMS_Master_WorkHoursCollection collection = new T_CMS_Master_WorkHoursCollection();
				T_CMS_Master_WorkHours obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_CMS_Master_WorkHours_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_CMS_Master_WorkHours model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@WorkHoursID", model.WorkHoursID),
					new SqlParameter("@WorkHours", model.WorkHours),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@IsPartTime", model.IsPartTime),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_CMS_Master_WorkHours_Delete", itemID);
			}
		}
	}
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
    public class T_COM_Master_Grade : BaseDBEntity
    {
       
	   [DataMember]public int GradeID{ get; set; }
[DataMember]public string GradeName{ get; set; }
[DataMember]public int EntityID{ get; set; }
[DataMember]public bool IsMLevel{ get; set; }
[DataMember]public string EmployeeType{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsActive{ get; set; }
[DataMember]public int Hierarchical_Level{ get; set; }


    }
    public class T_COM_Master_GradeCollection : BaseDBEntityCollection<T_COM_Master_Grade> { }
	 public class T_COM_Master_GradeManager
		{
			private static T_COM_Master_Grade GetItemFromReader(IDataReader dataReader)
			{
				T_COM_Master_Grade objItem = new T_COM_Master_Grade();
			
			     objItem.GradeID = SqlHelper.GetInt(dataReader, "GradeID");
objItem.GradeName = SqlHelper.GetString(dataReader, "GradeName");
objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
objItem.IsMLevel = SqlHelper.GetBoolean(dataReader, "IsMLevel");
objItem.EmployeeType = SqlHelper.GetString(dataReader, "EmployeeType");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");
objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");
objItem.Hierarchical_Level = SqlHelper.GetInt(dataReader, "Hierarchical_Level");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_COM_Master_Grade GetItemByID(int GradeID)
			{
				T_COM_Master_Grade item = new T_COM_Master_Grade();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@GradeID", GradeID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Grade_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}

        public static T_COM_Master_Grade GetItemByGradeName(string GradeName)
        {
            T_COM_Master_Grade item = new T_COM_Master_Grade();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@GradeName", GradeName);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Grade_GetByGradeName", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_COM_Master_Grade AddItem(T_COM_Master_Grade model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Grade_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_Grade UpdateItem(T_COM_Master_Grade model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Grade_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_GradeCollection GetAllItem()
			{
				T_COM_Master_GradeCollection collection = new T_COM_Master_GradeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Grade_GetAll", null))
				{
					while (reader.Read())
					{
						T_COM_Master_Grade obj = new T_COM_Master_Grade();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_COM_Master_GradeCollection Search(SearchFilter SearchKey)
			{
				T_COM_Master_GradeCollection collection = new T_COM_Master_GradeCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Grade_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COM_Master_Grade obj = new T_COM_Master_Grade();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COM_Master_GradeCollection GetbyUser(string CreatedUser)
			{
				T_COM_Master_GradeCollection collection = new T_COM_Master_GradeCollection();
				T_COM_Master_Grade obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Grade_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_COM_Master_Grade model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@GradeID", model.GradeID),
					new SqlParameter("@GradeName", model.GradeName),
					new SqlParameter("@EntityID", model.EntityID),
					new SqlParameter("@IsMLevel", model.IsMLevel),
					new SqlParameter("@EmployeeType", model.EmployeeType),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsActive", model.IsActive),
					new SqlParameter("@Hierarchical_Level", model.Hierarchical_Level),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COM_Master_Grade_Delete", itemID);
			}
		}
	}
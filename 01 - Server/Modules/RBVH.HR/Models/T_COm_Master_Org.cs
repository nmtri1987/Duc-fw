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
    public class T_COm_Master_Org : BaseDBEntity
    {
       
	   [DataMember]public int OrgId{ get; set; }
[DataMember]public string OrgName{ get; set; }
[DataMember]public string Description{ get; set; }
[DataMember]public int Entity_Id{ get; set; }
[DataMember]public int UnitType_Id{ get; set; }
[DataMember]public int Holder_Id{ get; set; }
[DataMember]public int ParentOrg_Id{ get; set; }
[DataMember]public bool Plant_Type{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsActive{ get; set; }


    }
    public class T_COm_Master_OrgCollection : BaseDBEntityCollection<T_COm_Master_Org> { }
	 public class T_COm_Master_OrgManager
		{
			private static T_COm_Master_Org GetItemFromReader(IDataReader dataReader)
			{
				T_COm_Master_Org objItem = new T_COm_Master_Org();
			
			     objItem.OrgId = SqlHelper.GetInt(dataReader, "OrgId");
objItem.OrgName = SqlHelper.GetString(dataReader, "OrgName");
objItem.Description = SqlHelper.GetString(dataReader, "Description");
objItem.Entity_Id = SqlHelper.GetInt(dataReader, "Entity_Id");
objItem.UnitType_Id = SqlHelper.GetInt(dataReader, "UnitType_Id");
objItem.Holder_Id = SqlHelper.GetInt(dataReader, "Holder_Id");
objItem.ParentOrg_Id = SqlHelper.GetInt(dataReader, "ParentOrg_Id");
objItem.Plant_Type = SqlHelper.GetBoolean(dataReader, "Plant_Type");
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
			public static T_COm_Master_Org GetItemByID(int OrgId)
			{
				T_COm_Master_Org item = new T_COm_Master_Org();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@OrgId", OrgId);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COm_Master_Org_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_COm_Master_Org AddItem(T_COm_Master_Org model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COm_Master_Org_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COm_Master_Org UpdateItem(T_COm_Master_Org model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COm_Master_Org_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COm_Master_OrgCollection GetAllItem()
			{
				T_COm_Master_OrgCollection collection = new T_COm_Master_OrgCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COm_Master_Org_GetAll", null))
				{
					while (reader.Read())
					{
						T_COm_Master_Org obj = new T_COm_Master_Org();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_COm_Master_OrgCollection Search(SearchFilter SearchKey)
			{
				T_COm_Master_OrgCollection collection = new T_COm_Master_OrgCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COm_Master_Org_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COm_Master_Org obj = new T_COm_Master_Org();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COm_Master_OrgCollection GetbyUser(string CreatedUser)
			{
				T_COm_Master_OrgCollection collection = new T_COm_Master_OrgCollection();
				T_COm_Master_Org obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COm_Master_Org_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

        public static T_COm_Master_Org GetItemByOrgName(string Orgname)
        {
            T_COm_Master_Org item = new T_COm_Master_Org();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CreatedUser", Orgname);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COm_Master_Org_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }

        private static SqlParameter[] CreateSqlParameter(T_COm_Master_Org model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@OrgId", model.OrgId),
					new SqlParameter("@OrgName", model.OrgName),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Entity_Id", model.Entity_Id),
					new SqlParameter("@UnitType_Id", model.UnitType_Id),
					new SqlParameter("@Holder_Id", model.Holder_Id),
					new SqlParameter("@ParentOrg_Id", model.ParentOrg_Id),
					new SqlParameter("@Plant_Type", model.Plant_Type),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsActive", model.IsActive),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COm_Master_Org_Delete", itemID);
			}
		}
	}
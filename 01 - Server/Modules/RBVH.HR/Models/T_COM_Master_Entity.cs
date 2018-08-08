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
    public class T_COM_Master_Entity : BaseDBEntity
    {
       
	   [DataMember]public int EntityId{ get; set; }
[DataMember]public string ShortName_EN{ get; set; }
[DataMember]public string ShortName_VT{ get; set; }
[DataMember]public string LongName_EN{ get; set; }
[DataMember]public string LongName_VT{ get; set; }
[DataMember]public string Description{ get; set; }
[DataMember]public string Address_EN{ get; set; }
[DataMember]public string Address_VT{ get; set; }
[DataMember]public decimal PerDayWorkingHour{ get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsAcitve{ get; set; }


    }
    public class T_COM_Master_EntityCollection : BaseDBEntityCollection<T_COM_Master_Entity> { }
	 public class T_COM_Master_EntityManager
		{
			private static T_COM_Master_Entity GetItemFromReader(IDataReader dataReader)
			{
				T_COM_Master_Entity objItem = new T_COM_Master_Entity();
			
			     objItem.EntityId = SqlHelper.GetInt(dataReader, "EntityId");
objItem.ShortName_EN = SqlHelper.GetString(dataReader, "ShortName_EN");
objItem.ShortName_VT = SqlHelper.GetString(dataReader, "ShortName_VT");
objItem.LongName_EN = SqlHelper.GetString(dataReader, "LongName_EN");
objItem.LongName_VT = SqlHelper.GetString(dataReader, "LongName_VT");
objItem.Description = SqlHelper.GetString(dataReader, "Description");
objItem.Address_EN = SqlHelper.GetString(dataReader, "Address_EN");
objItem.Address_VT = SqlHelper.GetString(dataReader, "Address_VT");
objItem.PerDayWorkingHour = SqlHelper.GetDecimal(dataReader, "PerDayWorkingHour");
objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");
objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");
objItem.IsAcitve = SqlHelper.GetBoolean(dataReader, "IsAcitve");


				 if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }
				return objItem;
			}
			public static T_COM_Master_Entity GetItemByID(int EntityId)
			{
				T_COM_Master_Entity item = new T_COM_Master_Entity();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@EntityId", EntityId);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Entity_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_COM_Master_Entity AddItem(T_COM_Master_Entity model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Entity_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_Entity UpdateItem(T_COM_Master_Entity model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Entity_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_EntityCollection GetAllItem()
			{
				T_COM_Master_EntityCollection collection = new T_COM_Master_EntityCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Entity_GetAll", null))
				{
					while (reader.Read())
					{
						T_COM_Master_Entity obj = new T_COM_Master_Entity();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}



			public static T_COM_Master_EntityCollection Search(SearchFilter SearchKey)
			{
				T_COM_Master_EntityCollection collection = new T_COM_Master_EntityCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Entity_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COM_Master_Entity obj = new T_COM_Master_Entity();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COM_Master_EntityCollection GetbyUser(string CreatedUser)
			{
				T_COM_Master_EntityCollection collection = new T_COM_Master_EntityCollection();
				T_COM_Master_Entity obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Entity_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

        public static T_COM_Master_EntityCollection GetbyEmployeeCode(int EmployeeCode)
        {
            T_COM_Master_EntityCollection collection = new T_COM_Master_EntityCollection();
            T_COM_Master_Entity obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Entity_GetAll_byEmployeeCode", new SqlParameter("@EmployeeCode", EmployeeCode)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_COM_Master_Entity model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@EntityId", model.EntityId),
					new SqlParameter("@ShortName_EN", model.ShortName_EN),
					new SqlParameter("@ShortName_VT", model.ShortName_VT),
					new SqlParameter("@LongName_EN", model.LongName_EN),
					new SqlParameter("@LongName_VT", model.LongName_VT),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Address_EN", model.Address_EN),
					new SqlParameter("@Address_VT", model.Address_VT),
					new SqlParameter("@PerDayWorkingHour", model.PerDayWorkingHour),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsAcitve", model.IsAcitve),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COM_Master_Entity_Delete", itemID);
			}
		}
	}
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.HR
{
	[DataContract]
    public class T_COM_Master_Employee_Position : BaseDBEntity
    {
       
	   [DataMember]public int ID{ get; set; }
[DataMember]public int EmployeeCode{ get; set; }
[DataMember]public int GradeID{ get; set; }
[DataMember]public int PositionID{ get; set; }
[DataMember]public decimal Salary{ get; set; }
[DataMember]public decimal HomeCountrySalary { get; set; }
[DataMember]public string HostCountryCurrency{ get; set; }
[DataMember]public string HomeCountryCurrency{ get; set; }
[DataMember]public DateTime EffectiveFrom{ get; set; }
[DataMember]public DateTime EffectiveTo { get; set; }
[DataMember]public int CreatedBy{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int ModifiedBy{ get; set; }
[DataMember]public DateTime ModifiedDate{ get; set; }
[DataMember]public bool IsAcitve{ get; set; }


    }
    public class T_COM_Master_Employee_PositionCollection : BaseDBEntityCollection<T_COM_Master_Employee_Position> { }
	 public class T_COM_Master_Employee_PositionManager
		{
			private static T_COM_Master_Employee_Position GetItemFromReader(IDataReader dataReader)
			{
				T_COM_Master_Employee_Position objItem = new T_COM_Master_Employee_Position();
			
			     objItem.ID = SqlHelper.GetInt(dataReader, "ID");
objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");
objItem.GradeID = SqlHelper.GetInt(dataReader, "GradeID");
objItem.PositionID = SqlHelper.GetInt(dataReader, "PositionID");
objItem.Salary = SqlHelper.GetDecimal(dataReader, "Salary");
objItem.HomeCountrySalary = SqlHelper.GetDecimal(dataReader, "HomeCountrySalary");
objItem.HostCountryCurrency = SqlHelper.GetString(dataReader, "HostCountryCurrency");
objItem.HomeCountryCurrency = SqlHelper.GetString(dataReader, "HomeCountryCurrency");
objItem.EffectiveFrom = SqlHelper.GetDateTime(dataReader, "EffectiveFrom");
objItem.EffectiveTo = SqlHelper.GetDateTime(dataReader, "EffectiveTo");
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
			public static T_COM_Master_Employee_Position GetItemByID(int ID)
			{
				T_COM_Master_Employee_Position item = new T_COM_Master_Employee_Position();
				var sqlParams = new SqlParameter[1];
				sqlParams[0] = new SqlParameter("@ID", ID);
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Employee_Position_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static T_COM_Master_Employee_Position AddItem(T_COM_Master_Employee_Position model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Employee_Position_Add", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_Employee_Position UpdateItem(T_COM_Master_Employee_Position model)
			{
				int result = 0;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,CommandType.StoredProcedure, "T_COM_Master_Employee_Position_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (int)reader[0];
					}
				}
				return GetItemByID(result);

			}
			public static T_COM_Master_Employee_PositionCollection GetAllItem()
			{
				T_COM_Master_Employee_PositionCollection collection = new T_COM_Master_Employee_PositionCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Employee_Position_GetAll", null))
				{
					while (reader.Read())
					{
						T_COM_Master_Employee_Position obj = new T_COM_Master_Employee_Position();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static T_COM_Master_Employee_PositionCollection Search(SearchFilter SearchKey)
			{
				T_COM_Master_Employee_PositionCollection collection = new T_COM_Master_Employee_PositionCollection();
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Employee_Position_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
				{
					while (reader.Read())
					{
						T_COM_Master_Employee_Position obj = new T_COM_Master_Employee_Position();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static T_COM_Master_Employee_PositionCollection GetbyUser(string CreatedUser)
			{
				T_COM_Master_Employee_PositionCollection collection = new T_COM_Master_Employee_PositionCollection();
				T_COM_Master_Employee_Position obj;
				using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Employee_Position_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(T_COM_Master_Employee_Position model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@ID", model.ID),
					new SqlParameter("@EmployeeCode", model.EmployeeCode),
					new SqlParameter("@GradeID", model.GradeID),
					new SqlParameter("@PositionID", model.PositionID),
					new SqlParameter("@Salary", model.Salary),
					new SqlParameter("@HomeCountrySalary", model.HomeCountrySalary),
					new SqlParameter("@HostCountryCurrency", model.HostCountryCurrency),
					new SqlParameter("@HomeCountryCurrency", model.HomeCountryCurrency),
					new SqlParameter("@EffectiveFrom", model.EffectiveFrom),
					new SqlParameter("@EffectiveTo", model.EffectiveTo),
					new SqlParameter("@CreatedBy", model.CreatedBy),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ModifiedBy", model.ModifiedBy),
					new SqlParameter("@ModifiedDate", model.ModifiedDate),
					new SqlParameter("@IsAcitve", model.IsAcitve),
					
					};
			}

			public static int DeleteItem(int itemID)
			{
				return SqlHelper.ExecuteNonQuery("T_COM_Master_Employee_Position_Delete", itemID);
			}
		}
	}
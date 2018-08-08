using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.Core.Models
{
	[DataContract]
    public class Setting : BaseDBEntity
    {
       
	   [DataMember]public int CompanyID{ get; set; }
[DataMember]public int SettingID{ get; set; }
[DataMember]public string Name{ get; set; }
[DataMember]public string Value{ get; set; }
[DataMember]public String Description{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public int CreatedUser{ get; set; }


    }
    public class SettingCollection : BaseDBEntityCollection<Setting> { }
	 public class SettingManager
		{
			private static Setting GetItemFromReader(IDataReader dataReader)
			{
				Setting objItem = new Setting();
			
			     objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");
objItem.SettingID = SqlHelper.GetInt(dataReader, "SettingID");
objItem.Name = SqlHelper.GetString(dataReader, "Name");
objItem.Value = SqlHelper.GetString(dataReader, "Value");
objItem.Description = SqlHelper.GetString(dataReader, "Description");
objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
objItem.CreatedUser = SqlHelper.GetInt(dataReader, "CreatedUser");


				  if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
				 {
					objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
				 }

				return objItem;
			}
			public static Setting GetItemByID(int SettingID,int CompanyID)
			{
				Setting item = new Setting();
				var sqlParams =  new SqlParameter[]
                        {
							new SqlParameter("@SettingID", SettingID),
							new SqlParameter("@CompanyID", CompanyID),
						};
				using (var reader = SqlHelper.ExecuteReader("Setting_GetByID", sqlParams))
				{
					while (reader.Read())
					{
						item = GetItemFromReader(reader);
					}
				}
				return item;


			}
			public static Setting AddItem(Setting model)
			{
				int result = 0;
				try{
					using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Setting_Add", CreateSqlParameter(model)))
					{
						while (reader.Read())
						{
							result = (int)reader[0];
						}
					}
				}
				catch(Exception ObjEx)
				{

				}
				return GetItemByID(result, model.CompanyID);

			}
			public static Setting UpdateItem(Setting model)
			{
				String result = String.Empty;
				using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Setting_Update", CreateSqlParameter(model)))
				{
					while (reader.Read())
					{
						result = (String)reader[0];
					}
				}
				return GetItemByID(model.SettingID, model.CompanyID);

			}
			public static SettingCollection GetAllItem(int CompanyID)
			{
				SettingCollection collection = new SettingCollection();
				 
				var sqlParams = new SqlParameter[]
                        {
							new SqlParameter("@CompanyID", CompanyID),
						};
				using (var reader = SqlHelper.ExecuteReader("Setting_GetAll", sqlParams))
				{
					while (reader.Read())
					{
						Setting obj = new Setting();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			public static SettingCollection Search(SearchFilter SearchKey)
			{
				SettingCollection collection = new SettingCollection();
				using (var reader = SqlHelper.ExecuteReader("Setting_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
				{
					while (reader.Read())
					{
						Setting obj = new Setting();
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}
			public static SettingCollection GetbyUser(string CreatedUser, int CompanyID)
			{
				SettingCollection collection = new SettingCollection();
				Setting obj;
				 var sqlParams = new SqlParameter[]
                   {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
                   };
				using (var reader = SqlHelper.ExecuteReader("Setting_GetAll_byUser", sqlParams))
				{
					while (reader.Read())
					{
						obj = GetItemFromReader(reader);
						collection.Add(obj);
					}
				}
				return collection;
			}

			private static SqlParameter[] CreateSqlParameter(Setting model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@CompanyID", model.CompanyID),
					new SqlParameter("@SettingID", model.SettingID),
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Value", model.Value),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					
					};
			}

			public static int DeleteItem(int itemID  , int CompanyID)
			{
				return SqlHelper.ExecuteNonQuery("Setting_Delete",  new SqlParameter[]
                {
                new SqlParameter(@"SettingID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
			}
		}
	}
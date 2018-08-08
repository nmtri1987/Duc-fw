using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace DTP.Object
{
    [DataContract]
    public class UsersInRoles : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Rolename { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string Descr { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }
    }
    public class UsersInRolesCollection : BaseDBEntityCollection<UsersInRoles> { }
    public class UsersInRolesManager
    {
        private static UsersInRoles GetItemFromReader(IDataReader dataReader)
        {
            UsersInRoles objItem = new UsersInRoles();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Username = SqlHelper.GetString(dataReader, "Username");

            objItem.Rolename = SqlHelper.GetString(dataReader, "Rolename");

            objItem.ApplicationName = SqlHelper.GetString(dataReader, "ApplicationName");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.UpdateUser = SqlHelper.GetString(dataReader, "UpdateUser");

            objItem.UpdateDate = SqlHelper.GetDateTime(dataReader, "UpdateDate");

            objItem.Descr = SqlHelper.GetString(dataReader, "Descr");
            objItem.IsSelected = SqlHelper.GetBoolean(dataReader, "IsSelected");

            return objItem;
        }
        public static UsersInRoles GetItemByID(String Username, int CompanyID)
        {
            UsersInRoles item = new UsersInRoles();
            var sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Username", Username);
            sqlParams[1] = new SqlParameter("@Username", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("UsersInRoles_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static UsersInRoles AddItem(UsersInRoles model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "UsersInRoles_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return model;

        }
        public static UsersInRoles UpdateItem(UsersInRoles model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "UsersInRoles_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result,model.CompanyID);

        }
        public static UsersInRolesCollection GetAllItem(int CompanyID)
        {
            UsersInRolesCollection collection = new UsersInRolesCollection();
            var sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@CompanyID", CompanyID),
                        };
            using (var reader = SqlHelper.ExecuteReader("UsersInRoles_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    UsersInRoles obj = new UsersInRoles();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static UsersInRolesCollection Search(SearchFilter SearchKey)
        {
            UsersInRolesCollection collection = new UsersInRolesCollection();
            using (var reader = SqlHelper.ExecuteReader("UsersInRoles_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    UsersInRoles obj = new UsersInRoles();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static UsersInRolesCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            UsersInRolesCollection collection = new UsersInRolesCollection();
            UsersInRoles obj;
            var sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID)
                        };
            using (var reader = SqlHelper.ExecuteReader("UsersInRoles_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(UsersInRoles model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Username", model.Username),
                    new SqlParameter("@Rolename", model.Rolename),
                    new SqlParameter("@ApplicationName", model.ApplicationName),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@UpdateUser", model.UpdateUser),
                    new SqlParameter("@UpdateDate", model.UpdateDate),

                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("UsersInRoles_Delete", itemID);
        }

        /// <summary>
        /// get Roles from table UserInRole
        /// 
        /// create by pham thanh
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static UsersInRolesCollection GetRolesbyUserName(string UserName,int companyID)
        {
            UsersInRolesCollection collection = new UsersInRolesCollection();
            UsersInRoles obj;
            var par = new SqlParameter[] {
                new SqlParameter("@UserName", UserName),
                 new SqlParameter("@CompanyID", companyID)
            };
            using (var reader = SqlHelper.ExecuteReader("UserInRoles_GetByUserName", par))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static int DeleteItemByUsername(string userName,int CompanyID)
        {
            var sqlParams = new SqlParameter[]
                       {
                            new SqlParameter("@Username", userName),
                            new SqlParameter("@CompanyID", CompanyID)
                       };
            return SqlHelper.ExecuteNonQuery("UsersInRoles_DeleteByUsername", sqlParams);
        }
    }
}
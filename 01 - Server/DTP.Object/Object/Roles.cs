using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace ifinds.Object.CS
{
    [DataContract]
    public class Roles : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Rolename { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public string Descr { get; set; }

        [DataMember]
        public bool isGuest { get; set; }

        [DataMember]
        public string Createduser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]        public int TotalRecord { get; set; }

    }
    public class RolesCollection : BaseDBEntityCollection<Roles> { }
    public class RolesManager
    {
        private static Roles GetItemFromReader(IDataReader dataReader)
        {
            Roles objItem = new Roles();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Rolename = SqlHelper.GetString(dataReader, "Rolename");

            objItem.ApplicationName = SqlHelper.GetString(dataReader, "ApplicationName");

            objItem.Descr = SqlHelper.GetString(dataReader, "Descr");

            objItem.isGuest = SqlHelper.GetBoolean(dataReader, "isGuest");

            objItem.Createduser = SqlHelper.GetString(dataReader, "Createduser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Roles GetItemByID(string Rolename, int CompanyID)
        {
            Roles item = new Roles();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@Rolename", Rolename),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Roles_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Roles AddItem(Roles model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Roles_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static Roles UpdateItem(Roles model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Roles_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.Rolename, model.CompanyID);

        }
        public static RolesCollection GetAllItem(int CompanyID)
        {
            RolesCollection collection = new RolesCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Roles_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Roles obj = new Roles();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static RolesCollection Search(SearchFilter SearchKey)
        {
            RolesCollection collection = new RolesCollection();
            using (var reader = SqlHelper.ExecuteReader("Roles_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Roles obj = new Roles();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static RolesCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            RolesCollection collection = new RolesCollection();
            Roles obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("Roles_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Roles model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Rolename", model.Rolename),
                    new SqlParameter("@ApplicationName", model.ApplicationName),
                    new SqlParameter("@Descr", model.Descr),
                    new SqlParameter("@isGuest", model.isGuest),
                    new SqlParameter("@Createduser", model.Createduser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(string itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("Roles_Delete", new SqlParameter[]
            {
                new SqlParameter(@"Rolename",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
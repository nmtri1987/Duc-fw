using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace DTP.Object
{
    [DataContract]
    public class AccessRights : BaseDBEntity
    {
        public AccessRights()
        {
            ListAccess = new AccessRightsCollection();
        } 

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public Guid NodeID { get; set; }

        [DataMember]
        public int Access { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]        public string Title { get; set; }

        [DataMember]        public string Url { get; set; }

        [DataMember]
        public AccessRightsCollection ListAccess { get; set; }

    }
    public class AccessRightsCollection : BaseDBEntityCollection<AccessRights> { }
    public class AccessRightsManager
    {
        private static AccessRights GetItemFromReader(IDataReader dataReader)
        {
            AccessRights objItem = new AccessRights();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.RoleName = SqlHelper.GetString(dataReader, "RoleName");

            objItem.ApplicationName = SqlHelper.GetString(dataReader, "ApplicationName");

            objItem.NodeID = SqlHelper.GetGuid(dataReader, "NodeID");

            objItem.Access = SqlHelper.GetSmallInt(dataReader, "Access");

            try
            {
                objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");
            }
            catch { }

            
            try
            {
                objItem.CreateDate = SqlHelper.GetDateTime(dataReader, "CreateDate");
            }
            catch { }

            
            try
            {
                objItem.Title = SqlHelper.GetString(dataReader, "Title");
            }
            catch { }
            try
            {
                objItem.Url = SqlHelper.GetString(dataReader, "Url");
            }
            catch { }


            return objItem;
        }
        public static AccessRightsCollection GetItemByID(String RoleName, int CompanyID,string ApplicationName, Guid? NodeID)
        {
            AccessRightsCollection collection = new AccessRightsCollection();
            var sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@RoleName", RoleName);
            sqlParams[2] = new SqlParameter("@ApplicationName", ApplicationName);
            sqlParams[3] = new SqlParameter("@NodeID", NodeID);
            using (var reader = SqlHelper.ExecuteReader("AccessRights_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    AccessRights obj = new AccessRights();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                    //item = GetItemFromReader(reader);
                }
            }
            return collection;


        }
        public static AccessRights AddItem(AccessRights model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "AccessRights_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return model;

        }
        public static AccessRights UpdateItem(AccessRights model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "AccessRights_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return model;

        }
        public static AccessRightsCollection GetAllItem(int CompanyID)
        {
            AccessRightsCollection collection = new AccessRightsCollection();

            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("AccessRights_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    AccessRights obj = new AccessRights();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static AccessRightsCollection Search(SearchFilter SearchKey)
        {
            AccessRightsCollection collection = new AccessRightsCollection();
            using (var reader = SqlHelper.ExecuteReader("AccessRights_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    AccessRights obj = new AccessRights();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static AccessRightsCollection GetbyUser(string CreatedUser)
        {
            AccessRightsCollection collection = new AccessRightsCollection();
            AccessRights obj;
            using (var reader = SqlHelper.ExecuteReader("AccessRights_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static AccessRightsCollection GetbyNoteParent(string roleName, string applicationName, string nodeParentID, int companyID)
        {
            AccessRightsCollection collection = new AccessRightsCollection();
            AccessRights obj;
            var par = new SqlParameter[] { new SqlParameter("@RoleName", roleName),
                new SqlParameter("@CompanyID",companyID),
               new SqlParameter("@ApplicationName",applicationName),
               new SqlParameter("@NodeParentID",new Guid(nodeParentID)),

                                            };
            using (var reader = SqlHelper.ExecuteReader("AccessRights_GetByNoteParent", par))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(AccessRights model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@RoleName", model.RoleName),
                    new SqlParameter("@ApplicationName", model.ApplicationName),
                    new SqlParameter("@NodeID", model.NodeID),
                    new SqlParameter("@Access", model.Access),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreateDate", model.CreateDate),

                };
        }

        public static int DeleteItem(String itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("AccessRights_Delete", new SqlParameter[]
            {
                new SqlParameter(@"RoleName",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }

        //change by pham thanh
        public static AccessRights InsertOrUpdateItem( AccessRights model)
        {
            SiteMapCollection listSiteMap = SiteMapManager.GetAllItem(model.CompanyID);
            AccessRights itemAccess = new AccessRights();
            AccessRightsCollection ListAccess= GetItemByID(model.RoleName, model.CompanyID, model.ApplicationName, null);
            int count = model.ListAccess.Count;
            foreach (var item in listSiteMap)
            {
                
                if (!string.IsNullOrEmpty(item.Url) && item.Url.Trim() != "/")
                {
                    if (ListAccess.Count != 0)
                    {
                        itemAccess = ListAccess.Where(m => m.NodeID.Equals(item.NodeID)).FirstOrDefault();
                        if (itemAccess == null)
                        {
                            itemAccess = new AccessRights();
                        }
                    }
                    model.Access = 0;
                    model.NodeID = item.NodeID;
                    bool isUpdate = false;
                    if (count > 0)
                    {
                        foreach (var access in model.ListAccess)
                        {
                            if (access.NodeID == item.NodeID)
                            {
                                model.Access = access.Access;
                                isUpdate = true;
                                break;
                            }
                        }
                    }
                    if (itemAccess.RoleName != null)
                    {
                            if (isUpdate)
                            {
                                SqlHelper.ExecuteReader(CommandType.StoredProcedure, "AccessRights_InsertOrUpdate", CreateSqlParameter(model));
                            }
                    }
                    else
                    {
                        ///chưa tồn tại trong db
                        ///
                        SqlHelper.ExecuteReader(CommandType.StoredProcedure, "AccessRights_InsertOrUpdate", CreateSqlParameter(model));
                    }


                }
            }
            return model;

        }
    }
}
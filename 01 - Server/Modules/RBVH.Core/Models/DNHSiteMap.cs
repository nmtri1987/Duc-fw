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
    public class DNHSiteMap : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Guid NodeID { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public bool Expanded { get; set; }

        [DataMember]
        public bool IsFolder { get; set; }

        [DataMember]
        public string ScreenID { get; set; }

        [DataMember]
        public Guid ParentID { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string IconImage { get; set; }

        [DataMember]
        public int Access { get; set; }

        [DataMember]
        public string RoleName { get; set; }


    }
    public class DNHSiteMapCollection : BaseDBEntityCollection<DNHSiteMap> { }
    public class DNHSiteMapManager
    {
        private static DNHSiteMap GetItemFromReader(IDataReader dataReader)
        {
            DNHSiteMap objItem = new DNHSiteMap();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.NodeID = SqlHelper.GetGuid(dataReader, "NodeID");

            objItem.Position = SqlHelper.GetInt(dataReader, "Position");

            objItem.Title = SqlHelper.GetString(dataReader, "Title");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.Url = SqlHelper.GetString(dataReader, "Url");

            objItem.Expanded = SqlHelper.GetBoolean(dataReader, "Expanded");

            objItem.IsFolder = SqlHelper.GetBoolean(dataReader, "IsFolder");

            objItem.ScreenID = SqlHelper.GetString(dataReader, "ScreenID");

            objItem.ParentID = SqlHelper.GetGuid(dataReader, "ParentID");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.IconImage = SqlHelper.GetString(dataReader, "IconImage");

            if (SqlHelper.ColumnExists(dataReader, "Access"))
            {
                objItem.Access = SqlHelper.GetInt(dataReader, "Access");
            }
            if (SqlHelper.ColumnExists(dataReader, "RoleName"))
            {
                objItem.RoleName = SqlHelper.GetString(dataReader, "RoleName");

            }

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            
            return objItem;
        }
        public static DNHSiteMap GetItemByID(Guid NodeID, int CompanyID)
        {
            DNHSiteMap item = new DNHSiteMap();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@NodeID", NodeID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("DNHSiteMap_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static DNHSiteMap AddItem(DNHSiteMap model)
        {
            Guid result = Guid.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHSiteMap_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (Guid)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static DNHSiteMap UpdateItem(DNHSiteMap model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHSiteMap_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.NodeID, model.CompanyID);

        }
        public static DNHSiteMapCollection GetAllItem(int CompanyID)
        {
            DNHSiteMapCollection collection = new DNHSiteMapCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("DNHSiteMap_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    DNHSiteMap obj = new DNHSiteMap();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static DNHSiteMapCollection Search(SearchFilter SearchKey)
        {
            DNHSiteMapCollection collection = new DNHSiteMapCollection();
            using (var reader = SqlHelper.ExecuteReader("DNHSiteMap_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    DNHSiteMap obj = new DNHSiteMap();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static DNHSiteMapCollection GetbyUser(string CreatedUser, int CompanyID, Guid? NodeID)
        {
            DNHSiteMapCollection collection = new DNHSiteMapCollection();
            DNHSiteMap obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@UserName", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
                            new SqlParameter("@ParentNodeID", NodeID),
              };
            using (var reader = SqlHelper.ExecuteReader("DNHSiteMap_GetByUserRoles", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(DNHSiteMap model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@NodeID", model.NodeID),
                    new SqlParameter("@Position", model.Position),
                    new SqlParameter("@Title", model.Title),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Url", model.Url),
                    new SqlParameter("@Expanded", model.Expanded),
                    new SqlParameter("@IsFolder", model.IsFolder),
                    new SqlParameter("@ScreenID", model.ScreenID),
                    new SqlParameter("@ParentID", model.ParentID),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@IconImage", model.IconImage),

                };
        }

        public static int DeleteItem(Guid itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("DNHSiteMap_Delete", new SqlParameter[]
            {
                new SqlParameter(@"NodeID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
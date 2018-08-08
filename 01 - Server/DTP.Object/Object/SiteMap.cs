using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Server.DAC;
//using Server.Helpers;
namespace DTP.Object
{
[DataContract]
public class SiteMap :BaseDBEntity
{
[DataMember]
public int CompanyID{ get; set; }

[DataMember]
public decimal Position{ get; set; }

[DataMember]
public string Title{ get; set; }

[DataMember]
public string Description{ get; set; }

[DataMember]
public string Url{ get; set; }

[DataMember]
public bool Expanded{ get; set; }

[DataMember]
public bool IsFolder{ get; set; }

[DataMember]
public string ScreenID{ get; set; }

[DataMember]
public Guid NodeID{ get; set; }

[DataMember]
public Guid ParentID{ get; set; }


}
public class SiteMapCollection : BaseDBEntityCollection<SiteMap> { }
public class SiteMapManager
{
private static SiteMap GetItemFromReader(IDataReader dataReader)
{
SiteMap objItem = new SiteMap();
objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

objItem.Position = SqlHelper.GetDecimal(dataReader, "Position");

objItem.Title = SqlHelper.GetString(dataReader, "Title");

objItem.Description = SqlHelper.GetString(dataReader, "Description");

objItem.Url = SqlHelper.GetString(dataReader, "Url");

objItem.Expanded = SqlHelper.GetBoolean(dataReader, "Expanded");

objItem.IsFolder = SqlHelper.GetBoolean(dataReader, "IsFolder");

objItem.ScreenID = SqlHelper.GetString(dataReader, "ScreenID");

objItem.NodeID = SqlHelper.GetGuid(dataReader, "NodeID");

objItem.ParentID = SqlHelper.GetGuid(dataReader, "ParentID");


return objItem;
}
        public static SiteMap GetItemByID(Guid NodeID, int CompanyID)
        {
            SiteMap item = new SiteMap();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@NodeID", NodeID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("SiteMap_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static SiteMap AddItem(SiteMap model)
        {
            Guid result = Guid.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "SiteMap_Add", CreateSqlParameter(model)))
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
        public static SiteMap UpdateItem(SiteMap model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "SiteMap_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.NodeID, model.CompanyID);

        }
        public static SiteMapCollection GetAllItem(int CompanyID)
        {
            SiteMapCollection collection = new SiteMapCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("SiteMap_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    SiteMap obj = new SiteMap();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static SiteMapCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            SiteMapCollection collection = new SiteMapCollection();
            SiteMap obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("SiteMap_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(SiteMap model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@CompanyID", model.CompanyID),
					new SqlParameter("@Position", model.Position),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@Url", model.Url),
					new SqlParameter("@Expanded", model.Expanded),
					new SqlParameter("@IsFolder", model.IsFolder),
					new SqlParameter("@ScreenID", model.ScreenID),
					new SqlParameter("@NodeID", model.NodeID),
					new SqlParameter("@ParentID", model.ParentID),
					
                };
        }

        public static int DeleteItem(Guid itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("SiteMap_Delete", new SqlParameter[]
            {
                new SqlParameter(@"NodeID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
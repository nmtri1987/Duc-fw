using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System;
using dtp.Web.Caching;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DTP.Object
{
    [DataContract]
    public class Area : BaseDBEntity
    {
        [DataMember]
        public int AreaID { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public bool isDeleted { get; set; }

        [DataMember]
        public string Createduser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string DeletedUser { get; set; }

        [DataMember]
        public DateTime DeketedDate { get; set; }
    }
    [CollectionDataContract]
    public class AreaCollection : BaseDBEntityCollection<Area> { }
    public class AreaManager
    {
        private static Area GetItemFromReader(IDataReader dataReader)
        {
            Area objItem = new Area();
            objItem.AreaID = SqlHelper.GetInt(dataReader, "AreaID");

            objItem.AreaName = SqlHelper.GetString(dataReader, "AreaName");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.CountryID = SqlHelper.GetInt(dataReader, "CountryID");

            objItem.isDeleted = SqlHelper.GetBoolean(dataReader, "isDeleted");

            objItem.Createduser = SqlHelper.GetString(dataReader, "Createduser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.DeletedUser = SqlHelper.GetString(dataReader, "DeletedUser");

            objItem.DeketedDate = SqlHelper.GetDateTime(dataReader, "DeketedDate");
            return objItem;
        }

        public static Area GetItemByID(Int64 areaID)
        {
            Area item = new Area();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AreaID", areaID);
            using (var reader = SqlHelper.ExecuteReader("tblArea_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Area AddItem(Area model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader( "tblArea_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = int.Parse(reader[0].ToString());
                }
            }
            return GetItemByID(result);

        }
        public static Area UpdateItem(Area model)
        {
            Area result = new Area();
            using (var reader = SqlHelper.ExecuteReader( "tblArea_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = GetItemByID(model.AreaID);
                }
            }
            return result;

        }

        public static AreaCollection GetAllItembyCountryID(int CountryID)
        {


            AreaCollection ItemCollection = new AreaCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblArea_GetByCountryID");
            db.AddInParameter(dbCommand, "CountryID", DbType.String, CountryID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                Area item;
                while (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }


            return ItemCollection;

        }
        public static AreaCollection GetAllItem()
        {


            //AreaCollection ItemCollection = new AreaCollection();
            //Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            //DbCommand dbCommand = db.GetStoredProcCommand("tblArea_GetAll");
            //using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            //{
            //    Area item;
            //    while (dataReader.Read())
            //    {
            //        item = GetItemFromReader(dataReader);
            //        ItemCollection.Add(item);
            //    }
            //}
            

            //return ItemCollection;


            AreaCollection collection = new AreaCollection();
            try
            {
                using (var reader = SqlHelper.ExecuteReader("tblArea_GetList", null))
                {
                    while (reader.Read())
                    {
                        Area obj = new Area();
                        obj = GetItemFromReader(reader);
                        collection.Add(obj);
                    }
                }
            }
            catch (Exception objEx)
            {
                collection.Add(new Area()
                {
                    AreaID = 0,
                    AreaName = objEx.Message,
                    Description = objEx.StackTrace
                });
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Area model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@AreaID", model.AreaID),
					new SqlParameter("@AreaName", model.AreaName),
					new SqlParameter("@Description", model.Description),
					new SqlParameter("@CountryID", model.CountryID),
					new SqlParameter("@isDeleted", model.isDeleted),
					new SqlParameter("@Createduser", model.Createduser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@DeletedUser", model.DeletedUser),
					new SqlParameter("@DeketedDate", model.DeketedDate),
					
                };
        }

        //public int DeleteItem(Int64 itemID)
        //{
        //    return SqlHelper.ExecuteNonQuery("tblArea_Delete", itemID);
        //}
    }
}

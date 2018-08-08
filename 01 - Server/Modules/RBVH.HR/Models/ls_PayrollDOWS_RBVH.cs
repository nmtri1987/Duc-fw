using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.TMS.Models
{
    [DataContract]
    public class ls_PayrollDOWS_RBVH : BaseDBEntity
    {

        [DataMember]
        public int Dow_ID { get; set; }

        [DataMember]
        public string Dow_Code { get; set; }

        [DataMember]
        public DateTime Beg_Day { get; set; }

        [DataMember]
        public DateTime End_Day { get; set; }

        [DataMember]
        public int Dow_Num { get; set; }

        [DataMember]
        public int ENtityID { get; set; }


    }
    public class ls_PayrollDOWS_RBVHCollection : BaseDBEntityCollection<ls_PayrollDOWS_RBVH> { }
    public class ls_PayrollDOWS_RBVHManager
    {
        private static ls_PayrollDOWS_RBVH GetItemFromReader(IDataReader dataReader)
        {
            ls_PayrollDOWS_RBVH objItem = new ls_PayrollDOWS_RBVH();

            objItem.Dow_ID = SqlHelper.GetInt(dataReader, "Dow_ID");

            objItem.Dow_Code = SqlHelper.GetString(dataReader, "Dow_Code");

            objItem.Beg_Day = SqlHelper.GetDateTime(dataReader, "Beg_Day");

            objItem.End_Day = SqlHelper.GetDateTime(dataReader, "End_Day");

           // objItem.Dow_Num = SqlHelper.GetInt(dataReader, "Dow_Num");

            objItem.ENtityID = SqlHelper.GetInt(dataReader, "ENtityID");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        public static ls_PayrollDOWS_RBVH GetItemByID(int ENtityID)
        {
            ls_PayrollDOWS_RBVH item = new ls_PayrollDOWS_RBVH();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Dow_ID", ENtityID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "ls_PayrollDOWS_RBVH_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ls_PayrollDOWS_RBVH AddItem(ls_PayrollDOWS_RBVH model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "ls_PayrollDOWS_RBVH_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ls_PayrollDOWS_RBVH UpdateItem(ls_PayrollDOWS_RBVH model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "ls_PayrollDOWS_RBVH_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ls_PayrollDOWS_RBVHCollection GetAllItemByEntity(int EntityID)
        {
            ls_PayrollDOWS_RBVHCollection collection = new ls_PayrollDOWS_RBVHCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "ls_PayrollDOWS_RBVH_GetAll_ByEntity", new SqlParameter("@EntityID", EntityID)))
            {
                while (reader.Read())
                {
                    ls_PayrollDOWS_RBVH obj = new ls_PayrollDOWS_RBVH();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ls_PayrollDOWS_RBVHCollection Search(SearchFilter SearchKey)
        {
            ls_PayrollDOWS_RBVHCollection collection = new ls_PayrollDOWS_RBVHCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "ls_PayrollDOWS_RBVH_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    ls_PayrollDOWS_RBVH obj = new ls_PayrollDOWS_RBVH();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ls_PayrollDOWS_RBVHCollection GetbyUser(string CreatedUser)
        {
            ls_PayrollDOWS_RBVHCollection collection = new ls_PayrollDOWS_RBVHCollection();
            ls_PayrollDOWS_RBVH obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "ls_PayrollDOWS_RBVH_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ls_PayrollDOWS_RBVH model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@Dow_ID", model.Dow_ID),
                    new SqlParameter("@Dow_Code", model.Dow_Code),
                    new SqlParameter("@Beg_Day", model.Beg_Day),
                    new SqlParameter("@End_Day", model.End_Day),
                    new SqlParameter("@Dow_Num", model.Dow_Num),
                    new SqlParameter("@ENtityID", model.ENtityID),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("ls_PayrollDOWS_RBVH_Delete", itemID);
        }
    }
}
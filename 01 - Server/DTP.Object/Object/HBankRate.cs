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
    public class HBankRate : BaseDBEntity
    {
        [DataMember]
        public int HouseRateID { get; set; }

        [DataMember]
        public string HousceCode { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal Rate { get; set; }
    }
    [CollectionDataContract]
    public class HBankRateCollection : BaseDBEntityCollection<HBankRate> { }
    public class HBankRateManager
    {
        private static HBankRate GetItemFromReader(IDataReader dataReader)
        {
            HBankRate objItem = new HBankRate();
            objItem.HouseRateID = SqlHelper.GetInt(dataReader, "HouseRateID");

            objItem.HousceCode = SqlHelper.GetString(dataReader, "HousceCode");

            objItem.Year = SqlHelper.GetInt(dataReader, "Year");

            objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");
            return objItem;
        }
        public static HBankRate GetItemByID(Int64 houseRateID)
        {
            HBankRate item = new HBankRate();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("HBankRate_GetByID");
            db.AddInParameter(dbCommand, "HouseRateID", DbType.Int64, houseRateID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
           
            return item;


        }
        public static HBankRate AddItem(HBankRate model)
        {
            //int result = 0;
            //using (var reader = SqlHelper.ExecuteReader("HBankRate_Add", CreateSqlParameter(model)))
            //{
            //    while (reader.Read())
            //    {
            //        result = int.Parse(reader[0].ToString());
            //    }
            //}
            //return GetItemByID(result);

            HBankRate item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("HBankRate_Add");
            db.AddOutParameter(dbCommand, "HouseRateID", DbType.Int32, 0);

            db.AddInParameter(dbCommand, "HousceCode", DbType.String, model.HousceCode);
            db.AddInParameter(dbCommand, "Year", DbType.Int32, model.Year);
            
            db.AddInParameter(dbCommand, "Rate", DbType.Decimal, model.Rate);

      
            if (db.ExecuteNonQuery(dbCommand) > 0)
            {

                int itemID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@HouseRateID"));
                item = GetItemByID(itemID);

            }
            return item;

        }
        public static HBankRate UpdateItem(HBankRate model)
        {
            //string result = string.Empty;
            //using (var reader = SqlHelper.ExecuteReader("HBankRate_Update", CreateSqlParameter(model)))
            //{
            //    while (reader.Read())
            //    {
            //        result = reader[0].ToString();
            //    }
            //}
            //return GetItemByID(model.HouseRateID);


            HBankRate item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("HBankRate_Update");
            db.AddInParameter(dbCommand, "HouseRateID", DbType.Int32, model.HouseRateID);

            db.AddInParameter(dbCommand, "Year", DbType.Int32, model.Year);
            db.AddInParameter(dbCommand, "HousceCode", DbType.String, model.HousceCode);

            db.AddInParameter(dbCommand, "Rate", DbType.Decimal, model.Rate);
            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(model.HouseRateID);
            //if (item != null)
            //{
            //    RemoveCache(item);
            //}
            return item;

        }

        public static HBankRateCollection HBankRateDefaultByHouseCode(DBtblHouseBanking objHouseItem)
        {
            HBankRateCollection collection = new HBankRateCollection();
            if (objHouseItem != null)
            {
                HBankRate objItem;
                for (int i = 0; i < objHouseItem.BYear; i++)
                {
                    objItem = new HBankRate(){
                        HouseRateID=0,
                        Year = i+1,
                        Rate = objHouseItem.BRate
                    };
                    collection.Add(objItem);
                }
            }
            return collection;
        }

        public static HBankRateCollection GetAllItem()
        {
            HBankRateCollection collection = new HBankRateCollection();
            using (var reader = SqlHelper.ExecuteReader("HBankRate_GetList", null))
            {
                HBankRate objItem;
                while (reader.Read())
                {
                    objItem = GetItemFromReader(reader);
                    if (objItem != null)
                    {
                        collection.Add(objItem);
                    }
                }
            }
            return collection;
        }
        public static HBankRateCollection HBankRateGetAllItemByHouseCode(string HouseCode)
        {


            HBankRateCollection ItemCollection = new HBankRateCollection();
            HBankRate item;
            try
            {
                Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
                DbCommand dbCommand = db.GetStoredProcCommand("HBankRate_GetByHouseCode");

                db.AddInParameter(dbCommand, "HousceCode", DbType.String, HouseCode);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    
                    while (dataReader.Read())
                    {
                        item = GetItemFromReader(dataReader);
                        ItemCollection.Add(item);
                    }
                }
            }
            catch (Exception objEx)
            {
                item = new HBankRate()
                {
                    HousceCode=objEx.Message
                };
                ItemCollection.Add(item);
            }


            return ItemCollection;

        }
        private static SqlParameter[] CreateSqlParameter(HBankRate model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@HouseRateID", model.HouseRateID),
					new SqlParameter("@HousceCode", model.HousceCode),
					new SqlParameter("@Year", model.Year),
					new SqlParameter("@Rate", model.Rate),
					
                };
        }

        public int DeleteItem(Int64 itemID)
        {
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("HBankRate_Delete");
            db.AddInParameter(dbCommand, "HouseRateID", DbType.Int64, itemID);
            DBgl_FriendRequest objItem = DBgl_FriendRequestManager.GetItemByID(itemID);
            //if (objItem != null) { RemoveCache(objItem); };
            return db.ExecuteNonQuery(dbCommand);
            // return DataHelper.ExecuteNonQuery("", itemID);
        }
    }
}

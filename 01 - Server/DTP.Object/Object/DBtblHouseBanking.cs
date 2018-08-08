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
    public class DBtblHouseBanking : BaseDBEntity
    {
        [DataMember]
        public string HouseCode { get; set; }

        [DataMember]
        public decimal HPrice { get; set; }

        [DataMember]
        public decimal HLength { get; set; }

        [DataMember]
        public decimal HWidth { get; set; }

        [DataMember]
        public decimal BRate { get; set; }

        [DataMember]
        public decimal BYear { get; set; }

        [DataMember]
        public string HAddress { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int AreaId { get; set; }

        [DataMember]
        public string GoogleLink { get; set; }

        [DataMember]
        public string HOwnerName { get; set; }

        [DataMember]
        public string HOwnerPhone { get; set; }

        [DataMember]
        public int BMonth { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ErroMessage { get; set; }

    }
    [CollectionDataContract]
    public class DBtblHouseBankingCollection : BaseDBEntityCollection<DBtblHouseBanking> { }

    [DataContract]
    public class HouseCalculation : BaseDBEntity
    {

        [DataMember]
        public string Date { get; set; }


        [DataMember]
        public int month { get; set; }


        [DataMember]
        public decimal Payment { get; set; }


        [DataMember]
        public decimal Principal { get; set; }

        [DataMember]
        public decimal Interest { get; set; }

        [DataMember]
        public decimal TotalInterest { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

    }

    [CollectionDataContract]
    public class HouseCalculationCollection : BaseDBEntityCollection<HouseCalculation> { }

    public class DBtblHouseBankingManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "DTP.Object.tblHouseBanking.all";
        private const string SETTINGS_ID_KEY = "DTP.Object.tblHouseBanking.{0}";
        public static Dictionary<string, string> column = new Dictionary<string, string>()
                {
                    {"Date","Date"},
                    {"Month","Month"},
                     {"Payment","Payment"},
                      {"Principal","Principal"},
                       {"Interest","Interest"},
                        {"TotalInterest","TotalInterest"},
                         {"Balance","Balance"},
                   // {"active","Active"}
                };
        #endregion
        private static void RemoveCache(DBtblHouseBanking objItem)
        {
            dtpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            dtpCache.Remove(SETTINGS_ALL_KEY);
            dtpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.HouseCode));
        }
        private static DBtblHouseBanking GetItemFromReader(IDataReader dataReader)
        {
            //DBtblHouseBanking objItem = new DBtblHouseBanking();
            //objItem.HouseCode = SqlHelper.GetString(dataReader, "HouseCode");

            //objItem.HPrice = SqlHelper.GetDecimal(dataReader, "HPrice");

            //objItem.HLength = SqlHelper.GetInt(dataReader, "HLength");

            //objItem.HWidth = SqlHelper.GetInt(dataReader, "HWidth");

            //objItem.BRate = SqlHelper.GetDecimal(dataReader, "BRate");

            //objItem.BYear = SqlHelper.GetDecimal(dataReader, "BYear");

            //objItem.BMonth = SqlHelper.GetInt(dataReader, "BMonth");

            //objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            //objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            //return objItem;

            DBtblHouseBanking objItem = new DBtblHouseBanking();
            objItem.HouseCode = SqlHelper.GetString(dataReader, "HouseCode");

            objItem.HPrice = SqlHelper.GetDecimal(dataReader, "HPrice");

            objItem.HLength = SqlHelper.GetDecimal(dataReader, "HLength");

            objItem.HWidth = SqlHelper.GetDecimal(dataReader, "HWidth");

            objItem.BRate = SqlHelper.GetDecimal(dataReader, "BRate");

            objItem.BYear = SqlHelper.GetDecimal(dataReader, "BYear");

            objItem.HAddress = SqlHelper.GetString(dataReader, "HAddress");

            objItem.CountryId = SqlHelper.GetInt(dataReader, "CountryId");

            objItem.AreaId = SqlHelper.GetInt(dataReader, "AreaId");

            objItem.GoogleLink = SqlHelper.GetString(dataReader, "GoogleLink");

            objItem.HOwnerName = SqlHelper.GetString(dataReader, "HOwnerName");

            objItem.HOwnerPhone = SqlHelper.GetString(dataReader, "HOwnerPhone");

            objItem.BMonth = SqlHelper.GetInt(dataReader, "BMonth");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            return objItem;

        }
        public static DBtblHouseBanking GetItemByID(String HouseCode)
        {
            string key = String.Format(SETTINGS_ID_KEY, HouseCode);
            object obj2 = dtpCache.Get(key);
            if (obj2 != null) { return (DBtblHouseBanking)obj2; }


            DBtblHouseBanking item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_GetByID");
            db.AddInParameter(dbCommand, "HouseCode", DbType.String, HouseCode);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    item = GetItemFromReader(dataReader);
                }
            }
            if (AllowCache)
            {
                dtpCache.Max(key, item);
            }
            return item;

        }
        public static DBtblHouseBanking AddItem(DBtblHouseBanking ObjItem)
        {
            DBtblHouseBanking item = null;
            try
            {
                Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
                DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_Add");

                db.AddOutParameter(dbCommand, "HouseCode", DbType.String, 20);

                db.AddInParameter(dbCommand, "HPrice", DbType.Decimal, ObjItem.HPrice);

                db.AddInParameter(dbCommand, "HLength", DbType.Decimal, ObjItem.HLength);

                db.AddInParameter(dbCommand, "HWidth", DbType.Decimal, ObjItem.HWidth);

                db.AddInParameter(dbCommand, "HAddress", DbType.String, ObjItem.HAddress);

                db.AddInParameter(dbCommand, "CountryId", DbType.Int32, ObjItem.CountryId);

                db.AddInParameter(dbCommand, "AreaId", DbType.Int32, ObjItem.AreaId);


                db.AddInParameter(dbCommand, "GoogleLink", DbType.String, ObjItem.GoogleLink);

                db.AddInParameter(dbCommand, "HOwnerName", DbType.String, ObjItem.HOwnerName);

                db.AddInParameter(dbCommand, "HOwnerPhone", DbType.String, ObjItem.HOwnerPhone);

                db.AddInParameter(dbCommand, "BRate", DbType.Decimal, ObjItem.BRate);

                db.AddInParameter(dbCommand, "BYear", DbType.Decimal, ObjItem.BYear);

                db.AddInParameter(dbCommand, "BMonth", DbType.Int32, ObjItem.BMonth);

                db.AddInParameter(dbCommand, "CreatedUser", DbType.String, ObjItem.CreatedUser);

                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, ObjItem.CreatedDate);


                if (db.ExecuteNonQuery(dbCommand) > 0)
                {

                    string housecode = Convert.ToString(db.GetParameterValue(dbCommand, "@HouseCode"));
                    item = GetItemByID(housecode);

                  
                }

                //using (IDataReader dataReader = SqlHelper.ExecuteReader("tblHouseBanking_Add", CreateSqlParameter(ObjItem)))
                //{
                //    if (dataReader.Read())
                //    {
                //        item = GetItemByID(dataReader[0].ToString());
                //    }
                //}
                //db.ExecuteReader("tblHouseBanking_Update", CreateSqlParameter(ObjItem));
                //if (db.ExecuteNonQuery(dbCommand) > 0)
                //{

                //    string itemID = Convert.ToString(db.GetParameterValue(dbCommand, "@HouseCode"));
                //    item = GetItemByID(itemID);

                //    if (item != null)
                //    {
                //        RemoveCache(item);
                //    }
                //}
            }
            catch (Exception ObjIte)
            {
                item = new DBtblHouseBanking();
                item.ErroMessage = ObjIte.Message;
            }
            return item;
        }
        private static SqlParameter[] CreateSqlParameter(DBtblHouseBanking model)
        {
            
            return new SqlParameter[]
                {
                new SqlParameter("@HouseCode",SqlDbType.Text){Value=model.HouseCode},
	            new SqlParameter("@HPrice",SqlDbType.Decimal){Value=model.HPrice},
	            new SqlParameter("@HLength",SqlDbType.Decimal){Value=model.HLength},
	            new SqlParameter("@HWidth",SqlDbType.Decimal){Value=model.HWidth},
	            new SqlParameter("@BRate",SqlDbType.Decimal){Value=model.BRate},
	            new SqlParameter("@BYear",SqlDbType.Int){Value=model.BYear},
	            new SqlParameter("@HAddress",SqlDbType.Text){Value=model.HAddress},
	            new SqlParameter("@CountryId", SqlDbType.Int){Value=model.CountryId},
	            new SqlParameter("@AreaId", SqlDbType.Int){Value=model.AreaId},
	            new SqlParameter("@GoogleLink",SqlDbType.Text){Value=model.GoogleLink},
	            new SqlParameter("@HOwnerName",SqlDbType.Text){Value=model.HOwnerName},
	            new SqlParameter("@HOwnerPhone",SqlDbType.Text){Value=model.HOwnerPhone},
	            new SqlParameter("@BMonth",SqlDbType.Int){Value=model.BMonth},
	            new SqlParameter("@CreatedUser",SqlDbType.Text){Value=model.CreatedUser},
	            new SqlParameter("@CreatedDate",SqlDbType.DateTime){Value=model.CreatedDate},
	
                };
        }
         
        public static DBtblHouseBanking UpdateItem(DBtblHouseBanking ObjItem)
        {
            DBtblHouseBanking item = null;
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_Update");

            db.AddInParameter(dbCommand, "HouseCode", DbType.String, ObjItem.HouseCode);

            db.AddInParameter(dbCommand, "HPrice", DbType.Decimal, ObjItem.HPrice);

            db.AddInParameter(dbCommand, "HLength", DbType.Decimal, ObjItem.HLength);

            db.AddInParameter(dbCommand, "HWidth", DbType.Decimal, ObjItem.HWidth);

            db.AddInParameter(dbCommand, "HAddress", DbType.String, ObjItem.HAddress);

            db.AddInParameter(dbCommand, "CountryId", DbType.Int32, ObjItem.CountryId);

            db.AddInParameter(dbCommand, "AreaId", DbType.Int32, ObjItem.AreaId);


            db.AddInParameter(dbCommand, "GoogleLink", DbType.String, ObjItem.GoogleLink);

            db.AddInParameter(dbCommand, "HOwnerName", DbType.String, ObjItem.HOwnerName);

            db.AddInParameter(dbCommand, "HOwnerPhone", DbType.String, ObjItem.HOwnerPhone);

            db.AddInParameter(dbCommand, "BRate", DbType.Decimal, ObjItem.BRate);

            db.AddInParameter(dbCommand, "BYear", DbType.Decimal, ObjItem.BYear);

            db.AddInParameter(dbCommand, "BMonth", DbType.Int32, ObjItem.BMonth);

            db.AddInParameter(dbCommand, "CreatedUser", DbType.String, ObjItem.CreatedUser);

            db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, ObjItem.CreatedDate);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                item = GetItemByID(ObjItem.HouseCode);
            //if (item != null)
            //{
            //    RemoveCache(item);
            //}
            return item;
        }
        
        
        public static DBtblHouseBankingCollection GetItemPagging(int page, int rec, string strSearch, out int TotalRecords)
        {
            TotalRecords = 0;
            DBtblHouseBankingCollection ItemCollection = new DBtblHouseBankingCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_Paging");
            db.AddInParameter(dbCommand, "Page", DbType.Int32, page);
            db.AddInParameter(dbCommand, "RecsPerPage", DbType.Int32, rec);
            db.AddInParameter(dbCommand, "SearchValue", DbType.String, strSearch);
            db.AddOutParameter(dbCommand, "TotalRecords", DbType.Int32, 0);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBtblHouseBanking item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            TotalRecords = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TotalRecords"));
            return ItemCollection;
        }
        public static DBtblHouseBankingCollection GetAllItem()
        {
            string key = SETTINGS_ALL_KEY;
            object obj2 = dtpCache.Get(key);
            if ((obj2 != null))
            {
                return (DBtblHouseBankingCollection)obj2;
            }
            DBtblHouseBankingCollection ItemCollection = new DBtblHouseBankingCollection();
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_GetAll");
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    DBtblHouseBanking item = GetItemFromReader(dataReader);
                    ItemCollection.Add(item);
                }
            }
            if (AllowCache)
            {
                dtpCache.Max(key, ItemCollection);
            }

            return ItemCollection;
        }
        public static string GetJson(DBtblHouseBankingCollection itemCollection)
        {
            StringBuilder builder = new StringBuilder();
            if (itemCollection.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("results");
                    jsonWriter.WriteStartArray();
                    itemCollection.ForEach(delegate(DBtblHouseBanking objItem)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("HouseCode");
                        jsonWriter.WriteValue(objItem.HouseCode.ToString());
                        jsonWriter.WritePropertyName("HPrice");
                        jsonWriter.WriteValue(objItem.HPrice.ToString());
                        jsonWriter.WritePropertyName("HLength");
                        jsonWriter.WriteValue(objItem.HLength.ToString());
                        jsonWriter.WritePropertyName("HWidth");
                        jsonWriter.WriteValue(objItem.HWidth.ToString());
                        jsonWriter.WritePropertyName("BRate");
                        jsonWriter.WriteValue(objItem.BRate.ToString());
                        jsonWriter.WritePropertyName("BYear");
                        jsonWriter.WriteValue(objItem.BYear.ToString());
                        jsonWriter.WritePropertyName("BMonth");
                        jsonWriter.WriteValue(objItem.BMonth.ToString());
                        jsonWriter.WritePropertyName("CreatedUser");
                        jsonWriter.WriteValue(objItem.CreatedUser.ToString());
                        jsonWriter.WritePropertyName("CreatedDate");
                        jsonWriter.WriteValue(objItem.CreatedDate.ToString());

                        jsonWriter.WriteEndObject();
                    });
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteEndObject();
                    builder.AppendLine(sw.ToString());

                }
            }
            else
            {
                // builder.AppendLine(@"{""results"":[{""id"":""-1"",""myvalue"":""" + MainFunction.ngonngu("gl.nodata") + @"""}]}");

            }
            return builder.ToString();
        }

        public static HouseCalculationCollection CalBanking(string HSCode)
        {
            DBtblHouseBanking ObjItem = GetItemByID(HSCode);
            return gridData(ObjItem);
        }
        public static HouseCalculationCollection gridData(DBtblHouseBanking objItem)
        {
            DataTable objDtb = new DataTable();
            HouseCalculationCollection objCollection = new HouseCalculationCollection();
            HouseCalculation objHCal;
            //objDtb.Columns.Add("Date", typeof(System.String));
            //objDtb.Columns.Add("Month", typeof(System.Int32));
            //objDtb.Columns.Add("Payment", typeof(System.Decimal));
            //objDtb.Columns.Add("Principal", typeof(System.Decimal));
            //objDtb.Columns.Add("Interest", typeof(System.Decimal));
            //objDtb.Columns.Add("TotalInterest", typeof(System.Decimal));
            //objDtb.Columns.Add("Balance", typeof(System.Decimal));
            if (objItem != null)
            {
                //DataRow _row;

                if (objItem.BRate != 0 && objItem.HPrice != 0)
                {
                   
                    decimal payment = 0;
                    decimal Principal = objItem.HPrice / objItem.BMonth;
                    decimal Balance = objItem.HPrice;
                    decimal Interest = 0;
                    decimal TotalInter = 0;
                    for (int i = 0; i < objItem.BMonth; i++)
                    {
                        objHCal = new HouseCalculation();
                        objHCal.Date = (i + 1).ToString();
                        objHCal.month = i + 1;
                        objHCal.Principal = Principal;
                        //_row = objDtb.NewRow();
                        //_row["Date"] = i + 1;
                        //_row["Month"] = i + 1;
                        //_row["Principal"] = Principal;
                        Interest = Balance * objItem.BRate / 100 / 12;
                        Balance = Balance - Principal;
                        TotalInter = TotalInter + Interest;
                        objHCal.Balance = Balance;
                        objHCal.Payment = Interest + Principal;
                        objHCal.Interest = Interest;
                        objHCal.TotalInterest = TotalInter;
                        //_row["Balance"] = Balance;
                        //_row["Payment"] = Interest + Principal;
                        //_row["Interest"] = Interest;
                        //_row["TotalInterest"] = TotalInter;
                        //objDtb.Rows.Add(_row);
                        objCollection.Add(objHCal);
                    }
                }
                ///add row to system
            }
            //  DataColumn objCol 
            return objCollection;
        }
        public static int DeleteItem(string ItemId)
        {
            Database db = SqlHelper.CreateConnection(SqlHelper.MyConnection);
            DbCommand dbCommand = db.GetStoredProcCommand("tblHouseBanking_Delete");
            db.AddInParameter(dbCommand, "HouseCode", DbType.String, ItemId);
            DBtblHouseBanking item = GetItemByID(ItemId);
            if (item != null)
            {
                RemoveCache(item);
            }
            return db.ExecuteNonQuery(dbCommand);
        }

        public static bool AllowCache
        {
            get
            {
                return false;
            }
        }
    }
}

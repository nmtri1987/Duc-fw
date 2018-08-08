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
    public class Country : BaseDBEntity
    {
        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool AllowsRegistration { get; set; }

        [DataMember]
        public bool AllowsBilling { get; set; }

        [DataMember]
        public bool AllowsShipping { get; set; }

        [DataMember]
        public string TwoLetterISOCode { get; set; }

        [DataMember]
        public string ThreeLetterISOCode { get; set; }

        [DataMember]
        public int NumericISOCode { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }


    }
    public class CountryCollection : BaseDBEntityCollection<Country> { }
    public class CountryManager
    {
        private static Country GetItemFromReader(IDataReader dataReader)
        {
            Country objItem = new Country();
            objItem.CountryID = SqlHelper.GetInt(dataReader, "CountryID");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.AllowsRegistration = SqlHelper.GetBoolean(dataReader, "AllowsRegistration");

            objItem.AllowsBilling = SqlHelper.GetBoolean(dataReader, "AllowsBilling");

            objItem.AllowsShipping = SqlHelper.GetBoolean(dataReader, "AllowsShipping");

            objItem.TwoLetterISOCode = SqlHelper.GetString(dataReader, "TwoLetterISOCode");

            objItem.ThreeLetterISOCode = SqlHelper.GetString(dataReader, "ThreeLetterISOCode");

            objItem.NumericISOCode = SqlHelper.GetInt(dataReader, "NumericISOCode");

            objItem.Published = SqlHelper.GetBoolean(dataReader, "Published");

            objItem.DisplayOrder = SqlHelper.GetInt(dataReader, "DisplayOrder");


            return objItem;
        }
        public static Country GetItemByID(Int32 countryID)
        {
            Country item = new Country();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CountryID", countryID);
            using (var reader = SqlHelper.ExecuteReader("tblCountry_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Country AddItem(Country model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblCountry_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static Country UpdateItem(Country model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblCountry_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static CountryCollection GetAllItem()
        {
            CountryCollection collection = new CountryCollection();
            using (var reader = SqlHelper.ExecuteReader("tblCountry_GetAll", null))
            {
                while (reader.Read())
                {
                    Country obj = new Country();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Country model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@CountryID", model.CountryID),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@AllowsRegistration", model.AllowsRegistration),
                    new SqlParameter("@AllowsBilling", model.AllowsBilling),
                    new SqlParameter("@AllowsShipping", model.AllowsShipping),
                    new SqlParameter("@TwoLetterISOCode", model.TwoLetterISOCode),
                    new SqlParameter("@ThreeLetterISOCode", model.ThreeLetterISOCode),
                    new SqlParameter("@NumericISOCode", model.NumericISOCode),
                    new SqlParameter("@Published", model.Published),
                    new SqlParameter("@DisplayOrder", model.DisplayOrder),

                };
        }

        public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblCountry_Delete", itemID);
        }
    }
}
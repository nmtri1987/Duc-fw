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
    public class Company : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string CompanyCD { get; set; }

        [DataMember]
        public string BaseCuryID { get; set; }

        [DataMember]
        public int BAccountID { get; set; }

        [DataMember]
        public string CountryID { get; set; }

        [DataMember]
        public string PhoneMask { get; set; }

        [DataMember]
        public int ParentCompanyID { get; set; }

        [DataMember]
        public string CompanyType { get; set; }

        [DataMember]
        public string Theme { get; set; }

        [DataMember]
        public bool IsReadOnly { get; set; }

        [DataMember]
        public bool IsTemplate { get; set; }

        [DataMember]
        public string CompanyKey { get; set; }

        [DataMember]
        public int Sequence { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]        public int TotalRecord { get; set; }

    }
    public class CompanyCollection : BaseDBEntityCollection<Company> { }
    public class CompanyManager
    {
        private static Company GetItemFromReader(IDataReader dataReader)
        {
            Company objItem = new Company();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.CompanyCD = SqlHelper.GetString(dataReader, "CompanyCD");

            objItem.BaseCuryID = SqlHelper.GetString(dataReader, "BaseCuryID");

            objItem.BAccountID = SqlHelper.GetInt(dataReader, "BAccountID");

            objItem.CountryID = SqlHelper.GetString(dataReader, "CountryID");

            objItem.PhoneMask = SqlHelper.GetString(dataReader, "PhoneMask");

            objItem.ParentCompanyID = SqlHelper.GetInt(dataReader, "ParentCompanyID");

            objItem.CompanyType = SqlHelper.GetString(dataReader, "CompanyType");

            objItem.Theme = SqlHelper.GetString(dataReader, "Theme");

            objItem.IsReadOnly = SqlHelper.GetBoolean(dataReader, "IsReadOnly");

            objItem.IsTemplate = SqlHelper.GetBoolean(dataReader, "IsTemplate");

            objItem.CompanyKey = SqlHelper.GetString(dataReader, "CompanyKey");

            objItem.Sequence = SqlHelper.GetInt(dataReader, "Sequence");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Company GetItemByID(int CompanyID)
        {
            Company item = new Company();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Company_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Company AddItem(Company model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Company_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (int)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result);

        }
        public static Company UpdateItem(Company model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Company_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.CompanyID);

        }
        public static CompanyCollection GetAllItem()
        {
            CompanyCollection collection = new CompanyCollection();

            using (var reader = SqlHelper.ExecuteReader("Company_GetAll", null))
            {
                while (reader.Read())
                {
                    Company obj = new Company();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static CompanyCollection Search(SearchFilter SearchKey)
        {
            CompanyCollection collection = new CompanyCollection();
            using (var reader = SqlHelper.ExecuteReader("Company_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Company obj = new Company();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static CompanyCollection GetbyUser(string CreatedUser)
        {
            CompanyCollection collection = new CompanyCollection();
            Company obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
              };
            using (var reader = SqlHelper.ExecuteReader("Company_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Company model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@CompanyCD", model.CompanyCD),
                    new SqlParameter("@BaseCuryID", model.BaseCuryID),
                    new SqlParameter("@BAccountID", model.BAccountID),
                    new SqlParameter("@CountryID", model.CountryID),
                    new SqlParameter("@PhoneMask", model.PhoneMask),
                    new SqlParameter("@ParentCompanyID", model.ParentCompanyID),
                    new SqlParameter("@CompanyType", model.CompanyType),
                    new SqlParameter("@Theme", model.Theme),
                    new SqlParameter("@IsReadOnly", model.IsReadOnly),
                    new SqlParameter("@IsTemplate", model.IsTemplate),
                    new SqlParameter("@CompanyKey", model.CompanyKey),
                    new SqlParameter("@Sequence", model.Sequence),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("Company_Delete", new SqlParameter[]
            {
                new SqlParameter("@CompanyID",itemID)});
        }
    }
}
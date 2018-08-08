using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace ifinds.Object.AR
{
    [DataContract]
    public class Customer : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int CustomerType { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Web { get; set; }

        [DataMember]
        public string Phone1 { get; set; }

        [DataMember]
        public string Phone2 { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string AccountRef { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string CustomerClass { get; set; }

        [DataMember]
        public string CuryID { get; set; }

        [DataMember]
        public string CuryRate { get; set; }

        [DataMember]
        public decimal CreditLimit { get; set; }

        [DataMember]
        public string TaxRegID { get; set; }

        [DataMember]
        public string PaymentMethodID { get; set; }

        [DataMember]
        public string CashAccountCD { get; set; }

        [DataMember]
        public string BankAccountID { get; set; }

        [DataMember]
        public int ARAccount { get; set; }

        [DataMember]
        public int ARAccountSub { get; set; }

        [DataMember]
        public int PrepaymentAccount { get; set; }

        [DataMember]
        public int PrepaymentAccountSub { get; set; }

        [DataMember]
        public decimal BalanceAmt { get; set; }

        [DataMember]
        public decimal CurBalanceAmt { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]        public int TotalRecord { get; set; }

        [DataMember]
        public string SettleTerm { get; set; }

        [DataMember]
        public decimal TotalDeposit { get; set; }

    }
    public class CustomerCollection : BaseDBEntityCollection<Customer> { }
    public class CustomerManager
    {
        private static Customer GetItemFromReader(IDataReader dataReader)
        {
            Customer objItem = new Customer();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.CustomerID = SqlHelper.GetString(dataReader, "CustomerID");

            objItem.CustomerType = SqlHelper.GetInt(dataReader, "CustomerType");

            objItem.CompanyName = SqlHelper.GetString(dataReader, "CompanyName");

            objItem.Email = SqlHelper.GetString(dataReader, "Email");

            objItem.Web = SqlHelper.GetString(dataReader, "Web");

            objItem.Phone1 = SqlHelper.GetString(dataReader, "Phone1");

            objItem.Phone2 = SqlHelper.GetString(dataReader, "Phone2");

            objItem.Fax = SqlHelper.GetString(dataReader, "Fax");

            objItem.AccountRef = SqlHelper.GetString(dataReader, "AccountRef");

            objItem.Address = SqlHelper.GetString(dataReader, "Address");

            objItem.City = SqlHelper.GetString(dataReader, "City");

            objItem.Country = SqlHelper.GetString(dataReader, "Country");

            objItem.PostalCode = SqlHelper.GetString(dataReader, "PostalCode");

            objItem.CustomerClass = SqlHelper.GetString(dataReader, "CustomerClass");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.CuryRate = SqlHelper.GetString(dataReader, "CuryRate");

            objItem.CreditLimit = SqlHelper.GetDecimal(dataReader, "CreditLimit");

            objItem.TaxRegID = SqlHelper.GetString(dataReader, "TaxRegID");

            objItem.PaymentMethodID = SqlHelper.GetString(dataReader, "PaymentMethodID");

            objItem.CashAccountCD = SqlHelper.GetString(dataReader, "CashAccountCD");

            objItem.BankAccountID = SqlHelper.GetString(dataReader, "BankAccountID");

            objItem.ARAccount = SqlHelper.GetInt(dataReader, "ARAccount");

            objItem.ARAccountSub = SqlHelper.GetInt(dataReader, "ARAccountSub");

            objItem.PrepaymentAccount = SqlHelper.GetInt(dataReader, "PrepaymentAccount");

            objItem.PrepaymentAccountSub = SqlHelper.GetInt(dataReader, "PrepaymentAccountSub");

            objItem.BalanceAmt = SqlHelper.GetDecimal(dataReader, "BalanceAmt");

            objItem.CurBalanceAmt = SqlHelper.GetDecimal(dataReader, "CurBalanceAmt");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.SettleTerm = SqlHelper.GetString(dataReader, "SettleTerm");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            if (SqlHelper.ColumnExists(dataReader, "TotalDeposit"))
            {
                objItem.TotalDeposit = SqlHelper.GetDecimal(dataReader, "TotalDeposit");
            }
            return objItem;
        }
        public static Customer GetItemByID(string CustomerID, int CompanyID)
        {
            Customer item = new Customer();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CustomerID", CustomerID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Customer_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Customer AddItem(Customer model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Customer_Add", CreateSqlParameter(model)))
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
        public static Customer UpdateItem(Customer model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Customer_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.CustomerID, model.CompanyID);

        }
        public static CustomerCollection GetAllItem(int CompanyID)
        {
            CustomerCollection collection = new CustomerCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Customer_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Customer obj = new Customer();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static CustomerCollection Search(SearchFilter SearchKey)
        {
            CustomerCollection collection = new CustomerCollection();
            using (var reader = SqlHelper.ExecuteReader("Customer_Search", SearchFilterManager.SqlSearchDynParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Customer obj = new Customer();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static CustomerCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            CustomerCollection collection = new CustomerCollection();
            Customer obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("Customer_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Customer model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@CustomerID", model.CustomerID),
                    new SqlParameter("@CustomerType", model.CustomerType),
                    new SqlParameter("@CompanyName", model.CompanyName),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@Web", model.Web),
                    new SqlParameter("@Phone1", model.Phone1),
                    new SqlParameter("@Phone2", model.Phone2),
                    new SqlParameter("@Fax", model.Fax),
                    new SqlParameter("@AccountRef", model.AccountRef),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@City", model.City),
                    new SqlParameter("@Country", model.Country),
                    new SqlParameter("@PostalCode", model.PostalCode),
                    new SqlParameter("@CustomerClass", model.CustomerClass),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@CuryRate", model.CuryRate),
                    new SqlParameter("@CreditLimit", model.CreditLimit),
                    new SqlParameter("@TaxRegID", model.TaxRegID),
                    new SqlParameter("@PaymentMethodID", model.PaymentMethodID),
                    new SqlParameter("@CashAccountCD", model.CashAccountCD),
                    new SqlParameter("@BankAccountID", model.BankAccountID),
                    new SqlParameter("@ARAccount", model.ARAccount),
                    new SqlParameter("@ARAccountSub", model.ARAccountSub),
                    new SqlParameter("@PrepaymentAccount", model.PrepaymentAccount),
                    new SqlParameter("@PrepaymentAccountSub", model.PrepaymentAccountSub),
                    new SqlParameter("@BalanceAmt", model.BalanceAmt),
                    new SqlParameter("@CurBalanceAmt", model.CurBalanceAmt),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@SettleTerm", model.SettleTerm),
                };
        }

        public static int DeleteItem(string itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("Customer_Delete", new SqlParameter[]
            {
                new SqlParameter(@"CustomerID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
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
    public class CustomerClass : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string CustomerClassID { get; set; }

        [DataMember]
        public string Descr { get; set; }

        [DataMember]
        public string TermsID { get; set; }

        [DataMember]
        public string TaxZoneID { get; set; }

        [DataMember]
        public bool RequireTaxZone { get; set; }

        [DataMember]
        public string AvalaraCustomerUsageType { get; set; }

        [DataMember]
        public bool RequireAvalaraCustomerUsageType { get; set; }

        [DataMember]
        public string DefPaymentMethodID { get; set; }

        [DataMember]
        public string CuryID { get; set; }

        [DataMember]
        public string CuryRateTypeID { get; set; }

        [DataMember]
        public bool AllowOverrideCury { get; set; }

        [DataMember]
        public bool AllowOverrideRate { get; set; }

        [DataMember]
        public int ARAcctID { get; set; }

        [DataMember]
        public int ARSubID { get; set; }

        [DataMember]
        public int ContraARAcctID { get; set; }

        [DataMember]
        public int ContraARSubID { get; set; }

        [DataMember]
        public int DiscountAcctID { get; set; }

        [DataMember]
        public int DiscountSubID { get; set; }

        [DataMember]
        public int DiscTakenAcctID { get; set; }

        [DataMember]
        public int DiscTakenSubID { get; set; }

        [DataMember]
        public int SalesAcctID { get; set; }

        [DataMember]
        public int SalesSubID { get; set; }

        [DataMember]
        public int COGSAcctID { get; set; }

        [DataMember]
        public int COGSSubID { get; set; }

        [DataMember]
        public int FreightAcctID { get; set; }

        [DataMember]
        public int FreightSubID { get; set; }

        [DataMember]
        public int MiscAcctID { get; set; }

        [DataMember]
        public int MiscSubID { get; set; }

        [DataMember]
        public int UnrealizedGainAcctID { get; set; }

        [DataMember]
        public int UnrealizedGainSubID { get; set; }

        [DataMember]
        public int UnrealizedLossAcctID { get; set; }

        [DataMember]
        public int UnrealizedLossSubID { get; set; }

        [DataMember]
        public bool AutoApplyPayments { get; set; }

        [DataMember]
        public bool PrintStatements { get; set; }

        [DataMember]
        public bool PrintCuryStatements { get; set; }

        [DataMember]
        public bool SendStatementByEmail { get; set; }

        [DataMember]
        public decimal CreditLimit { get; set; }

        [DataMember]
        public string CreditRule { get; set; }

        [DataMember]
        public int CreditDaysPastDue { get; set; }

        [DataMember]
        public string StatementCycleId { get; set; }

        [DataMember]
        public string StatementType { get; set; }

        [DataMember]
        public bool SmallBalanceAllow { get; set; }

        [DataMember]
        public decimal SmallBalanceLimit { get; set; }

        [DataMember]
        public bool FinChargeApply { get; set; }

        [DataMember]
        public string FinChargeID { get; set; }

        [DataMember]
        public string CountryID { get; set; }

        [DataMember]
        public decimal OverLimitAmount { get; set; }

        [DataMember]
        public int PrepaymentAcctID { get; set; }

        [DataMember]
        public int PrepaymentSubID { get; set; }

        [DataMember]
        public string ShipVia { get; set; }

        [DataMember]
        public string ShipComplete { get; set; }

        [DataMember]
        public string ShipTermsID { get; set; }

        [DataMember]
        public int SalesPersonID { get; set; }

        [DataMember]
        public decimal DiscountLimit { get; set; }

        [DataMember]
        public byte[] GroupMask { get; set; }

        [DataMember]
        public TimeSpan tstamp { get; set; }

        [DataMember]
        public bool PrintInvoices { get; set; }

        [DataMember]
        public bool MailInvoices { get; set; }

        [DataMember]
        public bool PrintDunningLetters { get; set; }

        [DataMember]
        public bool MailDunningLetters { get; set; }

        [DataMember]
        public string PriceClassID { get; set; }

        [DataMember]
        public bool DefaultLocationCDFromBranch { get; set; }

        [DataMember]
        public string LocaleName { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int TotalRecord { get; set; }

    }
    public class CustomerClassCollection : BaseDBEntityCollection<CustomerClass> { }
    public class CustomerClassManager
    {
        private static CustomerClass GetItemFromReader(IDataReader dataReader)
        {
            CustomerClass objItem = new CustomerClass();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.CustomerClassID = SqlHelper.GetString(dataReader, "CustomerClassID");

            objItem.Descr = SqlHelper.GetString(dataReader, "Descr");

            objItem.TermsID = SqlHelper.GetString(dataReader, "TermsID");

            objItem.TaxZoneID = SqlHelper.GetString(dataReader, "TaxZoneID");

            objItem.RequireTaxZone = SqlHelper.GetBoolean(dataReader, "RequireTaxZone");

            objItem.AvalaraCustomerUsageType = SqlHelper.GetString(dataReader, "AvalaraCustomerUsageType");

            objItem.RequireAvalaraCustomerUsageType = SqlHelper.GetBoolean(dataReader, "RequireAvalaraCustomerUsageType");

            objItem.DefPaymentMethodID = SqlHelper.GetString(dataReader, "DefPaymentMethodID");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.CuryRateTypeID = SqlHelper.GetString(dataReader, "CuryRateTypeID");

            objItem.AllowOverrideCury = SqlHelper.GetBoolean(dataReader, "AllowOverrideCury");

            objItem.AllowOverrideRate = SqlHelper.GetBoolean(dataReader, "AllowOverrideRate");

            objItem.ARAcctID = SqlHelper.GetInt(dataReader, "ARAcctID");

            objItem.ARSubID = SqlHelper.GetInt(dataReader, "ARSubID");

            objItem.ContraARAcctID = SqlHelper.GetInt(dataReader, "ContraARAcctID");

            objItem.ContraARSubID = SqlHelper.GetInt(dataReader, "ContraARSubID");

            objItem.DiscountAcctID = SqlHelper.GetInt(dataReader, "DiscountAcctID");

            objItem.DiscountSubID = SqlHelper.GetInt(dataReader, "DiscountSubID");

            objItem.DiscTakenAcctID = SqlHelper.GetInt(dataReader, "DiscTakenAcctID");

            objItem.DiscTakenSubID = SqlHelper.GetInt(dataReader, "DiscTakenSubID");

            objItem.SalesAcctID = SqlHelper.GetInt(dataReader, "SalesAcctID");

            objItem.SalesSubID = SqlHelper.GetInt(dataReader, "SalesSubID");

            objItem.COGSAcctID = SqlHelper.GetInt(dataReader, "COGSAcctID");

            objItem.COGSSubID = SqlHelper.GetInt(dataReader, "COGSSubID");

            objItem.FreightAcctID = SqlHelper.GetInt(dataReader, "FreightAcctID");

            objItem.FreightSubID = SqlHelper.GetInt(dataReader, "FreightSubID");

            objItem.MiscAcctID = SqlHelper.GetInt(dataReader, "MiscAcctID");

            objItem.MiscSubID = SqlHelper.GetInt(dataReader, "MiscSubID");

            objItem.UnrealizedGainAcctID = SqlHelper.GetInt(dataReader, "UnrealizedGainAcctID");

            objItem.UnrealizedGainSubID = SqlHelper.GetInt(dataReader, "UnrealizedGainSubID");

            objItem.UnrealizedLossAcctID = SqlHelper.GetInt(dataReader, "UnrealizedLossAcctID");

            objItem.UnrealizedLossSubID = SqlHelper.GetInt(dataReader, "UnrealizedLossSubID");

            objItem.AutoApplyPayments = SqlHelper.GetBoolean(dataReader, "AutoApplyPayments");

            objItem.PrintStatements = SqlHelper.GetBoolean(dataReader, "PrintStatements");

            objItem.PrintCuryStatements = SqlHelper.GetBoolean(dataReader, "PrintCuryStatements");

            objItem.SendStatementByEmail = SqlHelper.GetBoolean(dataReader, "SendStatementByEmail");

            objItem.CreditLimit = SqlHelper.GetDecimal(dataReader, "CreditLimit");

            objItem.CreditRule = SqlHelper.GetString(dataReader, "CreditRule");

            objItem.CreditDaysPastDue = SqlHelper.GetSmallInt(dataReader, "CreditDaysPastDue");

            objItem.StatementCycleId = SqlHelper.GetString(dataReader, "StatementCycleId");

            objItem.StatementType = SqlHelper.GetString(dataReader, "StatementType");

            objItem.SmallBalanceAllow = SqlHelper.GetBoolean(dataReader, "SmallBalanceAllow");

            objItem.SmallBalanceLimit = SqlHelper.GetDecimal(dataReader, "SmallBalanceLimit");

            objItem.FinChargeApply = SqlHelper.GetBoolean(dataReader, "FinChargeApply");

            objItem.FinChargeID = SqlHelper.GetString(dataReader, "FinChargeID");

            objItem.CountryID = SqlHelper.GetString(dataReader, "CountryID");

            objItem.OverLimitAmount = SqlHelper.GetDecimal(dataReader, "OverLimitAmount");

            objItem.PrepaymentAcctID = SqlHelper.GetInt(dataReader, "PrepaymentAcctID");

            objItem.PrepaymentSubID = SqlHelper.GetInt(dataReader, "PrepaymentSubID");

            objItem.ShipVia = SqlHelper.GetString(dataReader, "ShipVia");

            objItem.ShipComplete = SqlHelper.GetString(dataReader, "ShipComplete");

            objItem.ShipTermsID = SqlHelper.GetString(dataReader, "ShipTermsID");

            objItem.SalesPersonID = SqlHelper.GetInt(dataReader, "SalesPersonID");

            objItem.DiscountLimit = SqlHelper.GetDecimal(dataReader, "DiscountLimit");

            objItem.GroupMask = SqlHelper.GetBytes(dataReader, "GroupMask");

            objItem.tstamp = SqlHelper.GetTimeSpan(dataReader, "tstamp");

            objItem.PrintInvoices = SqlHelper.GetBoolean(dataReader, "PrintInvoices");

            objItem.MailInvoices = SqlHelper.GetBoolean(dataReader, "MailInvoices");

            objItem.PrintDunningLetters = SqlHelper.GetBoolean(dataReader, "PrintDunningLetters");

            objItem.MailDunningLetters = SqlHelper.GetBoolean(dataReader, "MailDunningLetters");

            objItem.PriceClassID = SqlHelper.GetString(dataReader, "PriceClassID");

            objItem.DefaultLocationCDFromBranch = SqlHelper.GetBoolean(dataReader, "DefaultLocationCDFromBranch");

            objItem.LocaleName = SqlHelper.GetString(dataReader, "LocaleName");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static CustomerClass GetItemByID(string CustomerClassID, int CompanyID)
        {
            CustomerClass item = new CustomerClass();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CustomerClassID", CustomerClassID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("CustomerClass_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static CustomerClass AddItem(CustomerClass model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CustomerClass_Add", CreateSqlParameter(model)))
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
        public static CustomerClass UpdateItem(CustomerClass model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CustomerClass_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.CustomerClassID, model.CompanyID);

        }
        public static CustomerClassCollection GetAllItem(int CompanyID)
        {
            CustomerClassCollection collection = new CustomerClassCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("CustomerClass_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    CustomerClass obj = new CustomerClass();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static CustomerClassCollection Search(SearchFilter SearchKey)
        {
            CustomerClassCollection collection = new CustomerClassCollection();
            using (var reader = SqlHelper.ExecuteReader("CustomerClass_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    CustomerClass obj = new CustomerClass();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static CustomerClassCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            CustomerClassCollection collection = new CustomerClassCollection();
            CustomerClass obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("CustomerClass_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(CustomerClass model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@CustomerClassID", model.CustomerClassID),
                    new SqlParameter("@Descr", model.Descr),
                    new SqlParameter("@TermsID", model.TermsID),
                    new SqlParameter("@TaxZoneID", model.TaxZoneID),
                    new SqlParameter("@RequireTaxZone", model.RequireTaxZone),
                    new SqlParameter("@AvalaraCustomerUsageType", model.AvalaraCustomerUsageType),
                    new SqlParameter("@RequireAvalaraCustomerUsageType", model.RequireAvalaraCustomerUsageType),
                    new SqlParameter("@DefPaymentMethodID", model.DefPaymentMethodID),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@CuryRateTypeID", model.CuryRateTypeID),
                    new SqlParameter("@AllowOverrideCury", model.AllowOverrideCury),
                    new SqlParameter("@AllowOverrideRate", model.AllowOverrideRate),
                    new SqlParameter("@ARAcctID", model.ARAcctID),
                    new SqlParameter("@ARSubID", model.ARSubID),
                    new SqlParameter("@ContraARAcctID", model.ContraARAcctID),
                    new SqlParameter("@ContraARSubID", model.ContraARSubID),
                    new SqlParameter("@DiscountAcctID", model.DiscountAcctID),
                    new SqlParameter("@DiscountSubID", model.DiscountSubID),
                    new SqlParameter("@DiscTakenAcctID", model.DiscTakenAcctID),
                    new SqlParameter("@DiscTakenSubID", model.DiscTakenSubID),
                    new SqlParameter("@SalesAcctID", model.SalesAcctID),
                    new SqlParameter("@SalesSubID", model.SalesSubID),
                    new SqlParameter("@COGSAcctID", model.COGSAcctID),
                    new SqlParameter("@COGSSubID", model.COGSSubID),
                    new SqlParameter("@FreightAcctID", model.FreightAcctID),
                    new SqlParameter("@FreightSubID", model.FreightSubID),
                    new SqlParameter("@MiscAcctID", model.MiscAcctID),
                    new SqlParameter("@MiscSubID", model.MiscSubID),
                    new SqlParameter("@UnrealizedGainAcctID", model.UnrealizedGainAcctID),
                    new SqlParameter("@UnrealizedGainSubID", model.UnrealizedGainSubID),
                    new SqlParameter("@UnrealizedLossAcctID", model.UnrealizedLossAcctID),
                    new SqlParameter("@UnrealizedLossSubID", model.UnrealizedLossSubID),
                    new SqlParameter("@AutoApplyPayments", model.AutoApplyPayments),
                    new SqlParameter("@PrintStatements", model.PrintStatements),
                    new SqlParameter("@PrintCuryStatements", model.PrintCuryStatements),
                    new SqlParameter("@SendStatementByEmail", model.SendStatementByEmail),
                    new SqlParameter("@CreditLimit", model.CreditLimit),
                    new SqlParameter("@CreditRule", model.CreditRule),
                    new SqlParameter("@CreditDaysPastDue", model.CreditDaysPastDue),
                    new SqlParameter("@StatementCycleId", model.StatementCycleId),
                    new SqlParameter("@StatementType", model.StatementType),
                    new SqlParameter("@SmallBalanceAllow", model.SmallBalanceAllow),
                    new SqlParameter("@SmallBalanceLimit", model.SmallBalanceLimit),
                    new SqlParameter("@FinChargeApply", model.FinChargeApply),
                    new SqlParameter("@FinChargeID", model.FinChargeID),
                    new SqlParameter("@CountryID", model.CountryID),
                    new SqlParameter("@OverLimitAmount", model.OverLimitAmount),
                    new SqlParameter("@PrepaymentAcctID", model.PrepaymentAcctID),
                    new SqlParameter("@PrepaymentSubID", model.PrepaymentSubID),
                    new SqlParameter("@ShipVia", model.ShipVia),
                    new SqlParameter("@ShipComplete", model.ShipComplete),
                    new SqlParameter("@ShipTermsID", model.ShipTermsID),
                    new SqlParameter("@SalesPersonID", model.SalesPersonID),
                    new SqlParameter("@DiscountLimit", model.DiscountLimit),
                    //new SqlParameter("@GroupMask", model.GroupMask),
                    //new SqlParameter("@tstamp", model.tstamp),
                    new SqlParameter("@PrintInvoices", model.PrintInvoices),
                    new SqlParameter("@MailInvoices", model.MailInvoices),
                    new SqlParameter("@PrintDunningLetters", model.PrintDunningLetters),
                    new SqlParameter("@MailDunningLetters", model.MailDunningLetters),
                    new SqlParameter("@PriceClassID", model.PriceClassID),
                    new SqlParameter("@DefaultLocationCDFromBranch", model.DefaultLocationCDFromBranch),
                    new SqlParameter("@LocaleName", model.LocaleName),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(string itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("CustomerClass_Delete", new SqlParameter[]
            {
                new SqlParameter(@"CustomerClassID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
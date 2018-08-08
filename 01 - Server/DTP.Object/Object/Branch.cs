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
    public class Branch : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int BranchID { get; set; }

        [DataMember]
        public string BranchCD { get; set; }

        [DataMember]
        public int BAccountID { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public int LedgerID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string CountryID { get; set; }

        [DataMember]
        public string PhoneMask { get; set; }

        [DataMember]
        public string LogoName { get; set; }

        [DataMember]
        public int AcctMapNbr { get; set; }

        [DataMember]
        public bool DeletedDatabaseRecord { get; set; }

        [DataMember]
        public bool AllowsRUTROT { get; set; }

        [DataMember]
        public decimal RUTROTDeductionPct { get; set; }

        [DataMember]
        public decimal RUTROTPersonalAllowanceLimit { get; set; }

        [DataMember]
        public decimal RUTDeductionPct { get; set; }

        [DataMember]
        public decimal RUTPersonalAllowanceLimit { get; set; }

        [DataMember]
        public decimal RUTExtraAllowanceLimit { get; set; }

        [DataMember]
        public decimal ROTDeductionPct { get; set; }

        [DataMember]
        public decimal ROTPersonalAllowanceLimit { get; set; }

        [DataMember]
        public decimal ROTExtraAllowanceLimit { get; set; }

        [DataMember]
        public string RUTROTCuryID { get; set; }

        [DataMember]
        public int RUTROTClaimNextRefNbr { get; set; }

        [DataMember]
        public string RUTROTOrgNbrValidRegEx { get; set; }

        [DataMember]
        public string TCC { get; set; }

        [DataMember]
        public bool ForeignEntity { get; set; }

        [DataMember]
        public bool CFSFiler { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string CTelNumber { get; set; }

        [DataMember]
        public string CEmail { get; set; }

        [DataMember]
        public string NameControl { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }



    }
    public class BranchCollection : BaseDBEntityCollection<Branch> { }
    public class BranchManager
    {
        private static Branch GetItemFromReader(IDataReader dataReader)
        {
            Branch objItem = new Branch();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.BranchID = SqlHelper.GetInt(dataReader, "BranchID");

            objItem.BranchCD = SqlHelper.GetString(dataReader, "BranchCD");

            objItem.BAccountID = SqlHelper.GetInt(dataReader, "BAccountID");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.LedgerID = SqlHelper.GetInt(dataReader, "LedgerID");

            objItem.RoleName = SqlHelper.GetString(dataReader, "RoleName");

            objItem.CountryID = SqlHelper.GetString(dataReader, "CountryID");

            objItem.PhoneMask = SqlHelper.GetString(dataReader, "PhoneMask");

            objItem.LogoName = SqlHelper.GetString(dataReader, "LogoName");

            objItem.AcctMapNbr = SqlHelper.GetInt(dataReader, "AcctMapNbr");

            objItem.DeletedDatabaseRecord = SqlHelper.GetBoolean(dataReader, "DeletedDatabaseRecord");

            objItem.AllowsRUTROT = SqlHelper.GetBoolean(dataReader, "AllowsRUTROT");

            objItem.RUTROTDeductionPct = SqlHelper.GetDecimal(dataReader, "RUTROTDeductionPct");

            objItem.RUTROTPersonalAllowanceLimit = SqlHelper.GetDecimal(dataReader, "RUTROTPersonalAllowanceLimit");

            objItem.RUTDeductionPct = SqlHelper.GetDecimal(dataReader, "RUTDeductionPct");

            objItem.RUTPersonalAllowanceLimit = SqlHelper.GetDecimal(dataReader, "RUTPersonalAllowanceLimit");

            objItem.RUTExtraAllowanceLimit = SqlHelper.GetDecimal(dataReader, "RUTExtraAllowanceLimit");

            objItem.ROTDeductionPct = SqlHelper.GetDecimal(dataReader, "ROTDeductionPct");

            objItem.ROTPersonalAllowanceLimit = SqlHelper.GetDecimal(dataReader, "ROTPersonalAllowanceLimit");

            objItem.ROTExtraAllowanceLimit = SqlHelper.GetDecimal(dataReader, "ROTExtraAllowanceLimit");

            objItem.RUTROTCuryID = SqlHelper.GetString(dataReader, "RUTROTCuryID");

            objItem.RUTROTClaimNextRefNbr = SqlHelper.GetInt(dataReader, "RUTROTClaimNextRefNbr");

            objItem.RUTROTOrgNbrValidRegEx = SqlHelper.GetString(dataReader, "RUTROTOrgNbrValidRegEx");

            objItem.TCC = SqlHelper.GetString(dataReader, "TCC");

            objItem.ForeignEntity = SqlHelper.GetBoolean(dataReader, "ForeignEntity");

            objItem.CFSFiler = SqlHelper.GetBoolean(dataReader, "CFSFiler");

            objItem.ContactName = SqlHelper.GetString(dataReader, "ContactName");

            objItem.CTelNumber = SqlHelper.GetString(dataReader, "CTelNumber");

            objItem.CEmail = SqlHelper.GetString(dataReader, "CEmail");

            objItem.NameControl = SqlHelper.GetString(dataReader, "NameControl");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Branch GetItemByID(int BranchID, int CompanyID)
        {
            Branch item = new Branch();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@BranchID", BranchID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Branch_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Branch AddItem(Branch model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Branch_Add", CreateSqlParameter(model)))
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
            return GetItemByID(result, model.CompanyID);

        }
        public static Branch UpdateItem(Branch model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Branch_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.BranchID, model.CompanyID);

        }
        public static BranchCollection GetAllItem(int CompanyID)
        {
            BranchCollection collection = new BranchCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("Branch_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Branch obj = new Branch();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static BranchCollection Search(SearchFilter SearchKey)
        {
            BranchCollection collection = new BranchCollection();
            using (var reader = SqlHelper.ExecuteReader("Branch_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Branch obj = new Branch();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static BranchCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            BranchCollection collection = new BranchCollection();
            Branch obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("Branch_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Branch model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@BranchID", model.BranchID),
                    new SqlParameter("@BranchCD", model.BranchCD),
                    new SqlParameter("@BAccountID", model.BAccountID),
                    new SqlParameter("@Active", model.Active),
                    new SqlParameter("@LedgerID", model.LedgerID),
                    new SqlParameter("@RoleName", model.RoleName),
                    new SqlParameter("@CountryID", model.CountryID),
                    new SqlParameter("@PhoneMask", model.PhoneMask),
                    new SqlParameter("@LogoName", model.LogoName),
                    new SqlParameter("@AcctMapNbr", model.AcctMapNbr),
                    new SqlParameter("@DeletedDatabaseRecord", model.DeletedDatabaseRecord),
                    new SqlParameter("@AllowsRUTROT", model.AllowsRUTROT),
                    new SqlParameter("@RUTROTDeductionPct", model.RUTROTDeductionPct),
                    new SqlParameter("@RUTROTPersonalAllowanceLimit", model.RUTROTPersonalAllowanceLimit),
                    new SqlParameter("@RUTDeductionPct", model.RUTDeductionPct),
                    new SqlParameter("@RUTPersonalAllowanceLimit", model.RUTPersonalAllowanceLimit),
                    new SqlParameter("@RUTExtraAllowanceLimit", model.RUTExtraAllowanceLimit),
                    new SqlParameter("@ROTDeductionPct", model.ROTDeductionPct),
                    new SqlParameter("@ROTPersonalAllowanceLimit", model.ROTPersonalAllowanceLimit),
                    new SqlParameter("@ROTExtraAllowanceLimit", model.ROTExtraAllowanceLimit),
                    new SqlParameter("@RUTROTCuryID", model.RUTROTCuryID),
                    new SqlParameter("@RUTROTClaimNextRefNbr", model.RUTROTClaimNextRefNbr),
                    new SqlParameter("@RUTROTOrgNbrValidRegEx", model.RUTROTOrgNbrValidRegEx),
                    new SqlParameter("@TCC", model.TCC),
                    new SqlParameter("@ForeignEntity", model.ForeignEntity),
                    new SqlParameter("@CFSFiler", model.CFSFiler),
                    new SqlParameter("@ContactName", model.ContactName),
                    new SqlParameter("@CTelNumber", model.CTelNumber),
                    new SqlParameter("@CEmail", model.CEmail),
                    new SqlParameter("@NameControl", model.NameControl),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("Branch_Delete", new SqlParameter[]
            {
                new SqlParameter(@"BranchID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
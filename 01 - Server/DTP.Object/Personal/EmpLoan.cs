using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace DTP.Object.Personal
{

    [DataContract]
    public class EmpLoan : BaseDBEntity
    {
        [DataMember]
        public string LoanCode { get; set; }

        [DataMember]
        public string EmpCode { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal BRate { get; set; }

        [DataMember]
        public int BMonth { get; set; }

        [DataMember]
        public decimal BYear { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string BankName { get; set; }

        [DataMember]
        public string LoanDesc { get; set; }

        [DataMember]
        public string CuryId { get; set; }
    }
    public class EmpLoanCollection : BaseDBEntityCollection<EmpLoan> { }
    public class EmpLoanManager
    {
        private static EmpLoan GetItemFromReader(IDataReader dataReader)
        {
            EmpLoan objItem = new EmpLoan();
            objItem.LoanCode = SqlHelper.GetString(dataReader, "LoanCode");

            objItem.EmpCode = SqlHelper.GetString(dataReader, "EmpCode");

            objItem.Amount = SqlHelper.GetDecimal(dataReader, "Amount");

            objItem.BRate = SqlHelper.GetDecimal(dataReader, "BRate");

            objItem.BMonth = SqlHelper.GetInt(dataReader, "BMonth");

            objItem.BYear = SqlHelper.GetDecimal(dataReader, "BYear");

            objItem.Balance = SqlHelper.GetDecimal(dataReader, "Balance");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            objItem.BankName = SqlHelper.GetString(dataReader, "BankName");

            objItem.LoanDesc = SqlHelper.GetString(dataReader, "LoanDesc");
            objItem.CuryId = SqlHelper.GetString(dataReader, "CuryId");

            return objItem;
        }
        public static EmpLoan GetItemByID(String loanCode)
        {
            EmpLoan item = new EmpLoan();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@LoanCode", loanCode);
            using (var reader = SqlHelper.ExecuteReader("tblEmpLoan_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static EmpLoan AddItem(EmpLoan model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpLoan_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EmpLoan UpdateItem(EmpLoan model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEmpLoan_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EmpLoanCollection GetAllItembyCreatedUser(string CreatedUser)
        {
            EmpLoanCollection collection = new EmpLoanCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEmpLoan_GetAllByUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    EmpLoan obj = new EmpLoan();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static EmpLoanCollection GetAllItem()
        {
            EmpLoanCollection collection = new EmpLoanCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEmpLoan_GetAll", null))
            {
                while (reader.Read())
                {
                    EmpLoan obj = new EmpLoan();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(EmpLoan model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@LoanCode", model.LoanCode),
					new SqlParameter("@EmpCode", model.EmpCode),
					new SqlParameter("@Amount", model.Amount),
					new SqlParameter("@BRate", model.BRate),
					new SqlParameter("@BMonth", model.BMonth),
					new SqlParameter("@BYear", model.BYear),
					new SqlParameter("@Balance", model.Balance),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
                    	new SqlParameter("@BankName", model.BankName),
					new SqlParameter("@LoanDesc", model.LoanDesc),
                    new SqlParameter("@CuryId", model.CuryId),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblEmpLoan_Delete", itemID);
        }
        #region add more method

        public static LoanCalCollection CalEmpLoan(EmpLoan objItem)
        {

            LoanCalCollection objCollection = new LoanCalCollection();
            LoanCal objHCal;

            if (objItem != null)
            {
                if (objItem.BRate != 0 && objItem.Amount != 0)
                {
                    decimal payment = 0;
                    decimal Principal = objItem.Amount / objItem.BMonth;
                    decimal Balance = objItem.Amount;
                    decimal Interest = 0;
                    decimal TotalInter = 0;
                    for (int i = 0; i < objItem.BMonth; i++)
                    {
                        objHCal = new LoanCal();
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
        #endregion
    }

    [DataContract]
    public class LoanCal: BaseDBEntity
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
    public class LoanCalCollection : BaseDBEntityCollection<LoanCal> { }
}

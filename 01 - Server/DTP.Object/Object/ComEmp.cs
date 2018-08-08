using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object
{
    [DataContract]
    public class ComEmp : BaseDBEntity
    {
        [DataMember]
        public string EmpCode { get; set; }

        [DataMember]
        public string EmpName { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public string DepartmentCode { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public decimal SalaryContractAmt { get; set; }

        [DataMember]
        public DateTime ContractDate { get; set; }

        [DataMember]
        public bool IsPermanent { get; set; }

        [DataMember]
        public string CuryCode { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        public string UpdatedUser { get; set; }
    }
    public class ComEmpCollection : BaseDBEntityCollection<ComEmp> { }
    public class ComEmpManager
    {
        private static ComEmp GetItemFromReader(IDataReader dataReader)
        {
            ComEmp objItem = new ComEmp();
            objItem.EmpCode = SqlHelper.GetString(dataReader, "EmpCode");

            objItem.EmpName = SqlHelper.GetString(dataReader, "EmpName");

            objItem.GroupCode = SqlHelper.GetString(dataReader, "GroupCode");

            objItem.DepartmentCode = SqlHelper.GetString(dataReader, "DepartmentCode");

            objItem.Email = SqlHelper.GetString(dataReader, "Email");

            objItem.Position = SqlHelper.GetString(dataReader, "Position");

            objItem.SalaryContractAmt = SqlHelper.GetDecimal(dataReader, "SalaryContractAmt");

            objItem.ContractDate = SqlHelper.GetDateTime(dataReader, "ContractDate");

            objItem.IsPermanent = SqlHelper.GetBoolean(dataReader, "IsPermanent");

            objItem.CuryCode = SqlHelper.GetString(dataReader, "CuryCode");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.UpdatedDate = SqlHelper.GetDateTime(dataReader, "UpdatedDate");

            objItem.UpdatedUser = SqlHelper.GetString(dataReader, "UpdatedUser");
            return objItem;
        }
        public static ComEmp GetItemByID(String empCode)
        {
            ComEmp item = new ComEmp();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EmpCode", empCode);
            using (var reader = SqlHelper.ExecuteReader("tblComEmp_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ComEmp AddItem(ComEmp model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComEmp_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComEmp UpdateItem(ComEmp model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComEmp_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComEmpCollection GetAllItem()
        {
            ComEmpCollection collection = new ComEmpCollection();
            using (var reader = SqlHelper.ExecuteReader("tblComEmp_GetAll", null))
            {
                while (reader.Read())
                {
                    ComEmp obj = new ComEmp();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ComEmp model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@EmpCode", model.EmpCode),
                new SqlParameter("@EmpName", model.EmpName),
					new SqlParameter("@GroupCode", model.GroupCode),
					new SqlParameter("@DepartmentCode", model.DepartmentCode),
					new SqlParameter("@Email", model.Email),
					new SqlParameter("@Position", model.Position),
					new SqlParameter("@SalaryContractAmt", model.SalaryContractAmt),
					new SqlParameter("@ContractDate", model.ContractDate),
					new SqlParameter("@IsPermanent", model.IsPermanent),
					new SqlParameter("@CuryCode", model.CuryCode),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@UpdatedDate", model.UpdatedDate),
					new SqlParameter("@UpdatedUser", model.UpdatedUser),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblComEmp_Delete", itemID);
        }

        public static ComEmpCollection GetAllItemByGroupCode(string GroupCode)
        {
            ComEmpCollection collection = new ComEmpCollection();
            using (var reader = SqlHelper.ExecuteReader("tblComEmp_GetByGroupCode", new SqlParameter("@GroupCode", GroupCode)))
            {
                while (reader.Read())
                {
                    ComEmp obj = new ComEmp();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
    }
}

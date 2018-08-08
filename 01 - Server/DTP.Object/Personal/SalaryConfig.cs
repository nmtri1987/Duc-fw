using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object.Personal
{
    [DataContract]
    public class SalaryConfig : BaseDBEntity
    {
        [DataMember]
        public string SalConfigId { get; set; }

        [DataMember]
        public decimal MinSalAmount { get; set; }

        [DataMember]
        public decimal BHXH { get; set; }

        [DataMember]
        public decimal BHYT { get; set; }

        [DataMember]
        public decimal BHTN { get; set; }

        [DataMember]
        public decimal PerTaxReduce { get; set; }

        [DataMember]
        public decimal RelationTaxReduce { get; set; }

        [DataMember]
        public DateTime DateApply { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }
    }
    public class SalaryConfigCollection : BaseDBEntityCollection<SalaryConfig> { }
    public class SalaryConfigManager
    {
        private static SalaryConfig GetItemFromReader(IDataReader dataReader)
        {
            SalaryConfig objItem = new SalaryConfig();
            objItem.SalConfigId = SqlHelper.GetString(dataReader, "SalConfigId");

            objItem.MinSalAmount = SqlHelper.GetDecimal(dataReader, "MinSalAmount");

            objItem.BHXH = SqlHelper.GetDecimal(dataReader, "BHXH");

            objItem.BHYT = SqlHelper.GetDecimal(dataReader, "BHYT");

            objItem.BHTN = SqlHelper.GetDecimal(dataReader, "BHTN");

            objItem.PerTaxReduce = SqlHelper.GetDecimal(dataReader, "PerTaxReduce");

            objItem.RelationTaxReduce = SqlHelper.GetDecimal(dataReader, "RelationTaxReduce");

            objItem.DateApply = SqlHelper.GetDateTime(dataReader, "DateApply");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");
            return objItem;
        }
        public static SalaryConfig GetItemByID(String salConfigId)
        {
            SalaryConfig item = new SalaryConfig();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SalConfigId", salConfigId);
            using (var reader = SqlHelper.ExecuteReader("tblSalaryConfig_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static SalaryConfig AddItem(SalaryConfig model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblSalaryConfig_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static SalaryConfig UpdateItem(SalaryConfig model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblSalaryConfig_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static SalaryConfigCollection GetAllItem()
        {
            SalaryConfigCollection collection = new SalaryConfigCollection();
            using (var reader = SqlHelper.ExecuteReader("tblSalaryConfig_GetAll", null))
            {
                while (reader.Read())
                {
                    SalaryConfig obj = new SalaryConfig();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(SalaryConfig model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@SalConfigId", model.SalConfigId),
					new SqlParameter("@MinSalAmount", model.MinSalAmount),
					new SqlParameter("@BHXH", model.BHXH),
					new SqlParameter("@BHYT", model.BHYT),
					new SqlParameter("@BHTN", model.BHTN),
					new SqlParameter("@PerTaxReduce", model.PerTaxReduce),
					new SqlParameter("@RelationTaxReduce", model.RelationTaxReduce),
					new SqlParameter("@DateApply", model.DateApply),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblSalaryConfig_Delete", itemID);
        }
    }
}

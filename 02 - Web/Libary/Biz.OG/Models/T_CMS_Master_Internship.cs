using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
//using Server.DAC;
//using Server.Helpers;
using Biz.OG.Services;
namespace Biz.OG.Models
{
    //[DataContract]
    public class T_CMS_Master_Internship : BaseEntity
    {

        
        public int ID { get; set; }


        public int EmployeeCode { get; set; }


        public string InternNo { get; set; }
        [ColumnAttribute(Hide = true)]
        public int SalutationID { get; set; }
        private string _SalutationCD = "";
        public string SalutationCD
        {
            get
            {
                if (string.IsNullOrEmpty(_SalutationCD))
                {
                    return T_CMS_Master_SalutationManager.GetById(SalutationID).SalutationCD;
                }
                else
                {
                    return _SalutationCD;
                }

            }
            set
            {
                _SalutationCD = value;
            }
        }
        public string FullName { get
            {
                return FirstName_EN + " " + MiddleName_EN + " " + LastName_EN;
            } }
        private string _DepartCD = "";
        public string DepartCD
        {
            get
            {
                if (string.IsNullOrEmpty(_DepartCD))
                {
                    return T_COm_Master_OrgManager.GetById(DeptID).OrgName;
                }
                else
                {
                    return _DepartCD;
                }
            }
            set { _DepartCD = value; }
        }
        private string _ProbationsPeriodCD = "";
        public string ProbationsPeriodCD
        {
            get
            {
                if (string.IsNullOrEmpty(_ProbationsPeriodCD))
                {
                    return T_CMS_Static_PeriodOfProbationManager.GetById(PeriodofInternship).CD;
                }
                else
                {
                    return _ProbationsPeriodCD;
                }

            }
            set
            {
                _ProbationsPeriodCD = value;
            }
        }
        [ColumnAttribute(Hide = true)]
        public string FirstName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string MiddleName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string LastName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string FirstName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string MiddleName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string LastName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public int StatusID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int ApproverLevel { get; set; }

        [ColumnAttribute(Hide = true)]
        public int InternLevel { get; set; }

        [ColumnAttribute(Hide = true)]
        public string IDCardNo { get; set; }

        [ColumnAttribute(Hide = true)]
        public string PassportNo { get; set; }

        public int DeptID { get; set; }


        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime Joiningdate { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime Enddate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int PeriodofInternship { get; set; }

        public int HousingAllowance { get; set; }

        [ColumnAttribute(Hide = true)]
        public int UniversityID { get; set; }

        private string _UniversityCD = "";
        public string UniversityCD
        {
            get
            {
                if (UniversityID!=0)
                {
                    T_COM_Master_University objitem = T_COM_Master_UniversityManager.GetById(UniversityID);
                    if (objitem.UniversityID != 0)
                    {
                        return objitem.UniversityName_EN.Trim();
                    }
                    
                }
                
                return _UniversityCD;

            }
            set
            {
                _UniversityCD = value;
            }
        }


        [ColumnAttribute(Hide = true)]
        public string Dept { get; set; }


        public int LocationID { get; set; }


        public int WorkHours { get; set; }

        private string _WorkHoursCD = "";
        public string WorkHoursCD
        {
            get
            {
                if (string.IsNullOrEmpty(_WorkHoursCD))
                {
                    return T_CMS_InterfaceLacviet_WorkingHoursManager.GetById(WorkHours).WorkingHours.ToString();
                }
                else
                {
                    return _WorkHoursCD;
                }

            }
            set
            {
                _WorkHoursCD = value;
            }
        }

        [ColumnAttribute(Hide = true)]
        public int EmpTypeID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int EmpSubTypeID { get; set; }


        public string Remarks { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool IsActive { get; set; }

        [ColumnAttribute(Hide = true)]
        public int CreatedBy { get; set; }

        [ColumnAttribute(Hide = true)]
        public DateTime CreatedDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int ModifiedBy { get; set; }

        
        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [ColumnAttribute(Hide = true)]
        public DateTime? ModifiedDate { get; set; }

        public decimal Grossoffer { get; set; }


    }
    public class T_CMS_Master_InternshipCollection : BaseEntityCollection<T_CMS_Master_Internship> { }
}
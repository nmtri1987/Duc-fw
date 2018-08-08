using System;
using System.ComponentModel.DataAnnotations;
namespace Biz.TMS
{
    public class T_TMS_EmployeeTimesheetWeeklyDetails : BaseEntity
    {
        [LocalizedDisplayNameAttribute("CompanyID")]

        public int CompanyID { get; set; }

        [LocalizedDisplayNameAttribute("ID")]

        public Guid ID { get; set; }

        [LocalizedDisplayNameAttribute("EntityID")]

        public int EntityID { get; set; }

        [LocalizedDisplayNameAttribute("WorkingTimeGroupID")]

        public int WorkingTimeGroupID { get; set; }

        [LocalizedDisplayNameAttribute("WorkDate")]

        public DateTime WorkDate { get; set; }

        [LocalizedDisplayNameAttribute("DateName")]

        public string DateName { get; set; }

        [LocalizedDisplayNameAttribute("WeekNo")]

        public int WeekNo { get; set; }

        [LocalizedDisplayNameAttribute("YearNo")]

        public int YearNo { get; set; }

        [LocalizedDisplayNameAttribute("FinPeriod")]

        public string FinPeriod { get; set; }

        [LocalizedDisplayNameAttribute("EmployeeCode")]

        public int EmployeeCode { get; set; }

        [LocalizedDisplayNameAttribute("EmployeeNo")]

        public string EmployeeNo { get; set; }

        [LocalizedDisplayNameAttribute("DowID")]

        public int DowID { get; set; }

        [LocalizedDisplayNameAttribute("In_Out")]

        public string In_Out { get; set; }

        [LocalizedDisplayNameAttribute("StandardWorkedHour")]

        public float StandardWorkedHour { get; set; }

        [LocalizedDisplayNameAttribute("WorkedHour")]

        public float WorkedHour { get; set; }

        [LocalizedDisplayNameAttribute("ND")]

        public string ND { get; set; }

        [LocalizedDisplayNameAttribute("OT")]

        public string OT { get; set; }

        [LocalizedDisplayNameAttribute("Leave")]

        public string Leave { get; set; }

        [LocalizedDisplayNameAttribute("LC_UnNoReg")]

        public string LC_UnNoReg { get; set; }

        [LocalizedDisplayNameAttribute("StatusID")]

        public int StatusID { get; set; }

        [LocalizedDisplayNameAttribute("IsRelease")]

        public bool IsRelease { get; set; }

        [LocalizedDisplayNameAttribute("CreatedDate")]

        public DateTime CreatedDate { get; set; }

        [LocalizedDisplayNameAttribute("CreateUser")]

        public string CreateUser { get; set; }

        [LocalizedDisplayNameAttribute("ModifiedDate")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime ModifiedDate { get; set; }

        [LocalizedDisplayNameAttribute("ModifiedUser")]

        public string ModifiedUser { get; set; }


    }
    public class T_TMS_EmployeeTimesheetWeeklyDetailsCollection : BaseEntityCollection<T_TMS_EmployeeTimesheetWeeklyDetails> { }
    public class EMPWeelFilter
    {
        public int Number { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public DateTime EndDateWeek { get; set; }
    }
    public class EmpWeekSummary
    {
        public decimal TotalWorkHour { get; set; }
        public decimal WorkHour { get; set; }
        public string WorkHourString
        {
            get
            {
                string Data = "";
                if (WorkHour != 0)
                {
                    Data += "<div class='WHClass'>Hour: <span>" + WorkHour + "</span></div>";
                }
                return Data;

            }
        }
        public decimal LackingHour { get; set; }
        public string LackingHourString
        {
            get
            {
                string Data = "";
                if (WorkHour != LackingHour)
                {
                    Data += "<div class='WHClass'>Hour: <span>" + LackingHour + "</span></div>";
                }
                return Data;

            }
        }
        public string IntoWeek { get
            {
                decimal total = TotalWorkHour - WorkHour - LeaveSummary - UnNoregSummary - LackingHour;
                total = total < 0 ? 0 : total;
                return "<div class='TMSRemain'>Hour remains: <span>" + String.Format("{0:0.##}", total)+ "</span> </div>";
            }
        }
        public decimal OTSummary { get; set; }
        public decimal LeaveSummary { get; set; }
        public decimal UnNoregSummary { get; set; }
        public TMSDataCollection OT { get; set; }
        public string OTString
        {
            get
            {
                string Data = "";
                if (OT != null)
                {
                    foreach (TMSData objData in OT)
                    {
                        Data += "<div class='OTClass'>" + objData.Name + ":<span>" + objData.summary + "</span></div>";
                    }
                }
                return Data;

            }
        }
        public TMSDataCollection Leave { get; set; }
        public string LeaveString
        {
            get
            {
                string Data = "";
                if (Leave != null)
                {
                    foreach (TMSData objData in Leave)
                    {
                        Data += "<div class='LeaveClass'>" + objData.Name + ":<span>" + objData.summary + "</span></div>";
                    }
                }
                return Data;
            }
        }
        public TMSDataCollection UnoReg { get; set; }
        public string UnoRegString { get
            {
                string Data = "";
                if (UnoReg != null)
                {
                    foreach (TMSData objData in UnoReg)
                    {
                        Data += "<div class='UnoRegClass'>" + objData.Name+ ": <span>" + objData.summary + "</span></div>";
                    }
                }
                return Data;
            }
        }

    }

    public class TMSData:BaseEntity
    {
        public string Name { get; set; }
        public decimal summary { get; set; }
    }
    public class TMSDataCollection : BaseEntityCollection<TMSData> { }
    public class EmpFilter
    {
        public int Dow_ID { get; set; }
        public int EmployeeCode { get; set; }
    }
}

//namespace Biz.TMS.Controllers
//{
//    public class T_TMS_EmployeeTimesheetWeeklyDetailsController : BaseController<T_TMS_EmployeeTimesheetWeeklyDetails> {}
//}
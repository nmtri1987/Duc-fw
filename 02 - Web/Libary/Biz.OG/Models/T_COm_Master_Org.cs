using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.OG.Services;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.OG.Models
{
    //[DataContract]
    public class T_COm_Master_Org : BaseEntity
    {


        public int OrgId { get; set; }


        public string OrgName { get; set; }


        public string Description { get; set; }


        public int Entity_Id { get; set; }


        public int UnitType_Id { get; set; }


        public int Holder_Id { get; set; }


        public int ParentOrg_Id { get; set; }


        public bool Plant_Type { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }
        public T_COM_Master_Entity OrgEntity
        {
            get
            {
                return T_COM_Master_EntityManager.GetById(Entity_Id);
            }
        }

    }
    public class T_COm_Master_OrgCollection : BaseEntityCollection<T_COm_Master_Org> { }
}
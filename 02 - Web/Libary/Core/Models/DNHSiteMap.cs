using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using Biz.Core.Services;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
//using Server.DAC;
//using Server.Helpers;
using Newtonsoft.Json;
namespace Biz.Core.Models
{
    [Serializable]
    [JsonObject]
    public class DNHSiteMap : BaseEntity
    {
        [LocalizedDisplayName("CompanyID")]
        [ColumnAttribute(Hide = true)]
        public int CompanyID { get; set; }

        [ColumnAttribute(DataType = "key", ActionLink = "Update")]
        public Guid NodeID { get; set; }


        public int Position { get; set; }


        public string Title { get; set; }


        public string Description { get; set; }


        public string Url { get; set; }


        public bool Expanded { get; set; }


        public bool IsFolder { get; set; }


        public string ScreenID { get; set; }


        public Guid ParentID { get; set; }


        public string CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }


        public string IconImage { get; set; }

        public string RoleName { get; set; }
        public int Access { get; set; }
        int lvl = 0;
        int MaxValue = 0;
        DNHSiteMapCollection myTree;
        public DNHSiteMapCollection GetNodeByLevel(DNHUsers objUser, int Max)
        {
            myTree = new DNHSiteMapCollection();
            MaxValue = Max;
            foreach (DNHSiteMap node in objUser.UserSiteMaps)
            {
                if (node.ParentID == null || node.ParentID == Guid.Empty)
                {
                    DigNode(node, objUser.UserName, objUser.CompanyID, 0);
                }
            }
            return myTree;
        }
        private void DigNode(DNHSiteMap node, string UserName, int CompanyID, int level)
        {
            DNHSiteMapCollection child = DNHSiteMapManager.GetAllByUser(UserName, CompanyID, node.NodeID);
            if (level > MaxValue)
            {
                //Max = lvl;

                myTree.AddRange(child);

            }
            else
            {
                foreach (DNHSiteMap b in child)
                {
                    DigNode(b, UserName, CompanyID, level + 1);
                }
            }


        }

    }
    [Serializable()]
    public class DNHSiteMapCollection : BaseEntityCollection<DNHSiteMap> { }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Runtime.Serialization;
using System.Configuration;
using Biz.Core.Models;
using Biz.Core.Services;

namespace Biz.Core.Security
{
   public static class CustomerAuthorize
    {
        public static string GetNTId()
        {
            //string sCurrentUserName = "APAC\\RAT2HC";// HttpContext.Current.User.Identity.Name.ToString();
            string sCurrentUserName = HttpContext.Current.User.Identity.Name.ToString();

            if (sCurrentUserName != null && sCurrentUserName.Trim().Length > 0)
            {
                string[] sSplittedName = sCurrentUserName.Split('\\');
                string sDomain = sSplittedName[0].ToString();
                return sSplittedName[1].ToString().Trim().ToUpper();
            }
            else
            {
                throw new Exception("Unable to get NT-Id. Please check the contact administrator.");
            }
        }
        /// <summary>
        /// check Employee has Domain account or not 
        /// Set Session for Employee
        /// </summary>
        /// <param name="NTId"></param>
        /// <returns></returns>
        public static bool setRBVHUser(string NTId)
        {
            bool Auth = false;
            DNHUsers objUser = RBVHUser();
            if (objUser != null)
            {
                Auth = true;
            }
            else
            {
                Auth = true;
            }
            return Auth;
        }
        public static void ChangeUser(string DomainID)
        {
            string UserName = DomainID;
            //if the system don't have this domain --> Create the new User for him 
            DNHUsers objUser  = DNHUsersManager.GetByID(UserName, 1);
            if (objUser != null)
            {
                RBVHEmployee objEmp = RBVHEmployeeManager.GetByDomainId(UserName);
                if (objEmp.EmployeeCode != 0)
                {
                    objUser.EmployeeCode = objEmp.EmployeeCode;
                    objUser.EntityID = objEmp.EntityID;
                    objUser.CompanyID = 1;
                }
                objUser.UserSiteMaps = CommonHelper.ReadWriteSiteMap(objUser);
                Biz.Core.Helpers.SessionHelper.SetRBVH(objUser);
            }
        }
        public static DNHUsers RBVHUser()
        {
            DNHUsers objUser = null;
            if (HttpContext.Current.Session[SystemConfig.loginKey] == null)
            {
                string UserName = GetNTId();
                //if the system don't have this domain --> Create the new User for him 
                objUser = DNHUsersManager.GetByID(UserName, 1); ///Default company=1 for Bosch Company
                if (objUser == null)
                {
                    RBVHEmployee objEmp = RBVHEmployeeManager.GetByDomainId(UserName);
                    if (objEmp.EmployeeCode != 0)
                    {
                        objUser = new DNHUsers()
                        {
                            EmployeeCode = objEmp.EmployeeCode,
                            EmployeeName = objEmp.FirstName_EN + " " + objEmp.LastName_EN,
                            EmployeeNumber = objEmp.EmployeeNo,
                            JoinedDate = objEmp.JoinedDate,
                            //DeprtmanetId = objEmp.EmpContract.DeptID,
                            EntityID = objEmp.EntityID,
                            //  NTId = objEmp.DomainId,
                            DomainID = objEmp.DomainId,
                            UserName = objEmp.DomainId,
                            CompanyID = 1,
                            Email = objEmp.Email,
                            PhoneNumber = objEmp.HandPhone,
                            Application = "APAC",
                            CreatedDate = SystemConfig.CurrentDate,
                            EmailConfirmed = true,
                            CreatedUser = "System"
                            ///think about to add Entity into this one
                        };
                        DNHUsersManager.Add(objUser);
                        //get UserSiteMapp Permission 
                        //   DNHSiteMapCollection objSMaps = CommonHelper.ReadWriteSiteMap(objUser);// new DNHSiteMapCollection(); //DNHSiteMapManager.GetAllByUser(objUser.DomainID, objUser.CompanyID, null);

                        ////save file 
                        //string FileName = IOHelper.GetDirectory(CommonHelper.myConfig.UserDataFolder + objUser.CompanyID + "\\SiteMap\\" + objUser.UserName) + "\\sitemap.bin";
                        //if (IOHelper.HasFile(FileName))
                        //{
                        //    //get form the file
                        //    objSMaps = IOHelper.ReadFromBinaryFile<DNHSiteMapCollection>(FileName);
                        //}
                        //else
                        //{
                        //    objSMaps = DNHSiteMapManager.GetAllByUser(objUser.DomainID, objUser.CompanyID, null);
                        //    //var nopConfig = 
                        //    if (objSMaps.Count > 0)
                        //    {

                        //        IOHelper.WriteToBinaryFile<IList<DNHSiteMap>>(FileName, objSMaps);
                        //    }
                        //}

                    }
                
                    
                }
                else
                {
                    RBVHEmployee objEmp = RBVHEmployeeManager.GetByDomainId(UserName);
                    if (objEmp.EmployeeCode != 0)
                    {
                        objUser.EmployeeCode = objEmp.EmployeeCode;
                        objUser.EntityID = objEmp.EntityID;
                        objUser.CompanyID = 1;
                    }
                    objUser.UserSiteMaps = CommonHelper.ReadWriteSiteMap(objUser);
                   // objUser.EmployeeCode = 2297; //just use for test
                    // Contract objContract = ContractManager.GetById(objEmp.EmployeeCode);
                    //add UserSession
                    Biz.Core.Helpers.SessionHelper.SetRBVH(objUser);

                }
                return objUser;

            }
            else
            {
                return HttpContext.Current.Session[SystemConfig.loginKey] as DNHUsers;



            }
        }
        public static void ResetUser()
        {
            DNHUsers objUser = null;
            RBVHEmployee objEmp = RBVHEmployeeManager.GetByDomainId(GetNTId());
            if (objEmp.EmployeeCode != 0)
            {
                objUser = new DNHUsers()
                {
                    EmployeeCode = objEmp.EmployeeCode,
                    EmployeeName = objEmp.FirstName_EN + " " + objEmp.LastName_EN,
                    EmployeeNumber = objEmp.EmployeeNo,
                    JoinedDate = objEmp.JoinedDate,
                    //DeprtmanetId = objEmp.EmpContract.DeptID,
                    EntityID = SystemConfig.DefaultEntity,
                    //  NTId = objEmp.DomainId,
                    DomainID = objEmp.DomainId,
                    UserName = objEmp.DomainId,
                    CompanyID = 1, ///think about to add Entity into this one
                };
                objUser.UserSiteMaps = CommonHelper.ReadWriteSiteMap(objUser);
                // Contract objContract = ContractManager.GetById(objEmp.EmployeeCode);
                //add UserSession
                Biz.Core.Helpers.SessionHelper.SetRBVH(objUser);
                //Auth = true;
            }
        }
        public static bool CheckLogin() {
            if (CurrentUser == null)
            {
                return setRBVHUser(GetNTId());
            }
            return true;
        }
        public static bool CheckSiteMapPermission(DNHUsers CurrentUser, string controllerName, string actionName)
        {
            DNHSiteMapCollection result = CurrentUser.UserSiteMaps;
            bool isAllow = false;
            DNHSitemapAction objAction = null;
            DNHSiteMap objItem;
            //foreach (DNHSiteMap objItem in result)
            if (controllerName.ToLower() != "home")
            {
                for (int i = 0; i < result.Count; i++)

                {
                    objItem = result[i];
                    if (objItem.Url.Trim().ToLower() == controllerName.ToLower())
                    {
                        isAllow = true;
                        // check user Action 
                        //objAction = CommonHelper.CheckActionPermission(objItem, actionName);
                        //if (objAction != null)
                        //{
                        //    isAllow = objAction.Allow;
                        //    if (objItem.Access != objAction.ID)
                        //    {
                        //        objItem.Access = objAction.ID;
                        //        result[i] = objItem;
                        //        CurrentUser.UserSiteMaps = result;
                        //    }

                        //}
                        //isAllow = true;
                        break;
                    }
                };
            }else
            {
                isAllow = true;
            }
            if (!isAllow && CurrentUser.IsAdmin)
            {
                isAllow = true;
            }
            return isAllow;
        }
        //public static AuthenticatedUser objUser
        //{
        //    get {
        //        string b = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //        AuthenticatedUser user = new AuthenticatedUser()
        //        {
        //            CompanyID = 1,
        //            UserName=b,

        //        };


        //         //HttpContext.Current.Session[SystemConfig.loginKey] as AuthenticatedUser;
        //        return user;
        //    }

        //}

        public static DNHUsers CurrentUser
        {
            get
            {
                return CustomerAuthorize.RBVHUser();
                //return objUser;
            }
        }
    }
}

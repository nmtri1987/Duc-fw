using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.ActiveDirectory;
using System.Collections.Generic;
using System.DirectoryServices;
namespace DTP.Core.Directory
{
    public static class DomainManager
    {
        static DomainManager()
        {
            Domain domain = null;
            DomainController domainController = null;
            try
            {
                domain = Domain.GetCurrentDomain();
                DomainName = domain.Name;
                domainController = domain.PdcRoleOwner;
                DomainControllerName = domainController.Name.Split('.')[0];
                ComputerName = Environment.MachineName;
            }
            finally
            {
                if (domain != null)
                    domain.Dispose();
                if (domainController != null)
                    domainController.Dispose();
            }
        }

        public static string DomainControllerName { get; private set; }

        public static string ComputerName { get; private set; }

        public static string DomainName { get; private set; }

        public static string DomainPath
        {
            get
            {
                bool bFirst = true;
                StringBuilder sbReturn = new StringBuilder(200);
                string[] strlstDc = DomainName.Split('.');
                foreach (string strDc in strlstDc)
                {
                    if (bFirst)
                    {
                        sbReturn.Append("DC=");
                        bFirst = false;
                    }
                    else
                        sbReturn.Append(",DC=");

                    sbReturn.Append(strDc);
                }
                return sbReturn.ToString();
            }
        }

        public static string RootPath
        {
            get
            {
                return string.Format("LDAP://{0}/{1}", DomainName, DomainPath);
            }
        }
    }

}


 
namespace ActiveDirectory
{
    /// <summary>
    /// Active Directory User.
    /// </summary>
    public class ADUser
    {
        #region constants

        /// <summary>
        /// Property name of sAM account name.
        /// </summary>
        public const string SamAccountNameProperty = "sAMAccountName";

        /// <summary>
        /// Property name of canonical name.
        /// </summary>
        public const string CanonicalNameProperty = "CN";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the canonical name of the user.
        /// </summary>
        public string CN { get; set; }

        /// <summary>
        /// Gets or sets the sAM account name
        /// </summary>
        public string SamAcountName { get; set; }

        #endregion

        /// <summary>
        /// Gets all users of a given domain.
        /// </summary>
        /// <param name="domain">Domain to query. Should be given in the form ldap://domain.com/ </param>
        /// <returns>A list of users.</returns>
        public static List<ADUser> GetUsers(string domain)
        {
            List<ADUser> users = new List<ADUser>();

            using (DirectoryEntry searchRoot = new DirectoryEntry(domain))
            using (DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot))
            {
                // Set the filter
                directorySearcher.Filter = "(&(objectCategory=person)(objectClass=user))";

                // Set the properties to load.
                directorySearcher.PropertiesToLoad.Add(CanonicalNameProperty);
                directorySearcher.PropertiesToLoad.Add(SamAccountNameProperty);

                using (SearchResultCollection searchResultCollection = directorySearcher.FindAll())
                {
                    foreach (SearchResult searchResult in searchResultCollection)
                    {
                        // Create new ADUser instance
                        var user = new ADUser();

                        // Set CN if available.
                        if (searchResult.Properties[CanonicalNameProperty].Count > 0)
                            user.CN = searchResult.Properties[CanonicalNameProperty][0].ToString();

                        // Set sAMAccountName if available
                        if (searchResult.Properties[SamAccountNameProperty].Count > 0)
                            user.SamAcountName = searchResult.Properties[SamAccountNameProperty][0].ToString();

                        // Add user to users list.
                        users.Add(user);
                    }
                }
            }

            // Return all found users.
            return users;
        }
    }
}

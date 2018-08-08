using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.Core.Messages;
using Biz.TMS.Models;
namespace Biz.TMS.Messages
{
    public class MessageTokenProvider: DNHBase
    {
        #region Allowed tokens

        private Dictionary<string, IEnumerable<string>> _allowedTokens;
        /// <summary>
        /// Get all available tokens by token groups
        /// </summary>
        protected Dictionary<string, IEnumerable<string>> AllowedTokens
        {
            get
            {
                if (_allowedTokens != null)
                    return _allowedTokens;

                _allowedTokens = new Dictionary<string, IEnumerable<string>>();

                //5 days reminder 
                _allowedTokens.Add(TokenGroupNames.FiveDaysReminder, new[]
                {
                    "%PDM.Employee(s)%",
                    "%PDM.Name%",
                });

                return _allowedTokens;
            }
        }

        #endregion
        public virtual void Add5DaysNotWorking(IList<Token> tokens, EmpReminderModel REmp)
        {
            tokens.Add(new Token("PM.Employee(s)", EmployeeListToHtmlTable(REmp.EmployeeList)));
            tokens.Add(new Token("PM.Name", REmp.ToName));
            //tokens.Add(new Token("Order.Product(s)", ProductListToHtmlTable(order, languageId, vendorId), true));

            ////TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            //tokens.Add(new Token("Order.OrderURLForCustomer", string.Format("{0}orderdetails/{1}", GetStoreUrl(order.StoreId), order.Id), true));

            ////event notification
            //_eventPublisher.EntityTokensAdded(order, tokens);
        }
        protected virtual string EmployeeListToHtmlTable(REmployeeCollections ObjEmpList)
        {
            string result;

            // var language = _languageService.GetLanguageById(languageId);

            var sb = new StringBuilder();
            sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

            //#region Products
            sb.AppendLine("<tr style=\"background-color:#1c84c6 !important;text-align:center;\">");
            sb.AppendLine(string.Format("<th>{0}</th>", L("S.No")));
            sb.AppendLine(string.Format("<th>{0}</th>", L("RMD.EmployeeNo")));
            sb.AppendLine(string.Format("<th>{0}</th>", L("RMD.AssociateName")));
            sb.AppendLine(string.Format("<th>{0}</th>", L("RMD.GroupName")));
            sb.AppendLine(string.Format("<th>{0}</th>", L("RMD.ReportDate")));
            sb.AppendLine(string.Format("<th>{0}</th>", L("RMD.DirectManagerName")));
            sb.AppendLine("</tr>");
            
            REmployee objEmp;
            for (int i=0;i<ObjEmpList.Count;i++)
            {
                sb.AppendLine("<tr>");
                objEmp = ObjEmpList[i];
                sb.AppendLine(string.Format("<td>{0}</td>", i+1));
                sb.AppendLine(string.Format("<td>{0}</td>", objEmp.EmployeeNo));
                sb.AppendLine(string.Format("<td>{0}</td>", objEmp.EmployeeName_EN));
                sb.AppendLine(string.Format("<td>{0}</td>", objEmp.GroupName));
                sb.AppendLine(string.Format("<td>{0}</td>", objEmp.RangeDate));
                sb.AppendLine(string.Format("<td>{0}</td>", objEmp.DirectManager));
                sb.AppendLine("</tr>");
            }
            
            sb.AppendLine("</table>");
            result = sb.ToString();
            return result;
        }
    }
}

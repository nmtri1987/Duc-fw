//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//    public class SearchFilter
//    {
//        public string Keyword { get; set; }
//        public string OrderBy { get; set; }
//        public string OrderDirection { get; set; }
//        public int Page { get; set; }
//        public int PageSize { get; set; }
//    }
//    public static class SearchFilterManager
//{
//        public static SqlParameter[] SqlSearchParam(SearchFilter value)
//        {
//            var pars = new SqlParameter[]
//                {
//                new SqlParameter("@Keyword",value.Keyword),
//                    new SqlParameter("@OrderBy", value.OrderBy),
//                    new SqlParameter("@OrderDirection", value.OrderDirection),
//                    new SqlParameter("@Page", value.Page),
//                    new SqlParameter("@PageSize",value.PageSize)

//                };
//            return pars;
//        }
//    }


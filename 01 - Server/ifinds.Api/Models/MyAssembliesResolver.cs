//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http.Dispatcher;
// using System.Web.Http;
//using System.Reflection;
//using System.IO;
//    public interface AssembliesResolver
//    {
//        ICollection<Assembly> GetAssemblies();
//    }

////http://www.strathweb.com/2012/06/using-controllers-from-an-external-assembly-in-asp-net-web-api/
//public class CustomAssemblyResolver : IAssembliesResolver
//{
//    public ICollection<Assembly> GetAssemblies()
//    {
//        List<Assembly> baseAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
//        var controllersAssembly = Assembly.LoadFrom(@"D:\ifinds.Object.OG.dll");
//        baseAssemblies.Add(controllersAssembly);
//        return baseAssemblies;
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ifinds.Api.Models;
using System.Web.Http.Dispatcher;
using System.Reflection;
using System.IO;
using DTP.Base.Infrastructure;
namespace ifinds.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.EnsureInitialized();
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // WebApiConfig.Register(GlobalConfiguration.Configuration);
            CustomAssembliesResolver assemblyResolver = new CustomAssembliesResolver();
            assemblyResolver.GetAssemblies();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);
         //   GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EngineContext.Initialize(false);

        }
    }

    public class MyCustomAssemblyResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);
            //Type myType = typeof(ifinds.Object.OG.Controllers.EPEmployeeController);
            //var controllersAssembly = Assembly.LoadFrom(@"~\bin\ifinds.Object.OG.dll");
            //baseAssemblies.Add(controllersAssembly);
            return assemblies;
        }
    }
    class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.AR.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.CA.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.CS.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.FI.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.GL.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.IN.dll"));
            //assemblies.Add(Assembly.LoadFrom(folder + @"\ifinds.Object.OG.dll"));

            return assemblies;
        }
    }
    public class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var baseAssemblies = base.GetAssemblies();
            var assemblies = new List<Assembly>(baseAssemblies);
            if(!Directory.Exists(appPath + "\\bin\\Plugins"))
            {
                Directory.CreateDirectory(appPath + "\\bin\\Plugins");
            }
            var files = Directory.GetFiles(appPath + "\\bin\\Plugins", "*.dll",
                            SearchOption.AllDirectories);
            var customAssemblies = files.Select(Assembly.LoadFile);

            // register Web API controllers
            var apiControllerAssemblies =
                from assembly in customAssemblies
                where !assembly.IsDynamic
                from type in assembly.GetExportedTypes()
                where typeof(System.Web.Http.Controllers.IHttpController).IsAssignableFrom(type)
                where !type.IsAbstract
                where !type.IsGenericTypeDefinition
                where type.Name.EndsWith("Controller", StringComparison.Ordinal)
                select assembly;

            foreach (var assembly in apiControllerAssemblies)
            {
                baseAssemblies.Add(assembly);
            }

            //return assemblies;
            return baseAssemblies;
        }
    }
}

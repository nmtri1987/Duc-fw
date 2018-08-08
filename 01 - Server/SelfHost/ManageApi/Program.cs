using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
//using WebApiHelpPage;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Http.Dispatcher;
using System.IO;
namespace SampleWebApiSelfHost
{
    internal class Program_bak
    {
        private static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000";
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(baseAddress);
            // Set our own assembly resolver where we add the assemblies we need
            AssembliesResolver assemblyResolver = new AssembliesResolver();
            config.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);

            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            // Configure help page
            HelpPageConfig.Register(config);

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            Console.WriteLine("Service listening at: {0}", baseAddress);
            Console.WriteLine("Help page available at: {0}/help", baseAddress);
            Console.WriteLine("Press Enter to shutdown the service.");
            Console.ReadLine();
        }
    }

    // Change the controller name if you want a different URI.
    //  public class HelpController : HelpControllerBase { }

    //class AssembliesResolver : DefaultAssembliesResolver
    //{
    //    public override ICollection<Assembly> GetAssemblies()
    //    {
    //        ICollection<Assembly> baseAssemblies = base.GetAssemblies();
    //        List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

    //        // Add whatever additional assemblies you wish

    //        var controllersAssembly = Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WebApiClasses.dll");
    //        baseAssemblies.Add(controllersAssembly);
    //        return assemblies;
    //    }


    //}
    class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ifinds.Object.OG.dll";
            assemblies.Add(Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ifinds.Object.OG.dll"));
            assemblies.Add(Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ifinds.Object.AR.dll"));

            return assemblies;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using DTP.Base;
using DTP.Base.Infrastructure;

public class BaseApi<T> : ApiController
{
    IServiceManager<T> _task;
    protected BaseApi()
    {
        _task = this.CreateT();
    }

    #region Standard API
    public virtual T Get([FromUri]GetParam value)
    {
        return _task.Get(value);
    }

    public virtual IEnumerable<T> Post(string method, [FromBody] SearchFilter value)
    {
        return _task.GetSearch(value);
    }

    public virtual T Post([FromBody]T value)
    {
        return _task.Add(value);
    }

    public virtual T Put(string id, [FromBody]T value)
    {
        return _task.Update(value);
    }
    public virtual int Delete([FromUri]GetParam value)
    {
        return _task.Del(value);
    }
    #endregion
    private IServiceManager<T> CreateT()
    {
        IServiceManager<T> task = null;

        var scope = EngineContext.Current.ContainerManager.Scope();
        var type2 = System.Type.GetType(DNHClass.ServiceMngClass);
        if (type2 != null)
        {
            object instance;
            //mapping string because ojbect and we use autofac
            if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
            {
                //not resolved
                instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                
            }
            task = instance as IServiceManager<T>;
        }
        return task;
    }
    private ObjectClass DNHClass
    {
        get
        {
            try
            {
                Type t = typeof(T);
                string[] NamespaceArr = t.ToString().Split('.');
                string dll = t.Assembly.GetName().Name;
                string classFullName = t.FullName; //ifinds.Object.CS.Company
                string typeName = classFullName + "Manager, " + dll;
                string column = "";

                string key = "";

                return new ObjectClass()
                {

                    dll = dll,
                    ServiceMngClass = typeName,
                    MyType = t.ToString(),
                    //MyColumn = column,
                    ViewFolder = "~/Views/" + NamespaceArr[1] + "/" + t.Name + "/",
                    TableName = t.Name
                };
            }
            catch
            {
            }
            return null;
        }
    }
}
    
    public class ObjectClass
    {
        public string Key { get; set; }
        public string TableName { get; set; }
        public string MyColumn { get; set; }
        public string MyType { get; set; }
        public string dll { get; set; }
        public string ServiceMngClass { get; set; }
        public string GetAllClass { get; set; }
        public string ViewFolder { get; set; }
    }


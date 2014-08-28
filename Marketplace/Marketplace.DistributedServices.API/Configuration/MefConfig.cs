using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Marketplace.DistributedServices.API.Security;

namespace Marketplace.DistributedServices.API.Configuration
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            InitializeDependencyResolver(Assembly.Load("Marketplace.Core"),
                Assembly.Load("Marketplace.Domain"),
                Assembly.Load("Marketplace.Security"),
                Assembly.Load("Marketplace.DistributedServices.API"),
                Assembly.Load("Marketplace.Infrastructure.SecurityContext"),
                Assembly.Load("Marketplace.Infrastructure.Repository"));
        }

        private static void InitializeDependencyResolver(params Assembly[] appAssemblies)
        {
            var configuration = new ContainerConfiguration().WithAssemblies(appAssemblies);
            var container = configuration.CreateContainer();

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new StandaloneDependencyResolver(container);
        }
    }

    // MEF 2
    public class StandaloneDependencyScope : IDependencyScope
    {
        readonly Export<CompositionContext> _compositionScope;

        public StandaloneDependencyScope(Export<CompositionContext> compositionScope)
        {
            if (compositionScope == null) throw new ArgumentNullException("compositionScope");
            _compositionScope = compositionScope;
        }

        protected CompositionContext CompositionScope { get { return _compositionScope.Value; } }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
                _compositionScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            object result = null;
            CompositionScope.TryGetExport(serviceType, out result);
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            return CompositionScope.GetExports(serviceType, null);
        }
    }

    public class StandaloneDependencyResolver : StandaloneDependencyScope, System.Web.Http.Dependencies.IDependencyResolver
    {
        readonly ExportFactory<CompositionContext> _requestScopeFactory;

        public StandaloneDependencyResolver(CompositionHost rootCompositionScope)
            : base(new Export<CompositionContext>(rootCompositionScope, rootCompositionScope.Dispose))
        {
            if (rootCompositionScope == null) throw new ArgumentNullException();
            var factoryContract = new CompositionContract(typeof(ExportFactory<CompositionContext>), null, new Dictionary<string, object> {
                { "SharingBoundaryNames", new[] { "HttpRequest" } }
            });

            _requestScopeFactory = (ExportFactory<CompositionContext>)rootCompositionScope.GetExport(factoryContract);
        }

        public IDependencyScope BeginScope()
        {
            return new StandaloneDependencyScope(_requestScopeFactory.CreateExport());
        }
    }
}

/*

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Marketplace.DistributedServices.API.Security;

namespace Marketplace.DistributedServices.API.Configuration
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            //var asmCatalog = new DirectoryCatalog(@"D:\VS 2014\Marketplace\Marketplace.DistributedServices.API\bin");

            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(@"D:\VS 2014\Marketplace\Marketplace.DistributedServices.API\bin\", "*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));

            InitializeDependencyResolver(allAssemblies.ToArray());
        }

        //private static void InitializeDependencyResolver(Assembly[] appAssemblies)
        //{
        //    var conventions = new ConventionBuilder();

        //    //conventions.ForTypesDerivedFrom<IHttpController>()
        //    //    .Export();

        //    //conventions.ForTypesMatching(t => t.Namespace != null &&
        //    //          (t.Namespace.EndsWith(".Parts") || t.Namespace.Contains(".Parts.")))
        //    //    .Export()
        //    //    @"D:\VS 2014\Marketplace\Marketplace.DistributedServices.API\bin".ExportInterfaces();

        //    var configuration = new ContainerConfiguration();

        //    configuration.WithAssemblies(appAssemblies, conventions);
            
        //    var container = configuration.CreateContainer();

        //    System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new StandaloneDependencyResolver(container);
        //}
    }

    // MEF
    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _compositionContainer;

        public MefControllerFactory(CompositionContainer compositionContainer)
        {
            _compositionContainer = compositionContainer;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            var export = _compositionContainer.GetExports(controllerType, null, null).SingleOrDefault();

            IController result;

            if (null != export)
            {
                result = export.Value as IController;
            }
            else
            {
                result = base.GetControllerInstance(requestContext, controllerType);
                _compositionContainer.ComposeParts(result);
            }

            return result;
        }
    }

    public class MefDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var export = _container.GetExports(serviceType, null, null).SingleOrDefault();

            return null != export ? export.Value : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var exports = _container.GetExports(serviceType, null, null);
            var createdObjects = new List<object>();

            if (exports.Any())
            {
                foreach (var export in exports)
                {
                    createdObjects.Add(export.Value);
                }
            }

            return createdObjects;
        }

        public void Dispose()
        {
            ;
        }
    }


    // MEF 2
    //public class StandaloneDependencyScope : IDependencyScope
    //{
    //    readonly Export<CompositionContext> _compositionScope;

    //    public StandaloneDependencyScope(Export<CompositionContext> compositionScope)
    //    {
    //        if (compositionScope == null) throw new ArgumentNullException("compositionScope");
    //        _compositionScope = compositionScope;
    //    }

    //    protected CompositionContext CompositionScope { get { return _compositionScope.Value; } }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //    }

    //    protected void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //            _compositionScope.Dispose();
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        if (serviceType == null) throw new ArgumentNullException("serviceType");

    //        object result;
    //        CompositionScope.TryGetExport(serviceType, null, out result);
    //        return result;
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        if (serviceType == null) throw new ArgumentNullException("serviceType");

    //        return CompositionScope.GetExports(serviceType, null);
    //    }
    //}

    //public class StandaloneDependencyResolver : StandaloneDependencyScope, System.Web.Http.Dependencies.IDependencyResolver
    //{
    //    readonly ExportFactory<CompositionContext> _requestScopeFactory;

    //    public StandaloneDependencyResolver(CompositionHost rootCompositionScope)
    //        : base(new Export<CompositionContext>(rootCompositionScope, rootCompositionScope.Dispose))
    //    {
    //        if (rootCompositionScope == null) throw new ArgumentNullException();
    //        var factoryContract = new CompositionContract(typeof(ExportFactory<CompositionContext>), null, new Dictionary<string, object> {
    //            { "SharingBoundaryNames", new[] { "HttpRequest" } }
    //        });

    //        _requestScopeFactory = (ExportFactory<CompositionContext>)rootCompositionScope.GetExport(factoryContract);
    //    }

    //    public IDependencyScope BeginScope()
    //    {
    //        return new StandaloneDependencyScope(_requestScopeFactory.CreateExport());
    //    }
    //}
}*/
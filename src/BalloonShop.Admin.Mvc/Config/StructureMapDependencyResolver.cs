using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace BalloonShop.Mvc.Config
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        private IContainer _container;

        public object GetService(Type serviceType)
        {
            object instance = _container.TryGetInstance(serviceType);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}
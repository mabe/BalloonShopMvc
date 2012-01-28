using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.Web.Mvc;
using System.Web.Routing;

namespace BalloonShop.Mvc.Config
{
    public class StructureMapControllerActivator : IControllerActivator
    {
        public StructureMapControllerActivator(IContainer container)
        {
            _container = container;
        }

        private IContainer _container;

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;

namespace BalloonShop.Mvc.Config
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<IControllerActivator>().Use<StructureMapControllerActivator>();
        }
    }
}
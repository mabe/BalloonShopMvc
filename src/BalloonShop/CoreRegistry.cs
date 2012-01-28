using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using NHibernate;

namespace BalloonShop
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<ISessionFactory>().Singleton().Use(ctx => NHibernateConfiguration.Factory());
            For<ISession>().HttpContextScoped().Use(ctx => ctx.GetInstance<ISessionFactory>().GetCurrentSession());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using System.Reflection;

namespace BalloonShop
{
    public static class NHibernateConfiguration
    {
        public static ISessionFactory Factory() {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard.ConnectionString(x => x.FromConnectionStringWithKey("BalloonShopConnection")))
                .Mappings(x => {
					x.FluentMappings.AddFromAssembly(Assembly.Load("BalloonShop")); //.ExportTo(AppDomain.CurrentDomain.BaseDirectory);
                    //x.HbmMappings.AddFromAssembly(Assembly.Load("BalloonShop"));
                })
                .ExposeConfiguration(cfg => cfg.SetProperty(NHibernate.Cfg.Environment.CurrentSessionContextClass,"web"))
                .BuildSessionFactory();
        }
    }
}

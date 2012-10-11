using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus;
using Rhino.ServiceBus.StructureMap;
using Rhino.ServiceBus.Hosting;
using StructureMap;
using Rhino.ServiceBus.Msmq;
using NHibernate;
using Rhino.ServiceBus.MessageModules;
using BalloonShop.Services;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Sagas.Persisters;
using Rhino.ServiceBus.Sagas;


namespace BalloonShop.Server
{
    public class Program
    {
        static void Main(string[] args) {

            PrepareQueues.Prepare("msmq://localhost/BalloonShop.Server", QueueType.Standard);

            Console.WriteLine("Starting to listen for incoming messages ...");

            var host = new DefaultHost();

            host.Start<BootStrapper>();

            Console.ReadLine();

        }
    }

    public class BootStrapper : StructureMapBootStrapper {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.Configure(cfg => {
                cfg.For<IEmailService>().Use<SmtpEmailService>();
                cfg.For<ISessionFactory>().Singleton().Use(() => NHibernateConfiguration.Factory());
                cfg.For<IMessageModule>().Singleton().Use<NHibernateMessageModule>();
                cfg.For<ISession>().Use(() => NHibernateMessageModule.CurrentSession);
                cfg.For(typeof(ISagaPersister<>)).Use(typeof(InMemorySagaPersister<>));

                //cfg.Scan(x =>
                //{
                //    x.TheCallingAssembly();
                //    x.ConnectImplementationsToTypesClosing(typeof(Orchestrates<>));
                //});
            });
        }
    }

    
}

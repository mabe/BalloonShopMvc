using NHibernate;
using Rhino.ServiceBus.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BalloonShop.Server
{
    public class NHibernateSagaPersister<TSaga> : ISagaPersister<TSaga> where TSaga : class, IAccessibleSaga
    {
        private readonly IServiceLocator serviceLocator;
        private readonly ISession session;
        private readonly IMessageSerializer messageSerializer;
        private readonly IReflection reflection;

        public NHibernateSagaPersister(INHibernateSessionProvider session, IServiceLocator serviceLocator, IMessageSerializer messageSerializer, IReflection reflection)
        {
            this.session = session.CurrentSession;
            this.serviceLocator = serviceLocator;
            this.messageSerializer = messageSerializer;
            this.reflection = reflection;
        }

        public void Complete(TSaga saga)
        {
            session.Connection.Execute("delete from Sage where Id = @Id", new { Id = saga.Id });
        }

        public TSaga Get(Guid id)
        {
            var state = session.Connection.Query<byte[]>("select State from Saga where Id = @Id", new { Id = id }).SingleOrDefault();
            if (state == null)
            {
                return null;
            }
            
            using (var ms = new MemoryStream(state))
            {
                var saga = serviceLocator.Resolve<TSaga>();
                saga.Id = id;
                reflection.Set(saga, "State", type => new BinaryFormatter().Deserialize(ms));
                return saga;
            }
        }

        public void Save(TSaga saga)
        {
            dynamic dynamicSaga = saga;
            dynamic state = dynamicSaga.State;

            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, state);
                var data = memoryStream.ToArray();
                session.Connection.Execute("update Saga set State = @State where Id = @Id if @@ROWCOUNT = 0 begin insert into Saga(Id, State) values(@Id, @State) end", new { Id = saga.Id, State = data });
            }
        }
    }
}

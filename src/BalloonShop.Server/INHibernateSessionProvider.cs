using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Server
{
    public interface INHibernateSessionProvider
    {
        ISession CurrentSession { get; }
    }
}

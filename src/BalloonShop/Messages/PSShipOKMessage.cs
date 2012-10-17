using Rhino.ServiceBus.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalloonShop.Messages
{
    public class PSShipOKMessage : ISagaMessage
    {
        public Guid CorrelationId { get; set; }
    }
}

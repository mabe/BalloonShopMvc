using Rhino.ServiceBus.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Messages
{
    public class PSTakePaymentMessage : ISagaMessage
    {
        public Guid CorrelationId { get; set; }
    }
}

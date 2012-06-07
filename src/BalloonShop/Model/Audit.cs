using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class Audit
    {
        public Audit()
        {

        }

        public Audit(int orderId, int number, string message)
        {
            OrderId = orderId;
            MessageNumber = number;
            Message = message;
            Date = DateTime.Now;
        }

        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Message { get; set; }
        public virtual int MessageNumber { get; set; }

    }

    public class AuditMap : ClassMap<Audit>
    {
        public AuditMap()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("AuditId");
            Map(x => x.OrderId);
            Map(x => x.Date).Column("DateStamp");
            Map(x => x.Message);
            Map(x => x.MessageNumber);
        }
    }
}

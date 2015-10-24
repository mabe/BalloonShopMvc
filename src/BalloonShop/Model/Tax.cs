using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class Tax
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Percentage { get; set; }
    }

    public class TaxMap : ClassMap<Tax> {
        public TaxMap()
        {
			Table ("tax");
            Id(x => x.Id).GeneratedBy.Identity().Column("TaxId");
            Map(x => x.Name).Column("TaxType");
            Map(x => x.Percentage).Column("TaxPercentage");
        }
    }
}

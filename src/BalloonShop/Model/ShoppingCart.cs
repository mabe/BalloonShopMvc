using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class ShoppingCart
    {
        public virtual string CartId { get; set; }
        public virtual Balloon Balloon { get; set; }

        public virtual int Quantity { get; set; }
        public virtual DateTime DateAdded { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ShoppingCart;
            if (t == null)
                return false;
            if (Balloon.Id == t.Balloon.Id && CartId == t.CartId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (Balloon.Id + "|" + CartId).GetHashCode();
        }  
    }

    public class ShoppingCartMap : ClassMap<ShoppingCart>
    {
        public ShoppingCartMap()
        {
            CompositeId().KeyReference(x => x.Balloon, "ProductId").KeyProperty(x => x.CartId);

            Map(x => x.Quantity);
            Map(x => x.DateAdded);
        }
    }
}

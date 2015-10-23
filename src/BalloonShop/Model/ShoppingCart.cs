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
        public virtual Product Product { get; set; }

        public virtual int Quantity { get; set; }
        public virtual DateTime DateAdded { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ShoppingCart;
            if (t == null)
                return false;
            if (Product.Id == t.Product.Id && CartId == t.CartId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (Product.Id + "|" + CartId).GetHashCode();
        }  
    }

    public class ShoppingCartMap : ClassMap<ShoppingCart>
    {
        public ShoppingCartMap()
        {
			Table ("shoppingcart");

            CompositeId().KeyReference(x => x.Product, "ProductId").KeyProperty(x => x.CartId);

            Map(x => x.Quantity);
            Map(x => x.DateAdded);
        }
    }
    
}

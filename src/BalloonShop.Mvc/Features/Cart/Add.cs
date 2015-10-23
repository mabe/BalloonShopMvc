namespace BalloonShop.Mvc.Features.Cart {
	using BalloonShop.Model;
	using NHibernate;
	using System;

  public class Add {
    public class Command {
      public string customerCartId { get; set; }
      public int balloonId { get; set; }
      public int quantity { get; set; }
      public string returnurl { get; set; }
    }

    public class Handler {
      private ISession _session;
      public Handler(ISession session) {
        _session = session;
      }

      public void Execute(Command command) {
        var balloon = _session.Load<Product>(command.balloonId);

        var item = _session.Get<ShoppingCart>(new ShoppingCart { Product = balloon, CartId = command.customerCartId });

        if (item == null) {
            item = new ShoppingCart() { Product = balloon, CartId = command.customerCartId, DateAdded = DateTime.Now };
            _session.Save(item);
        }

        item.Quantity += command.quantity;

        if (item.Quantity <= 0) {
            _session.Delete(item);
        }
      }
    }
  }
}

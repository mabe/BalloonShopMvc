using System;
using NHibernate;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Features.Cart
{
	public class Remove
	{
		public class Command : ICommand {
			public string customerCartId { get; set; }
			public int remove { get; set; }
		}

		public class Handler : ICommandHandler<Command> {
			private readonly ISession _session;

			public Handler (ISession session)
			{
				_session = session;
			}

			public void Execute(Command request) {
				_session.Delete(_session.Load<ShoppingCart>(new ShoppingCart() { CartId = request.customerCartId, Product = _session.Load<Product>(request.remove) }));
			}
		}
	}
}


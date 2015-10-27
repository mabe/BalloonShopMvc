using System;
using NHibernate;
using BalloonShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace BalloonShop.Mvc.Features.Cart
{
	public class Update
	{
		public class Command : ICommand
		{
			public string customerCartId { get; set; }
			public IDictionary<int, int> items { get; set; }
		}

		public class Handler : ICommandHandler<Command>
		{
			private readonly ISession _session;

			public Handler (ISession session)
			{
				_session = session;
			}

			public void Execute(Command command) {
				var cart = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == command.customerCartId).List();

				foreach (var item in command.items)
				{
					cart.Single(x => x.Product.Id == item.Key).Quantity = item.Value;
				}
			}	
		}
	}
}


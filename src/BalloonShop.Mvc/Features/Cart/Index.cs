using System;
using BalloonShop.Mvc;
using NHibernate;
using BalloonShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace BalloonShop.Mvc.Features.Cart
{
	public class Index
	{
		public class Query : IQuery<Response>
		{
			public string customerCartId { get; set; }
		}

		public class Response
		{
			public IList<ShoppingCart> List {
				get;
				set;
			}

			public decimal Total {
				get;
				set;
			}

			public bool HideCartNavigation {
				get;
				set;
			}
		}

		public class Handler : IQueryHandler<Query,Response>
		{
			private readonly ISession _session;

			public Handler (ISession session)
			{
				_session = session;
			}

			public Response ExecuteQuery(Query query)
			{
				var list = _session.QueryOver<ShoppingCart> ().Where (x => x.CartId == query.customerCartId).List ();

				return new Response () {
					List = list,
					Total = list.Sum(x => x.Product.Price * x.Quantity),
					HideCartNavigation = true
				};
			}
		}
	}
}


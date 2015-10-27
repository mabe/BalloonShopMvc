using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Queries
{
    public static class Queries
    {
        public static IQueryOver<Category> CategoriesInDepartment(this ISession session, int departmentid) {
            return session.QueryOver<Category>().Where(x => x.Department.Id == departmentid);
        }

        public static IQueryOver<Product> BalloonsInCategory(this ISession session, int categoryid)
        {
            Category c = null;

            return session.QueryOver<Product>().JoinAlias(x => x.Categories, () => c).Where(Restrictions.On<Category>(x => c.Id).IsIn(new[] { categoryid }));
        }

		public static IQueryOver<Product> BalloonsOnCatalogPromotion(this ISession session) {
			return session.QueryOver<Product> ()
				//.Where(x => x.OnCatalogPromotion == true);
				.Where (Expression.Sql ("PromoFront = B'1'"));
		}

        public static IQueryOver<Product> BalloonsInDepartment(this ISession session, Department department) {
            Product b = null;


			return session.QueryOver<Product>()
				.WithSubquery.WhereProperty(x => x.Id)
				.In(QueryOver.Of<Product>(() => b)
					//.Where(x => x.OnDepartmentPromotion == true)
					//.Where(Expression.Eq("OnDepartmentPromotion", true))
					.Where(Expression.Sql("PromoDept = B'1'"))
					.JoinQueryOver(x => x.Categories)
					.Where(x => x.Department == department)
					.Select(Projections.Distinct(Projections.Property(() => b.Id))));
        }

		public static IQueryOver<ShoppingCart> ShoppingCartByCartId (this ISession session, string cartId){
			return session.QueryOver<ShoppingCart> ().Where (x => x.CartId == cartId);
		}

        public static PagedList<T> PagedList<T>(this IQueryOver<T> query, int itemsperpage, int? page) {
            var howManyPages = (int)Math.Ceiling((decimal)query.Clone().ClearOrders().RowCount() / (decimal)itemsperpage);

            var items = query
                .Skip(((page ?? 1) - 1) * itemsperpage)
                .Take(itemsperpage)
                .List();

            return new PagedList<T>(page ?? 1, itemsperpage, howManyPages, items);
        }
    }
}

using FluentNHibernate.Mapping;
using System.Collections.Generic;
namespace BalloonShop.Model
{
    public class Product
    {
        public Product()
        {
            Thumb = "Generic1.png";
            Image = "Generic2.png";
        }

        public virtual string Name { get; set; }

        public virtual string Thumb { get; set; }

        public virtual int Id { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string Description { get; set; }

        public virtual string Image { get; set; }

        public virtual bool OnCatalogPromotion { get; set; }
        public virtual bool OnDepartmentPromotion { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");

            Id(x => x.Id).GeneratedBy.Identity().Column("ProductId");
            Map(x => x.Name);
            Map(x => x.Thumb).Column("thumbnail");
            Map(x => x.Price);
            Map(x => x.Description);
            Map(x => x.Image);
			Map(x => x.OnCatalogPromotion).Column("PromoFront");
			Map(x => x.OnDepartmentPromotion).Column("PromoDept");

            HasManyToMany(x => x.Categories).Table("ProductCategory").ParentKeyColumn("ProductId").ChildKeyColumn("CategoryId");
        }
    }
}

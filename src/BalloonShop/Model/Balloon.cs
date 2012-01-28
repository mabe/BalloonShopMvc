using FluentNHibernate.Mapping;
using System.Collections.Generic;
namespace BalloonShop.Model
{
    public class Balloon
    {
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

    public class BalloonMap : ClassMap<Balloon> {
        public BalloonMap()
        {
            Table("Product");

            Id(x => x.Id).GeneratedBy.Identity().Column("ProductId");
            Map(x => x.Name);
            Map(x => x.Thumb).Column("Image1FileName");
            Map(x => x.Price);
            Map(x => x.Description);
            Map(x => x.Image).Column("Image2FileName");
            Map(x => x.OnCatalogPromotion);
            Map(x => x.OnDepartmentPromotion);

            HasManyToMany(x => x.Categories).Table("ProductCategory").ParentKeyColumn("ProductId").ChildKeyColumn("CategoryId");
        }
    }
}

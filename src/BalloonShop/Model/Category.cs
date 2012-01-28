using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Department Department { get; set; }

        public virtual int DepartmentId { get; set; }
    }

    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("CategoryId");
            Map(x => x.Name);
            Map(x => x.Description);
            References(x => x.Department, "DepartmentId");
        }
    }
}

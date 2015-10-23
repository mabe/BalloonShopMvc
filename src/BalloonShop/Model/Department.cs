using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class Department
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int Id { get; set; }
    }

    public class DepartmentMap : ClassMap<Department> 
    {
        public DepartmentMap()
        {
			Table ("department");

            Id(x => x.Id).GeneratedBy.Identity().Column("DepartmentID");
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
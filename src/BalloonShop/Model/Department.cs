using System;
using System.Collections.Generic;

namespace BalloonShop.Model
{
    public class Department
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public IEnumerable<Balloon> PromotedBalloons { get; set; }
    }
}
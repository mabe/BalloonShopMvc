using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalloonShop.Model
{
    public class ProductReview
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Review { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Model
{
    /// <summary>
    /// Wraps product details data
    /// </summary>
    public struct ProductDetails
    {
        public string Name;
        public string Description;
        public decimal Price;
        public string Image1FileName;
        public string Image2FileName;
        public bool OnDepartmentPromotion;
        public bool OnCatalogPromotion;
    }
}

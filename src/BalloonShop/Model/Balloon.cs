namespace BalloonShop.Model
{
    public class Balloon
    {
        public string Name { get; set; }

        public string Thumb { get; set; }

        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool OnCatalogPromotion { get; set; }
        public bool OnDepartmentPromotion { get; set;}
    }
}

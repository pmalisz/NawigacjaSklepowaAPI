namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Shelf : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X {get;set;}
        public int Y { get;set;}
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

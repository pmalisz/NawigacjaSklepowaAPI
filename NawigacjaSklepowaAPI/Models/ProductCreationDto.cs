namespace NawigacjaSklepowaAPI.Models
{
    public class ProductCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Alley { get; set; }
        public int Floor { get; set; }
        public int ShopId { get; set; }
    }
}
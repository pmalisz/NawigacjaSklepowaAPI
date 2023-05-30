namespace NawigacjaSklepowaAPI.Models.Products
{
    public class ProductCreationDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required float Price { get; set; }
        public required int Floor { get; set; }
        public required string Shelves { get; set; }
        public required int ShopId { get; set; }
    }
}
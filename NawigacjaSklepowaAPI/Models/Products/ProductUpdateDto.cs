namespace NawigacjaSklepowaAPI.Models.Products
{
    public class ProductUpdateDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required float Price { get; set; }
        public required int ShelfId { get; set; }
    }
}
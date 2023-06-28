using System.ComponentModel;

namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        [DefaultValue(0)]
        public int Rating { get; set; }
        [DefaultValue(0)]
        public int RatingCount { get; set; }
        public int? ShelfId { get; set; }
        public Shelf Shelf { get; set; }
    }
}
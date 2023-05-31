using System.ComponentModel;

namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public int Floor { get; set; }
        public string Shelves { get; set; }
        [DefaultValue(0)]
        public int Rating { get; set; }
        [DefaultValue(0)]
        public int RatingCount { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
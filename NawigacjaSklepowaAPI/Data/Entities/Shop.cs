using System.ComponentModel;

namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DefaultValue(0)]
        public int Rating { get; set; }
        [DefaultValue(0)]
        public int RatingCount { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Shelf> Shelves { get; set; }
    }
}
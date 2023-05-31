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
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Product> Products { get; set; }
        public Layout Layout { get; set; }
    }
}
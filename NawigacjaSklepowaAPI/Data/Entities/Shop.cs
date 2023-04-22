namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public Company Company { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
namespace NawigacjaSklepowaAPI.Models.Shops
{
    public class ShopCreationDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
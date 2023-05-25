namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}

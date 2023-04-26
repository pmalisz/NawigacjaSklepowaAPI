﻿namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Shop> Shops { get; set; }
        public List<ShopUser> ShopUsers { get; set; }
    }
}

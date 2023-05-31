using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Models.Auth
{
    public class UserEditionDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}

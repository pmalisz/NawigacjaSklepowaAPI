namespace NawigacjaSklepowaAPI.Models.Auth
{
    public class UserRegistrationDto : UserLoginDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ConfirmPassword { get; set; }
        public required int ShopId { get; set; }
        public required int RoleId { get; set;}
    }
}

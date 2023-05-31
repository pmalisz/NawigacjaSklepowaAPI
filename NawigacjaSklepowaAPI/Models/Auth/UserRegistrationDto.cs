namespace NawigacjaSklepowaAPI.Models.Auth
{
    public class UserRegistrationDto : UserLoginDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required bool ShopOwner { get; set; }
    }
}

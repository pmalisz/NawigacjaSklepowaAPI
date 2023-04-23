namespace NawigacjaSklepowaAPI.Models.Auth
{
    public class AccountCreationDto : UserLoginDto
    {
        public required string ConfirmPassword { get; set; }
        public required AccountType AccountType { get; set; }
    }
}

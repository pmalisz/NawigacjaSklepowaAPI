using System.Text.RegularExpressions;

namespace NawigacjaSklepowaAPI.Helpers.Validators
{
    public class MyValidators
    {
        public static (bool, string) CheckPostalCode(string postalCode)
        {
            return (Regex.IsMatch(postalCode, @"^[0-9]{2}-[0-9]{3}$"),
                    "kod pocztowy powinien zawierać 2 cyfry myślnik i 3 cyfry.");
        }

        public static (bool, string) CheckPhoneNumber(string phone)
        {
            return (Regex.IsMatch(phone, @"^([0-9]{9})$"),
                    "Numer telefonu powinien zawierać 9 cyfr.");
        }

        public static (bool, string) CheckEmail(string email)
        {
            return (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"),
                                   "Niepoprawny adres email.");
        }

        public static (bool, string) CheckPassword(string Password)
        {
            // Check if password is strong enough
            var min_chars = 8;
            var max_chars = 32;

            if (Password.Length < min_chars)
                return (false, $"Hasło musi mieć co najmniej {min_chars} znaków.");

            if (Password.Length > max_chars)
                return (false, $"Hasło może mieć maksymalnie {max_chars} znaki.");

            if (!Password.Any(char.IsUpper))
                return (false, "Hasło musi zawierać co najmniej jedną wielką literę.");

            if (!Password.Any(char.IsLower))
                return (false, "Hasło musi zawierać co najmniej jedną małą literę.");

            if (!Password.Any(char.IsDigit))
                return (false, "Hasło musi zawierać co najmniej jedną cyfrę.");

            return (true, "");
        }
    }
}

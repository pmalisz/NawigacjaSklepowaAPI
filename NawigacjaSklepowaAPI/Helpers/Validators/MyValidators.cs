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
    }
}

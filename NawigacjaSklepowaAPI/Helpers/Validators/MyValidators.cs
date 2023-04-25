using NawigacjaSklepowaAPI.Data.Entities;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NawigacjaSklepowaAPI.Helpers.Validators
{
    public class MyValidators
    {
        public static (bool, string) checkPostalCode(string postalCode)
        {
            return (Regex.IsMatch(postalCode, @"^[0-9]{2}-[0-9]{3}$"), 
                    "kod pocztowy powinien zawierać 2 cyfry myślnik i 3 cyfry.");
        }

        public static (bool, string) checkPhoneNumber(string phone)
        {
            return (Regex.IsMatch(phone, @"^([0-9]{9})$"), 
                    "Numer telefonu powinien zawierać 9 cyfr.");
        }
    }
}

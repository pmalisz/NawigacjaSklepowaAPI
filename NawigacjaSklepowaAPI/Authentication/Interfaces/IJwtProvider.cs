using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Authentication.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}

using System.Security.Claims;

namespace PhoneBook.API.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        Task<ClaimsIdentity> GenerateClaimsIdentity(string userName, string id);
    }
}

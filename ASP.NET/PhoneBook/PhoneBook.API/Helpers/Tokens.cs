using Newtonsoft.Json;
using PhoneBook.API.Auth;
using PhoneBook.API.Models;
using PhoneBook.Models;
using System.Security.Claims;

namespace PhoneBook.API.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(
            ClaimsIdentity identity,
            IJwtFactory jwtFactory,
            string userName,
            JwtIssuerOptions jwtOptions,
            JsonSerializerSettings serializerSettings)
        {
            var response = new AuthResponse()
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                ExpiresInSec = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}

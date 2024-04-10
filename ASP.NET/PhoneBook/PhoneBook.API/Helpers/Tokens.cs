using Newtonsoft.Json;
using PhoneBook.API.Auth;
using PhoneBook.API.Models;
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
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}

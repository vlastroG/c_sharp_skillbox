using Newtonsoft.Json;
using PhoneBook.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace PhoneBook.Desktop.Services
{
    public class AccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            Token = string.Empty;
        }


        public string Token { get; private set; }


        public async Task<bool> Login(string email, string password)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            var builder = new UriBuilder(Helpers.Constants.LoginUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["userName"] = email;
            query["password"] = password;
            builder.Query = query.ToString();
            string url = builder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var userToken = JsonConvert.DeserializeObject<AuthResponse>(content);
                Token = userToken?.AuthToken ?? string.Empty;
                return true;
            } catch (JsonReaderException)
            {
                Token = string.Empty;
                return false;
            }
        }

        public void Logout()
        {
            Token = string.Empty;
        }


        public async Task<bool> Register(string email, string password)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            var builder = new UriBuilder(Helpers.Constants.RegisterUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["email"] = email;
            query["password"] = password;
            builder.Query = query.ToString();
            string url = builder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = await httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }


        public string GetUserName()
        {
            if (!string.IsNullOrWhiteSpace(Token))
            {
                return new JwtSecurityToken(Token).Subject;
            } else
            {
                return string.Empty;
            }
        }


        public UserRoles GetUserRole()
        {
            if (!string.IsNullOrWhiteSpace(Token))
            {
                if (new JwtSecurityToken(Token).Claims.Contains(new Claim("admin_access", "true")))
                {
                    return UserRoles.Admin;
                } else
                {
                    return UserRoles.User;
                }
            } else
            {
                return UserRoles.Anonym;
            }
        }
    }


    public enum UserRoles
    {
        Anonym,
        User,
        Admin
    }
}

﻿using Newtonsoft.Json;
using PhoneBook.Exceptions;
using PhoneBook.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Windows.Controls;

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


        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<bool> Login(string email, PasswordBox passwordBox)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }
            if (passwordBox is null) { throw new ArgumentNullException(nameof(passwordBox)); }

            using HttpClient httpClient = _httpClientFactory.CreateClient();

            var builder = new UriBuilder(Helpers.Constants.LoginUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["userName"] = email;
            query["password"] = passwordBox.Password;
            builder.Query = query.ToString();
            string url = builder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            } catch (HttpRequestException)
            {
                throw new ServerNotResponseException();
            }
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


        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<bool> Register(string email, PasswordBox passwordBox)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }
            if (passwordBox is null) { throw new ArgumentNullException(nameof(passwordBox)); }

            using HttpClient httpClient = _httpClientFactory.CreateClient();

            var builder = new UriBuilder(Helpers.Constants.RegisterUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["email"] = email;
            query["password"] = passwordBox.Password;
            builder.Query = query.ToString();
            string url = builder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(request);
            } catch (HttpRequestException)
            {
                throw new ServerNotResponseException();
            }
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
                if (new JwtSecurityToken(Token).Claims.Contains(new Claim("admin_access", "true"), new ClaimsComparer()))
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

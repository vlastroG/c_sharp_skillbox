using PhoneBook.Exceptions;
using PhoneBook.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PhoneBook.Desktop.Services
{
    public class ContactsRepository
    {
        private readonly AccountService _accountService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactsRepository(AccountService accountService, IHttpClientFactory httpClientFactory)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }


        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<IEnumerable<Contact>> Get()
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            try
            {
                var contacts = await client.GetFromJsonAsync<IEnumerable<Contact>>(Helpers.Constants.ContactsUri);
                return contacts ?? Array.Empty<Contact>();
            } catch (HttpRequestException)
            {
                throw new ServerNotResponseException();
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotAuthorizedUserException"></exception>
        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<bool> Create(Contact contact)
        {
            if (contact is null) { throw new ArgumentNullException(nameof(contact)); }

            if (IsAuthorizedUser())
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                HttpResponseMessage response;
                try
                {
                    response = await client.PutAsJsonAsync(Helpers.Constants.ContactsUri + "Create", contact);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedUserException();
                    } else
                    {
                        return false;
                    }
                } catch (HttpRequestException)
                {
                    throw new ServerNotResponseException();
                }
            } else
            {
                throw new NotAuthorizedUserException();
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotAuthorizedUserException"></exception>
        /// <exception cref="AccessDeniedException"></exception>
        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<bool> Edit(Contact contact)
        {
            if (contact is null) { throw new ArgumentNullException(nameof(contact)); }

            if (IsAdmin())
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsJsonAsync(
                        Helpers.Constants.ContactsUri + $"Update/{contact.Id}", contact);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedUserException();
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        throw new AccessDeniedException();
                    } else
                    {
                        return false;
                    }
                } catch (HttpRequestException)
                {
                    throw new ServerNotResponseException();
                }
            } else
            {
                throw new AccessDeniedException();
            }
        }

        /// <exception cref="NotAuthorizedUserException"></exception>
        /// <exception cref="AccessDeniedException"></exception>
        /// <exception cref="ServerNotResponseException"></exception>
        public async Task<bool> Delete(int id)
        {
            if (IsAdmin())
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                HttpResponseMessage response;
                try
                {
                    response = await client.DeleteAsync(Helpers.Constants.ContactsUri + $"Delete/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedUserException();
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        throw new AccessDeniedException();
                    } else
                    {
                        return false;
                    }
                } catch (HttpRequestException)
                {
                    throw new ServerNotResponseException();
                }
            } else
            {
                throw new AccessDeniedException();
            }
        }


        private bool IsAuthorizedUser()
        {
            return _accountService.GetUserRole() != UserRoles.Anonym;
        }

        private bool IsAdmin()
        {
            return _accountService.GetUserRole() == UserRoles.Admin;
        }

        private void AddAuthenticationHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _accountService.Token);
        }
    }
}

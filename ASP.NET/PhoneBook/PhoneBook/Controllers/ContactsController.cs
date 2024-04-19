using Microsoft.AspNetCore.Mvc;
using PhoneBook.Helpers;
using PhoneBook.Models;
using System.Net.Http.Headers;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public ContactsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        // GET: Contacts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            var contacts = await client.GetFromJsonAsync<IEnumerable<Contact>>(Helpers.Constants.ContactsUri);

            return View(contacts);
        }

        // GET: Contacts/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using HttpClient client = _httpClientFactory.CreateClient();
            var contact = await client.GetFromJsonAsync<Contact>(Helpers.Constants.ContactsUri + id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.IsAuthorizedUser())
            {
                return View();
            } else
            {
                return RedirectToLogin();
            }
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] Contact contact)
        {
            if (HttpContext.Session.IsAuthorizedUser())
            {
                if (ModelState.IsValid)
                {
                    using HttpClient client = _httpClientFactory.CreateClient();
                    AddAuthenticationHeader(client);
                    var response = await client.PutAsJsonAsync(Constants.ContactsUri + "Create", contact);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToLogin();
                    } else
                    {
                        return BadRequest(response);
                    }
                }
                return View(contact);
            } else
            {
                return RedirectToLogin();
            }
        }

        // GET: Contacts/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.IsAdminUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                var contact = await client.GetFromJsonAsync<Contact>(Constants.ContactsUri + id);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            } else
            {
                return RedirectToAccessDenied();
            }
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] Contact contact)
        {
            if (HttpContext.Session.IsAdminUser())
            {
                if (id != contact.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    using HttpClient client = _httpClientFactory.CreateClient();
                    AddAuthenticationHeader(client);
                    var response = await client.PostAsJsonAsync(Constants.ContactsUri + $"Update/{id}", contact);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToLogin();
                    } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        return RedirectToAccessDenied();
                    } else
                    {
                        return NotFound();
                    }
                }
                return View(contact);
            } else
            {
                return RedirectToAccessDenied();
            }
        }

        // GET: Contacts/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.IsAdminUser())
            {
                if (id == null)
                {
                    return NotFound();
                }

                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                var contact = await client.GetFromJsonAsync<Contact>(Constants.ContactsUri + id);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            } else
            {
                return RedirectToAccessDenied();
            }
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.IsAdminUser())
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                AddAuthenticationHeader(client);
                var response = await client.DeleteAsync(Constants.ContactsUri + $"Delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToLogin();
                } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAccessDenied();
                } else
                {
                    return NotFound();
                }
            } else
            {
                return RedirectToAccessDenied();
            }
        }


        private RedirectToPageResult RedirectToLogin()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        private RedirectToPageResult RedirectToAccessDenied()
        {
            return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
        }

        private void AddAuthenticationHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString(Constants.TokenName));
        }
    }
}

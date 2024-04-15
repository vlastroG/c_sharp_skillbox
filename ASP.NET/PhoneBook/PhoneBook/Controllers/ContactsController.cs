using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;

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
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.PutAsJsonAsync(Helpers.Constants.ContactsUri + "Create", contact);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return BadRequest(response);
                }
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                using HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(Helpers.Constants.ContactsUri + $"Update/{id}", contact);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return NotFound();
                }
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync(Helpers.Constants.ContactsUri + $"Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            } else
            {
                return NotFound();
            }
        }
    }
}

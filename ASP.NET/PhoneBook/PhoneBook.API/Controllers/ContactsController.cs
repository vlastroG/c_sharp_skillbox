using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Data;
using PhoneBook.API.Models;


namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly PhoneBookContext _phoneBookContext;

        public ContactsController(PhoneBookContext phoneBookContext)
        {
            _phoneBookContext = phoneBookContext ?? throw new ArgumentNullException(nameof(phoneBookContext));
        }


        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _phoneBookContext.Contact.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _phoneBookContext.Contact.FindAsync(id);
            if (contact != null)
            {
                return Ok(contact);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Contact contact)
        {
            if (contact is null || id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _phoneBookContext.Update(contact);
                    await _phoneBookContext.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("Create")]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = 0;
                _phoneBookContext.Add(contact);
                await _phoneBookContext.SaveChangesAsync();
                return Ok();
            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _phoneBookContext.Contact.FindAsync(id);
            if (contact != null)
            {
                _phoneBookContext.Contact.Remove(contact);
                await _phoneBookContext.SaveChangesAsync();
                return Ok();
            } else
            {
                return NotFound();
            }
        }
    }
}

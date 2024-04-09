using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.API.Data;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly PhoneBookContext _phoneBookContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(PhoneBookContext phoneBookContext, UserManager<ApplicationUser> userManager)
        {
            _phoneBookContext = phoneBookContext ?? throw new ArgumentNullException(nameof(phoneBookContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Create(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest();
            }

            var user = new ApplicationUser() { Email = email, UserName = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            await _phoneBookContext.SaveChangesAsync();
            return Ok();
        }
    }
}

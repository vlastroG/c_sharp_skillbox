using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhoneBook.API.Auth;
using PhoneBook.API.Data;
using PhoneBook.API.Helpers;
using PhoneBook.API.Models;
using System.Security.Claims;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest();
            }

            var identity = await GetClaimsIdentity(userName, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password");
            }

            var jwt = await Tokens.GenerateJwt(
                identity,
                _jwtFactory,
                userName,
                _jwtOptions,
                new Newtonsoft.Json.JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented });

            return new OkObjectResult(jwt);
        }


        private async Task<ClaimsIdentity?> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return await Task.FromResult<ClaimsIdentity?>(default);
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity?>(null);
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                return await Task.FromResult(await _jwtFactory.GenerateClaimsIdentity(userName, user.Id));
            }

            return await Task.FromResult<ClaimsIdentity?>(null);
        }
    }
}

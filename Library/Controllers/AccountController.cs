using Library.Domains;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly UserManager<UserDomain> _userManager;
        private readonly IUserStore<UserDomain> _userStore;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<UserDomain> userManager,
            IUserStore<UserDomain> userStore,
            SignInManager<UserDomain> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDomain
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    FatherName = model.FatherName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok(new { name=model.Name + ' ' + model.Surname, email = model.Email, message = "Registration successful." });
                }
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
            }
            return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    return Ok(new { name = user.Name + ' ' + user.Surname, email = user.Email, message = "Login successful." });
                }

                if (result.IsLockedOut)
                {
                    return BadRequest(new { errors = "Account locked out. Please try again later." });
                }
                else
                {
                    return BadRequest(new { errors = "Invalid login or password attempt." });
                }
            }

            return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

        }
    }
}

using Library.Domains;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient.Server;
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
        private readonly IUserEmailStore<UserDomain> _emailStore;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            _emailStore = GetEmailStore();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.FatherName = model.FatherName;
                user.EmailConfirmed = true;
                if (DateTime.TryParse(model.DateOfBirth, out DateTime parsedDate))
                {
                    user.DateOfBirth = parsedDate;
                }
                else { user.DateOfBirth = null; }

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    using (var scope = HttpContext.RequestServices.CreateScope())
                    {
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        var roles = new[] { "Admin", "Ordinar" };

                        foreach (var role in roles)
                        {
                            if (!await roleManager.RoleExistsAsync(role))
                            {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }
                        }
                    }
                    await _userManager.AddToRoleAsync(user, "Ordinar");

                    await _signInManager.SignInAsync(user, isPersistent: true);
                    _logger.LogInformation("User created a new account with password.");

                    return Ok(new { name = model.Name + ' ' + model.Surname });
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
                    _logger.LogInformation("User logged in.");
                    return Ok(new { name = user.Name + ' ' + user.Surname });
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok(new { message = "User logged out successfully." });
        }

        private IUserEmailStore<UserDomain> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UserDomain>)_userStore;
        }

        private UserDomain CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserDomain>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}

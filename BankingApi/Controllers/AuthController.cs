using BankingApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var user = new User
			{
				UserName = model.Email,
				Email = model.Email,
				EmailConfirmed = false,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				AccessFailedCount = 0
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				return Ok();
			}
			return BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

			if (result.Succeeded)
			{
				return Ok();
			}
			return Unauthorized();
		}
	}
}

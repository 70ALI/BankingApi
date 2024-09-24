using Microsoft.AspNetCore.Identity;

namespace BankingApi.Models
{
	public class User : IdentityUser
	{
		public int AccessFailedCount { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }

	}
}

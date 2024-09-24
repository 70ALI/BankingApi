using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingApi.Models
{
	public class Client
	{
		public int Id { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, MaxLength(60)]
		public string FirstName { get; set; }

		[Required, MaxLength(60)]
		public string LastName { get; set; }

		[Required, StringLength(11, MinimumLength = 11)]
		public string PersonalId { get; set; }

		public string ProfilePhoto { get; set; }

		[Required]
		public string MobileNumber { get; set; }

		[Required]
		public string Sex { get; set; }

		public Address Address { get; set; }

		[Required]
		public List<Account> Accounts { get; set; } = new List<Account>();
	}
}

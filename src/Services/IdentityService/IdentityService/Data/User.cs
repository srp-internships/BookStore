using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data
{
	public sealed class User : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}

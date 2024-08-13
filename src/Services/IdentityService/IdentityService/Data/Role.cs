using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data
{
	public sealed class Role : IdentityRole<Guid>
	{
		public const string Admin = "admin";
		public const string Seller = "seller";
		public const string Customer = "customer";

		public Role(Guid id, string roleName) : base(roleName)
		{
			Id = id;
			NormalizedName = roleName.ToUpper();
		}

		public Role() : base()
		{
		}
	}
}

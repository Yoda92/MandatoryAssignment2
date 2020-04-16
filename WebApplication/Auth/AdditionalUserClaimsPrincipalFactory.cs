using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication.Models;

namespace WebApplication.Auth
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
		public AdditionalUserClaimsPrincipalFactory( 
				UserManager<ApplicationUser> userManager,
				RoleManager<IdentityRole> roleManager, 
				IOptions<IdentityOptions> optionsAccessor) 
				: base(userManager, roleManager, optionsAccessor)
			{}

			public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
			{
				var principal = await base.CreateAsync(user);
				var identity = (ClaimsIdentity)principal.Identity;

				var claims = new List<Claim>();
				if (user.IsReceptionist)
				{
					claims.Add(new Claim(JwtClaimTypes.Role, "receptionist"));
				}
				if (user.IsWaiter)
				{
					claims.Add(new Claim(JwtClaimTypes.Role, "waiter"));
				}

				identity.AddClaims(claims);
				return principal;
			}
    }
}
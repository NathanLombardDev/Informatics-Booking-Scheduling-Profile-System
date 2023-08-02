using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PedalProAPI.Models;
using System.Security.Claims;

namespace PedalProAPI.Factory
{
    /*public class AppUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<PedalProUser, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(UserManager<PedalProUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {
        }
    }*/


    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PedalProUser, IdentityRole>
    {
        private readonly UserManager<PedalProUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserClaimsPrincipalFactory(UserManager<PedalProUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(PedalProUser user)
        {
            var principal = await base.CreateAsync(user);

            // Check if the user has a specific role
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Role", "Admin"));
            }
            else if (await _userManager.IsInRoleAsync(user, "Employee"))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Role", "Employee"));
            }
            else if (await _userManager.IsInRoleAsync(user, "Client"))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Role", "Client"));
            }
            // Add more conditions for other roles if needed

            return principal;
        }
    }
}

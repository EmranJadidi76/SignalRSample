using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.ClaimFactory
{
    public class ApplicationClaimFactory : UserClaimsPrincipalFactory<Users, Roles>
    {

        public ApplicationClaimFactory(UserManager<Users> userManager, RoleManager<Roles> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(Users user)
        {
            var principal = await base.CreateAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                 new Claim("FirstName", user.FirstName ?? ""),
                 new Claim("LastName",  user.LastName ?? ""),
                 new Claim("FullName",  user.FirstName + " " + user.LastName),
                 new Claim("UserProfile" , user.ProfilePic ?? "/Uploads/UserImage/NoPhoto.jpg")
            });

            return principal;
        }
    }
}

using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identify.Application.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IIdentityDbContext _dbContext;

        public ProfileService(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectClaim = context.Subject.FindFirst("sub");
            if (!int.TryParse(subjectClaim.Value, out var userId))
                return;

            //retrieve the user from the db
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == default)
                return;

            //create claims that we'll want to leverage in consuming applications
            var emailClaim = new Claim("email_address", user.EmailAddress);
            var usernameClaim = new Claim("username", user.Username);
            var userIdClaim = new Claim("user_id", user.Id.ToString());

            //add the claims to the context
            context.IssuedClaims = new List<Claim> { emailClaim, usernameClaim, userIdClaim };
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectClaim = context.Subject.FindFirst("sub");
            if (!int.TryParse(subjectClaim.Value, out var userId))
                return;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == default)
                return;

            context.IsActive = user.Active;
        }
    }
}

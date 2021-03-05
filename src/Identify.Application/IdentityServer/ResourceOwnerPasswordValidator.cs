using Identify.Application.Password;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identify.Application.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IPasswordValidator _passwordValidator;
        private readonly IIdentityDbContext _dbContext;

        public ResourceOwnerPasswordValidator(IIdentityDbContext dbContext, IPasswordValidator passwordValidator)
        {
            _passwordValidator = passwordValidator;
            _dbContext = dbContext;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //Fetch user's hashed password and compare.
            var username = context.UserName;
            var userSuppliedPassword = context.Password;

            if (!await _passwordValidator.ValidateAsync(username, userSuppliedPassword))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                return;
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            context.Result = new GrantValidationResult(subject: user.Id.ToString(), "ResourceOwnerGrantType");
        }
    }
}

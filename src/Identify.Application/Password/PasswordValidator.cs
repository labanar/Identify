using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IIdentityDbContext _context;
        private readonly IHashingServiceFactory _hashingServiceFactory;
        private readonly ILogger<PasswordValidator> _logger;

        public PasswordValidator(IIdentityDbContext context,
                                 IHashingServiceFactory hashingServiceFactory,
                                 ILogger<PasswordValidator> logger)
        {
            _context = context;
            _hashingServiceFactory = hashingServiceFactory;
            _logger = logger;
        }

        public async Task<bool> ValidateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == default)
            {
                _logger.LogError($"Error validaing user password, a User with the username [{username}] cannot be found.");
                return false;
            }

            var hashingService = await _hashingServiceFactory.CreateHashingService(user.HashedPassword.HashingAlgorithm);
            if (hashingService == default)
            {
                _logger.LogError($"Error validaing user password, hashing service with algorithm [{user.HashedPassword.HashingAlgorithm.ToString()}] cannot be found.");
                return false;
            }

            var reHashed = await hashingService.ComputeHash(password, user.HashedPassword.Salt);
            if (reHashed != user.HashedPassword.HashedValue)
            {
                _logger.LogError($"Error validaing user password, supplied password did not match.");
                return false;
            }

            return true;
        }
    }
}

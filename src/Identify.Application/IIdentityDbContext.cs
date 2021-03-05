using Identify.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application
{
    public interface IIdentityDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserActivationToken> ActivationTokens { get; set; }
        DbSet<UserPasswordResetToken> PasswordResetTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

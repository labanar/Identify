using Identify.Application;
using Identify.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identify.Infrastructure.IdentityDb
{
    public class IdentityDbContext : DbContext, IIdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OneTimeUseToken> OneTimeUseTokens { get; set; }
        public DbSet<UserPasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<UserActivationToken> ActivationTokens { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(options =>
            {
                options.OwnsOne(x => x.HashedPassword);
                options.HasIndex(x => x.EmailAddress).IsUnique();
                options.HasIndex(x => x.Username).IsUnique();
            });

            builder.Entity<UserPasswordResetToken>().HasKey(prt => new { prt.UserId, prt.TokenId });
            builder.Entity<UserPasswordResetToken>()
                .HasOne(x => x.User)
                .WithMany(x => x.PasswordResetTokens)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserActivationToken>().HasKey(at => new { at.UserId, at.TokenId });
            builder.Entity<UserActivationToken>()
                .HasOne(x => x.User)
                .WithMany(x => x.ActivationTokens)
                .HasForeignKey(x => x.UserId);

        }
    }
}

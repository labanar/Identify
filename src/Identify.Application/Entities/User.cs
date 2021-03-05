using Identify.Application.Password;
using System;
using System.Collections.Generic;

namespace Identify.Application.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public Hash HashedPassword { get; set; } = Hash.Empty;
        public bool Active { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime DateLastLogin { get; set; }
        public ICollection<UserPasswordResetToken> PasswordResetTokens { get; set; } = new List<UserPasswordResetToken>();
        public ICollection<UserActivationToken> ActivationTokens { get; set; } = new List<UserActivationToken>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identify.Application
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string recipient, string activationToken);
        Task SendPasswordResetEmail(string recipient, string resetToken);
    }
}

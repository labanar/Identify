using Identify.Application;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identify.Infrastructure
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string WelcomeEmailTemplateId { get; set; }
        public string PasswordResetEmailTemplateId { get; set; }
        public string ActivationLinkFormatString { get; set; }
        public string ResetLinkFormatString { get; set; }
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
    }

    public class SendGridEmailService : IEmailService
    {
        private readonly ILogger<SendGridEmailService> _logger;
        private readonly SendGridOptions _options;

        public SendGridEmailService(IOptions<SendGridOptions> options, ILogger<SendGridEmailService> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendPasswordResetEmail(string recipient, string resetToken)
        {
            try
            {
                var client = new SendGridClient(_options.ApiKey);
                var from = new EmailAddress(_options.SenderAddress, _options.SenderName);
                var message = MailHelper.CreateSingleTemplateEmail(from, new EmailAddress(recipient), _options.PasswordResetEmailTemplateId, new Dictionary<string, string>
                {
                    {"resetLink", string.Format(_options.ResetLinkFormatString, resetToken)}
                });

                await client.SendEmailAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending email password reset email {recipient}");
            }
        }

        public async Task SendWelcomeEmail(string recipient, string activationToken)
        {
            try
            {
                var client = new SendGridClient(_options.ApiKey);
                var from = new EmailAddress(_options.SenderAddress, _options.SenderName);
                var message = MailHelper.CreateSingleTemplateEmail(from, new EmailAddress(recipient), _options.WelcomeEmailTemplateId, new Dictionary<string, string>
                {
                    {"activationLink", string.Format(_options.ActivationLinkFormatString, activationToken)}
                });


                var response = await client.SendEmailAsync(message);
                if (!response.IsSuccessStatusCode)
                    _logger.LogError($"Error sending welcome email [{response.StatusCode}]");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending welcome email");
            }
        }
    }
}

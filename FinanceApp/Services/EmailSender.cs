using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinanceApp.Services
{
    public class EmailSender : IEmailSender
    {
        private static IConfiguration configuration { get; set; }
        private const string URL = "https://api.sendgrid.com/v3/mail/send";
        private string urlParameters = "?api_key=123";

        public EmailSender()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional:true)
                .Build();
        }

        
        private async Task<Response> CreateMessage(string email, string subject, string htmlMessage)
        {
            var apiKey = configuration["SendGrid:ApiKey"];
            var from = new EmailAddress(configuration["SendGrid:From"]);
            var to = new EmailAddress(email);
            SendGridClient client = new SendGridClient(apiKey);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlMessage);

            return await client.SendEmailAsync(msg);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                await CreateMessage(email, subject, htmlMessage);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

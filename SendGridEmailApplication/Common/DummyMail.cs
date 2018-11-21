using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SendGridEmailApplication.Common
{
    public class DummyMail
    {
        public static async Task SendDummyMail()
        {
            var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("test@example.com", "Example User");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("kaushal.pareek@happiestminds.com", "Kaushal"),
                new EmailAddress("surinder.kumar@happiestminds.com", "Surinder"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            var subject = "Sending with SendGrid is Fun";
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var showAllRecipients = false; // Set to true if you want the recipients to see each others email addresses

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from,
                                                                       tos,
                                                                       subject,
                                                                       plainTextContent,
                                                                       htmlContent,
                                                                       showAllRecipients
                                                                       );
            var response = await client.SendEmailAsync(msg);
        }

    }
}
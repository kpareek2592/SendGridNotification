using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridEmailApplication.Models;
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
            var msg = new SendGridMessage();
            var from = new EmailAddress("Kaushal.Pareek@email.com", "Kaushal Pareek");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("kaushal.pareek@happiestminds.com", "Kaushal"),
                new EmailAddress("surinder.kumar@happiestminds.com", "Surinder"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            msg.AddTos(tos,0);

            var cc_emails = new List<EmailAddress>
            {
                new EmailAddress("surinder.kumar@happiestminds.com", "Surinder"),
                new EmailAddress("test6@example.com", "Example User6")
            };
            msg.AddCcs(cc_emails, 0);

            var bcc_emails = new List<EmailAddress>
            {
                new EmailAddress("rahul.varade@happiestminds.com", "Rahul"),
                new EmailAddress("test6@example.com", "Example User6")
            };
            msg.AddBccs(bcc_emails,0);

            msg.SetSubject("This is new subject with Setgrid");

            var dynamicTemplateData = new EmailContract
            {
                Subject = "Sending with SendGrid is Fun",
                Body = "This is the body fo email."
            };
            msg.SetTemplateData(dynamicTemplateData);
            msg.SetBccSetting(true, "snehi.raj@happiestminds.com");

            var subject = "Sending with SendGrid is Fun";
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var showAllRecipients = true; // Set to true if you want the recipients to see each others email addresses

            var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(from,
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridEmailApplication.Models;

namespace SendGridEmailApplication.Common
{
    public class SendGridEmailService
    {
        public static async void SendEmail(EmailContract contract)
        {
            try
            {
                //var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
                var apikey = ConfigurationManager.AppSettings["SendGridApiKey"];
                var client = new SendGridClient(apikey);
                var msg = new SendGridMessage();

                var emailMessage = new SendGridMessage()
                {
                    From = new EmailAddress(contract.FromEmailAddress, contract.Alias),
                    Subject = contract.Subject,
                    HtmlContent = contract.Body
                };

                emailMessage.AddTo(new EmailAddress(contract.ToEmailAddress));
                if (!string.IsNullOrWhiteSpace(contract.BccEmailAddress))
                {
                    emailMessage.AddBcc(new EmailAddress(contract.BccEmailAddress));
                }

                if (!string.IsNullOrWhiteSpace(contract.CcEmailAddress))
                {
                    emailMessage.AddCc(new EmailAddress(contract.CcEmailAddress));
                }

                Response response = await client.SendEmailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //private EmailResponse ProcessResponse(Response response)
        //{
        //    if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted)
        //        || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
        //    {
        //        return ToMailResponse(response);
        //    }

        //    //TODO check for null
        //    var errorResponse = response.Body.ReadAsStringAsync().Result;

        //    throw new EmailServiceException(response.StatusCode.ToString(), errorResponse);
        //}

        //private static EmailResponse ToMailResponse(Response response)
        //{
        //    if (response == null)
        //        return null;

        //    var headers = (HttpHeaders)response.Headers;
        //    var messageId = headers.GetValues(MessageId).FirstOrDefault();
        //    return new EmailResponse()
        //    {
        //        UniqueMessageId = messageId,
        //        DateSent = DateTime.UtcNow,
        //    };
        //}
    }
}
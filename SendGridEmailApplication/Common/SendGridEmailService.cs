using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;

namespace SendGridEmailApplication.Common
{
    public class SendGridEmailService : INotificationSender
    {
        //private volatile static SendGridEmailService sendGridEmailService;
        private static SendGridEmailService sendGridEmailService;
        private SendGridEmailService(){ }

        public static SendGridEmailService InstanceCreation
        {            
            get
            {
                if (sendGridEmailService == null)
                {
                    sendGridEmailService = new SendGridEmailService();
                }
                return sendGridEmailService;
            }
        }

        /// <summary>
        /// Method to send email
        /// </summary>
        /// <param name="contract"></param>
        public async Task SendNotification(NotificationContract notificationcontract) 
        {
            try
            {
                var apikey = ConfigurationManager.AppSettings["SendGridApiKey"];
                var client = new SendGridClient(apikey);

                EmailContract contract = (EmailContract)notificationcontract;

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(contract.From, contract.Alias),
                    Subject = contract.Subject,
                    HtmlContent = contract.Body,
                    PlainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!"
                };

                if (contract.ToEmailAddress != null)
                {
                    string[] split_To = contract.ToEmailAddress.Split(new Char[] { ',', ';' });

                    var toos = new List<EmailAddress>();
                    foreach (var toEmail in split_To)
                    {
                        toos.Add(new EmailAddress(toEmail));
                    }
                    msg.AddTos(toos);
                }

                if (contract.CcEmailAddress != null)
                {
                    string[] split_Cc = contract.CcEmailAddress.Split(new Char[] { ',', ';' });
                    var ccs = new List<EmailAddress>();
                    foreach (var ccEmail in split_Cc)
                    {
                        ccs.Add(new EmailAddress(ccEmail));
                    }
                    msg.AddCcs(ccs); 
                }

                if (contract.BccEmailAddress != null)
                {
                    string[] split_Bcc = contract.BccEmailAddress.Split(new Char[] { ',', ';' });
                    var bccs = new List<EmailAddress>();
                    foreach (var bccEmail in split_Bcc)
                    {
                        bccs.Add(new EmailAddress(bccEmail));
                    }

                    msg.AddBccs(bccs); 
                }

                //using (var fileStream = File.OpenRead(@"D:\TestData\fp_dc_setup_guide.pdf"))
                //{
                //    await msg.AddAttachmentAsync("fp_dc_setup_guide.pdf", fileStream);
                //}

                //Sending the email
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
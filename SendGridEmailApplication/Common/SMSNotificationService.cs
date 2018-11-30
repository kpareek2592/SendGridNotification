using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Clients;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SendGridEmailApplication.Common
{
    public class SMSNotificationService: INotificationSender
    {
        //private volatile static SendGridEmailService sendGridEmailService;
        private static SMSNotificationService m_smsNotificationService; 
        private SMSNotificationService() { }

        public static SMSNotificationService InstanceCreation
        {
            get
            {
                if (m_smsNotificationService == null)
                {
                    m_smsNotificationService = new SMSNotificationService();
                }
                return m_smsNotificationService;
            }
        }

        /// <summary>
        /// Method to send email
        /// </summary>
        /// <param name="contract"></param>
        public async Task SendNotification(NotificationContract notificationcontract)
        {
            // Use your account SID and authentication token instead
            // of the placeholders shown here.
            const string accountSID = "ACdfa7c77b34b80936d3f9511d0ac02777";
            const string authToken = "d1c44577391070452d84adc85011351c";

            // Initialize the TwilioClient.
            TwilioClient.Init(accountSID, authToken);
            TwilioRestClient twilio = new TwilioRestClient(accountSID, authToken);

            try
            {
                // Use the Twilio-provided site for the TwiML response.
                //var url = "http://twimlets.com/message";
                //url = $"{url}?Message%5B0%5D=Hello%20World";

                //// Set the call From, To, and URL values to use for the call.
                //// This sample uses the sandbox number provided by
                //// Twilio to make the call.
                //var call = CallResource.Create(
                //    to: new PhoneNumber("+919529625298"),
                //    from: new PhoneNumber("+13022020271"),
                //    url: new Uri(url));

                // Send an SMS message.
                var message = MessageResource.Create(
                    to: new PhoneNumber("+919529625298"),
                    //to: new PhoneNumber("+919343538518"),
                    from: new PhoneNumber("+13022020271"),
                    body: "This is my SMS message.");
            }
            catch (TwilioException ex)
            {
                // An exception occurred making the REST call
                Console.WriteLine(ex.Message);
            }
            await Task.Delay(10);
        }
    }
}
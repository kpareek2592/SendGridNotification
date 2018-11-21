﻿using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SendGridEmailApplication.Common
{
    public class MultipleReceipients
    {
        public static async Task SendDummyMailToMultipleReceipients()
        {
            var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            // For a detailed description of each of these settings, please see the [documentation](https://sendgrid.com/docs/API_Reference/api_v3.html).

            //msg.AddTo(new EmailAddress("test1@example.com", "Example User 1"));
            var to_emails = new List<EmailAddress>
            {
                new EmailAddress("surinder.kumar@happiestminds.com", "Example User2"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            msg.AddTos(to_emails);

            //msg.AddCc(new EmailAddress("test4@example.com", "Example User 4"));
            var cc_emails = new List<EmailAddress>
            {
                new EmailAddress("kaushal.pareek@happiestminds.com", "Example User5"),
                new EmailAddress("test6@example.com", "Example User6")
            };
            msg.AddCcs(cc_emails);

            //msg.AddBcc(new EmailAddress("test7@example.com", "Example User 7"));
            var bcc_emails = new List<EmailAddress>
            {
                new EmailAddress("snehi.raj@happiestminds.com", "Example User8"),
                new EmailAddress("test9@example.com", "Example User9")
            };
            msg.AddBccs(bcc_emails);

            //msg.AddHeader("X-Test1", "Test1");
            //msg.AddHeader("X-Test2", "Test2");
            var headers = new Dictionary<string, string>()
            {
                { "X-Test3", "Test3" },
                { "X-Test4", "Test4" }
            };
            msg.AddHeaders(headers);

            // If you require complex substitutions this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#transactional-templates).
            //var dynamicTemplateData = new ExampleTemplateData
            //{
            //    Subject = "Hi!",
            //    Name = "Example User",
            //    Location = new Location
            //    {
            //        City = "Birmingham",
            //        Country = "United Kingdom"
            //    }
            //};

            //msg.SetTemplateData(dynamicTemplateData);

            //msg.AddCustomArg("marketing1", "false");
            //msg.AddCustomArg("transactional1", "true");
            var customArgs = new Dictionary<string, string>()
            {
                { "marketing2", "true" },
                { "transactional2", "false" }
            };
            msg.AddCustomArgs(customArgs);

            //msg.SetSendAt(1461775051);

            // If you need to add more [Personalizations](https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html), here is an example of adding another Personalization by passing in a personalization index

            //msg.AddTo(new EmailAddress("test10@example.com", "Example User 10"), 1);
            var to_emails1 = new List<EmailAddress>
            {
                new EmailAddress("surinder.kumar@happiestminds.com", "Example User11"),
                new EmailAddress("test12@example.com", "Example User12")
            };
            msg.AddTos(to_emails1, 1);

            //msg.AddCc(new EmailAddress("test13@example.com", "Example User 13"), 1);
            var cc_emails1 = new List<EmailAddress>
            {
                new EmailAddress("kaushal.pareek@happiestminds.com", "Example User14"),
                new EmailAddress("test15@example.com", "Example User15")
            };
            msg.AddCcs(cc_emails1, 1);

            //msg.AddBcc(new EmailAddress("test16@example.com", "Example User 16"), 1);
            var bcc_emails1 = new List<EmailAddress>
            {
                new EmailAddress("snehi.raj@happiestminds.com", "Example User17"),
                new EmailAddress("test18@example.com", "Example User18")
            };
            msg.AddBccs(bcc_emails1, 1);

            //msg.AddHeader("X-Test5", "Test5", 1);
            //msg.AddHeader("X-Test6", "Test6", 1);
            var headers1 = new Dictionary<string, string>()
            {
                { "X-Test7", "Test7" },
                { "X-Test8", "Test8" }
            };
            msg.AddHeaders(headers1, 1);

            // For a full transactional template example, please see this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#transactional-templates).
            //var dynamicTemplateData2 = new ExampleTemplateData
            //{
            //    Subject = "Hi 2!",
            //    Name = "Example User 2",
            //    Location = new Location
            //    {
            //        City = "Birmingham 2",
            //        Country = "United Kingdom 2"
            //    }
            //};

            //msg.SetTemplateData(dynamicTemplateData2, 1);

            //msg.AddCustomArg("marketing3", "true", 1);
            //msg.AddCustomArg("transactional3", "false", 1);
            var customArgs1 = new Dictionary<string, string>()
            {
                { "marketing4", "false" },
                { "transactional4", "true" }
            };
            msg.AddCustomArgs(customArgs1, 1);

            //msg.SetSendAt(1461775052, 1);

            // The values below this comment are global to entire message

            msg.SetFrom("kaushal.pareek@happiestminds.com", "Example User 0");

            msg.SetSubject("this subject overrides the Global Subject");

            msg.SetGlobalSubject("Sending with SendGrid is Fun");

            msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
            msg.AddContent(MimeType.Html, "<strong>and easy to do anywhere, even with C#</strong>");
            var contents = new List<Content>()
            {
                new Content("text/calendar", "Party Time!!"),
                new Content("text/calendar2", "Party Time2!!")
            };
            msg.AddContents(contents);

            // For base64 encoding, see [`Convert.ToBase64String`](https://msdn.microsoft.com/en-us/library/system.convert.tobase64string(v=vs.110).aspx)
            // For an example using an attachment, please see this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#attachments).
            //msg.AddAttachment("balance_001.pdf",
            //                  "base64 encoded string",
            //                  "application/pdf",
            //                  "attachment",
            //                  "Balance Sheet");
            var attachments = new List<Attachment>()
            {
                new Attachment()
                {
                    Content = "base64 encoded string",
                    Type = "image/png",
                    Filename = "banner.png",
                    Disposition = "inline",
                    ContentId = "Banner"
                },
                new Attachment()
                {
                    Content = "base64 encoded string",
                    Type = "image/png",
                    Filename = "banner2.png",
                    Disposition = "inline",
                    ContentId = "Banner 2"
                }
            };
            msg.AddAttachments(attachments);

            // For a full transactional template example, please see this [use case](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md#transactional-templates).
            //msg.SetTemplateId("d-d42b0eea09964d1ab957c18986c01828");

            //msg.AddGlobalHeader("X-Day", "Monday");
            //var globalHeaders = new Dictionary<string, string>
            //{
            //    { "X-Month", "January" },
            //    { "X-Year", "2017" }
            //};
            //msg.AddGlobalHeaders(globalHeaders);

            //msg.AddSection("%section1", "Substitution for Section 1 Tag");
            //var sections = new Dictionary<string, string>
            //{
            //    {"%section2%", "Substitution for Section 2 Tag"},
            //    {"%section3%", "Substitution for Section 3 Tag"}
            //};
            //msg.AddSections(sections);

            //msg.AddCategory("customer");
            //var categories = new List<string>
            //{
            //    "vip",
            //    "new_account"
            //};
            //msg.AddCategories(categories);

            //msg.AddGlobalCustomArg("campaign", "welcome");
            //var globalCustomArgs = new Dictionary<string, string>
            //{
            //    { "sequence2", "2" },
            //    { "sequence3", "3" }
            //};
            //msg.AddGlobalCustomArgs(globalCustomArgs);

            //msg.SetAsm(3, new List<int>() { 1, 4, 5 });

            //msg.SetGlobalSendAt(1461775051);

            //msg.SetIpPoolName("23");

            // This must be a valid [batch ID](https://sendgrid.com/docs/API_Reference/SMTP_API/scheduling_parameters.html)
            //msg.SetBatchId("some_batch_id");

            msg.SetBccSetting(true, "kaushal.pareek@happiestminds.com");

            msg.SetBypassListManagement(true);

            msg.SetFooterSetting(true, "Some Footer HTML", "Some Footer Text");

            msg.SetSandBoxMode(true);

            //msg.SetSpamCheck(true, 1, "https://gotchya.example.com");

            //msg.SetClickTracking(true, false);

            //msg.SetOpenTracking(true, "Optional tag to replace with the open image in the body of the message");

            //msg.SetSubscriptionTracking(true,
            //                           "HTML to insert into the text / html portion of the message",
            //                           "text to insert into the text/plain portion of the message",
            //                           "substitution tag");

            //msg.SetGoogleAnalytics(true,
            //                       "some campaign",
            //                       "some content",
            //                       "some medium",
            //                       "some source",
            //                       "some term");

            msg.SetReplyTo(new EmailAddress("test+kaushal.pareek@happiestminds.com", "Reply To Me"));

            var response = await client.SendEmailAsync(msg);

        }
    }
}
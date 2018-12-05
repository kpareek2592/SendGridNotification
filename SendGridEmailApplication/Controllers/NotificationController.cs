using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGridEmailApplication.Common;
using SendGridEmailApplication.Factory;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;
using SendGridEmailApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SendGridEmailApplication.Controllers
{
    public class NotificationController : ApiController
    {
        INotificationSender m_NotificationSenderType = null;
        IEmailSender emailSender = null;
        ISmsSender smsSender = null;

        EmailSenderFactory emailSenderFactory = null;
        SmsSenderFactory smsSenderFactory = null;
        NotificationSenderFactory m_NotificationFactory = null;  

        public NotificationController()
        {
            m_NotificationFactory = new NotificationSenderFactory();
            emailSenderFactory = new EmailSenderFactory();
            smsSenderFactory = new SmsSenderFactory();
        }

        //[HttpPost]
        //[Route("api/email")]
        //public async Task<HttpResponseMessage> SendEmail()
        //{
        //    var provider = GetMultipartProvider();
        //    var result = await Request.Content.ReadAsMultipartAsync(provider);

        //    // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
        //    // so this is how you can get the original file name
        //    var originalFileName = GetDeserializedFileName(result.FileData.First());

        //    // uploadedFileInfo object will give you some additional stuff like file length,
        //    // creation time, directory name, a few filesystem methods etc..
        //    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
        //    HttpRequestMessage request = this.Request;

        //    var data = HttpContext.Current.Request.Params["data"];
        //    //if (data == null)
        //    // return new ActionResult(new ResponseModel() { Status = false, StatusCode = 111, Reason = "Param Not found data" }, Request, HttpStatusCode.BadRequest);
        //    var contract = new EmailContract();
        //    var content = HttpContext.Current.Request.Params["content"];
        //    var jsonContent = JObject.Parse(content);
        //    //contract = JsonConvert.DeserializeObject(jsonContent);

        //    try
        //    {
        //        await MultipleReceipients.SendEmail(data, content);
        //        var message = Request.CreateResponse(HttpStatusCode.OK);
        //        return message;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //        return message;
        //    }
        //}

        //[HttpPost]
        //[Route("api/notification/{provider}")]
        //public async Task<HttpResponseMessage> SendEmail([FromUri]NotificationType notificationType, EmailContract contract)
        //{
        //    m_NotificationSenderType = null;
        //    m_NotificationSenderType = m_NotificationFactory.NotificationSender(notificationType);
        //    try
        //    {
        //        await this.m_NotificationSenderType.SendNotification(contract);
        //        var message = Request.CreateResponse(HttpStatusCode.OK);
        //        return message;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //        return message;
        //    }
        //}

        //[HttpPost]
        //[Route("api/email/{provider}")]
        //public async Task<HttpResponseMessage> SendEmail([FromUri]EmailProviders provider, HttpPostedFileBase attachment, [FromBody]EmailContract contract)
        //{
        //    emailSender = null;
        //    emailSender = emailSenderFactory.EmailSender(provider);
        //    try
        //    {
        //        await this.emailSender.SendEmail(contract);
        //        var message = Request.CreateResponse(HttpStatusCode.OK);
        //        return message;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //        return message;
        //    }
        //}

        //[HttpPost]
        //[Route("api/email/{provider}")]
        //public async Task<HttpResponseMessage> SendEmail([FromUri]EmailProviders provider)
        //{
        //    emailSender = null;
        //    emailSender = emailSenderFactory.EmailSender(provider);
        //    try
        //    {
        //        HttpRequestMessage request = this.Request;
                
        //        var data = HttpContext.Current.Request.Params["data"];
        //        //if (data == null)
        //        // return new ActionResult(new ResponseModel() { Status = false, StatusCode = 111, Reason = "Param Not found data" }, Request, HttpStatusCode.BadRequest);

        //        var content = HttpContext.Current.Request.Params["content"];

        //        try
        //        {
        //        await this.emailSender.SendEmail(contract);
        //        var message = Request.CreateResponse(HttpStatusCode.OK);
        //        return message;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //        return message;
        //    }
        //}

        [HttpPost]
        [Route("api/sms/{provider}")]
        public async Task<HttpResponseMessage> SendSms([FromUri]SmsProviders provider, SmsContract contract)
        {
            smsSender = null;
            smsSender = smsSenderFactory.SmsSender(provider);
            try
            {
                await this.smsSender.SendSms(contract);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        [HttpPost]
        [Route("api/fileUpload")]
        public async Task<HttpResponseMessage> UploadAttachment()
        {
            AttachmentEmail email = new AttachmentEmail();
            HttpResponseMessage result = null;
            string filespath = null;
            HttpContext context = null;

            var httpRequest = HttpContext.Current.Request;
            var stringContract = HttpContext.Current.Request.Params["content"];
            EmailContract contract = JsonConvert.DeserializeObject<EmailContract>(stringContract);

            //try
            //{
            //    List<HttpPostedFile> files = (List<HttpPostedFile>)context.Cache[context.Request.Params["files"]];

            //    HttpPostedFile postedFile = context.Request.Files["Filedata"];
            //    files.Add(postedFile);
            //    string filename = postedFile.FileName;
            //    context.Response.Write(filename);
            //    context.Response.StatusCode = 200;

            //    await email.AttachEmail(filespath, contract);
            //    var message = Request.CreateResponse(HttpStatusCode.OK);
            //    return message;
            //}

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                //var filepath = HttpContext.Current.Server.MapPath("~/" + httpRequest.Files[file].FileName);
            }
            try
            {
                await email.AttachEmail(contract);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

    }
}

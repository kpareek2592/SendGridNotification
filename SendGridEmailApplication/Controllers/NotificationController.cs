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

        [HttpPost]
        [Route("api/email/{provider}")]
        public async Task<HttpResponseMessage> SendEmail([FromUri]EmailProviders provider, EmailContract contract)
        {
            emailSender = null;
            emailSender = emailSenderFactory.EmailSender(provider);
            try
            {
                await this.emailSender.SendEmail(contract);
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
    }
}

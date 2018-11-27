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
    public class EmailController : ApiController
    {
        INotificationSender notificationSender = null;

        public EmailController()
        {

        }

        [HttpPost]
        [Route("api/sendgrid/{notificationType}")]
        public async Task<HttpResponseMessage> SendEmail([FromUri]NotificationType notificationType, EmailContract contract)
        {
            NotificationSenderFactory factory = new NotificationSenderFactory();
            this.notificationSender = factory.NotificationSender(notificationType);
            try
            {
                await this.notificationSender.SendEmail(contract);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        [HttpGet]
        [Route("api/dummyemail")]
        public async Task<HttpResponseMessage> SendEmail()
        {
            try
            {
                await DummyMail.SendDummyMail();
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }

        }

        [HttpGet]
        [Route("api/multiemail")]
        public async Task<HttpResponseMessage> SendDummyMailToMultipleReceipients()
        {
            try
            {
                await MultipleReceipients.SendDummyMailToMultipleReceipients();
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

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
        NotificationSenderFactory m_NotificationFactory = null;  

        public NotificationController()
        {
            m_NotificationFactory = new NotificationSenderFactory();
        }

        [HttpPost]
        [Route("api/notification/{notificationType}")]
        public async Task<HttpResponseMessage> SendEmail([FromUri]NotificationType notificationType, EmailContract contract)
        {
            m_NotificationSenderType = null;
            m_NotificationSenderType = m_NotificationFactory.NotificationSender(notificationType);
            try
            {
                await this.m_NotificationSenderType.SendNotification(contract);
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

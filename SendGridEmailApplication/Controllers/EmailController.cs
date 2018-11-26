using SendGridEmailApplication.Common;
using SendGridEmailApplication.FactoryCreator;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;
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
        private readonly IEmail _emailService;
        //Creator creator = new ConcreteCreator();

        //IEmail email = new SendEmailType("SendGrid");

        //public EmailController(IEmail emailService)
        //{
        //    _emailService = emailService;
        //}

        public EmailController()
        {

        }

        [HttpPost]
        [Route("api/sendgrid")]
        public async Task<HttpResponseMessage> SendEmail(EmailContract contract)
        {
            try
            {
                await SendGridEmailService.SendEmail(contract);
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

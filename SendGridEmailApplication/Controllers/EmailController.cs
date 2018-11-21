using SendGridEmailApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SendGridEmailApplication.Controllers
{

    public class EmailController : ApiController
    {
        private readonly SendGridEmailService _sendGridEmailService;

        public EmailController(SendGridEmailService sendGridEmailService)
        {
            _sendGridEmailService = sendGridEmailService;
        }

        public EmailController()
        {

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SendGridEmailApplication.Controllers;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;
using SendGridEmailApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGridEmailApplication.Tests.Controllers
{
    [TestClass]
    class NotificationControllerTest
    {
        private Mock<EmailContract> _emailContract;
        private Mock<INotificationSender> _notificationSender;
        private Mock<NotificationController> _controller;
        private Mock<SmsContract> _smsContract;

        [TestInitialize]
        public void Init()
        {
            _controller = new Mock<NotificationController>();
            _emailContract = new Mock<EmailContract>();
            _smsContract = new Mock<SmsContract>();
            _notificationSender = new Mock<INotificationSender>();
        }

        //[TestMethod]
        //public void SendEmail_Test1()
        //{
        //    //Arrange
        //    //var _mockController = new Mock<NotificationController> { CallBase = true };

        //    //var _controller = _mockController.Object;
        //    _controller.Setup(x => x.SendEmail()).Returns(new Delegate);


        //    //Act
        //    _mockController.sen

        //    Assert
        //}

        //[TestMethod]
        //public void SendEmail_Test2()
        //{
        //    //Arrange
        //    //var _mockController = new Mock<NotificationController> { CallBase = true };
        //    var notificationType = new 

        //    //var _controller = _mockController.Object;
        //    _controller.Setup(x => x.Se)


        //    //Act
        //    _mockController.sen

        //    Assert
        //}

        #region Private Field
        private EmailContract GetEmailContract()
        {
            return new EmailContract()
            {
              From = "Kaushal.Pareek@email.com",
              Subject = "Test Email from Send Grid",
              Body = "This is a test email sent via test grid",
              ToEmailAddress = "surinder.kumar@happiestminds.com,test3@example.com",
              CcEmailAddress = "kpareek2592@gmail.com;test6@example.com",
              BccEmailAddress = "snehi.raj@happiestminds.com,kaushal.pareek@happiestminds.com"
            };
        }

        private EmailProviders GetEmailProviders()
        {
            return new EmailProviders()
            {

            };
        }

        #endregion
    }
}

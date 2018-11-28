using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SendGridEmailApplication.Controllers;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGridEmailApplication.Tests.Controllers
{
    [TestClass]
    class EmailControllerTest
    {
        private Mock<EmailContract> _emailContract;
        private Mock<INotificationSender> _notificationSender;

        [TestInitialize]
        public void Init()
        {
            _emailContract = new Mock<EmailContract>();
            _notificationSender = new Mock<INotificationSender>();
        }

        //[TestMethod]
        //public void SendEmail()
        //{
        //    //Arrange
        //    var _mockController = new Mock<EmailController> { CallBase = true };
            
        //    var _controller = _mockController.Object;
        //    Mock<INotificationSender> notificationSender = new Mock<INotificationSender>();
        //    notificationSender.Setup(x => x.SendEmail(contract)).ret

        //    //Act
        //    _mockController.sen

        //    //Assert
        //}
    }
}

using SendGridEmailApplication.Common;
using SendGridEmailApplication.Interface;
using SendGridEmailApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridEmailApplication.Factory
{
    public class NotificationSenderFactory
    {
        public virtual INotificationSender NotificationSender(NotificationType notificationType)
        {
            INotificationSender notificationSender = null;

            switch (notificationType)
            {
                case NotificationType.Email:
                    notificationSender = new SendGridEmailService();
                    break;
                case NotificationType.SMS:
                    break;
                case NotificationType.Push:
                    break;
                default:
                    throw new ArgumentException("Invalid notification type");
            }

            return notificationSender;
        }
    }
}
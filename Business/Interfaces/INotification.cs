using Business.Notifications;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface INotification
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}

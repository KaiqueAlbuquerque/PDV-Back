using Business.Interfaces;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Api.Controllers
{
    public abstract class MainController : ControllerBase
    {
        private readonly INotification _notification;

        protected MainController(INotification notification)
        {
            _notification = notification;
        }

        protected bool ValidOperation()
        {
            return !_notification.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            var message = _notification.GetNotifications().Select(n => n.Message).FirstOrDefault();

            return BadRequest(message);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModelError(modelState);
            return CustomResponse();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificationError(errorMsg);
            }
        }

        protected void NotificationError(string message)
        {
            _notification.Handle(new Notification(message));
        }
    }
}

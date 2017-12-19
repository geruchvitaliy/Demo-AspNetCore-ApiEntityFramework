using Common.Attributes;
using MediatR;
using Newtonsoft.Json.Linq;
using System;

namespace HistoryHandler
{
    [Service]
    class HistoryHandler<T> : NotificationHandler<T> where T : Common.Events.Event
    {
        protected override void HandleCore(T notification)
        {
            var caption = $"Event type: {notification.GetType().Name}, user id: {notification.UserId}";
            var body = JObject.FromObject(notification);

            //TODO: Write history into database, storage, etc
            System.Diagnostics.Debug.WriteLine(caption + Environment.NewLine + body);
        }
    }
}
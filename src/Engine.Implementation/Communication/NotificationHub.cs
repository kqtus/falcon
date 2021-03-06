﻿using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;

namespace Falcon.Engine.Implementation.Communication
{
    public class NotificationHub : INotificationHub
    {
        protected Dictionary<string, List<Notification>> Subscribers;

        public NotificationHub()
        {
            Subscribers = new Dictionary<string, List<Notification>>();
        }

        public void Subscribe(string topic, Notification callback)
        {
            if (!Subscribers.ContainsKey(topic))
            {
                Subscribers.Add(topic, new List<Notification>());
            }

            Subscribers[topic].Add(callback);
        }

        public bool Unsubscribe(string topic, Notification callback)
        {
            if (!Subscribers.ContainsKey(topic))
            {
                return false;
            }

            return Subscribers[topic].Remove(callback);
        }

        public bool DispatchNotification(string topic, object msg, object sender)
        {
            if (!Subscribers.ContainsKey(topic))
            {
                return false;
            }

            foreach (var callback in Subscribers[topic])
            {
                callback(msg, sender);
            }

            return true;
        }
    }
}

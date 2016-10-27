using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LiteBroker
{
    public static class Broker
    {
        private static readonly object locker = new object();

        static Broker()
        {
            Subscriptions = new List<Subscription>();
        }

        private static List<Subscription> Subscriptions { get; set; }

        public static void Subscribe<T>(this object sender, Action<T> action)
        {   
            lock(locker)
            {
                if (IsSubscribed<T>(sender))
                    return;

                var subscription = new Subscription(sender, action, typeof(T));
                Subscriptions.Add(subscription);
            }
        }

        public static void Unsubscribe<T>(this object sender)
        {
            lock(locker)
            {
                var subscription = Subscriptions.FirstOrDefault(x => (x.Subscriber.Target == sender && x.Type == typeof(T)));
                
                if (subscription != null)
                    Subscriptions.Remove(subscription);
            }
        }

        public static void Publish<T>(this T sender)
        {
            lock(locker)
            {
                Cleanup();

                var subscriptions = Subscriptions.Where(x => x.Type.IsAssignableFrom(sender.GetType()));

                foreach (var subscription in subscriptions) 
                    ((Action<T>)subscription.Action)(sender);
            }
        }
        
        private static bool IsSubscribed<T>(this object sender)
        {
            var subscription = Subscriptions.FirstOrDefault(x => (x.Subscriber.Target == sender && x.Type == typeof(T)));
            return subscription != null;
        }

        private static void Cleanup()
        { 
            Subscriptions.RemoveAll(x => { return !x.Subscriber.IsAlive; });
        }
    }
}


using System;
using System.Collections.Generic;

namespace EventOperatorDemo
{
    public class EventHelper
    {
        private static readonly Dictionary<Delegate, bool> registeredHandlers = new();

        public static void AddUnique<T>(ref EventHandler<T> eventRef, EventHandler<T> handler) where T : EventArgs
        {
            if (!registeredHandlers.ContainsKey(handler))
            {
                eventRef += handler;
                registeredHandlers[handler] = true;
                Console.WriteLine("Handler added uniquely.");
            }
            else
            {
                Console.WriteLine("Handler already registered, ignoring.");
            }
        }

        public static void AddWeak<T>(ref EventHandler<T> eventRef, T target, Action<object?, T> handler) where T : EventArgs
        {
            var weakRef = new WeakReference(target);
            EventHandler<T> wrapper = null!;
            wrapper = (s, e) =>
            {
                if (weakRef.Target is not null)
                {
                    handler(s, e);
                }
                else
                {
                    eventRef -= wrapper;
                }
            };
            eventRef += wrapper;
            Console.WriteLine("Weak handler registered.");
        }
    }

    class Program
    {
        public static event EventHandler<EventArgs> DemoEvent;

        static void Main()
        {
            EventHandler<EventArgs> handler = (s, e) => Console.WriteLine("Handled!");

            // Simulated usage of +==
            EventHelper.AddUnique(ref DemoEvent, handler);
            EventHelper.AddUnique(ref DemoEvent, handler); // should be ignored

            // Simulated usage of *=
            var target = new object();
            EventHelper.AddWeak(ref DemoEvent, EventArgs.Empty, (s, e) => Console.WriteLine("Weak handled!"));

            DemoEvent?.Invoke(null, EventArgs.Empty);
        }
    }
}

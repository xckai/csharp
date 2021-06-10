using System;
using System.Collections.Generic;
using System.Threading;

namespace EventTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            var messsageManager = new CustomEvents<EventArgs>();
            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(() =>
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    messsageManager.Event += OnMessage;
                });
                thread.Start();
            }
            while (true)
            {
                messsageManager.Raise(new EventArgs()
                {
                    message = "haha"
                });
                Thread.Sleep(5000);
            }
        }

       static private void OnMessage(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Thread is {Thread.CurrentThread.ManagedThreadId}, received {e.message}");

        }

    }

    public class EventArgs
    {
        public string sender;
        public string message;
    }

    internal class MessageManager
    {
        public event EventHandler<EventArgs> MsgEvent;
        protected virtual void onNewMessage(EventArgs e)
        {
            if (MsgEvent != null) MsgEvent(this, e);
        }

        public void Simulate(string sender, string message)
        {
            onNewMessage(new EventArgs(){sender = sender,message = message});
        }
    }
    public sealed  class EventKey{}
    public sealed class EventSet<T>
    {
        private readonly  Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();

        public void Add(EventKey eventKey, Delegate handler)
        {
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey, out d);
            m_events[eventKey] = Delegate.Combine(d,handler);
            Monitor.Exit(m_events);
        }
        public void Remove(EventKey eventKey, Delegate handler)
        {
            Monitor.Enter(m_events);
            Delegate d;
            if (m_events.TryGetValue(eventKey, out d))
            {
                d = Delegate.Remove(d,handler);
                if (d != null) m_events[eventKey] = d;
                else m_events.Remove(eventKey);
            }
            Monitor.Exit(m_events);
        }

        public void Raise(EventKey eventKey, Object sender, T e)
        {
            Delegate d;
            Monitor.Enter(m_events);
            m_events.TryGetValue(eventKey, out d);
            Monitor.Exit(m_events);
            if (d != null)
            {
                d.DynamicInvoke(new object[] {sender, e});
            }
        }
    }

    public class CustomEvents<T>
    {
        private readonly  EventSet<T> mEventSet = new EventSet<T>();
        protected EventSet<T> Eventset
        {
            get { return mEventSet; }
        }
        protected  static  readonly  EventKey cusEventKey = new EventKey();
        public event EventHandler<T> Event
        {
            add{mEventSet.Add(cusEventKey,value);}
            remove{mEventSet.Remove(cusEventKey,value);}
        }

        public void Raise(T e)
        {
            mEventSet.Raise(cusEventKey,this, e);
        }
    }
}

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Golem
{
    public sealed class ScheduleEventArgs : EventArgs, IComparable<ScheduleEventArgs>
    {
        public readonly Action method;

        public readonly float time;

        public ScheduleEventArgs(Action method, float time)
        {
            this.method = method;
            this.time = time;
        }

        public static ScheduleEventArgs CreateEventArgs(Action method, float time)
        {
            return new ScheduleEventArgs(method, time);
        }

        public int CompareTo(ScheduleEventArgs other)
        {
            Debug.Log("sorted");

            return time > other.time ? 1 : 0;
        }
    }

    public sealed class Schedule : Singleton<Schedule>
    {
        private readonly Queue<ScheduleEventArgs> m_ScheduledEvents = new Queue<ScheduleEventArgs>();

        public void Add(Action method, float time)
        {
            Add(ScheduleEventArgs.CreateEventArgs(method, time));
        }

        public void Add(ScheduleEventArgs args)
        {
            m_ScheduledEvents.Enqueue(args);
        }

        private void Update()
        {
            if (m_ScheduledEvents.Count > 0)
            {
                var current = m_ScheduledEvents.Peek();

                if (current.time <= Time.time)
                {
                    current.method();

                    Remove();
                }
            }
        }

        private void Remove()
        {
            m_ScheduledEvents.Dequeue();

            if (m_ScheduledEvents.Count <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

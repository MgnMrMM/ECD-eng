using System;
using System.Collections.Generic;
using ECD_Engine.Components;

namespace ECD_Engine.Patterns
{
    public interface IObserver
    {
        void OnNotify(Message message);
        void OnNotify<T>(Message message, T eventData);
        void OnNotify<T, Y>(Message message, T eventData, Y eventData2);
    }

    public class Subject : Singleton<Subject>
    {
        private readonly List<IObserver> observers = new List<IObserver>();
        public Action<IObserver> AddObserver, RemoveObserver;

        public Subject()
        {
            AddObserver = observer => observers.Add(observer);
            RemoveObserver = observer => observers.Add(observer);
        }

        public void Notify(Message message)
        {
            observers.ForEach(observer => observer.OnNotify(message));
        }

        public void Notify<T>(Message message, T eventData)
        {
            observers.ForEach(observer => observer.OnNotify<T>(message, eventData));
        }

        public void Notify<T, Y>(Message message, T eventData, Y eventData2)
        {
            observers.ForEach(observer => observer.OnNotify<T, Y>(message, eventData, eventData2));
        }


    }


}

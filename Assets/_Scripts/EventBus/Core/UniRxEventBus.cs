using System;
using UniRx;

namespace _Scripts.EventBus
{
    public class UniRxEventBus : IEventBus
    {
        public IObservable<T> OnEvent<T>()
        {
            return MessageBroker.Default.Receive<T>();
        }

        public void Publish<T>(T evt)
        {
            MessageBroker.Default.Publish(evt);
        }
    }
}
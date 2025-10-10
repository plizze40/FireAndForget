using System;
using System.Collections.Generic;

namespace Core.Messages
{
    public class MessageBus
    {
        private Dictionary<Type, List<Delegate>> _subscribers = new();

        public event Action<GameMessage> OnMessageSent;

        public void Subscribe<T>(Action<T> handler) where T : GameMessage
        {
            Type messageType = typeof(T);

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers.Add(messageType, new List<Delegate>());
            }

            _subscribers[messageType].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : GameMessage
        {
            Type messageType = typeof(T);

            if (_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType].Remove(handler);
            }
        }

        public void Send<T>(T message) where T : GameMessage
        {
            Type messageType = typeof(T);
            
            OnMessageSent?.Invoke(message);

            if (_subscribers.TryGetValue(messageType, out var subscriber))
            {
                foreach (var handler in subscriber)
                {
                    ((Action<T>)handler)(message);
                }
            }
        }
    }
}
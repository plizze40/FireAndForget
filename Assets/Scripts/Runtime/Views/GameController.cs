using System;
using Core;
using Core.Messages;
using UnityEngine;

namespace Runtime.Views
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private WorldRenderer _renderer;
        [SerializeField] private Transform _player;

        private MessageBus _messageBus;
        private World _world;

        private void Start()
        {
            _messageBus = new MessageBus();

            _messageBus.OnMessageSent += (msg) =>
            {
                Debug.Log($"Message Sent: {msg.GetType().Name}");
            };
            
            _world = new World(_messageBus);
            _renderer.Initialize(_messageBus);
        }

        private void FixedUpdate()
        {
            _world?.Update(TimeSpan.FromSeconds(Time.fixedDeltaTime));
        }
    }
}
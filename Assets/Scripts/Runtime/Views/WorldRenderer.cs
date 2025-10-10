using Core.Messages;
using UnityEngine;

namespace Runtime.Views
{
    public class WorldRenderer : MonoBehaviour
    {
        private MessageBus _messageBus;

        public void Initialize(MessageBus messageBus)
        {
            _messageBus = messageBus;
            _messageBus.Subscribe<CreatureSpawnedMessage>(OnCreatureSpawned);
        }

        private void OnCreatureSpawned(CreatureSpawnedMessage msg)
        {
            
        }
    }
}
using Core.Messages;

namespace Core
{
    public sealed class World
    {
        private readonly MessageBus _messageBus;

        public World(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        
        
    }
}
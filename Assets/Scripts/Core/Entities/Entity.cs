using System;

namespace Core.Entities
{
    public abstract class Entity
    {
        protected Entity(World world)
        {
            World = world;
        }
        
        public Position Position { get; set; }
        public World World { get; }
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual void Update(TimeSpan tick)
        {
            
        }
    }
}
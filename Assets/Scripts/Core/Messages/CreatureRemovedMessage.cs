namespace Core.Messages
{
    public class CreatureRemovedMessage : GameMessage
    {
        public int CreatureId { get; set; }
    }
}
namespace Core.Messages
{
    public class CreatureMovedMessage : GameMessage
    {
        public int CreatureId;
        public int X;
        public int Y;
    }
}
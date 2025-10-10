namespace Core.Messages
{
    public class CreatureSpawnedMessage : GameMessage
    {
        public int CreatureId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string AppearanceId { get; set; }
    }
}
namespace Core.Messages
{
    public class GameMessage
    {
        public string MessageType { get; set; }
        public float Timestamp { get; set; }
        
        protected GameMessage()
        {
            MessageType = GetType().Name;
            Timestamp = UnityEngine.Time.time;
        }
    }
}
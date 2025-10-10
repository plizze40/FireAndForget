namespace Core.Entities
{
    public class ItemData
    {
        public int Id { get; }
        public string Name { get; }
        public int Attack { get; }
        public int Defense { get; }

        public ItemData(int id, string name, int attack, int defense)
        {
            Id = id;
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }
}
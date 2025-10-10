using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Item")]
    public class ItemSO : ScriptableObject
    {
        public int itemId;           
        public string itemName;      
        public string description;
        public int attack;
        public int defense;
        public Sprite icon;
    }
}
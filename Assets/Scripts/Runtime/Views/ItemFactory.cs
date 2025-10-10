using System.Collections.Generic;
using Core.Entities;
using Data;

namespace Runtime.Views
{
    public static class ItemFactory
    {
        public static ItemData Create(ItemSO so)
        {
            return new ItemData(so.itemId, so.itemName, so.attack, so.defense);
        }

        public static List<ItemData> CreateAll(List<ItemSO> itemsSO)
        {
            var result = new List<ItemData>();
            foreach (var so in itemsSO)
                result.Add(Create(so));
            return result;
        }
    }
}
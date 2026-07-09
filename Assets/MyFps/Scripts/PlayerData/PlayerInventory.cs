using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyFps
{
    public enum ItemType
    {
        DoorKey,
        EyePuzzleR,
        EyePuzzleL
    }

    public class PlayerInventory : PersistentSingleton<PlayerInventory>
    {
        private HashSet<ItemType> items = new();

        //public event Action OnInventoryChanged;

        public void AddItem(ItemType item)
        {
            if (items.Add(item))
            {
                Debug.Log($"{item} 획득");

                //OnInventoryChanged?.Invoke();
            }
        }

        public void RemoveItem(ItemType item)
        {
            if (items.Remove(item))
            {
                Debug.Log($"{item} 사용");

                //OnInventoryChanged?.Invoke();
            }
        }

        public bool HasItem(ItemType item)
        {
            return items.Contains(item);
        }
    }
}
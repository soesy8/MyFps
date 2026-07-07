using UnityEngine;

namespace MyFps
{
    public class GetKey : Interactable
    {
        public override void Interact(PlayerInteraction player)
        {
            PlayerInventory.Instance.AddItem(ItemType.DoorKey);

            Debug.Log("Get Key in Inventory");
            Destroy(gameObject);
        }
    }
}
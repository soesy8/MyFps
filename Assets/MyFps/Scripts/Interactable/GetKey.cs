using UnityEngine;

namespace MyFps
{
    public class GetKey : Interactable
    {
        [SerializeField] private ItemType dropKey;
        public override void Interact(PlayerInteraction player)
        {
            PlayerInventory.Instance.AddItem(dropKey);

            Debug.Log("Get Key in Inventory");
            Destroy(gameObject);
        }
    }
}
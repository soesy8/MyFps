using UnityEngine;

namespace MyFps
{
    public class EyePuzzle : Interactable
    {
        [SerializeField] private ItemType puzzleType;

        public override void Interact(PlayerInteraction player)
        {
            PlayerInventory.Instance.AddItem(puzzleType);

            Debug.Log($"Get {puzzleType} in Inventory");
            Destroy(gameObject);
        }
    }
}
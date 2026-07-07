using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFps
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private bool interactByTrigger;

        [SerializeField] private string interactionText = "InteractionText";

        public string InteractionText => interactionText;

        public bool InteractByTrigger => interactByTrigger;

        public abstract void Interact(PlayerInteraction player);

        private void OnTriggerEnter(Collider other)
        {
            if (!interactByTrigger) return;

            if (other.gameObject.tag != "Player") return;

            PlayerInteraction player = other.GetComponent<PlayerInteraction>();

            if (player != null)
            {
                Interact(player);
            }
        }
    }
}
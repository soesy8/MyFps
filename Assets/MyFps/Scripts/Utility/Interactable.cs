using UnityEngine;

namespace MyFps
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string interactionText = "InteractionText";

        public string InteractionText => interactionText;

        public abstract void Interact();
    }
}
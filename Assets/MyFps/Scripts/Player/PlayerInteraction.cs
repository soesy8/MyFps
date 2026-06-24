using UnityEngine;

namespace MyFps
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 2f;

        private CharacterInput input;

        private void Awake()
        {
            input = GetComponent<CharacterInput>();
        }
    }
}
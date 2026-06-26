using UnityEngine;

namespace MyFps
{
    public class SlideDoor : MonoBehaviour
    {
        public Animator animator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetBool("IsOpen", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetBool("IsOpen", false);
            }
        }
    }
}
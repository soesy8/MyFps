using UnityEngine;

namespace MyFps
{
    public class DamageEffect : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] hitSounds;

        public void Play()
        {
            animator.Play("DamageAnim", 0, 0f);

            PlayRandomSound();
        }

        private void PlayRandomSound()
        {
            if (hitSounds.Length == 0)
                return;

            int index = Random.Range(0, hitSounds.Length);

            audioSource.PlayOneShot(hitSounds[index]);
        }
    }
}
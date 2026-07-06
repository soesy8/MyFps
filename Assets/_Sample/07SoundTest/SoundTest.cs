using UnityEngine;

namespace MySample
{
    public class SoundTest : MonoBehaviour
    {
        //사운드 플레이 테스트 예제

        public AudioClip clip;

        [SerializeField] private float volume = 1f;
        [SerializeField] private float pitch = 1f;
        [SerializeField] private bool isLoop = false;
        [SerializeField] private bool playOnAwake = false;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource= gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = isLoop;
            audioSource.playOnAwake = playOnAwake;
        }

        private void Start()
        {
            audioSource.Play();
        }

    }
}
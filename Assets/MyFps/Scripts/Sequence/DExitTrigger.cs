using UnityEngine;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private AudioSource jumpScare;

        private void OnTriggerEnter(Collider other)
        {
            jumpScare.Stop();
            fader.FadeTo("PlayScene02");
        }
    }
}
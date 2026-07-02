using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    public class DExitTrigger : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;

        private void OnTriggerEnter(Collider other)
        {
            fader.FadeTo("PlayScene02");
        }
    }
}
using UnityEngine;

namespace MyFps
{
    public class FEscapeTrigger : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private string loadToScene = "MainMenu";

        private void OnTriggerEnter(Collider other)
        {
            AudioManager.Instance.Stop("Bgm01");
            fader.FadeTo(loadToScene);
        }
    }
}
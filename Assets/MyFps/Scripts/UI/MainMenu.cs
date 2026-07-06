using UnityEngine;

namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        private AudioManager audioManager;

        public SceneFader fader;
        [SerializeField] private string loadScene = "PlayScene01";

        private void Awake()
        {
            audioManager = AudioManager.Instance;
        }

        private void Start()
        {
            //배경음 플레이
            audioManager.PlayBgm("MenuBgm");
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            audioManager.Stop("MenuBgm");
            audioManager.Play("MenuButton");
            fader.FadeTo(loadScene);
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
            audioManager.Play("MenuButton");
        }

        public void Options()
        {
            Debug.Log("Options");
            audioManager.Play("MenuButton");
        }

        public void Credits()
        {
            Debug.Log("Credits");
            audioManager.Play("MenuButton");
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame");
            audioManager.Play("MenuButton");
            fader.FadeTo("");
        }
    }
}
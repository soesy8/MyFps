using UnityEngine;

namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        public SceneFader fader;

        public void StartGame()
        {
            fader.FadeTo("PlayScene");
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
        }

        public void Options()
        {
            Debug.Log("Options");
        }

        public void Credits()
        {
            Debug.Log("Credits");
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame");
            fader.FadeTo("");
        }
    }
}
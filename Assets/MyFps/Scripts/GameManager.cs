using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyFps
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneFader fader;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject gameoverUI;
        [SerializeField] private InputActionReference pauseAction;

        private void Awake()
        {
            //fader = GetComponent<SceneFader>();
            fader = FindFirstObjectByType<SceneFader>();
        }

        private void Update()
        {
            if (pauseAction.action.WasPressedThisFrame())
            {
                Debug.Log("P");
                Toggle();
            }
        }

        public void GameOver()
        {
            gameoverUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }

        //재시작 - 씬로드
        public void Retry()
        {
            Debug.Log("ReTry");
            Time.timeScale = 1.0f;
            fader.FadeTo("PlayScene");
        }

        //메인메뉴 - 씬페이더로 어두워지기까지만 구현
        public void GoToMenu()
        {
            //페이더 효과만 구현
            Debug.Log("Go To Menu");
            Time.timeScale = 1.0f;
            fader.FadeTo("MainMenu");
        }

        //일시정지 pause - 토글기능, timescale = 0, 
        public void Toggle()
        {
            Debug.Log("Toggle");
        }
    }
}
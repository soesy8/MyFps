using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    /// <summary>
    /// 인트로 씬을 관리하는 클래스
    /// 인트로 연출, ...
    /// </summary>
    public class IntroOpening : MonoBehaviour
    {
        #region Variables
        [SerializeField] private string loadToScene = "PlayScene01";
        public SceneFader fader;

        public InputActionReference escapeAction;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            fader.FadeStart(1f);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (escapeAction.action.WasPressedThisFrame())
            {
                Exit();
            }
        }
        #endregion

        #region Custom Method
        public void Exit()
        {
            fader.FadeTo(loadToScene);
        }
        #endregion
    }
}
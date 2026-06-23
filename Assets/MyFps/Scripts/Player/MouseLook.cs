using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 마우스 움직임에 따라 플레이어의 시야 구현 클래스
    /// </summary>
    public class MouseLook : MonoBehaviour
    {
        #region
        public Transform cameraRoot;    //카메라 트래킹 오브젝트 인스턴스

        //참조
        private CharacterInput input;

        //회전
        [SerializeField] private float rotationSpeed = 1f;  //회전속도
        [SerializeField] private float sensivity = 100f;    //마우스 움직임 감도, 보정값

        private float cameraTargetPitch = 0f;               //카메라 회전 연산값 (위, 아래)
        private float rotationVelocity = 0f;               //카메라 회전 연산값 (좌, 우)

        [SerializeField] private float topClamp = 45f;      //카메라 위아래 최대값
        [SerializeField] private float bottomClamp = -90f;  //카메라 위아래 최소값
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            input = GetComponent<CharacterInput>();
        }

        private void LateUpdate()
        {
            //카메라 회전
            CameraRotate();
        }
        #endregion

        #region Custom Method
        void CameraRotate()
        {
            if (input.Look.sqrMagnitude < 0.01f)
            {

            }
        }
        #endregion
    }
}
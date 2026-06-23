using System;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이어의 이동을 관리하는 클래스
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        #region Variables
        //참조
        private CharacterInput input;
        private CharacterController controller;

        [Header("Move")]      //해더 특성 : 직렬화된 속성 중에 Player와 관련된 내용이다 표시
        [SerializeField] private float walkSpeed = 5.0f;        //걷기 속도
        [SerializeField] private float sprintSpeed = 8.0f;      //달리기 속도
        private float moveSpeed;                                //이동 속도

        //[Header("Jump")]
        #endregion

        //================================================

        #region Unity Event Method
        private void Awake()
        {
            //참조
            input = GetComponent<CharacterInput>();
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //이동
            Move();
        }

        #endregion

        //==================================================

        #region Custom Method
        void Move()
        {
            moveSpeed = walkSpeed;

            //인풋체크
            if (input.Move == Vector2.zero) moveSpeed = 0f;

            //인풋에서 방향값 얻어오기
            Vector3 inputDir = Vector3.zero;

            //플레이어의 로컬 방향 구하기
            if (input.Move != Vector2.zero)
            {
                inputDir = transform.right * input.Move.x + transform.forward * input.Move.y;
            }

            //이동 방향 * 시간 * 속도
            //moveSpeed * Time.deltaTime
            controller.Move(inputDir.normalized * Time.deltaTime * moveSpeed);
        }
        #endregion
    }
}
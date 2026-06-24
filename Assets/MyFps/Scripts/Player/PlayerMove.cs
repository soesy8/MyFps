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

        [Header("Ground Check")]
        [SerializeField] private bool isGrounded = false;

        [SerializeField] private float groundOffset = -0.14f;   //체크 지점 조정값
        [SerializeField] private float groundedRadius = 0.5f;   //체크 범위 영역
        public LayerMask groundLayers;                          //그라운드 레이어

        [Header("Jump")]
        [SerializeField] private float gravity = -9.81f;        //중력
        [SerializeField] private float verticalVelocity = 0f;   //y축의 속도값
        [SerializeField] private float jumpHeight = 1.2f;       //점프 높이
        [SerializeField] private float jumpTimeout = 0.1f;      //점프 키 입력 타이머

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
            //ground check
            CheckGround();

            //중력 및 점프처리
            GravityAndJump();

            //이동
            Move();
        }

        #endregion

        //==================================================

        #region Custom Method
        void CheckGround()
        {
            //캐릭터 컨트롤러에서 체크값 가져오기
            //isGrounded = controller.isGrounded;

            //체크 위치 설정
            Vector3 checkPosition = new Vector3
                (transform.position.x, transform.position.y - groundOffset, transform.position.z);
            //체크 지점에서 그라운드 체크
            isGrounded = Physics.CheckSphere(checkPosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }

        void GravityAndJump()
        {
            if (isGrounded)
            {
                if (verticalVelocity < 0f)
                {
                    verticalVelocity = -2f;     //바닥에 있으면 추락속도 -2f로 고정
                }
                
                if (input.IsJump && jumpTimeout <= 0f)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                }
                if (jumpTimeout > 0f)
                {
                    jumpTimeout -= Time.deltaTime;
                }
            }
            else
            {
                input.IsJump = false;
                jumpTimeout = 0.1f;
            }
            //중력
            verticalVelocity += gravity * Time.deltaTime;
        }
        void Move()
        {
            moveSpeed = input.IsSprint ? sprintSpeed : walkSpeed;

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
            //이동 : 방향(앞뒤좌우) * Time.deltaTime * speed + (위아래) * Time.deltaTime * verticalVelocity
            controller.Move(inputDir * Time.deltaTime * moveSpeed + Vector3.up *Time.deltaTime * verticalVelocity );
        }
        #endregion
    }
}
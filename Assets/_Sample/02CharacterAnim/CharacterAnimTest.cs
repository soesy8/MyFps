using MyFps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace MySample
{
    /// <summary>
    /// 캐릭터 애니메이션을 제어하는 예제 클래스
    /// new input system
    /// 기본 상태 : 대기
    /// W키 입력받으면 걷기 상태 , 걷기 애니메이션 재생
    /// + Shift키 입력받으면 뛰기 상태 , 뛰기 애니메이션 재생
    /// </summary>
    public class CharacterAnimTest : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;

        private bool isMove;
        private bool isRun;

        //애니 파라미터 스트링
        [SerializeField] private string move = "IsMove";
        [SerializeField] private string run = "IsRun";

        [SerializeField] private string moveSpeedParam = "MoveSpeed";
        private float curSpeed;

        /*[SerializeField] private float walkSpeed = 4;
        [SerializeField] private float runSpeed = 7;
        private float moveSpeed = 0f;*/

        public InputActionReference moveAction;
        public InputActionReference sprintAction;

        private CharacterInput input;

        #endregion

        #region Property
        public bool IsMove
        {
            get { return isMove; }
            private set
            {
                isMove = value;
                animator.SetBool(move, value);
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            private set
            {
                isRun = value;
                animator.SetBool(run, value);
            }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            animator = GetComponent<Animator>();
            input = GetComponent<CharacterInput>();
        }
        private void OnEnable()
        {
            moveAction.action.Enable();
            sprintAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
            sprintAction.action.Disable();
        }

        private void Update()
        {
            //테스트 스크립트
            float targetSpeed = 0f;

            if (input.Move != Vector2.zero)
            {
                targetSpeed = input.IsSprint ? 1f : 0.5f;
            }

            curSpeed = Mathf.Lerp(
                curSpeed,
                targetSpeed,
                Time.deltaTime * 5f);

            animator.SetFloat(moveSpeedParam, curSpeed);

            //원래 스크립트
            //인풋처리
            /*Vector2 inputMove = moveAction.action.ReadValue<Vector2>();
            IsMove = inputMove != Vector2.zero;

            if (sprintAction.action.WasPressedThisFrame())
            {
                IsRun = true;
            }
            else if (sprintAction.action.WasReleasedThisFrame())
            {
                IsRun= false;
            }*/

        }
        #endregion
    }
}
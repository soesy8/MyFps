using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    /// <summary>
    /// 애니메이터의 블랜드 트리 테스트 예제 클래스
    /// </summary>
    public class BlendTreeTest : MonoBehaviour
    {
        #region
        private Animator animator;
        [SerializeField] private float moveSpeed = 5f;
        public InputActionReference moveAction;
        private Vector2 inputMove;
        private string moveState = "MoveState";
        private string moveX = "MoveX";
        private string moveY = "MoveY";
        #endregion

        #region
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            moveAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
        }

        private void Update()
        {
            inputMove = moveAction.action.ReadValue<Vector2>();

            //AnimationStateTest(inputMove);
            AnimationBlendTreeTest(inputMove);

            Vector3 dir = new Vector3(inputMove.x, 0f, inputMove.y);
            transform.Translate(dir * Time.deltaTime * moveSpeed, Space.World);
        }
        #endregion

        #region Custom Method
        void AnimationBlendTreeTest(Vector2 moveDir)
        {
            animator.SetFloat(moveX, moveDir.x);
            animator.SetFloat(moveY, moveDir.y);

        }
        void AnimationStateTest(Vector2 moveDir)
        {
            if (moveDir == Vector2.zero)
            {
                animator.SetInteger(moveState, 0);          //대기, 중립
            }
            else
            {
                if (moveDir.y > 0f)  { animator.SetInteger(moveState, 1); }     //앞
                if (moveDir.y < 0f)  { animator.SetInteger(moveState, 2); }     //뒤
                if (moveDir.x < 0f)  { animator.SetInteger(moveState, 3); }     //좌
                if (moveDir.x > 0f)  { animator.SetInteger(moveState, 4); }     //우
            }
        }
        #endregion

    }
}
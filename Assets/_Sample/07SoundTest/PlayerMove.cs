using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    /// <summary>
    /// 시작하면 앞으로 이동
    /// 좌우 입력을 받아 좌우 이동
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        private Rigidbody rb;
        [SerializeField] private float moveForce = 5f;
        [SerializeField] private float sideForce = 5f;

        private Vector2 move;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            move = moveAction.action.ReadValue<Vector2>();

        }

        private void FixedUpdate()
        {
            //앞으로 이동
            rb.AddForce(0f, 0f, moveForce, ForceMode.Force);
            
            if (move.x < 0f)
            {
                rb.AddForce(-sideForce, 0f, 0f, ForceMode.Acceleration);
            }
            else if (move.x > 0f)
            {
                rb.AddForce(sideForce, 0f, 0f, ForceMode.Acceleration);
            }
        }
    }
}

/*
Rigidbody

이동 방법
1. 이동 시키려고 하는 방향으로 힘을 준다
2. linearVelocity의 값을 직접 조정하여 이동한다

힘의 종류
ForceMode.Force : 연속적인 힘, 무게 O, 자동차 드라이브
ForceMode.Acceleration : 연속적인 힘, 무게 X, 바람
ForceMode.Impulse : 일회성 힘, 무게 O, 점프
ForceMode.VelocityChange : 일회성 힘, 무게 X, 플레이어 이동

*/
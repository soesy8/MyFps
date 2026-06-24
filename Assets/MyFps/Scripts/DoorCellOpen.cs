using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이어와 인터렉티브 기능 구현
    /// 가까이 가서 마우스 가져가면 인터렉티브 UI를 보여준다
    /// 액션 : 문을 연다
    /// </summary>
    public class DoorCellOpen : MonoBehaviour
    {
        //참조 / 확실하지 않음
        private Animator animator;
        private BoxCollider triggerCollider;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            triggerCollider = GetComponent<BoxCollider>();
        }

        public void OpneDoor()
        {
            animator.SetBool("IsOpen", true);
            triggerCollider.enabled = false;
        }
    }
}
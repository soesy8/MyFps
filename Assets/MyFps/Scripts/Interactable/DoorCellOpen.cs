using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 플레이어와 인터렉티브 기능 구현
    /// 가까이 가서 마우스 가져가면 인터렉티브 UI를 보여준다
    /// 액션 : 문을 연다
    /// </summary>
    public class DoorCellOpen : Interactable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Collider doorTrigger;
        [SerializeField] private AudioSource audioSource;

        public override void Interact(PlayerInteraction player)
        {
            if (audioSource == null) return;

            animator.SetBool("IsOpen", true);
            audioSource.Play();

            doorTrigger.enabled = false;
        }
    }
}
using UnityEngine;
using System;
using System.Collections;
using TMPro;

namespace MyFps
{
    /// <summary>
    /// 문 열기/닫기
    /// 문 열릴 때 등록된 함수 호출하는 이벤트 구현
    /// 문 닫힐 떄 등록된 함수 호출하는 이벤트 구현
    /// 열쇠가 없으면 안열리고 메세지를 띄워준다
    /// </summary>
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private bool isOpen = false;
        [SerializeField] private ItemType requireItem;
        [SerializeField] private TextMeshProUGUI sequenceText;

        public event Action OnDoorOpened;
        public event Action OnDoorClosed;

        private Coroutine messageCoroutine;

        public bool IsOpen => isOpen;
        public ItemType RequireItem => requireItem;


        public void Open()
        {
            if (isOpen) return;

            if (!PlayerInventory.Instance.HasItem(requireItem))
            {
                //Debug.Log("Need Key");

                if (messageCoroutine != null)
                {
                    StopCoroutine(messageCoroutine);
                }

                messageCoroutine = StartCoroutine(NeedKeyRoutine());
                //StartCoroutine(NeedKeyRoutine());

                return;
            }

            animator.SetBool("IsOpen", true);
            isOpen = true;
            OnDoorOpened?.Invoke();
        }

        public void Close()
        {
            if (!isOpen) return;

            animator.SetBool("IsOpen", false);
            isOpen = false;
            OnDoorClosed?.Invoke();
        }

        public void DoorToggle()
        {
            if (isOpen)
                Close();
            else Open();
        }

        IEnumerator NeedKeyRoutine()
        {
            sequenceText.text = "Need Key";

            yield return new WaitForSeconds(2f);

            sequenceText.text = "";
        }
    }
}
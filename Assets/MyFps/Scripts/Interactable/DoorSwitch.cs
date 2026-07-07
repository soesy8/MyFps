using UnityEngine;
using UnityEngine.UI;

namespace MyFps
{
    /// <summary>
    /// 문 스위치 구현
    /// 문 스위치는 토글로 구현, 문이 열려 있으면 빨간색, 문이 닫혀있으면 처음 색으로 설정
    /// </summary>
    public class DoorSwitch : Interactable
    {
        [SerializeField] private Door door;

        private Color originColor;

        /*private void Awake()
        {
            door = FindFirstObjectByType<Door>();
        }*/

        public override string InteractionText
        {
            get
            {
                if (door.IsOpen) return "Close Door";
                return "Open Door";
            }
        }

        private void Start()
        {
            originColor = Color.white;
        }

        public override void Interact(PlayerInteraction player)
        {
            //DoorText();
            //Debug.Log(door);
            door.DoorToggle();
        }

        /*public string DoorText()
        {
            if (door.IsOpen) return "Close Door";

            //if (PlayerInventory.Instance.HasItem(door.RequireItem)) return "Locked Door";

            return "Open Door";
        }*/
    }
}
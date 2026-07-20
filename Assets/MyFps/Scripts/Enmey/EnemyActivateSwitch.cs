using UnityEngine;

namespace MyFps
{
    public class EnemyActivateSwitch : Interactable
    {
        [SerializeField] private GunMan[] enemies;

        //[SerializeField] private bool isActivated = false;


        [SerializeField] private Door door;
        [SerializeField] private Renderer doorSwitch;
        private Color originColor;

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
            originColor = doorSwitch.material.color;
        }

        private void OnEnable()
        {
            door.OnDoorOpened += DoorOpened;
            door.OnDoorClosed += DoorClosed;
        }

        private void OnDisable()
        {
            door.OnDoorOpened -= DoorOpened;
            door.OnDoorClosed -= DoorClosed;
        }

        public override void Interact(PlayerInteraction player)
        {
            door.DoorToggle();
        }

        private void DoorOpened()
        {
            doorSwitch.material.color = Color.red;

            foreach (GunMan enemy in enemies)
            {
                if (enemy == null) continue;
                enemy.IsDetecting = true;
            }
        }

        private void DoorClosed()
        {
            doorSwitch.material.color = originColor;

            foreach (GunMan enemy in enemies)
            {
                if (enemy == null) continue;
                enemy.IsDetecting = false;
            }
        }
    }
}
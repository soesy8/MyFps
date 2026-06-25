using UnityEngine;

namespace MyFps
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private GameObject actionUI;
        [SerializeField] private GameObject extraCross;

        private CharacterInput input;

        private void Awake()
        {
            input = GetComponent<CharacterInput>();
        }

        private void Update()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                DoorCellOpen door = hit.collider.GetComponentInParent<DoorCellOpen>();

                if (door != null)
                {
                    actionUI.SetActive(true);
                    extraCross.SetActive(true);

                    if (input.IsInteract)
                    {
                        door.OpenDoor();

                        actionUI.SetActive(false);
                        extraCross.SetActive(false);

                        input.IsInteract = false;
                    }
                }
                else
                {
                    actionUI.SetActive(false);
                    extraCross.SetActive(false);
                }
            }
            else
            {
                actionUI.SetActive(false);
                extraCross.SetActive(false);
            }
        }
    }
}
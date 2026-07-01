using UnityEngine;
using TMPro;

namespace MyFps
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private GameObject actionUI;
        [SerializeField] private GameObject extraCross;
        [SerializeField] private TMP_Text actionText;

        private CharacterInput input;
        private PlayerShoot playerShoot;

        public PlayerShoot PlayerShoot => playerShoot;

        private void Awake()
        {
            input = GetComponent<CharacterInput>();
            playerShoot = GetComponentInChildren<PlayerShoot>();
        }

        private void Update()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

                if (interactable != null)
                {
                    actionUI.SetActive(true);
                    extraCross.SetActive(true);
                    actionText.text = interactable.InteractionText;

                    if (input.IsInteract)
                    {
                        interactable.Interact(this);

                        HideInteractionUI();

                        input.IsInteract = false;
                    }
                }
                else
                {
                    HideInteractionUI();
                }
            }
            else
            {
                HideInteractionUI();
            }
        }

        //Custom Method
        private void HideInteractionUI()
        {
            actionText.text = "";
            actionUI.SetActive(false);
            extraCross.SetActive(false);
        }

        public void SetPlayerShoot(PlayerShoot shoot)
        {
            playerShoot = shoot;
        }
    }
}
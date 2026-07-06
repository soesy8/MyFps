using UnityEngine;
using TMPro;

namespace MyFps
{
    public class GetWeapon : Interactable
    {
        [SerializeField] private Collider gunTrigger;
        [SerializeField] private GameObject guideArrow;
        [SerializeField] private GameObject playerPistol;
        [SerializeField] private GameObject ammoUIObject;
        private AmmoUI ammoUI;

        /*private void Start()
        {
            AmmoUI _ammoUI = FindFirstObjectByType<AmmoUI>();
        }*/

        public override void Interact(PlayerInteraction player)
        {
            if (guideArrow != null)
            {
                guideArrow.SetActive(false);
            }

            playerPistol.SetActive(true);

            ammoUIObject.SetActive(true);

            PlayerShoot shoot = playerPistol.GetComponent<PlayerShoot>();
            player.SetPlayerShoot(shoot);

            AmmoUI ammoUI = ammoUIObject.GetComponent<AmmoUI>();
            ammoUI.UpdateAmmoUI();

            Destroy(gunTrigger.gameObject);
        }
    }
}
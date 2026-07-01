using UnityEngine;
using TMPro;

namespace MyFps
{
    public class GetWeapon : Interactable
    {
        [SerializeField] private Collider gunTrigger;
        [SerializeField] private GameObject guideArrow;
        [SerializeField] private GameObject playerPistol;
        [SerializeField] private GameObject ammoUI;

        public override void Interact(PlayerInteraction player)
        {
            guideArrow.SetActive(false);

            playerPistol.SetActive(true);

            ammoUI.SetActive(true);

            PlayerShoot shoot = playerPistol.GetComponent<PlayerShoot>();

            player.SetPlayerShoot(shoot);

            shoot.UpdateAmmoUI();

            Destroy(gunTrigger.gameObject);
        }
    }
}
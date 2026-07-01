using UnityEngine;

namespace MyFps
{
    public class GetWeapon : Interactable
    {
        [SerializeField] private Collider gunTrigger;
        [SerializeField] private GameObject guideArrow;
        [SerializeField] private GameObject playerPistol;

        public override void Interact(PlayerInteraction player)
        {
            guideArrow.SetActive(false);

            playerPistol.SetActive(true);

            PlayerShoot shoot = playerPistol.GetComponent<PlayerShoot>();

            player.SetPlayerShoot(shoot);

            Destroy(gunTrigger.gameObject);
        }
    }
}
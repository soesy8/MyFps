using UnityEngine;

namespace MyFps
{
    public class GetWeapon : Interactable
    {
        [SerializeField] private Collider gunTrigger;
        [SerializeField] private GameObject guideArrow;
        [SerializeField] private GameObject playerPistol;

        public override void Interact()
        {
            //gunTrigger.enabled = false;
            guideArrow.SetActive(false);
            playerPistol.SetActive(true);
            Destroy(gunTrigger.gameObject);
        }
    }
}
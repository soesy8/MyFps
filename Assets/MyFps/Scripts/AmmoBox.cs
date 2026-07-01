using UnityEngine;

namespace MyFps
{
    public class AmmoBox : Interactable
    {
        [SerializeField] private int ammoAmount = 7;

        public override void Interact(PlayerInteraction player)
        {
            player.PlayerShoot.AddAmmo(ammoAmount);

            Debug.Log($"Ammo +{ammoAmount}");

            Destroy(gameObject);
        }
    }
}
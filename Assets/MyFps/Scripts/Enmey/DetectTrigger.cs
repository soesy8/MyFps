using UnityEngine;

namespace MyFps
{
    public class EnemyActivateTrigger : MonoBehaviour
    {
        [SerializeField] private GunMan[] enemies;

        private bool isActivated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (isActivated) return;

            if (!other.CompareTag("Player")) return;

            foreach (GunMan enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.IsDetecting = true;
                }
            }

            isActivated = true;
        }
    }
}
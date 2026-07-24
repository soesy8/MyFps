using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 충돌체에 부착되어 데미지를 입는 클래스
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        // ======== Variables ========
        //참조
        private Health health;

        //데미지 계수
        [SerializeField] private float damageMultiplier = 1f;

        //자신에게 입히는 데미지 계수
        [SerializeField] private float sensiblityToSelfDamage = 0f;

        // ======== Unity Event Method ========
        private void Awake()
        {
            //참조
            health = GetComponent<Health>();
            if (health == null)
            {
                health = GetComponentInParent<Health>();
            }
        }

        // ======== Custom Method ========
        //데미지 처리, 에미지량, 폭발 데미지 여부, 데미지 주는 주체
        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (health == null) return;

            var totalDamage = damage;

            //폭발 체크
            if (isExplosionDamage)
            {
                totalDamage *= 1f;
            }
            else
            {
                totalDamage *= damageMultiplier;
            }

            //셀프 데미지 체크
            if (health.gameObject == damageSource)
            {
                totalDamage *= sensiblityToSelfDamage;
            }
        }

    }
}
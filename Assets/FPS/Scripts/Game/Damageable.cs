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

        }

    }
}
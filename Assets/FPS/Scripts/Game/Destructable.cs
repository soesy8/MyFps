using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 죽었을 때 오브젝트(Health를 가지고 있는)를 킬하는 클래스
    /// </summary>
    public class Destructable : MonoBehaviour
    {
        // ======== Variables ========
        //참조
        private Health health;

        //킬 딜레이
        [SerializeField] private float killdelay = 0f;


        // ======== Unity Event Method ========
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            //health 이벤트 함수 등록
            health.onDeath += OnDie;
            health.onDamaged += OnDamaged;
        }

        private void OnDisable()
        {
            health.onDeath -= OnDie;
            health.onDamaged -= OnDamaged;
        }


        // ======== Custom Method ========
        //데미지 입었을 때 호출되어 실행되는 함수
        private void OnDamaged(float damage, GameObject damageSource)
        {
            //데미지 구현 내용
        }

        //죽었을 때 호출되어 실행되는 함수
        private void OnDie()
        {
            //킬
            Destroy(gameObject, killdelay);
        }

    }
}
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class Health : MonoBehaviour
    {
        // ======== Variables ========
        [SerializeField] private float maxHealth = 100f;        //최대체력
        private bool isDead = false;                            //죽음여부

        //체력 위험 경계 비율
        [SerializeField] private float criticalHealthRatio = 0.3f;

        //이벤트 함수
        public UnityAction<float, GameObject> onDamaged;        //데미지 입었을 떄 등록된 함수 호출
        public UnityAction onDeath;                             //죽었을 때 등록된 함수 호출
        public UnityAction<float> onHeal;                       //힐 할 떄 등록된 함수 호출


        // ======== Properties ========
        public float CurrentHealth { get; private set; }                //현재 체력
        public bool Invincible { get; private set; }                    //무적모드
        public float HealthRatio => CurrentHealth / maxHealth;          //체력 게이지비율 - UI
        public bool IsCritical => HealthRatio <= criticalHealthRatio;   //위험
        public bool CanPickup => CurrentHealth < maxHealth;             //힐 아이템을 먹을 수 있는지 체크

        // ======== Unity Event Method ========
        private void Start()
        {
            CurrentHealth = maxHealth;
            Invincible = false;
        }


        // ======== Custom Method ========
        public bool Heal(float amout)
        {
            float beforeHealth = CurrentHealth;     //데미지 계산 전의 체력 값

            CurrentHealth += amout;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
            Debug.Log($"HP : {CurrentHealth}");

            //realHeal
            float realHeal = CurrentHealth - beforeHealth;

            if (realHeal > 0)
            {
                onHeal?.Invoke(realHeal);

                return true; //힐 성공
            }

            return false;       //힐 실패
        }

        //데미지 처리. damage : 데미지량, damageSource : 데미지를 주는 주체
        public void TakeDamage(float damage, GameObject damageSource)
        {
            //무적 모드 체크
            if (Invincible) return;

            float beforeHealth = CurrentHealth;     //데미지 계산 전의 체력 값

            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
            Debug.Log($"HP : {CurrentHealth}");

            //realDamage
            float realDamage = beforeHealth - CurrentHealth;

            if (realDamage > 0f)
            {
                //데미지 구현 (VFX, SFX, UI ...)
                onDamaged?.Invoke(realDamage, damageSource);
            }
            //죽음 처리
            HandleDeath();
        }

        void HandleDeath()
        {
            if (isDead) return;

            if (CurrentHealth < 0)
            {
                isDead = true;

                onDeath?.Invoke();
            }
        }

    }
}
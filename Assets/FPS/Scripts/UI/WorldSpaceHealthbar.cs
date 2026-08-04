using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    //캐릭터의 머리 위에 있는 healthBarUI 관리
    //1. 현재 health값에 따른 게이지 관리
    //2. 월드 캔버스 UI - 게이지바가 항상 플레이어(카메라)를 바라본다
    //3. 현재 health값이 maxHealth이면 게이지바 UI를 보이지 않게 한다
    public class WorldSpaceHealthbar : MonoBehaviour
    {
        [SerializeField] private Image healthFillImage;
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Start()
        {
            UpdateHealthBar();

            health.onDamaged += OnHealthChanged;
            health.onHeal += OnHealthChanged;
            health.onDeath += OnDeath;
        }

        private void OnDestroy()
        {
            if (health == null) return;

            health.onDamaged -= OnHealthChanged;
            health.onHeal -= OnHealthChanged;
            health.onDeath -= OnDeath;
        }

        #region Custom Method
        private void OnHealthChanged(float value)
        {
            UpdateHealthBar();
        }

        private void OnHealthChanged(float value, GameObject damageSource)
        {
            UpdateHealthBar();
        }

        private void OnDeath()
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthFillImage.fillAmount = health.HealthRatio;
        }
        #endregion
    }
}
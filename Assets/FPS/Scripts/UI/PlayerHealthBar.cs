using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

namespace Unity.FPS.UI
{
    //HUD에 있는 플레이어 HealthBar를 관리하는 클래스
    //HealthBar 게이지 관리
    //플레이어의 health는 Find Object로 가져와서 참조
    public class PlayerHealthBar : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Image healthFillImage;
        private Health playerHealth;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            PlayerCharacterController cc
                = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            playerHealth = cc.GetComponent<Health>();
        }

        private void Start()
        {
            UpdateHealthBar();

            playerHealth.onDamaged += OnHealthChanged;
            playerHealth.onHeal += OnHealthChanged;
            playerHealth.onDeath += OnDeath;
        }

        private void OnDestroy()
        {
            if (playerHealth == null) return;

            playerHealth.onDamaged -= OnHealthChanged;
            playerHealth.onHeal -= OnHealthChanged;
            playerHealth.onDeath -= OnDeath;
        }
        #endregion

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
            if(playerHealth.HealthRatio == 1f)
            {
                healthFillImage.fillAmount = 1f;
            }
            healthFillImage.fillAmount = playerHealth.HealthRatio;
        }
        #endregion
    }
}
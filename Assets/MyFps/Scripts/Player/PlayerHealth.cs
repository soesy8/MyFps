using UnityEngine;

namespace MyFps
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables
        [SerializeField] private float maxHp = 20f;
        private float hp;

        //[SerializeField] private GameObject player;

        [SerializeField] private DamageEffect damageEffect;

        [SerializeField] private GameObject gameoverUI;

        private bool isDeath = false;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //player = GetComponent<CharacterController>();
        }
        private void Start()
        {
            hp = maxHp;
        }
        #endregion

        #region Custom Method
        public void TakeDamage(float damage)
        {
            hp -= damage;
            damageEffect.Play();
            //Debug.Log($"{gameObject.name}, -{damage}, HP : {hp}");
            //뎀지 효과 처리
            if (hp <= 0f && isDeath == false)
            {
                Die();
            }
        }

        void Die()
        {
            isDeath = true;

            //죽음처리
            //게임오버
            //Debug.Log("GameOver");
            gameoverUI.SetActive(true);
            
            

            //Time.timeScale = 0f;
        }
        #endregion
    }
}
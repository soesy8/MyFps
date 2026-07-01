using UnityEngine;
//using UnityEngine.Events;

namespace MyFps
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables
        [SerializeField] private float maxHp = 20f;
        private float hp;
        [SerializeField] private DamageEffect damageEffect;
        //public UnityAction<float> onDamaged;
        //public UnityAcion onDie;

        private bool isDeath = false;

        [SerializeField] private GameManager gameManager;
        #endregion


        /*#region Properties
        public float HP => hp;
        #endregion*/



        #region Unity Event Method
        private void Awake()
        {
            //player = GetComponent<CharacterController>();
            gameManager = FindFirstObjectByType<GameManager>();
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

            //onDamaged?.Invoke(damage);

            //Debug.Log($"{gameObject.name}, -{damage}, HP : {hp}");
            //뎀지 효과 처리
            if (hp <= 0f && isDeath == false)
            {
                Die();
            }
        }

        void Die()
        {
            //죽음처리
            //게임오버
            //onDie?.Invoke();
            gameManager.GameOver();
            isDeath = true;
        }
        #endregion
    }
}
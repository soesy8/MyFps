using UnityEngine;

namespace MyFps
{
    public enum EnemyState
    {
        Idle = 0,
        Patrol,
        Chase,
        Attack,
        Return,
        Death
    }

    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        #region Variables

        protected Animator animator;
        protected Transform thePlayer;

        [SerializeField] protected EnemyState currentState;
        protected EnemyState beforeState;

        [SerializeField] protected float moveSpeed = 2f;

        [SerializeField] protected float attackRange = 1.5f;
        [SerializeField] protected float attackDamage = 5f;
        [SerializeField] protected float attackCooldown = 1f;

        protected float attackTimer;

        [SerializeField] private float maxHealth = 20f;
        private float currentHealth = 0f;
        private bool isDeath = false;

        protected static readonly int MoveStateHash =
            Animator.StringToHash("MoveState");

        protected static readonly int FireTriggerHash =
            Animator.StringToHash("FireTrigger");

        protected static readonly int DeathTriggerHash =
            Animator.StringToHash("DeathTrigger");

        #endregion

        #region Unity Event Method

        protected void Awake()
        {
            animator = GetComponent<Animator>();
            FindPlayer();
        }

        protected virtual void Start()
        {
            ChangeState(EnemyState.Idle);
            currentHealth = maxHealth;
        }

        protected void Update()
        {
            if (isDeath)
                return;

            if (thePlayer == null)
            {
                FindPlayer();
                return;
            }

            UpdateAI();
        }

        #endregion

        #region Custom Method

        protected abstract void UpdateAI();

        protected void FindPlayer()
        {
            PlayerMove playerMove = FindFirstObjectByType<PlayerMove>();

            if (playerMove != null)
            {
                thePlayer = FindFirstObjectByType<PlayerMove>().transform;
            }
        }

        protected void MoveTo(Vector3 targetPos)
        {
            Vector3 dir = targetPos - transform.position;

            transform.Translate(
                dir.normalized * moveSpeed * Time.deltaTime,
                Space.World);

            transform.LookAt(targetPos);
        }

        public void ChangeState(EnemyState newState)
        {
            if (currentState == newState)
                return;

            beforeState = currentState;
            currentState = newState;

            switch (newState)
            {
                case EnemyState.Idle:
                    animator.SetInteger(MoveStateHash, 0);
                    break;

                case EnemyState.Patrol:
                    animator.SetInteger(MoveStateHash, 1);
                    break;

                case EnemyState.Chase:
                    animator.SetInteger(MoveStateHash, 1);
                    break;

                case EnemyState.Return:
                    animator.SetInteger(MoveStateHash, 1);
                    break;

                case EnemyState.Attack:
                    animator.SetTrigger(FireTriggerHash);
                    break;

                case EnemyState.Death:
                    animator.SetTrigger(DeathTriggerHash);
                    break;
            }
        }

        protected void Attack()
        {
            if (thePlayer == null)
                return;

            IDamageable damageable =
                thePlayer.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                Debug.Log("Player Damaged");
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f && !isDeath)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            isDeath = true;

            ChangeState(EnemyState.Death);
        }

        #endregion
    }
}
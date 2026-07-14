using UnityEngine;

namespace MyFps
{
    public class PointGuard : Enemy
    {
        private Vector3 originPosition;

        [SerializeField] private float detectRange = 7f;
        [SerializeField] private float chaseRange = 10f;
        //[SerializeField] private float attackCooldown = 1f;

        //private float attackTimer;

        protected override void Start()
        {
            base.Start();

            originPosition = transform.position;
        }

        protected override void UpdateAI()
        {
            switch (currentState)
            {
                case _EnemyState.Idle:
                    EnemyIdle();
                    break;

                case _EnemyState.Chase:
                    EnemyChase();
                    break;

                case _EnemyState.Attack:
                    EnemyAttack();
                    break;

                case _EnemyState.Return:
                    EnemyReturn();
                    break;

                case _EnemyState.Death:
                    break;
            }
        }

        private void EnemyIdle()
        {
            float distance =
                Vector3.Distance(transform.position, thePlayer.position);

            if (distance <= detectRange)
            {
                ChangeState(_EnemyState.Chase);
            }
        }

        private void EnemyChase()
        {
            float distance =
                Vector3.Distance(transform.position, thePlayer.position);

            MoveTo(thePlayer.position);

            if (distance <= attackRange)
            {
                ChangeState(_EnemyState.Attack);
            }

            if (distance > chaseRange)
            {
                ChangeState(_EnemyState.Return);
            }
        }

        private void EnemyAttack()
        {
            transform.LookAt(thePlayer);

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;

                animator.SetTrigger(FireTriggerHash);
            }

            float distance = Vector3.Distance(transform.position, thePlayer.position);

            if (distance > attackRange)
            {
                ChangeState(_EnemyState.Chase);
            }
        }

        private void EnemyReturn()
        {
            MoveTo(originPosition);

            float distance =
                Vector3.Distance(transform.position, originPosition);

            if (distance < 0.2f)
            {
                ChangeState(_EnemyState.Idle);
            }
        }
    }
}
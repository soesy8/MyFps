using UnityEngine;

namespace MyFps
{
    public class PointGuard : Enemy
    {
        private Vector3 originPosition;

        [SerializeField] private float detectRange = 7f;
        [SerializeField] private float chaseRange = 10f;

        protected override void Start()
        {
            base.Start();
            originPosition = transform.position;
        }


        protected override void UpdateAI()
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    EnemyIdle();
                    break;
                case EnemyState.Chase:
                    EnemyChase();
                    break;
                case EnemyState.Attack:
                    EnemyAttack();
                    break;
                case EnemyState.Return:
                    EnemyReturn();
                    break;
                case EnemyState.Death:
                    break;
            }
        }

        private void EnemyIdle()
        {
            float distance = Vector3.Distance(transform.position, thePlayer.position);

            if (distance <= detectRange)
            {
                ChangeState(EnemyState.Chase);
            }
        }

        private void EnemyChase()
        {
            float distance =Vector3.Distance(transform.position, thePlayer.position);

            MoveTo(thePlayer.position);

            if (distance <= attackRange)
            {
                ChangeState(EnemyState.Attack);
            }

            if (distance > chaseRange)
            {
                ChangeState(EnemyState.Return);
            }
        }

        private void EnemyAttack()
        {

        }

        private void EnemyReturn()
        {
            MoveTo(originPosition);

            float distance =Vector3.Distance(transform.position, originPosition);

            if (distance < 0.2f)
            {
                ChangeState(EnemyState.Idle);
            }
        }
    }
}
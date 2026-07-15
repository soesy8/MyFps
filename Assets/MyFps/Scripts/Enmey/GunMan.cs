using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace MyFps
{
    //적 캐릭터 상태 정의
    public enum EnemyState
    {
        E_Idle,     //대기
        E_Walk,     //걷기 (패트롤)
        E_Chase,    //추격
        E_Attack,   //공격
        E_Death     //죽기
    }

    /// <summary>
    /// 총을 쏘는 적 유닛을 관리하는 클래스
    /// </summary>
    public class GunMan : MonoBehaviour, IDamageable
    {
        #region Variables
        //참조
        private Animator animator;
        private NavMeshAgent agent;
        private Transform thePlayer;

        //상태
        [SerializeField] private EnemyState currentState;       //현재상태 / 직렬화해서 디버깅
        private EnemyState beforeState;        //이전 상태

        [SerializeField] private float maxHealth = 20f;
        private float currentHealth = 0f;
        private bool isDeath = false;       //죽음 체크

        //대기상태
        [SerializeField] private float idleTimer = 2f;      //대기 타이머, 웨이 포인트 도착 시 다음 포인트 출발 전 2초 대기
        private float countdown = 0f;

        //패트롤 여부
        [SerializeField] private bool isPatrol = false;
        public Transform[] wayPoints;                       //웨이 포인트
        private int wayPointIndex = 0;                      //다음 포인트 지점 인덱스

        //처음 스폰 위치
        private Vector3 startPosition = Vector3.zero;

        //추격 상태
        [SerializeField] private float detectDistance = 10f;        //적이 디텍팅 거리에 들어오면 추격
        [SerializeField] private bool isDetecting = false;

        //공격 상태
        [SerializeField] private float attackRange = 5f;            //적이 사거리 안에 들어오면 추격을 멈추고 공격
        [SerializeField] private float attackTimer = 2f;            //총 발사 간격
        //private float attackCountdown = 0f;
        [SerializeField] private float attackDamage = 5f;           //공격 대미지

        //애니메이션 매개변수
        private const string MoveSpeed = "MoveSpeed";
        private const string IsDeath = "IsDeath";
        private const string FireTrigger = "FireTrigger";
        #endregion

        //property
        public bool IsDetecting
        {
            get { return isDetecting; }
            set
            {
                isDetecting = value; 
                if (value == false)
                {
                    ChangeState(EnemyState.E_Walk);
                }
            }
        }

        #region Unity Event Method
        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            thePlayer = FindFirstObjectByType<PlayerMove>().transform;
        }

        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
            startPosition = this.transform.position;

            isDetecting = false;

            ChangeState(EnemyState.E_Idle);
            
            isPatrol = wayPoints.Length >= 2 ? true : false;
            wayPointIndex = 1;

            //ChangeState(EnemyState.E_Walk);
        }

        private void Update()
        {
            //죽음 체크
            if (isDeath || thePlayer == null) return;

            //플레이어 디텍팅
            float targetDis = Vector3.Distance(transform.position, thePlayer.position);

            Chase(targetDis);

            /*if (targetDis <= attackRange)
            {
                //공격
                ChangeState(EnemyState.E_Attack);
            }
            else if(targetDis <= detectDistance)
            {
                ChangeState(EnemyState.E_Chase);
            }*/

            //이동 애니메이션
            animator.SetFloat(MoveSpeed, agent.velocity.magnitude);

            //상태 구현
            switch (currentState)
            {
                case EnemyState.E_Idle:
                    if (isPatrol)
                    {
                        countdown += Time.deltaTime;
                        if (countdown > idleTimer)
                        {
                            //다음 포인트 지점으로 이동(패트롤)
                            ChangeState(EnemyState.E_Walk);

                            //초기화
                            countdown = 0;
                        }
                    }
                    break;

                case EnemyState.E_Walk: //패트롤
                    //도착판정
                    if(agent.remainingDistance < 0.1f)
                    {
                        //animator.SetFloat(MoveSpeed, speed);
                        
                        //인덱스 증가
                        if (isPatrol)
                        {
                            wayPointIndex++;
                            if (wayPointIndex >= wayPoints.Length)
                            {
                                wayPointIndex = 0;
                            }
                        }
                        ChangeState(EnemyState.E_Idle);
                    }
                    break;

                case EnemyState.E_Chase:
                    //agent 목표 설정
                    agent.SetDestination(thePlayer.position);
                    
                    //플레이어가 인식범위에서 벗어나면
                    if(targetDis > detectDistance)
                    {
                        ChangeState(EnemyState.E_Walk);
                    }
                    break;

                case EnemyState.E_Attack:
                    countdown += Time.deltaTime;
                    if(countdown >= attackTimer)
                    {
                        //공격
                        Shoot();

                        //초기화
                        countdown = 0f;
                    }

                    //플레이어 바라보기
                    transform.LookAt(thePlayer.position);
                    break;

                case EnemyState.E_Death:
                    break;
            }

        }

        //디텍팅 거리, 공격 거리 기즈모 그리기
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        #endregion

        #region Custom Method
        public void ChangeState(EnemyState newState)
        {
            //현재 상태 체크
            if (currentState == newState) return;

            //상태 변경전에 현재상태를 이전상태에 저장
            beforeState = currentState;

            //새로운 상태로 변경
            currentState = newState;

            //agent 초기화
            agent.ResetPath();

            countdown = 0f;

            //새로운 상태변경에 따른 처리사항 구현
            switch (currentState)
            {
                case EnemyState.E_Idle:
                    break;

                case EnemyState.E_Walk:
                    //이동 목표 지점 설정
                    if (isPatrol)
                    {
                        agent.SetDestination(wayPoints[wayPointIndex].position);
                    }
                    else
                    {
                        agent.SetDestination(startPosition);
                    }
                    break;
            }

            //조준
            if (newState == EnemyState.E_Chase || newState == EnemyState.E_Attack)
            {
                animator.SetLayerWeight(1, 1f);
            }
            else
            {
                animator.SetLayerWeight(1, 0f);
            }

        }

        //추격
        private void Chase(float targetDis)
        {
            if (targetDis <= attackRange && IsDetecting)
            {
                //공격
                ChangeState(EnemyState.E_Attack);
            }
            else if (targetDis <= detectDistance && IsDetecting)
            {
                ChangeState(EnemyState.E_Chase);
            }
        }

        //공격
        void Shoot()
        {
            //애니메이션
            animator.SetTrigger(FireTrigger);
            
            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            //데미지 효과 처리(VFX, SFX)

            //죽음 체크
            if (currentHealth <= 0f && isDeath == false)
            {
                Die();
            }
        }

        void Die()
        {
            isDeath = true;

            //상태 변경
            ChangeState(EnemyState.E_Death);

            //죽음 처리 (VFX, SFX, 보상처리)
            animator.SetBool(IsDeath, true);
            agent.isStopped = true;     //이동 중지
            agent.enabled = false;      //Agent 비활성화

            //제거
            //Destroy(gameObject, 3f);
        }
        #endregion
    }
}
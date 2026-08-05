using System;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.FPS.Utility;
using Unity.AI;
using UnityEngine.AI;

namespace Unity.FPS.AI
{
    /// <summary>
    /// 적의 공통적인 상태를 관리하는 클래스
    /// 적의 데미지 처리, 죽음 처리
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        #region Struct
        //렌더러와 관련된 데이터 정의
        [Serializable]
        public struct RendererIndexData
        {
            public Renderer renderer;
            public int materialIndex;
            
            //생성자
            public RendererIndexData(Renderer _renderer, int index)
            {
                renderer = _renderer;
                materialIndex = index;
            }
        }
        #endregion
        
        #region Variables
        //참조
        private Health health;
        private EnemyManager enemyManager;
        
        // ==== damage ====
        public UnityAction onDamaged;       //적이 데미지 입었을 때 등록된 함수 호출
        
        public Material bodyMaterial;       //적 몸체 메테리얼
        //데미지 연출되는 컬러 그라디언트
        [GradientUsage(true)] public Gradient onHitBodyGradient;
        
        //바디메테리얼이 있는 렌더러와 바디메테리얼 인덱스를 가진 구조첼 리스트
        private List<RendererIndexData> bodyRenderers =
            new List<RendererIndexData>();
        private MaterialPropertyBlock bodyFlashMaterialPropertyBlock;

        //데미지를 입은 시간
        private float lastTimeDamaged = float.NegativeInfinity;
        //플래시 효과 지속
        [SerializeField] private float flashOnHitDuration = 0.5f;
        private bool wasDamagedThisFrame = false;   //이번 프레임에 데미지 입었는지 여부
        
        public AudioClip damageSfx;     //데미지 사운드 클립
        
        // ==== death ====
        public GameObject deathVfxPrefab;
        public Transform deathVfxSpawnPosition;
        public AudioClip deathSfx;
        
        // ==== move, patrol ====
        public NavMeshAgent Agent { get; private set; }
        
        // ==== detecting ====
        public DetectionModule DetectionModule { get; private set; }
        
        public Material eyeMaterial;        //평시 eyeColor
        private Renderer eyeRendererData;
        //플레이어 감지 시 eyeColor
        private MaterialPropertyBlock eyeColorMaterialPropertyBlock;
        
        // ==== attack ====
        private WeaponController[] weapons;     //무기 슬롯
        private WeaponController currentWeapon; //현재 무기
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //EnemyManager 등록
            enemyManager = GameObject.FindAnyObjectByType<EnemyManager>();
            enemyManager.RegisterEnemy(this);
            
            //참조
            health = GetComponent<Health>();
            Agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            //health 이벤트 함수 등록
            health.onDamaged += OnDamaged;
            health.onDeath += OnDeath;
            health.onHeal += OnHeal;
        }

        private void OnDisable()
        {
            health.onDamaged -=  OnDamaged;
            health.onDeath -=  OnDeath;
            health.onHeal -=  OnHeal;
        }

        private void Start()
        {
            //초기화
            foreach (var renderer in GetComponentsInChildren<Renderer>(true))
            {
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                {
                    if (renderer.sharedMaterials[i] == bodyMaterial)
                    {
                        bodyRenderers.Add(new RendererIndexData(renderer, i));
                    }
                }
            }
            //MaterialPropertyBlock 객체 생성
            bodyFlashMaterialPropertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            //데미지에 따른 메테리얼 컬러 변경
            Color currentColor = onHitBodyGradient.Evaluate(
                (Time.time - lastTimeDamaged) / flashOnHitDuration);
            bodyFlashMaterialPropertyBlock.SetColor("_EmissionColor", currentColor);

            foreach (var data in bodyRenderers)
            {
                data.renderer.SetPropertyBlock(bodyFlashMaterialPropertyBlock, data.materialIndex);
            }
            
            wasDamagedThisFrame = false;
        }

        #endregion

        #region Custom Method
        //데미지 처리
        private void OnDamaged(float damage, GameObject damageSource)
        {
            //damageSource 체크
            if (damageSource && damageSource.GetComponent<EnemyController>())
                return;
            
            onDamaged?.Invoke();
            
            //데미지 효과(vfx, sfx)
            lastTimeDamaged = Time.time;

            if (damageSfx && wasDamagedThisFrame == false)
            {
                AudioUtility.CreateSFX(damageSfx, transform.position, 0f);
            }
            wasDamagedThisFrame = true;
        }

        //죽음처리
        private void OnDeath()
        {
            //EnemyManager 제거
            enemyManager.RemoveEnemy(this);
            
            //이펙트 효과
            GameObject vfxGo = Instantiate(deathVfxPrefab, deathVfxSpawnPosition.position, Quaternion.identity);
            Destroy(vfxGo, 3f);
            
            //sfx
            if (deathSfx)
            {
                AudioUtility.CreateSFX(deathSfx, transform.position, 0f);
            }
            
            //적 킬
            Destroy(gameObject);
        }

        //힐처리
        private void OnHeal(float amount)
        {
            
        }
        #endregion
    }
}
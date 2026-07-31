using Unity.FPS.Game;
using Unity.FPS.Utility;
using UnityEngine;
using System.Collections.Generic;

namespace Unity.FPS.Gameplay
{
    //발사체 기본형
    public class ProjectileStandard : ProjectileBase
    {
        #region Variables
        //참조
        private ProjectileBase projectileBase;      //부모 객체

        //라이프타임
        public float maxLifeTime = 5f;

        //이동
        public float speed = 20f;                   //이동속도
        public Transform root;                      //발사체 오브젝트 기준점
        public Transform tip;                       //발사체 맨 앞 기준점

        private Vector3 lastRootPosition;           //이전 프레임에서의 루트 위치
        private Vector3 velocity;                   //속도

        public float gravityDown = 0f;              //중력 계수

        //충돌
        public float radius = 0.01f;                //충돌 체크 범위 (구의 반경)

        public LayerMask hitableLayers = -1;        //충돌 레이어 마스크
        private List<Collider> ignoreColliders;     //충돌체크에서 제외되는 충돌체 리스트

        //충돌 효과
        public GameObject impactVfxPrefab;         //충돌 이펙트 프리팹
        public float impactVfxLifeTime = 3f;        //충돌 이펙트 라이프 타임
        public float impactVfxSpawnOffset = 0.1f;   // 충돌 이펙트 생성 위치 조정

        public AudioClip impactSfxClip;             //충돌 효과 사운드

        //데미지
        public float damage = 15f;                  //데미지값
        #endregion

        #region Unity Event Method
        private void Enable()
        {
            //참조
            projectileBase = GetComponent<ProjectileBase>();
            //이벤트 함수 등록
            projectileBase.onShoot += OnShoot;

            //킬 예약
            Destroy(gameObject, maxLifeTime);
        }

        private void Update()
        {
            //이동
            transform.position += velocity * Time.deltaTime;

            //중력
            if (gravityDown > 0f)
            {
                velocity += Vector3.down * gravityDown * Time.deltaTime;
            }

            //충돌체크
            RaycastHit closestHit = new RaycastHit();
            closestHit.distance = Mathf.Infinity;
            bool foundHit = false;

            //Sphere cast
            Vector3 displacementSinceLastFrame =
                tip.position - lastRootPosition;
            RaycastHit[] hits = Physics.SphereCastAll(
                lastRootPosition, radius, displacementSinceLastFrame.normalized,
                displacementSinceLastFrame.magnitude, hitableLayers,
                QueryTriggerInteraction.Collide);

            foreach (var hit in hits)
            {
                if (IsHitValid(hit) == true && hit.distance < closestHit.distance)
                {
                    foundHit = true;
                    closestHit = hit;
                }
            }

            if (foundHit)
            {
                //
                if (closestHit.distance < 0)
                {
                    closestHit.point = root.position;
                    closestHit.normal = -transform.forward;
                }

                //충돌 처리
                OnHit(closestHit.point, closestHit.normal, closestHit.collider);
            }

            //루트 위치 저장
            lastRootPosition = root.position;
        }
        #endregion

        #region Custom Method
        //발사체를 생성 시 초기값 설정
        private void OnShoot()
        {
            velocity = transform.forward * speed;
            transform.position +=
                projectileBase.InheritedMuzzleVelocity * Time.deltaTime;
            lastRootPosition = root.position;

            //쏘는 플레이어의 충돌체들 가져와 충돌 제외 리스트에 등록
            ignoreColliders = new List<Collider>();
            /* Collider[] ownerColliders = 
                projectileBase.Owner.GetComponentInChildren<Collider>();
            ignoreColliders.AddRange(onwerColliders); */

        }

        //충돌체 체크
        private bool IsHitValid(RaycastHit hit)
        {
            //hit를 무효화하는 컴포넌트를 가진 콜라이더 무효
            if (hit.collider.GetComponent<IgnoreHitDetection>() != null)
            {
                return false;
            }

            //트리거 충돌체이면서 damageable 컴포넌트가 없는 충돌체는 무효
            if (hit.collider.isTrigger == true && hit.collider.GetComponent<Damageable>() == null)
            {
                return false;
            }

            //충돌 체크에서 제외되는 충돌체 리스트에 포함되어 있으면 무효
            if (ignoreColliders != null && ignoreColliders.Contains(hit.collider))
            {
                return false;
            }

            return true;
        }

        private void OnHit(Vector3 point, Vector3 normal, Collider collider)
        {
            //데미지
            //Debug.Log($"Damage : {damage}");
            Damageable damageable = collider.GetComponent<Damageable>();
            if (damageable)
            {
                damageable.InflictDamage(damage, false, projectileBase.Owner);
            }

            //충돌효과 (vfx, sfx)
            if (impactVfxPrefab)
            {
                GameObject impactGo = Instantiate(impactVfxPrefab, point +
                    (normal * impactVfxSpawnOffset), Quaternion.LookRotation(normal));

                if (impactVfxLifeTime > 0f)
                {
                    Destroy(impactGo, impactVfxLifeTime);
                }
            }

            if (impactSfxClip)
            {
                AudioUtility.CreateSFX(impactSfxClip, point, 1f, 3f);
            }

            //발사체 제거

            //kill
            Destroy(gameObject);
        }
        #endregion
    }
}
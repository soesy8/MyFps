using Unity.FPS.Game;
using Unity.FPS.Utility;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    //충전 슈팅 이펙트 연출을 관리하는 클래스
    public class ChargedWeaponEffectHandler : MonoBehaviour
    {
        #region Variables
        //VFX
        public GameObject chargingObject;       //충전되는 오브젝트
        public GameObject spiningFrame;         //충전 시 회전하는 오브젝트 프레임
        public GameObject diskOrbitParticlePrefab; //충전되는 오브젝트 주위를 돌아가는 파티클 이펙트

        public MinMaxVector3 scale;             //충전에 따른 오브젝트의 크기 설정값

        public Vector3 offset;                  //파티클 이펙트의 위치 조정값
        public Transform parentTransform;       //파티클 이펙트의 부모 오브젝트
        
        public MinMaxFloat orbitY;              //파티클 시스템의 회전 설정값
        public MinMaxVector3 radius;            //파티클 시스템 오브젝트의 크기 설정값

        public MinMaxFloat spiningSpeed;        //오브젝트 프레임의 회전 속도 설정값

        //SFX
        public AudioClip chargeSound;           //충전 효과음
        public AudioClip loopChargeWeaponSfx;   //충전 사운드 효과 - 회전효과음

        public float fadeLoopDuration = 0.5f;       //사운드 페이드 효과
        public bool useProceduralPitchOnLoop = false;   //페이드효과 / 재생속도 효과 여부
        [Range(1f,5f)] public float maxProceduralPitchValue = 2f;   //최대 재생 속도

        public GameObject ParticleInstance { get; private set; }
        private ParticleSystem diskOrbitParticle;
        private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule;

        //참조
        private WeaponController weaponController;
        private AudioSource audioSource;
        private AudioSource audioSourceLoop;

        private float lastChargeTriggerTimeTamp;        //충전 시작하는 시간 체크
        private float endChargeTime;                    //충전 사운드 플레이가 끝나는 시간

        private float chargeRatio;                      //무기 충전값
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //충전 사운드
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = chargeSound;
            audioSource.playOnAwake = false;

            audioSourceLoop = gameObject.AddComponent<AudioSource>();
            audioSourceLoop.clip = loopChargeWeaponSfx;
            audioSourceLoop.playOnAwake = false;
            audioSourceLoop.loop = true;
        }

        private void Update()
        {
            //particleInstance null 체크
            if(ParticleInstance == null)
            {
            SpawnParticleSystem();    //1회만
            }

            ParticleInstance.gameObject.SetActive(weaponController.IsWeaponActive);

            chargeRatio = weaponController.CurrentCharge;

            //충전되는 오브젝트
            chargingObject.transform.localScale = scale.GetValueFromRatio(chargeRatio);
            if(spiningFrame)
            {
                spiningFrame.transform.localRotation *=
                    Quaternion.Euler(0f, spiningSpeed.GetValueFromRatio(chargeRatio)
                    * Time.deltaTime, 0f);
            }
            //파티클
            ParticleInstance.transform.localScale = radius.GetValueFromRatio(chargeRatio);
            velocityOverLifetimeModule.orbitalY = orbitY.GetValueFromRatio(chargeRatio);

            //SFX
            if(chargeRatio > 0f)
            {
                if(!audioSourceLoop.isPlaying &&
                    weaponController.lastChargeTriggerTimeTamp > lastChargeTriggerTimeTamp)
                {
                    lastChargeTriggerTimeTamp = weaponController.lastChargeTriggerTimeTamp;
                    if(useProceduralPitchOnLoop == false)
                    {
                        //사운드 페이드 효과
                        endChargeTime = Time.time + chargeSound.length;
                        audioSource.Play();
                    }
                    audioSourceLoop.Play();
                }

                if(useProceduralPitchOnLoop == true)
                {
                    //사운드 페이드 효과
                    float volumeRatio = Mathf.Clamp01(
                        (endChargeTime - Time.time - fadeLoopDuration )
                        / fadeLoopDuration);
                    audioSource.volume = volumeRatio;
                    audioSourceLoop.volume = 1 - volumeRatio;
                }
                else
                {
                    audioSourceLoop.pitch =
                        Mathf.Lerp(1.0f,maxProceduralPitchValue, chargeRatio);
                }
            }
            else
            {
                audioSource.Stop();
                audioSourceLoop.Stop();
            }
        }
        #endregion

        #region Custom Method
        //파티클 스폰
        private void SpawnParticleSystem()
        {
            ParticleInstance = Instantiate(diskOrbitParticlePrefab,
                parentTransform != null ? parentTransform : transform);
            ParticleInstance.transform.localPosition += offset;

            FindReference();
        }

        //참조
        private void FindReference()
        {
            diskOrbitParticle = ParticleInstance.GetComponent<ParticleSystem>();
            velocityOverLifetimeModule = diskOrbitParticle.velocityOverLifetime;
            weaponController = GetComponent<WeaponController>();
        }
        #endregion
    }
}
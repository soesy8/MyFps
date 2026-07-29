using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Unity.FPS.Game
{
    /// <summary>
    /// 조준점 데이터 정의
    /// 이미지, 크기, 색깔
    /// </summary>
    [System.Serializable]
    public struct CrossHairData
    {
        public Sprite CrossHairSprite;
        public float CrossHairSize;
        public Color CrossHairColor;
    }
    
    /// <summary>
    /// 무기별 발사 타입 정의
    /// </summary>
    public enum WeaponShootType
    {
        Manual,
        AutoMatic,
        Charge
        // etc...
    }


    /// <summary>
    /// 총기류 무기를 관리하는 클래스
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        // ======== Variables ========
        //무기 활성화, 비활성화
        public GameObject weaponRoot;
        //슈팅 오디오
        private AudioSource shootAudioSource;
        public AudioClip switchWeaponSfx;       //무기 교체 사운드

        //크로스헤어 - 기본
        public CrossHairData crossHairDefault;          //기본
        public CrossHairData crossHairTargetInSight;    //적 포착 시(타겟팅)

        //조준
        [Range(0, 1)] public float aimZoomRatio = 1f;   //조준 시 줌 비율
        public Vector3 aimOffset = Vector3.zero;        //조준 위치 이동 시 무기별 위치 조정값

        //슈팅
        [SerializeField] private WeaponShootType shootType; //슈팅 타입

        [SerializeField] private float maxAmmo = 8f;        //최대 탄환 갯수
        private float currentAmmo;                          //현재 탄환 갯수

        [SerializeField] private float delayBetweenShots = 0.5f;    //연사 방지, 초당 발사 갯수
        private float lastTimeShot;


        // ======== Properties =========
        public GameObject Owner { get; set; }               //무기 주인
        public GameObject SourcePrefab { get; set; }        //무기를 생성한 프리팹
        public bool IsWeaponAcitve { get; private set; }    //무기 활성화 여부

        // ======== Unity Event Method ========
        private void Awake()
        {
            shootAudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            //초기화
            currentAmmo = maxAmmo;
            lastTimeShot = Time.time;
        }

        // ======== Custom Method ========
        //무기 활성화, 비활성화
        public void ShowWeapon(bool show)
        {
            weaponRoot.SetActive(show);
            if (show == true && switchWeaponSfx != null)
            {
                //무기 교체 효과음 플레이
                shootAudioSource.PlayOneShot(switchWeaponSfx);
            }
            IsWeaponAcitve = show;
        }

        //인풋에 따른 발사 처리
        public bool HandleShootInputs(bool inputDown, bool inputHeld, bool inputUp)
        {
            switch(shootType)
            {
                case WeaponShootType.Manual:
                if(inputDown == true)
                {
                    //발사
                    return TryShoot();
                }
                break;
                case WeaponShootType.AutoMatic:
                if(inputHeld == true)
                {
                    return TryShoot();
                }
                break;
                case WeaponShootType.Charge:
                break;
            }
            return false;
        }

        //발사처리
        private bool TryShoot()
        {
            //Ammo 체크, 연사방지 체크
            if(currentAmmo >= 1f && lastTimeShot + delayBetweenShots > Time.time)
            {
                Debug.Log("Shoot");
                currentAmmo -= 1f;
                Debug.Log($"Ammo : {currentAmmo}");
                
                HandleShoot();

                return true;
            }

            return false;
        }

        //슛 연출 처리
        private void HandleShoot()
        {

        }
    }
}
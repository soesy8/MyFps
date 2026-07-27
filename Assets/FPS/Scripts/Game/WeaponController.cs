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

        public GameObject Owner { get; set; }               //무기 주인
        public GameObject SourcePrefab { get; set; }        //무기를 생성한 프리팹
        public bool IsWeaponAcitve { get; private set; }    //무기 활성화 여부

        // ======== Unity Event Method ========
        private void Awake()
        {
            shootAudioSource = GetComponent<AudioSource>();
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

    }
}
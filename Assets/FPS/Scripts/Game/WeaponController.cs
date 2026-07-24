using UnityEngine;
using UnityEngine.Audio;

namespace Unity.FPS.Game
{
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
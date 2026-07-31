using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Unity.FPS.UI
{
    //무기(Ammo) UI를 관리하는 클래스
    public class WeaponHUDManager : MonoBehaviour
    {
        #region ==== Variables ====
        //참조
        private WeaponHUDManager playerWeaponManager;

        //UI 추가
        public RectTransform ammoPannel;            //무기(Ammo) UI를 관리하는 부모 오브젝트
        public GameObject ammoCountPrefab;          //무기(Ammo) UI 프리팹

        //무기 Ammo UI리스트
        private List<AmmoCounter> ammoCounters = new List<AmmoCounter>();
        #endregion

        #region ==== Unity Event Method ====
        private void Awake()
        {
            //참조
            playerWeaponManager =
                GameObject.FindFirstObjectByType<WeaponHUDManager>();
        }

        void OnEnable()
        {
            //이벤트 함수 등록
            /* playerWeaponManager.OnAddedWeapon += AddWeapon;
            playerWeaponManager.OnRemovedWeapon += RemoveWeapon;
            playerWeaponManager.OnSwitchedWeapon += SwitchWeapon; */
        }

        void OnDisable()
        {
            //이벤트 함수 제거
            /* playerWeaponManager.OnAddedWeapon -= AddWeapon;
            playerWeaponManager.OnRemovedWeapon -= RemoveWeapon;
            playerWeaponManager.OnSwitchedWeapon -= SwitchWeapon; */
        }
        #endregion

        #region ==== Custom Method ====
        //무기 추가 시 호출되는 함수
        private void AddWeapon(WeaponController newWeapon, int weaponIndex)
        {
            //AmmoCounter ammoCounter = Instantiate(ammoCountPrefab, ammoPannel);

            //UI 초기화
            //ammoCounter.Initialize();
            //ammoCounters.Add(ammoCounter);
        }

        //무기 제거 시 호출되는 함수
        private void RemoveWeapon(WeaponController oldWeapon, int weaponIndex)
        {

        }

        //무기 교체 시 호출되는 함수
        private void SwitchWeapon(WeaponController weapon)
        {

        }
        #endregion
    }
}
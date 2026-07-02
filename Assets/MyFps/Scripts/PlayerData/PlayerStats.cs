using UnityEngine;
using System;

namespace MyFps
{
    /// <summary>
    /// 플레이어의 속성값을 관리하는 클래스
    /// </summary>
    public class PlayerStats : PersistentSingleton<PlayerStats>
    {
        #region Var
        private int ammoCount;
        private int ammoSize = 7;
        private int reserveAmmo;

        public event Action OnAmmoChanged;
        #endregion

        #region Property
        public int AmmoCount => ammoCount;
        public int AmmoSize => ammoSize;
        public int ReserveAmmo => reserveAmmo;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            ammoCount = 0;
            reserveAmmo = 0;
        }
        #endregion

        #region Custom Method
        //탄약 추가
        public void AddAmmo(int amount)
        {
            reserveAmmo += amount;
            OnAmmoChanged?.Invoke();
        }
        //탄약 소모
        public bool UseAmmo(int amount)
        {
            if (ammoCount < amount)
            {
                Debug.Log("You need to reload");
                return false;
            }

            ammoCount -= amount;
            OnAmmoChanged?.Invoke();
            return true;
        }

        public void Reload()
        {
            Debug.Log("Reload");
            int ammoNeeded = ammoSize - ammoCount;
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);
            ammoCount += ammoToReload;
            reserveAmmo -= ammoToReload;
            OnAmmoChanged?.Invoke();
        }

        /*public void UpdateAmmoUI()
        {
            ammoUI.text = $"{ammoCount} / {reserveAmmo}";
        }*/
        #endregion

    }
}
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

        private float health;
        [SerializeField] private float maxHp = 20f;     //체력 초기값
        private int sceneNumber;

        public event Action OnAmmoChanged;
        #endregion

        #region Property
        public int AmmoCount => ammoCount;
        public int AmmoSize => ammoSize;
        public int ReserveAmmo => reserveAmmo;

        public float Health
        {
            get {  return health; }
            set {  health = value; }
        }

        public int SceneNumber
        {
            get { return sceneNumber; }
            set { sceneNumber = value; }
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            //PlayerStats 초기화
            PlayerStatsInit(null);
        }


        #region Custom Method
        //플레이어 스탯 초기화 - 매개변수로 저장된 데이터 가져오기
        public void PlayerStatsInit(PlayData playData)
        {
            //저장된 데이터 체크
            if (playData != null)
            {
                sceneNumber = playData.sceneNumber;
                ammoCount = playData.ammoCount;
                health = playData.health;
                reserveAmmo = 0;
            }
            else
            {
                sceneNumber = -1;
                ammoCount = 0;
                reserveAmmo = 0;
                health = maxHp;
            }
        }


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
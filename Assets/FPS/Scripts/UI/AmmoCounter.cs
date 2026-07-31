using UnityEngine.UI;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.UI
{
    //무기(Ammo) UI를 관리하는 클래스
    public class AmmoCounter : MonoBehaviour
    {
        #region Variables
        //참조
        private PlayerWeaponManager weaponManager;

        private WeaponController weapon;            //UI와 매칭되는 무기
        private int weaponCounterIndex;             //무기 Ui의 인덱스

        //UI
        public TextMeshProUGUI weaponIndexText;       
        public Image ammoFillImage;

        public float ammoFillSharpness = 10f;       //게이지 채울 때 UI 연출 lerp 계수
        public float weaponSwitchSharpness = 10f;   //무기 교체 시 연출 Lerp 계수

        public CanvasGroup canvasGroup;
        [Range(0,1)] public float unSelectedOpacity = 0.5f;     //액티브 무기가 아니면 투명처리
        private Vector3 unSelectedScale = Vector3.one * 0.8f;   //액티브한 무기가 아니면 크기 80%
        #endregion

        #region Unity Evnet Method
        private void Update()
        {

        }
        #endregion

        #region Custom Method
        public void Initialize(WeaponController _weapon, int weaponIndex)
        {
            //참조
            weaponManager = GameObject.FindFirstObjectByType<PlayerWeaponManager>();
            weapon = _weapon;
            weaponCounterIndex = weaponIndex;

            //weaponIndexText.text = 
        }
        #endregion
    }
}
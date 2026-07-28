using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    /// <summary>
    /// 크로스헤어를 관리하는 클래스
    /// </summary>
    public class CrossHairManager : MonoBehaviour
    {
        // ======== Variables ========
        public Image crosshairImage;        //UI이미지
        public Sprite nullCrosshairSprite;  //데이터가 없을 때 보여지는 스프라이트

        //참조
        private PlayerWeaponManager weaponManager;

        //무기 교체 연출
        [SerializeField] private float crosshairUpdateSharpness = 5f;       //Lerp 계수

        private RectTransform crosshairRectTransform;                       //UI RectTransform

        private CrossHairData crosshairDefault;         //평소에 보여지는 크로스헤어
        private CrossHairData crosshairTarget;          //타게팅 되었을 때 보여지는 크로스헤어

        private CrossHairData crosshairCurrent;         //현재 화면에 보여지는 크로스헤어

        //적 포착 순간 또는 적 잃어버리는 순간을 계산하기 위한 변수
        private bool wasPointingAtEnemy;


        // ======== Unity Event Method ========

        private void Awake()
        {
            weaponManager = GameObject.FindFirstObjectByType<PlayerWeaponManager>();
            crosshairRectTransform = crosshairImage.GetComponent<RectTransform>();
        }

        private void Start()
        {
            //현재 활성화된 무기의 크로스헤어 교체
            OnWeaponChanged(weaponManager.GetActiveWeapon());

            //무기 교체 이벤트 함수에 등록
            weaponManager.OnSwitchToWeapon += OnWeaponChanged;
        }

        private void Update()
        {
            //크로스헤어 보여주기
            UpdateCrosshairPointAtEnemy(false);

            //was변수 저장
            wasPointingAtEnemy = weaponManager.IsPointingAtEnemy;
        }


        // ======== Custom Method ========
        //크로스헤어 보여주기 : force true 강제로 보여주기
        private void UpdateCrosshairPointAtEnemy(bool force)
        {
            //크로스헤어 데이터 체크
            if (crosshairDefault.CrossHairSprite == null) return;

            //적을 포착한 순간
            if ((force == true || wasPointingAtEnemy == false) && weaponManager.IsPointingAtEnemy == true)
            {
                crosshairCurrent = crosshairTarget;
                crosshairImage.sprite = crosshairCurrent.CrossHairSprite;
            }
            //적을 놓치는 순간
            else if ((force == true || wasPointingAtEnemy == true) && weaponManager.IsPointingAtEnemy == false)
            {
                crosshairCurrent = crosshairDefault;
                crosshairImage.sprite = crosshairCurrent.CrossHairSprite;
            }

            //Lerp 변경
            crosshairImage.color =Color.Lerp
                (crosshairImage.color,
                crosshairCurrent.CrossHairColor,
                crosshairUpdateSharpness * Time.deltaTime);
            crosshairRectTransform.sizeDelta = Mathf.Lerp
                (crosshairRectTransform.sizeDelta.x,
                crosshairCurrent.CrossHairSize,
                crosshairUpdateSharpness * Time.deltaTime) * Vector2.one;


        }


        //무기 교체 시 호출되는 함수
        private void OnWeaponChanged(WeaponController newWeapon)
        {
            //newWeapon 체크
            if (newWeapon == null)
            {
                if (nullCrosshairSprite)
                {
                    crosshairImage.sprite = nullCrosshairSprite;
                }
                else
                {
                    crosshairImage.enabled = false;
                }
            }
            else //새로운 무기가 들어오면
            {
                crosshairImage.enabled = true;
                crosshairDefault = newWeapon.crossHairDefault;
                crosshairTarget = newWeapon.crossHairTargetInSight;
            }

            //강제로 크로스헤어 보여주기
            UpdateCrosshairPointAtEnemy(true);
        }




    }
}
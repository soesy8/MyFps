using Unity.FPS.Game;
using Unity.FPS.Utility;
using UnityEngine;


namespace Unity.FPS.Gameplay
{
    //충전 발사체의 충전량에 따른 속성값
    public class ProjectileChargeParameters : MonoBehaviour
    {
        #region Variables
        //충전량에 따른 설정할 속성값
        public MinMaxFloat damage;          //데미지
        public MinMaxFloat speed;           //이동 속도
        public MinMaxFloat gravityDown;     //중력값
        public MinMaxFloat radius;          //반경

        //참조
        private ProjectileBase projectileBase;
        #endregion

        #region Unity Event Method
        private void OnEnable()
        {
            projectileBase = GetComponent<ProjectileBase>();
            projectileBase.onShoot += OnShoot;
        }
        #endregion

        #region Custom Method
        private void OnShoot()
        {
            ProjectileStandard projectileStandard = GetComponent<ProjectileStandard>();

            projectileStandard.damage = damage.GetValueFromRatio(projectileBase.InitialCharge);
            projectileStandard.speed = speed.GetValueFromRatio(projectileBase.InitialCharge);
            projectileStandard.gravityDown = gravityDown.GetValueFromRatio(projectileBase.InitialCharge);
            projectileStandard.radius = radius.GetValueFromRatio(projectileBase.InitialCharge);
        }
        #endregion
    }
}
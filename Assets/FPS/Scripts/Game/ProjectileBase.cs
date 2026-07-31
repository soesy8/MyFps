using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    //모든 발사체들의 부모가 되는 추상 클래스
    public abstract class ProjectileBase : MonoBehaviour
    {
        #region Variables
        //발사체 발사 시 등록된 함수 호출
        public UnityAction onShoot;
        #endregion


        #region Properties
        //발사체를 발사한 무기의 주인
        public GameObject Owner { get; private set; }
        //발사체의 초기 위치
        public Vector3 InitialPosition { get; private set; }
        //발사체의 초기 발사 방향
        public Vector3 InitialDirection { get; private set; }
        //발사 할 때의 총구의 속도
        public Vector3 InheritedMuzzleVelocity { get; private set; }
        //충전 발사체의 발사 시 충전값
        public float InitialCharge { get; private set; }
        #endregion


        #region Unity Event Method
        
        #endregion


        #region Custom Method
        //무기가 발사체를 발사하는 순간 호출

        public void Shoot(WeaponController weaponController)
        {
            //속성값 초기화
            Owner = weaponController.Owner;
            InitialPosition = transform.position;
            InitialDirection = transform.forward;
            InheritedMuzzleVelocity = weaponController.MuzzleWorldVelocity;
            InitialCharge = weaponController.CurrentCharge;

            //등록된 함수 호출
            onShoot?.Invoke();
        }
        #endregion
    }
}
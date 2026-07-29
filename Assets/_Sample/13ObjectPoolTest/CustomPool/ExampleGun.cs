using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    //풀에 저장되어 있는 발사체를 발사하는 총을 관리하는 오브젝트
    public class ExampleGun : MonoBehaviour
    {
        // varibales
        public InputActionReference fireAction;

        //탄환 프리펩
        //public GameObject bulletPrefab;

        public float muzzleVelocity = 700f;
        public Transform muzzlePositioin;
        public float cooldownWindow = 0.1f;
        private float nextTimeToShoot;

        //오브젝트 풀
        public ObjectPool objectPool;


        // unity envet methods
        private void FixedUpdate()
        {
            //마우스 좌클릭하면 발사
            if (fireAction.action.IsPressed() && objectPool != null && Time.time >= cooldownWindow)
            {
                /*GameObject bulletGo = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Destroy(bulletGo,3f);*/

                GameObject bulletObject = objectPool.GetPooledObject().gameObject;

                if (bulletObject != null)
                {
                    bulletObject.SetActive(true);

                    bulletObject.transform.SetLocalPositionAndRotation(
                        muzzlePositioin.position, muzzlePositioin.rotation);

                    bulletObject.GetComponent<Rigidbody>().AddForce(
                        bulletObject.transform.forward * muzzleVelocity,
                        ForceMode.Acceleration);

                    //킬 예약
                    ExampleProjectile projectile = bulletObject.GetComponent<ExampleProjectile>();
                    //projectile?.DeActivate();
                    //다음에 쏠 시간
                    nextTimeToShoot = Time.time + cooldownWindow;
                }
            }
        }

        // custom method

    }
}

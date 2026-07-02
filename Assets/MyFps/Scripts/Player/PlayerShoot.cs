using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace MyFps
{
    public class PlayerShoot : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        public Transform firePoint;
        [SerializeField] private InputActionReference shootAction;
        [SerializeField] private InputActionReference reloadAction;

        //무기 옵션
        [SerializeField] private float attackRange = 100f;
        [SerializeField] private float attackDamage = 5f;
        private AmmoUI ammoUI;

        /*[SerializeField] private int ammoSize = 7;      // 탄창 크기
        [SerializeField] private int currentAmmo = 7;   // 현재 탄창
        [SerializeField] private int reserveAmmo = 0;      // 예비 탄약

        [SerializeField] private TextMeshProUGUI ammoUI;*/

        //효과
        public GameObject hitImpactPrefab;
        public AudioSource pistolShot;
        public ParticleSystem muzzleFlash;

        private string shootTrigger = "ShootTrigger";
        #endregion

        /*public int CurrentAmmo => currentAmmo;
        public int AmmoSize => ammoSize;
        public int ReserveAmmo => reserveAmmo;*/


        #region Unity Event Method
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            shootAction.action.Enable();
        }

        private void OnDisable()
        {
            shootAction.action.Disable();
        }

        private void Update()
        {
            if(reloadAction.action.WasPressedThisFrame())
            {
                //PlayerStats.Instance.Reload();

                if (PlayerStats.Instance.AmmoCount < PlayerStats.Instance.AmmoSize
                    && PlayerStats.Instance.ReserveAmmo > 0)
                {
                    PlayerStats.Instance.Reload();
                }
            }

            if (shootAction.action.WasPressedThisFrame())
            {
                if (PlayerStats.Instance.AmmoCount <= 0)
                {
                    Debug.Log("You need to reload");
                    return;
                }

                Shoot();
                PlayerStats.Instance.UseAmmo(1);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackRange);

            if (isHit)
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * attackRange);
            }
        }
        #endregion

        #region Custom Method
        void Shoot()
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackRange);

            if (isHit)
            {
                //Debug.Log($"{hit.transform.name} hit");

                if (hitImpactPrefab != null)
                {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.identity);
                    Destroy(effectGo, 0.5f);
                }

                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
            }

            //명중하지 못해도 발사 연출은 나감
            animator.SetTrigger(shootTrigger);
            pistolShot.Play();

            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }
        #endregion
    }
}
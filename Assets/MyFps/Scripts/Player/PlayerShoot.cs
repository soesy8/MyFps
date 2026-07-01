using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    public class PlayerShoot : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        public Transform firePoint;
        [SerializeField] private InputActionReference shootAction;
        public AudioSource shootAudio;

        //무기 옵션
        [SerializeField] private float attackRange = 100f;
        [SerializeField] private float attackDamage = 5f;

        //효과
        public GameObject hitImpactPrefab;
        public AudioSource pistolShot;

        private string shootTrigger = "ShootTrigger";
        #endregion

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
            if (shootAction.action.WasPressedThisFrame())
            {
                Shoot();
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
                Debug.Log($"{hit.transform.name} hit");

                if (hitImpactPrefab != null)
                {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.identity);
                }

                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
            }

            //명중하지 못해도 발사 연출은 나감
            animator.SetTrigger(shootTrigger);
            shootAudio.Play();

        }
        #endregion
    }
}
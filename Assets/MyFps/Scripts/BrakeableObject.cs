using UnityEngine;

namespace MyFps
{
    public class BrakeableObject : MonoBehaviour, IDamageable
    {
        //체력
        [SerializeField] private float maxHealth = 5f;
        private float currentHealth = 0f;
        private bool isBrake = false;       //부서짐 체크

        [SerializeField] private GameObject dropItem;       //부서진 오브젝트에서 나올 아이템

        [SerializeField] private GameObject brokenPrefab;       //부서질 오브젝트 프리펩

        private void Start()
        {
            currentHealth = maxHealth;
            isBrake = false;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            //Debug.Log($"{gameObject.name} currentHealth: {currentHealth}");

            //데미지 효과 처리(VFX, SFX)

            //죽음 체크
            if (currentHealth <= 0f && isBrake == false)
            {
                Brake();
            }
        }

        public void Brake()
        {
            GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);

            Rigidbody[] pieces = broken.GetComponentsInChildren<Rigidbody>();
            
            foreach (Rigidbody rb in pieces)
            {
                rb.AddExplosionForce(10f, broken.transform.position, 3f, 0.5f, ForceMode.Force);
            }

            Debug.Log("Brake Object");

            isBrake = true;

            if (dropItem != null)
            {
                Debug.Log("Drop Key");
                GameObject item = Instantiate(dropItem, transform.position + new Vector3(0f,1f,0f), transform.rotation);
            }
            
            Destroy(gameObject);
            Destroy(broken, 2f);
        }
    }
}
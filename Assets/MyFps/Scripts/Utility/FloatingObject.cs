using UnityEngine;

namespace MyFps
{
    public class FloatingObject : MonoBehaviour
    {
        [SerializeField] private float floatHeight = 0.1f;
        [SerializeField] private float floatSpeed = 3f;
        [SerializeField] private float rotateSpeed = 80f;

        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

            transform.position = startPos + Vector3.up * y;
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}